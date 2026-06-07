using System.Data.Common;

using Dapper;

using Microsoft.Data.SqlClient;

using azureDatabase.Models.Entities.Orders;
using azureDatabase.Repositories.Interfaces;

namespace azureDatabase.Repositories.Orders;

public class OrderRepository : IOrderRepository
{
    public async Task<long> CreateOrderAsync(
        SqlConnection connection,
        DbTransaction transaction,
        OrderEntity order,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
INSERT INTO Orders
(
    ShopID,
    GroupID,
    SetNumber,
    OrderDateTime,
    Category,
    SideMenu,
    ProductName,
    Amount,
    Quantity,
    BackAmount,
    BackUnit,
    MixerSelectable,
    CastSelectable,
    CastName,
    Status
)
VALUES
(
    @ShopID,
    @GroupID,
    @SetNumber,
    @OrderDateTime,
    @Category,
    @SideMenu,
    @ProductName,
    @Amount,
    @Quantity,
    @BackAmount,
    @BackUnit,
    @MixerSelectable,
    @CastSelectable,
    @CastName,
    @Status
);

SELECT CAST(SCOPE_IDENTITY() AS BIGINT);
";
        return await connection
            .ExecuteScalarAsync<long>(sql, order, transaction);
    }

    public async Task<List<OrderEntity>> GetOrdersAsync(
        SqlConnection connection,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    OrderID,
    GroupID,
    SetNumber,
    OrderDateTime,
    Category,
    SideMenu,
    ProductName,
    Amount,
    Quantity,
    BackAmount,
    BackUnit,
    MixerSelectable,
    CastSelectable,
    CastName,
    Status,
    CreatedAt
FROM Orders
ORDER BY CreatedAt DESC;
";
        var result = await connection.QueryAsync<OrderEntity>(sql);
        return result.ToList();
    }

    public async Task UpdateOrderStatusAsync(
        SqlConnection connection,
        DbTransaction transaction,
        string shopId,
        long orderId,
        bool status,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
UPDATE Orders
SET Status = @Status
WHERE ShopID = @ShopID AND OrderID = @OrderID;
";
        await connection.ExecuteAsync(
            sql,
            new { ShopID = shopId, OrderID = orderId, Status = status },
            transaction);
    }
}
