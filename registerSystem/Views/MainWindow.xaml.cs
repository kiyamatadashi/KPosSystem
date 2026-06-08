using System.Windows;

using registerSystem.ViewModels;
using registerSystem.Views.Pages;

namespace registerSystem.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel  _viewModel      = new();
    private readonly SettingPage    _settingPage    = new();
    private readonly TablePage      _tablePage      = new();
    private readonly AttendancePage _attendancePage = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = _viewModel;
        NavigateTo(_settingPage);
        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
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

    private void NavSetting_Click(object sender, RoutedEventArgs e)
        => NavigateTo(_settingPage);

    private void NavTable_Click(object sender, RoutedEventArgs e)
        => NavigateTo(_tablePage);

    private void NavAttendance_Click(object sender, RoutedEventArgs e)
        => NavigateTo(_attendancePage);

    private void CloseButton_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown();

    private void NavigateTo(object page)
        => MainContent.Content = page;
}
