using System.Text.Json;

using azureDatabase.Infrastructure.Database;
using azureDatabase.Models.Entities.Orders;
using azureDatabase.Repositories.Interfaces;

using shared.Models.Requests.Orders;
using shared.Models.Responses.Orders;

namespace azureDatabase.Services.Orders;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly DapperContext _context;

    public OrderService(
        IOrderRepository orderRepository,
        DapperContext context)
    {
        _orderRepository = orderRepository;
        _context = context;
    }

    public async Task<long> CreateOrderAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction =
            await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            var order = new OrderEntity
            {
                ShopID          = request.ShopID,
                GroupID         = request.GroupID,
                SetNumber       = request.SetNumber,
                OrderDateTime   = DateTime.UtcNow,
                Category        = request.Category,
                SideMenu        = request.SideMenu,
                ProductName     = request.ProductName,
                Amount          = request.Amount,
                Quantity        = request.Quantity,
                BackAmount      = request.BackAmount,
                BackUnit        = request.BackUnit,
                MixerSelectable = request.MixerSelectable,
                CastSelectable  = request.CastSelectable,
                // CastNames（List<string>）をJSON文字列に変換してDBへ保存
                CastName        = request.CastNames.Count > 0
                                    ? JsonSerializer.Serialize(request.CastNames)
                                    : null,
                Status          = false
            };

            var orderId = await _orderRepository.CreateOrderAsync(
                connection, transaction, order, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return orderId;
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<OrderResponse>> GetOrdersAsync(
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        var orders = await _orderRepository.GetOrdersAsync(
            connection, cancellationToken);

        return orders.Select(x => new OrderResponse
        {
            OrderID         = x.OrderID,
            GroupID         = x.GroupID,
            SetNumber       = x.SetNumber,
            OrderDateTime   = x.OrderDateTime,
            Category        = x.Category,
            SideMenu        = x.SideMenu,
            ProductName     = x.ProductName,
            Amount          = x.Amount,
            Quantity        = x.Quantity,
            BackAmount      = x.BackAmount,
            BackUnit        = x.BackUnit,
            MixerSelectable = x.MixerSelectable,
            CastSelectable  = x.CastSelectable,
            // JSON文字列をそのままレスポンスに渡す（デシリアライズはOrderResponse側で行う）
            CastName        = x.CastName,
            Status          = x.Status
        }).ToList();
    }

    public async Task UpdateOrderStatusAsync(
        string shopId,
        long orderId,
        bool status,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction =
            await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _orderRepository.UpdateOrderStatusAsync(
                connection, transaction, shopId, orderId, status, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
