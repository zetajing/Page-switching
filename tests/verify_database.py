"""链接实际归档/同步代码和Total DLL，用内存存储替身检查补传；不连接SQL Server或PLC。"""
from pathlib import Path
import subprocess
import tempfile
import unittest
from xml.sax.saxutils import escape

ROOT = Path(__file__).resolve().parents[1]


class DatabaseChecks(unittest.TestCase):
    def test_archive_and_dll_storage_contract(self):
        sources = ['Database/DatabaseArchive.cs', 'Database/DatabaseStore.cs',
                   'Database/DatabaseRows.cs', 'Database/DatabaseSyncService.cs',
                   'Wave/Acquisition/WaveDataModels.cs', 'Wave/Legacy/WaveTrajectoryFile.cs']
        with tempfile.TemporaryDirectory(prefix='SwitchDatabase_') as directory:
            folder = Path(directory)
            links = ''.join(f'<Compile Include="{escape(str(ROOT / path))}" />' for path in sources)
            references = ''.join(f'<Reference Include="{name}"><HintPath>{escape(str(ROOT / "lib" / (name + ".dll")))}</HintPath></Reference>'
                                 for name in ['InduLink.Abstractions', 'InduLink.Storage'])
            (folder / 'Probe.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup>'
                '<OutputType>Exe</OutputType><TargetFramework>net8.0-windows</TargetFramework><UseWindowsForms>true</UseWindowsForms>'
                '<ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable><TreatWarningsAsErrors>true</TreatWarningsAsErrors>'
                '</PropertyGroup><ItemGroup>' + links + references +
                '<PackageReference Include="System.Configuration.ConfigurationManager" Version="8.0.0" />'
                '<PackageReference Include="System.Data.SqlClient" Version="4.8.6" /></ItemGroup></Project>', encoding='utf-8')
            (folder / 'Program.cs').write_text((ROOT / 'tests/database_checks.cs.txt').read_text(encoding='utf-8'), encoding='utf-8')
            result = subprocess.run(['dotnet', 'run', '--project', str(folder / 'Probe.csproj'), '-c', 'Release', '-v', 'quiet', '--', str(folder)],
                                    capture_output=True, text=True, encoding='utf-8', timeout=120)
            self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
            print(result.stdout.strip())


if __name__ == '__main__': unittest.main(verbosity=2)
