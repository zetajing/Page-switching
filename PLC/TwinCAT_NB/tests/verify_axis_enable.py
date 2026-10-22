"""执行基础轴块的真实 ST 条件，检查使能、运动参数和停车之间的边界。"""

from pathlib import Path
from types import SimpleNamespace as NS
import re
import unittest
import xml.etree.ElementTree as ET
from verify_upload_assembly import StBranch

ROOT = Path(__file__).resolve().parents[1]


class AxisEnableChecks(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        source = ET.parse(ROOT / 'axis_4/POUs/FB_AxisControl.TcPOU').findtext('.//ST')
        source = re.sub(r'//[^\n]*', '', source).replace('1.0E12', 'cLimit')

        def assignment(name):
            return re.search(r'\b' + name + r'\s*:=.*?;', source, re.S)[0]

        def argument(block, name, following):
            return re.search(block + r'\s*\(.*?\b' + name + r'\s*:=\s*(.*?),\s*'
                             + following + r'\s*:=', source, re.S)[1]

        cls.program = StBranch('\n'.join([
            assignment('xPositionValid'), assignment('xConfigValid'),
            re.search(r'lrStopDeceleration := 0;.*?END_IF', source, re.S)[0],
            assignment('xHoldPower'),
            'xEnable := ' + argument('fbPower', 'Enable', 'Enable_Positive') + ';',
            'xPositive := ' + argument('fbPower', 'Enable_Positive', 'Enable_Negative') + ';',
            'xNegative := ' + argument('fbPower', 'Enable_Negative', 'Override') + ';',
            assignment('xStopRequest'),
            'xStopExecute := ' + argument('fbStop', 'Execute', 'Deceleration') + ';',
            assignment('xManual')]))

    def fixture(self, configured=True):
        config = NS(lrMinPosition=-10, lrMaxPosition=10, lrMaxVelocity=5,
                    lrMaxAcceleration=10, lrStopDeceleration=5)
        if not configured:
            config = NS(**{name: 0 for name in vars(config)})
        return dict(Config=config, cLimit=1.0E12,
                    Axis=NS(Status=NS(Moving=False, IoDataInvalid=False, Error=False,
                                      ExtSetPointGenEnabled=False), NcToPlc=NS(ActPos=0)),
                    Data=NS(xStart=True, xStop=False, reset=False),
                    xPermit=True, xGroupBusy=False, xPositiveLimit=False, xNegativeLimit=False,
                    fbPower=NS(Status=True), fbStop=NS(Busy=False, Done=False, Error=False),
                    fbJog=NS(Busy=False), fbHome=NS(Busy=False), fbReset=NS(Busy=False))

    def test_zero_parameters_allow_enable_without_motion_or_stop_lock(self):
        values = self.fixture(configured=False)
        for enabled in (False, True):
            values['fbPower'].Status = enabled
            self.program.run(values)
            self.assertTrue(values['xEnable'])
            self.assertTrue(values['xPositive'] and values['xNegative'])
            self.assertFalse(values['xConfigValid'] or values['xManual'])
            self.assertFalse(values['xStopRequest'] or values['xStopExecute'])

    def test_authority_permit_io_and_enable_request_still_gate_new_enable(self):
        for condition in ('permit', 'io', 'request'):
            values = self.fixture(configured=False)
            values['fbPower'].Status = False
            if condition == 'permit': values['xPermit'] = False
            elif condition == 'io': values['Axis'].Status.IoDataInvalid = True
            else: values['Data'].xStart = False
            self.program.run(values)
            self.assertFalse(values['xEnable'], condition)

    def test_position_limits_remain_active_when_only_speed_is_missing(self):
        values = self.fixture()
        values['Config'].lrMaxVelocity = 0
        for position, positive, negative in ((-10, True, False), (0, True, True), (10, False, True)):
            values['Axis'].NcToPlc.ActPos = position
            self.program.run(values)
            self.assertTrue(values['xEnable'])
            self.assertEqual((values['xPositive'], values['xNegative']), (positive, negative))
            self.assertFalse(values['xManual'])
        values['xPositiveLimit'] = values['xNegativeLimit'] = True
        self.program.run(values)
        self.assertFalse(values['xPositive'] or values['xNegative'])
        self.assertTrue(values['xStopRequest'])

    def test_parameter_loss_during_motion_still_requests_stop(self):
        for active in ('Moving', 'Jog', 'Home'):
            values = self.fixture(configured=False)
            if active == 'Moving': values['Axis'].Status.Moving = True
            elif active == 'Jog': values['fbJog'].Busy = True
            else: values['fbHome'].Busy = True
            self.program.run(values)
            self.assertTrue(values['xStopRequest'] and values['xStopExecute'], active)
            self.assertFalse(values['xManual'])
            self.assertEqual(values['lrStopDeceleration'], 0)

    def test_stop_uses_valid_deceleration_or_documented_zero_fallback(self):
        for configured in (False, True):
            values = self.fixture(configured)
            values['Data'].xStop = True
            self.program.run(values)
            self.assertTrue(values['xStopExecute'])
            self.assertEqual(values['lrStopDeceleration'], 5 if configured else 0)
            values['xGroupBusy'] = True
            self.program.run(values)
            self.assertFalse(values['xStopExecute'], '轨迹占用时仍先由组控制退出')
            values['xGroupBusy'] = False
            values['Axis'].Status.ExtSetPointGenEnabled = True
            self.program.run(values)
            self.assertFalse(values['xStopExecute'])

    def test_power_is_held_until_deceleration_finishes_then_disable_is_allowed(self):
        values = self.fixture(configured=False)
        values['Data'].xStart = False
        values['Axis'].Status.Moving = True
        self.program.run(values)
        self.assertTrue(values['xHoldPower'] and values['xEnable'] and values['xStopExecute'])
        values['Axis'].Status.Moving = False
        values['fbStop'].Busy = True
        self.program.run(values)
        self.assertTrue(values['xHoldPower'] and values['xEnable'])
        values['fbStop'].Done = True
        self.program.run(values)
        self.assertFalse(values['xHoldPower'] or values['xEnable'])


if __name__ == '__main__':
    unittest.main(verbosity=2)
