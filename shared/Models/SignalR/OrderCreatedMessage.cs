using System.Text.Json;

namespace shared.Models.SignalR;

public class OrderCreatedMessage
{
    public string ShopID { get; set; } = string.Empty;
    public long OrderID { get; set; }
    public string GroupID { get; set; } = string.Empty;
    public string SetNumber { get; set; } = string.Empty;
    public DateTime OrderDateTime { get; set; }
    public string Category { get; set; } = string.Empty;
    public string SideMenu { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Amount { get; set; }
    public int Quantity { get; set; }
    public int BackAmount { get; set; }
    public string BackUnit { get; set; } = string.Empty;
    public bool MixerSelectable { get; set; }
    public bool CastSelectable { get; set; }

    /// <summary>
    /// DBから取得したJSON文字列。例: ["さくら","あおい"]
    /// </summary>
    public string? CastName { get; set; }

    /// <summary>
    /// CastName（JSON文字列）をデシリアライズしたリスト。
    /// UI表示・ロジック処理にはこちらを使う。
    /// </summary>
    public List<string> CastNames =>
        string.IsNullOrWhiteSpace(CastName)
            ? new List<string>()
            : JsonSerializer.Deserialize<List<string>>(CastName)
              ?? new List<string>();

    /// <summary>
    /// キャスト名をカンマ区切りで返す表示用プロパティ。
    /// 例: "さくら, あおい"
    /// </summary>
    public string CastNamesDisplay =>
        CastNames.Count > 0
            ? string.Join(", ", CastNames)
            : string.Empty;

    public bool Status { get; set; }
}
