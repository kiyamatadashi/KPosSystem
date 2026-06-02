using System.Windows;

using wpf.ViewModels;

namespace wpf.Views;

public partial class MainWindow
    : Window
{
    private readonly MainViewModel
        _viewModel = new();

    public MainWindow()
    {
        InitializeComponent();

        DataContext = _viewModel;

        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            await _viewModel
                .InitializeAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void ReloadOrders_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            await _viewModel
                .ReloadOrdersAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async void CreateOrder_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            _viewModel.ProductName =
                ProductNameTextBox.Text;

            _viewModel.Quantity =
                QuantityTextBox.Text;

            await _viewModel
                .CreateOrderAsync();

            ProductNameTextBox.Text = "";

            QuantityTextBox.Text = "1";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}