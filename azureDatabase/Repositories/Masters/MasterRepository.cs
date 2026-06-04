using System.Data.Common;

using Dapper;

using Microsoft.Data.SqlClient;

using azureDatabase.Models.Entities.Masters;
using azureDatabase.Repositories.Interfaces;

namespace azureDatabase.Repositories.Masters;

public class MasterRepository : IMasterRepository
{
    // ─── SystemMaster ─────────────────────────────────────────────────────

    public async Task UpsertSystemMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        SystemMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO SystemMaster AS target
USING (SELECT @ShopID AS ShopID, @SystemType AS SystemType, @SystemTypeName AS SystemTypeName) AS source
ON target.ShopID = source.ShopID
   AND target.SystemType = source.SystemType
   AND target.SystemTypeName = source.SystemTypeName
WHEN MATCHED THEN
    UPDATE SET Amount = @Amount, Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, SystemType, SystemTypeName, Amount, Deleted)
    VALUES (@ShopID, @SystemType, @SystemTypeName, @Amount, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<SystemMasterEntity>> GetSystemMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    SystemType,
    SystemTypeName,
    Amount,
    Deleted
FROM SystemMaster
WHERE ShopID = @ShopID
ORDER BY SystemType, SystemTypeName;
";
        var result = await connection.QueryAsync<SystemMasterEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }

    // ─── TimeMaster ───────────────────────────────────────────────────────

    public async Task UpsertTimeMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        TimeMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO TimeMaster AS target
USING (SELECT @ShopID AS ShopID, @TimeType AS TimeType, @TimeValue AS TimeValue) AS source
ON target.ShopID = source.ShopID
   AND target.TimeType = source.TimeType
   AND target.TimeValue = source.TimeValue
WHEN MATCHED THEN
    UPDATE SET Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, TimeType, TimeValue, Deleted)
    VALUES (@ShopID, @TimeType, @TimeValue, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<TimeMasterEntity>> GetTimeMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    TimeType,
    TimeValue,
    Deleted
FROM TimeMaster
WHERE ShopID = @ShopID
ORDER BY TimeType, TimeValue;
";
        var result = await connection.QueryAsync<TimeMasterEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }

    // ─── SeatMaster ───────────────────────────────────────────────────────

    public async Task UpsertSeatMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        SeatMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO SeatMaster AS target
USING (SELECT @ShopID AS ShopID, @SeatName AS SeatName) AS source
ON target.ShopID = source.ShopID
   AND target.SeatName = source.SeatName
WHEN MATCHED THEN
    UPDATE SET
        ServiceCharge = @ServiceCharge,
        SeatCharge = @SeatCharge,
        Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, SeatName, ServiceCharge, SeatCharge, Deleted)
    VALUES (@ShopID, @SeatName, @ServiceCharge, @SeatCharge, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<SeatMasterEntity>> GetSeatMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    SeatName,
    ServiceCharge,
    SeatCharge,
    Deleted
FROM SeatMaster
WHERE ShopID = @ShopID
ORDER BY SeatName;
";
        var result = await connection.QueryAsync<SeatMasterEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }

    // ─── SystemOptionMaster ───────────────────────────────────────────────

    public async Task UpsertSystemOptionMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        SystemOptionMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO SystemOptionMaster AS target
USING (SELECT @ShopID AS ShopID, @Option AS Option) AS source
ON target.ShopID = source.ShopID
   AND target.Option = source.Option
WHEN MATCHED THEN
    UPDATE SET Amount = @Amount, BackAmount = @BackAmount, Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, Option, Amount, BackAmount, Deleted)
    VALUES (@ShopID, @Option, @Amount, @BackAmount, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<SystemOptionMasterEntity>> GetSystemOptionMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    Option,
    Amount,
    BackAmount,
    Deleted
FROM SystemOptionMaster
WHERE ShopID = @ShopID
ORDER BY Option;
";
        var result = await connection.QueryAsync<SystemOptionMasterEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }

    // ─── BonusMaster ──────────────────────────────────────────────────────

    public async Task UpsertBonusMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        BonusMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO BonusMaster AS target
USING (SELECT @ShopID AS ShopID, @BonusName AS BonusName) AS source
ON target.ShopID = source.ShopID
   AND target.BonusName = source.BonusName
WHEN MATCHED THEN
    UPDATE SET
        Threshold = @Threshold,
        FirstPlace = @FirstPlace,
        SecondPlace = @SecondPlace,
        ThirdPlace = @ThirdPlace,
        Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, BonusName, Threshold, FirstPlace, SecondPlace, ThirdPlace, Deleted)
    VALUES (@ShopID, @BonusName, @Threshold, @FirstPlace, @SecondPlace, @ThirdPlace, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<BonusMasterEntity>> GetBonusMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    BonusName,
    Threshold,
    FirstPlace,
    SecondPlace,
    ThirdPlace,
    Deleted
FROM BonusMaster
WHERE ShopID = @ShopID
ORDER BY BonusName;
";
        var result = await connection.QueryAsync<BonusMasterEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }

    // ─── ProductMaster ────────────────────────────────────────────────────

    public async Task UpsertProductMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        ProductMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO ProductMaster AS target
USING (SELECT @ShopID AS ShopID, @Category AS Category, @SideMenu AS SideMenu, @ProductName AS ProductName) AS source
ON target.ShopID = source.ShopID
   AND target.Category = source.Category
   AND target.SideMenu = source.SideMenu
   AND target.ProductName = source.ProductName
WHEN MATCHED THEN
    UPDATE SET
        Amount = @Amount,
        BackAmount = @BackAmount,
        BackUnit = @BackUnit,
        MixSelectable = @MixSelectable,
        CastSelectable = @CastSelectable,
        Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, Category, SideMenu, ProductName, Amount, BackAmount, BackUnit, MixSelectable, CastSelectable, Deleted)
    VALUES (@ShopID, @Category, @SideMenu, @ProductName, @Amount, @BackAmount, @BackUnit, @MixSelectable, @CastSelectable, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<ProductMasterEntity>> GetProductMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    Category,
    SideMenu,
    ProductName,
    Amount,
    BackAmount,
    BackUnit,
    MixSelectable,
    CastSelectable,
    Deleted
FROM ProductMaster
WHERE ShopID = @ShopID
ORDER BY Category, SideMenu, ProductName;
";
        var result = await connection.QueryAsync<ProductMasterEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }

    // ─── HourlyWageAdditionMaster ─────────────────────────────────────────

    public async Task UpsertHourlyWageAdditionMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        HourlyWageAdditionMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO HourlyWageAdditionMaster AS target
USING (SELECT @ShopID AS ShopID, @ItemName AS ItemName) AS source
ON target.ShopID = source.ShopID
   AND target.ItemName = source.ItemName
WHEN MATCHED THEN
    UPDATE SET Amount = @Amount, Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, ItemName, Amount, Deleted)
    VALUES (@ShopID, @ItemName, @Amount, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<HourlyWageAdditionMasterEntity>> GetHourlyWageAdditionMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    ItemName,
    Amount,
    Deleted
FROM HourlyWageAdditionMaster
WHERE ShopID = @ShopID
ORDER BY ItemName;
";
        var result = await connection.QueryAsync<HourlyWageAdditionMasterEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }

    // ─── CastMaster ───────────────────────────────────────────────────────

    public async Task UpsertCastMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        CastMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO CastMaster AS target
USING (SELECT @ShopID AS ShopID, @CastName AS CastName) AS source
ON target.ShopID = source.ShopID
   AND target.CastName = source.CastName
WHEN MATCHED THEN
    UPDATE SET
        WorkType = @WorkType,
        SalaryType = @SalaryType,
        Amount = @Amount,
        TrainingHours = @TrainingHours,
        Password = @Password,
        Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, CastName, WorkType, SalaryType, Amount, TrainingHours, Password, Deleted)
    VALUES (@ShopID, @CastName, @WorkType, @SalaryType, @Amount, @TrainingHours, @Password, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<CastMasterEntity>> GetCastMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    CastName,
    WorkType,
    SalaryType,
    Amount,
    TrainingHours,
    Password,
    Deleted
FROM CastMaster
WHERE ShopID = @ShopID
ORDER BY CastName;
";
        var result = await connection.QueryAsync<CastMasterEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }

    // ─── CashInOutMaster ──────────────────────────────────────────────────

    public async Task UpsertCashInOutMasterAsync(
        SqlConnection connection,
        DbTransaction transaction,
        CashInOutMasterEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO CashInOutMaster AS target
USING (SELECT @ShopID AS ShopID, @ItemName AS ItemName, @Category AS Category) AS source
ON target.ShopID = source.ShopID
   AND target.ItemName = source.ItemName
   AND target.Category = source.Category
WHEN MATCHED THEN
    UPDATE SET CashInOutType = @CashInOutType, Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, ItemName, Category, CashInOutType, Deleted)
    VALUES (@ShopID, @ItemName, @Category, @CashInOutType, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<CashInOutMasterEntity>> GetCashInOutMastersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    ItemName,
    Category,
    CashInOutType,
    Deleted
FROM CashInOutMaster
WHERE ShopID = @ShopID
ORDER BY Category, ItemName;
";
        var result = await connection.QueryAsync<CashInOutMasterEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }
}
