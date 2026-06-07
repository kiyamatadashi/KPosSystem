using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

using shared.Models.Requests.Orders;
using shared.Models.Responses.Orders;
using shared.Models.SignalR;

using registerSystem.Config;
using registerSystem.Services;

namespace registerSystem.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly OrderApiService _orderApiService = new();
    private readonly SignalRService  _signalRService  = new();

    // ─── Orders ──────────────────────────────────────

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

    // 注文作成用（暫定）
    public string ProductName { get; set; } = "";
    public string Quantity    { get; set; } = "1";

    // ─── コンストラクタ ───────────────────────────────

    public MainViewModel()
    {
        FilteredOrders = CollectionViewSource.GetDefaultView(Orders);
        FilteredOrders.Filter = FilterOrders;
    }

    // ─── 初期化 ───────────────────────────────────────

    public async Task InitializeAsync()
    {
        _signalRService.OrderCreated += OnOrderCreated;

        await _signalRService.StartAsync();
        await ReloadOrdersAsync();
    }

    // ─── Orders操作 ──────────────────────────────────

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

        var names = Orders
            .Select(x => x.SetNumber)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .OrderBy(x => x);

        foreach (var name in names)
            TableNames.Add(name);
    }

    private bool FilterOrders(object obj)
    {
        if (obj is not OrderResponse order) return false;

        bool tableMatch =
            string.IsNullOrWhiteSpace(SelectedTableNameFilter)
            || order.SetNumber == SelectedTableNameFilter;

        string statusText = order.Status switch
        {
            true  => "済",
            false => "キャンセル",
            null  => ""
        };

        bool statusMatch =
            string.IsNullOrWhiteSpace(SelectedStatusFilter)
            || statusText == SelectedStatusFilter;

        return tableMatch && statusMatch;
    }

    public async Task CreateOrderAsync()
    {
        if (string.IsNullOrWhiteSpace(ProductName))
            throw new Exception("ProductName is required.");

        if (!int.TryParse(Quantity, out var quantity))
            throw new Exception("Quantity is invalid.");

        var now = DateTime.Now;

        var order = new CreateOrderRequest
        {
            ShopID          = AppSettings.ShopId,
            GroupID         = now.ToString("yyyyMMddHHmmss"),
            SetNumber       = "A01",
            Category        = "Drink",
            SideMenu        = "Whiskey",
            ProductName     = ProductName,
            Amount          = 1200,
            Quantity        = quantity,
            BackAmount      = 200,
            BackUnit        = "杯",
            MixerSelectable = false,
            CastSelectable  = false
        };

        await _orderApiService.CreateOrderAsync(
            new List<CreateOrderRequest> { order });

        ProductName = "";
        Quantity    = "1";
    }

    private async void OnOrderCreated(List<OrderCreatedMessage> messages)
    {
        await Application.Current.Dispatcher.InvokeAsync(async () =>
        {
            await ReloadOrdersAsync();
        });
    }

    // ─── INotifyPropertyChanged ───────────────────────

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
}
