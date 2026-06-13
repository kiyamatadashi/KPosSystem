using System.Windows;

namespace drinkOrderSystem;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ApplyScreenScale();
    }

    private static void ApplyScreenScale()
    {
        double screenH = SystemParameters.PrimaryScreenHeight;
        double scale   = screenH / 1080.0;
        scale = Math.Max(0.5, Math.Min(scale, 2.0));

        Current.Resources["Scale"]             = scale;
        Current.Resources["FontSizeSm"]        = Math.Round(12 * scale);
        Current.Resources["FontSizeMd"]        = Math.Round(14 * scale);
        Current.Resources["FontSizeLg"]        = Math.Round(16 * scale);
        Current.Resources["FontSizeXl"]        = Math.Round(18 * scale);
        Current.Resources["FontSizeHeader"]    = Math.Round(18 * scale);
        Current.Resources["FontSizeNav"]       = Math.Round(18 * scale);
        Current.Resources["BtnSizeNav"]        = Math.Round(80 * scale);
        // ボタン80px + 上下余白10pxずつ = 100px
        Current.Resources["HeaderHeight"]      = Math.Round(100 * scale);
        Current.Resources["RowHeight"]         = Math.Round(34 * scale);
        // フィルタ用ComboBox高さ（RowHeightの70%程度）
        Current.Resources["FilterComboHeight"] = Math.Round(24 * scale);
    }
}
