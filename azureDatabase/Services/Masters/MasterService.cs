using azureDatabase.Infrastructure.Database;
using azureDatabase.Models.Entities.Masters;
using azureDatabase.Repositories.Interfaces;

namespace azureDatabase.Services.Masters;

public class MasterService
{
    private readonly IMasterRepository _masterRepository;
    private readonly DapperContext _context;

    public MasterService(
        IMasterRepository masterRepository,
        DapperContext context)
    {
        _masterRepository = masterRepository;
        _context = context;
    }

    // ─── SystemMaster ─────────────────────────────────────────────────────

    public async Task UpsertSystemMasterAsync(
        SystemMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _masterRepository.UpsertSystemMasterAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<SystemMasterEntity>> GetSystemMastersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _masterRepository.GetSystemMastersAsync(connection, shopId, cancellationToken);
    }

    // ─── TimeMaster ───────────────────────────────────────────────────────

    public async Task UpsertTimeMasterAsync(
        TimeMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _masterRepository.UpsertTimeMasterAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<TimeMasterEntity>> GetTimeMastersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _masterRepository.GetTimeMastersAsync(connection, shopId, cancellationToken);
    }

    // ─── SeatMaster ───────────────────────────────────────────────────────

    public async Task UpsertSeatMasterAsync(
        SeatMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _masterRepository.UpsertSeatMasterAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<SeatMasterEntity>> GetSeatMastersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _masterRepository.GetSeatMastersAsync(connection, shopId, cancellationToken);
    }

    // ─── SystemOptionMaster ───────────────────────────────────────────────

    public async Task UpsertSystemOptionMasterAsync(
        SystemOptionMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _masterRepository.UpsertSystemOptionMasterAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<SystemOptionMasterEntity>> GetSystemOptionMastersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _masterRepository.GetSystemOptionMastersAsync(connection, shopId, cancellationToken);
    }

    // ─── BonusMaster ──────────────────────────────────────────────────────

    public async Task UpsertBonusMasterAsync(
        BonusMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _masterRepository.UpsertBonusMasterAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<BonusMasterEntity>> GetBonusMastersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _masterRepository.GetBonusMastersAsync(connection, shopId, cancellationToken);
    }

    // ─── ProductMaster ────────────────────────────────────────────────────

    public async Task UpsertProductMasterAsync(
        ProductMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _masterRepository.UpsertProductMasterAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<ProductMasterEntity>> GetProductMastersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _masterRepository.GetProductMastersAsync(connection, shopId, cancellationToken);
    }

    // ─── HourlyWageAdditionMaster ─────────────────────────────────────────

    public async Task UpsertHourlyWageAdditionMasterAsync(
        HourlyWageAdditionMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _masterRepository.UpsertHourlyWageAdditionMasterAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<HourlyWageAdditionMasterEntity>> GetHourlyWageAdditionMastersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _masterRepository.GetHourlyWageAdditionMastersAsync(connection, shopId, cancellationToken);
    }

    // ─── CastMaster ───────────────────────────────────────────────────────

    public async Task UpsertCastMasterAsync(
        CastMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _masterRepository.UpsertCastMasterAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<CastMasterEntity>> GetCastMastersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _masterRepository.GetCastMastersAsync(connection, shopId, cancellationToken);
    }

    // ─── CashInOutMaster ──────────────────────────────────────────────────

    public async Task UpsertCashInOutMasterAsync(
        CashInOutMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            await _masterRepository.UpsertCashInOutMasterAsync(connection, transaction, entity, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            if (transaction.Connection != null)
                await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<CashInOutMasterEntity>> GetCashInOutMastersAsync(
        string shopId,
        CancellationToken cancellationToken = default)
    {
        await using var connection = _context.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        return await _masterRepository.GetCashInOutMastersAsync(connection, shopId, cancellationToken);
    }
}
