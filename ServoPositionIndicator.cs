using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Page_switching;

public sealed class ServoPositionIndicator : Control
{
    private double _minimumPosition = -20;
    private double _maximumPosition = 20;
    private double? _actualPosition;
    private string _unitText = "°";
    private bool _isConnected;
    private bool _hasAlarm;
    private bool _positiveLimit;
    private bool _negativeLimit;

    // 初始化伺服位置指示器的字体、颜色和双缓冲绘制。
    public ServoPositionIndicator()
    {
        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true);

        BackColor = Color.White;
        ForeColor = Color.FromArgb(31, 41, 55);
        MinimumSize = new Size(180, 66);
        Height = 82;
        TabStop = false;
    }

    [Category("位置")]
    public double MinimumPosition
    {
        get => _minimumPosition;
        set
        {
            if (value >= _maximumPosition) return;
            _minimumPosition = value;
            Invalidate();
        }
    }

    [Category("位置")]
    public double MaximumPosition
    {
        get => _maximumPosition;
        set
        {
            if (value <= _minimumPosition) return;
            _maximumPosition = value;
            Invalidate();
        }
    }

    [Category("位置")]
    public double? ActualPosition
    {
        get => _actualPosition;
        set
        {
            _actualPosition = value;
            Invalidate();
        }
    }

    [Category("位置")]
    public string UnitText
    {
        get => _unitText;
        set
        {
            _unitText = value ?? string.Empty;
            Invalidate();
        }
    }

    [Category("状态")]
    public bool IsConnected
    {
        get => _isConnected;
        set
        {
            _isConnected = value;
            Invalidate();
        }
    }

    [Category("状态")]
    public bool HasAlarm
    {
        get => _hasAlarm;
        set
        {
            _hasAlarm = value;
            Invalidate();
        }
    }

    [Category("状态")]
    public bool PositiveLimit
    {
        get => _positiveLimit;
        set
        {
            _positiveLimit = value;
            Invalidate();
        }
    }

    [Category("状态")]
    public bool NegativeLimit
    {
        get => _negativeLimit;
        set
        {
            _negativeLimit = value;
            Invalidate();
        }
    }

    // 使用最新轴状态更新位置、范围、单位和连接状态。
    public void Update(AxisSnapshot snapshot, string unit, double minimum, double maximum, bool connected)
    {
        MinimumPosition = minimum;
        MaximumPosition = maximum;
        UnitText = unit;
        ActualPosition = snapshot.ActualPosition;
        HasAlarm = snapshot.HasAlarm;
        PositiveLimit = snapshot.PositiveLimit;
        NegativeLimit = snapshot.NegativeLimit;
        IsConnected = connected;
    }

    // 绘制位置刻度、当前位置和轴状态。
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.Clear(BackColor);

        using var borderPen = new Pen(Color.FromArgb(226, 232, 240));
        using var trackPen = new Pen(Color.FromArgb(203, 213, 225), 8)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round
        };
        using var actualBrush = new SolidBrush(GetActualColor());
        using var actualTextBrush = new SolidBrush(GetActualColor());

        var border = new Rectangle(0, 0, Math.Max(0, Width - 1), Math.Max(0, Height - 1));
        graphics.DrawRectangle(borderPen, border);

        var left = 26f;
        var right = Math.Max(left + 20, Width - 26f);
        var trackY = Math.Min(Height - 23f, 45f);
        graphics.DrawLine(trackPen, left, trackY, right, trackY);

        var zeroX = MapPosition(0, left, right);
        using var zeroPen = new Pen(Color.FromArgb(148, 163, 184), 1)
        {
            DashStyle = DashStyle.Dash
        };
        graphics.DrawLine(zeroPen, zeroX, trackY - 13, zeroX, trackY + 13);

        if (_actualPosition.HasValue && _isConnected)
        {
            var actualX = MapPosition(_actualPosition.Value, left, right);
            graphics.FillEllipse(actualBrush, actualX - 7, trackY - 7, 14, 14);
            DrawCenteredString(
                graphics,
                $"{_actualPosition.Value:0.##}{_unitText}",
                Font,
                actualTextBrush,
                actualX,
                Height - 20);
        }
        else
        {
            DrawCenteredString(graphics, "暂无位置数据", Font, Brushes.Gray, (left + right) / 2, 6);
        }

        DrawAlignedString(graphics, $"{_minimumPosition:0.##}{_unitText}", Font, Brushes.Gray, left, Height - 20);
        DrawAlignedString(graphics, $"{_maximumPosition:0.##}{_unitText}", Font, Brushes.Gray, right, Height - 20, rightAligned: true);
    }

    // 将实际轴位置换算为控件中的横坐标。
    private float MapPosition(double position, float left, float right)
    {
        var rate = (position - _minimumPosition) / (_maximumPosition - _minimumPosition);
        rate = Math.Clamp(rate, 0, 1);
        return left + (float)((right - left) * rate);
    }

    // 根据连接、报警和限位状态返回当前位置的显示颜色。
    private Color GetActualColor()
    {
        if (_hasAlarm) return Color.FromArgb(220, 38, 38);
        if (_positiveLimit || _negativeLimit) return Color.FromArgb(234, 88, 12);
        return Color.FromArgb(14, 116, 144);
    }

    // 以指定横坐标为中心绘制文字。
    private static void DrawCenteredString(Graphics graphics, string text, Font font, Brush brush, float centerX, float y)
    {
        var size = graphics.MeasureString(text, font);
        graphics.DrawString(text, font, brush, centerX - size.Width / 2, y);
    }

    // 按左对齐或右对齐方式绘制文字。
    private static void DrawAlignedString(Graphics graphics, string text, Font font, Brush brush, float x, float y, bool rightAligned = false)
    {
        var size = graphics.MeasureString(text, font);
        graphics.DrawString(text, font, brush, rightAligned ? x - size.Width : x, y);
    }
}
