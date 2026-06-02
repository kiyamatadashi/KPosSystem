using System.Data.Common;

using Microsoft.Data.SqlClient;

using azureDatabase.Models.Entities.Orders;

namespace azureDatabase.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<long> CreateOrderAsync(
        SqlConnection connection,
        DbTransaction transaction,
        OrderEntity order,
        CancellationToken cancellationToken = default);

    Task<List<OrderEntity>> GetOrdersAsync(
        SqlConnection connection,
        CancellationToken cancellationToken = default);

    Task UpdateOrderStatusAsync(
        SqlConnection connection,
        DbTransaction transaction,
        string shopId,
        long orderId,
        bool status,
        CancellationToken cancellationToken = default);
}