using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

using shared.Models.Requests.Orders;
using shared.Models.Responses.Orders;
using shared.Models.SignalR;

using drinkOrderSystem.Services;

namespace drinkOrderSystem.ViewModels;

public class DrinkOrderViewModel : INotifyPropertyChanged
{
    private readonly OrderApiService _orderApiService = new();
    private readonly SignalRService  _signalRService  = new();

    public ObservableCollection<OrderResponse> Orders { get; } = new();

    public ICollectionView FilteredOrders { get; }

    public ObservableCollection<string> TableNames { get; } = new();

    public ObservableCollection<string> StatusList { get; } = new()
    {
        "",
        "済",
        "キャンセル"
    };

    private string? _selectedTableNameFilter;
    public string? SelectedTableNameFilter
    {
        get => _selectedTableNameFilter;
        set
        {
            _selectedTableNameFilter = value;
            OnPropertyChanged();
            FilteredOrders.Refresh();
        }
    }

    private string? _selectedStatusFilter;
    public string? SelectedStatusFilter
    {
        get => _selectedStatusFilter;
        set
        {
            _selectedStatusFilter = value;
            OnPropertyChanged();
            FilteredOrders.Refresh();
        }
    }

    public DrinkOrderViewModel()
    {
        FilteredOrders = CollectionViewSource.GetDefaultView(Orders);
        FilteredOrders.Filter = FilterOrders;
    }

    public async Task InitializeAsync()
    {
        _signalRService.OrderCreated += OnOrderCreated;
        await _signalRService.StartAsync();
        await ReloadOrdersAsync();
    }

    public async Task ReloadOrdersAsync()
    {
        Orders.Clear();
        var orders = await _orderApiService.GetOrdersAsync();
        if (orders is null) return;

        foreach (var order in orders)
            Orders.Add(order);

        RefreshTableNames();
        FilteredOrders.Refresh();
    }

    public void RefreshTableNames()
    {
        TableNames.Clear();
        foreach (var name in Orders
            .Select(x => x.SetNumber)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .OrderBy(x => x))
        {
            TableNames.Add(name);
        }
    }

    private bool FilterOrders(object obj)
    {
        if (obj is not OrderResponse order) return false;

        bool tableMatch =
            string.IsNullOrWhiteSpace(SelectedTableNameFilter)
            || order.SetNumber == SelectedTableNameFilter;

        // ステータス判定は OrderResponse.StatusText を使用
        bool statusMatch =
            string.IsNullOrWhiteSpace(SelectedStatusFilter)
            || order.StatusText == SelectedStatusFilter;

        return tableMatch && statusMatch;
    }

    private async void OnOrderCreated(List<OrderCreatedMessage> messages)
    {
        await Application.Current.Dispatcher.InvokeAsync(async () =>
        {
            await ReloadOrdersAsync();
        });
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
