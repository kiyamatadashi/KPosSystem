using System.Windows;

using drinkOrderSystem.ViewModels;

namespace drinkOrderSystem.Views;

public partial class DrinkOrderWindow : Window
{
    private readonly DrinkOrderViewModel _viewModel = new();

    public DrinkOrderWindow()
    {
        InitializeComponent();
        DataContext = _viewModel;
        Loaded += DrinkOrderWindow_Loaded;
    }

    private async void DrinkOrderWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            await _viewModel.InitializeAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"初期化に失敗しました。\n{ex.Message}",
                "エラー",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown();

    private async void ReflectButton_Click(object sender, RoutedEventArgs e)
    {
        // ボタン制御は別途指示
    }
}
