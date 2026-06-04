using azureDatabase.Infrastructure.Database;
using azureDatabase.Models.Entities.Groups;
using azureDatabase.Repositories.Interfaces;

namespace azureDatabase.Services.Groups;

public class GroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly DapperContext _context;

    public GroupService(
        IGroupRepository groupRepository,
        DapperContext context)
    {
        _groupRepository = groupRepository;
        _context = context;
    }

    // ─── Groups ───────────────────────────────────────────────────────────

    public async Task CreateGroupAsync(
        GroupEntity group,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _groupRepository.CreateGroupAsync(connection, transaction, group, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<GroupEntity>> GetGroupsAsync(
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _groupRepository.GetGroupsAsync(connection, cancellationToken);
    }

    public async Task DeleteGroupAsync(
        string shopId,
        string groupId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _groupRepository.DeleteGroupAsync(connection, transaction, shopId, groupId, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    // ─── Users ────────────────────────────────────────────────────────────

    public async Task CreateUserAsync(
        UserEntity user,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _groupRepository.CreateUserAsync(connection, transaction, user, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<UserEntity>> GetUsersAsync(
        string shopId,
        string groupId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _groupRepository.GetUsersAsync(connection, shopId, groupId, cancellationToken);
    }

    // ─── Requests ─────────────────────────────────────────────────────────

    public async Task UpsertRequestAsync(
        RequestEntity request,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _groupRepository.UpsertRequestAsync(connection, transaction, request, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<RequestEntity>> GetRequestsAsync(
        string shopId,
        string groupId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _groupRepository.GetRequestsAsync(connection, shopId, groupId, cancellationToken);
    }

    // ─── Works ────────────────────────────────────────────────────────────

    public async Task UpsertWorkAsync(
        WorkEntity work,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _groupRepository.UpsertWorkAsync(connection, transaction, work, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<WorkEntity>> GetWorksAsync(
        string shopId,
        DateTime businessDate,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _groupRepository.GetWorksAsync(connection, shopId, businessDate, cancellationToken);
    }

    // ─── TrainingRemainingTime ─────────────────────────────────────────────

    public async Task UpsertTrainingRemainingTimeAsync(
        TrainingRemainingTimeEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _groupRepository.UpsertTrainingRemainingTimeAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<TrainingRemainingTimeEntity>> GetTrainingRemainingTimesAsync(
        string shopId,
        string closingYearMonth,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _groupRepository.GetTrainingRemainingTimesAsync(connection, shopId, closingYearMonth, cancellationToken);
    }

    // ─── PayrollAdjustment ────────────────────────────────────────────────

    public async Task UpsertPayrollAdjustmentAsync(
        PayrollAdjustmentEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _groupRepository.UpsertPayrollAdjustmentAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<PayrollAdjustmentEntity>> GetPayrollAdjustmentsAsync(
        string shopId,
        string closingYearMonth,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _groupRepository.GetPayrollAdjustmentsAsync(connection, shopId, closingYearMonth, cancellationToken);
    }

    // ─── CashInOut ────────────────────────────────────────────────────────

    public async Task CreateCashInOutAsync(
        CashInOutEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _groupRepository.CreateCashInOutAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<CashInOutEntity>> GetCashInOutsAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _groupRepository.GetCashInOutsAsync(connection, shopId, cancellationToken);
    }

    // ─── RemoteOrders ─────────────────────────────────────────────────────

    public async Task UpsertRemoteOrderAsync(
        RemoteOrderEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _groupRepository.UpsertRemoteOrderAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<RemoteOrderEntity>> GetRemoteOrdersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _groupRepository.GetRemoteOrdersAsync(connection, shopId, cancellationToken);
    }
}
