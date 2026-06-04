using System.Data.Common;

using Microsoft.Data.SqlClient;

using azureDatabase.Models.Entities.Masters;

namespace azureDatabase.Repositories.Interfaces;

public interface IMasterRepository
{
    // SystemMaster
    Task UpsertSystemMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        SystemMasterEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<SystemMasterEntity>> GetSystemMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);

    // TimeMaster
    Task UpsertTimeMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        TimeMasterEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<TimeMasterEntity>> GetTimeMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);

    // SeatMaster
    Task UpsertSeatMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        SeatMasterEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<SeatMasterEntity>> GetSeatMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);

    // SystemOptionMaster
    Task UpsertSystemOptionMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        SystemOptionMasterEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<SystemOptionMasterEntity>> GetSystemOptionMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);

    // BonusMaster
    Task UpsertBonusMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        BonusMasterEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<BonusMasterEntity>> GetBonusMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);

    // ProductMaster
    Task UpsertProductMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        ProductMasterEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<ProductMasterEntity>> GetProductMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);

    // HourlyWageAdditionMaster
    Task UpsertHourlyWageAdditionMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        HourlyWageAdditionMasterEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<HourlyWageAdditionMasterEntity>> GetHourlyWageAdditionMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);

    // CastMaster
    Task UpsertCastMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        CastMasterEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<CastMasterEntity>> GetCastMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);

    // CashInOutMaster
    Task UpsertCashInOutMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        CashInOutMasterEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<CashInOutMasterEntity>> GetCashInOutMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);
}
