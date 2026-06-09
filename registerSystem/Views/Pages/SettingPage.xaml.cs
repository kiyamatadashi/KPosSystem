using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using registerSystem.Config;
using registerSystem.Services;
using shared.Models.Requests.Masters;
using shared.Models.Responses.Masters;

namespace registerSystem.Views.Pages;

// ─── DataGrid バインディング用 ViewModel ────────────────────────────────────

/// <summary>
/// ProductMaster の1行分を DataGrid にバインドするためのラッパー。
/// MixSelectable・CastSelectable を「可/否」文字列として扱う。
/// </summary>
public class ProductMasterRow : INotifyPropertyChanged
{
    private string _category      = string.Empty;
    private string _sideMenu      = string.Empty;
    private string _productName   = string.Empty;
    private int    _amount;
    private int    _backAmount;
    private string _backUnit      = "円";
    private string _mixSelectableDisplay  = "否";
    private string _castSelectableDisplay = "否";
    private bool   _isDeleted;

    public string Category
    {
        get => _category;
        set { _category = value; OnPropertyChanged(); }
    }

    public string SideMenu
    {
        get => _sideMenu;
        set { _sideMenu = value; OnPropertyChanged(); }
    }

    public string ProductName
    {
        get => _productName;
        set { _productName = value; OnPropertyChanged(); }
    }

    public int Amount
    {
        get => _amount;
        set { _amount = value; OnPropertyChanged(); }
    }

    public int BackAmount
    {
        get => _backAmount;
        set { _backAmount = value; OnPropertyChanged(); }
    }

    public string BackUnit
    {
        get => _backUnit;
        set { _backUnit = value; OnPropertyChanged(); }
    }

    /// <summary>「可」または「否」の文字列。MixSelectable の表示用。</summary>
    public string MixSelectableDisplay
    {
        get => _mixSelectableDisplay;
        set { _mixSelectableDisplay = value; OnPropertyChanged(); }
    }

    /// <summary>「可」または「否」の文字列。CastSelectable の表示用。</summary>
    public string CastSelectableDisplay
    {
        get => _castSelectableDisplay;
        set { _castSelectableDisplay = value; OnPropertyChanged(); }
    }

    /// <summary>削除チェックボックスの状態。</summary>
    public bool IsDeleted
    {
        get => _isDeleted;
        set { _isDeleted = value; OnPropertyChanged(); }
    }

    // ─── INotifyPropertyChanged ───────────────────────────────────────────

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    // ─── 変換ヘルパー ─────────────────────────────────────────────────────

    /// <summary>レスポンス DTO からラッパーを生成する。</summary>
    public static ProductMasterRow FromResponse(ProductMasterResponse r) => new()
    {
        Category             = r.Category,
        SideMenu             = r.SideMenu,
        ProductName          = r.ProductName,
        Amount               = r.Amount,
        BackAmount           = r.BackAmount,
        BackUnit             = r.BackUnit,
        MixSelectableDisplay  = r.MixSelectable  ? "可" : "否",
        CastSelectableDisplay = r.CastSelectable ? "可" : "否",
        IsDeleted            = r.Deleted == true,
    };

    /// <summary>リクエスト DTO に変換する。</summary>
    public UpsertProductMasterRequest ToRequest(string shopId) => new()
    {
        ShopID        = shopId,
        Category      = Category,
        SideMenu      = SideMenu,
        ProductName   = ProductName,
        Amount        = Amount,
        BackAmount    = BackAmount,
        BackUnit      = BackUnit,
        MixSelectable  = MixSelectableDisplay == "可",
        CastSelectable = CastSelectableDisplay == "可",
        Deleted       = IsDeleted,
    };
}

// ─── SettingPage ─────────────────────────────────────────────────────────────

public partial class SettingPage : UserControl
{
    private readonly OrderApiService _apiService = new();

    /// <summary>DataGrid のデータソース。</summary>
    private readonly ObservableCollection<ProductMasterRow> _productRows = new();

    public SettingPage()
    {
        InitializeComponent();
        ProductGrid.ItemsSource = _productRows;
    }

    // ─── サイドボタン ─────────────────────────────────────────────────────

    private void SideButton1_Click(object sender, RoutedEventArgs e)
    {
        // 料金：通常コンテンツエリアを表示
        ShowContentArea();
    }

    private void SideButton2_Click(object sender, RoutedEventArgs e)
    {
        // テーブル：通常コンテンツエリアを表示
        ShowContentArea();
    }

    private void SideButton3_Click(object sender, RoutedEventArgs e)
    {
        // 入出金：通常コンテンツエリアを表示
        ShowContentArea();
    }

    private async void SideButtonMenu_Click(object sender, RoutedEventArgs e)
    {
        // メニュー：ProductMaster 画面を表示してデータ読み込み
        ShowMenuContent();
        await LoadProductMastersAsync();
    }

    // ─── 表示切り替え ─────────────────────────────────────────────────────

    private void ShowContentArea()
    {
        ContentArea.Visibility = Visibility.Visible;
        MenuContent.Visibility = Visibility.Collapsed;
    }

    private void ShowMenuContent()
    {
        ContentArea.Visibility = Visibility.Collapsed;
        MenuContent.Visibility = Visibility.Visible;
    }

    // ─── ProductMaster 読み込み ──────────────────────────────────────────

    private async Task LoadProductMastersAsync()
    {
        try
        {
            var items = await _apiService.GetProductMastersAsync();
            _productRows.Clear();

            if (items is null) return;

            foreach (var item in items)
                _productRows.Add(ProductMasterRow.FromResponse(item));
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"商品マスタの取得に失敗しました。\n{ex.Message}",
                "エラー",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    // ─── 追加ボタン ───────────────────────────────────────────────────────

    private void AddRowButton_Click(object sender, RoutedEventArgs e)
    {
        // 空行を末尾に追加してスクロール
        var newRow = new ProductMasterRow
        {
            BackUnit             = "円",
            MixSelectableDisplay  = "否",
            CastSelectableDisplay = "否",
            IsDeleted            = false,
        };
        _productRows.Add(newRow);

        // 追加行が見えるようにスクロール
        ProductGrid.ScrollIntoView(newRow);
        ProductGrid.SelectedItem = newRow;
    }

    // ─── 反映ボタン ───────────────────────────────────────────────────────

    private async void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
        // 編集中セルを確定させる
        ProductGrid.CommitEdit(DataGridEditingUnit.Row, exitEditingMode: true);

        // 全行（削除含む）を POST する
        var requests = _productRows
            .Select(r => r.ToRequest(AppSettings.ShopId))
            .ToList();

        if (requests.Count == 0)
        {
            MessageBox.Show("登録対象データがありません。", "確認",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        try
        {
            await _apiService.UpsertProductMastersAsync(requests);

            // 成功後：削除チェックがついた行を除外して再描画
            var toRemove = _productRows.Where(r => r.IsDeleted).ToList();
            foreach (var r in toRemove)
                _productRows.Remove(r);

            MessageBox.Show("反映しました。", "完了",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"反映に失敗しました。\n{ex.Message}",
                "エラー",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
