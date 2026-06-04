using System.Data.Common;

using Dapper;

using Microsoft.Data.SqlClient;

using azureDatabase.Models.Entities.Groups;
using azureDatabase.Repositories.Interfaces;

namespace azureDatabase.Repositories.Groups;

public class GroupRepository : IGroupRepository
{
    // ─── Groups ───────────────────────────────────────────────────────────

    public async Task CreateGroupAsync(
        SqlConnection connection,
        DbTransaction transaction,
        GroupEntity group,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
INSERT INTO Groups
(
    ShopID,
    GroupID,
    GuestCount,
    Companion,
    Nomination,
    CatchStaff,
    Deleted
)
VALUES
(
    @ShopID,
    @GroupID,
    @GuestCount,
    @Companion,
    @Nomination,
    @CatchStaff,
    @Deleted
);
";
        await connection.ExecuteAsync(sql, group, transaction);
    }

    public async Task<List<GroupEntity>> GetGroupsAsync(
        SqlConnection connection,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    GroupID,
    GuestCount,
    Companion,
    Nomination,
    CatchStaff,
    Deleted
FROM Groups
ORDER BY GroupID DESC;
";
        var result = await connection.QueryAsync<GroupEntity>(sql);
        return result.ToList();
    }

    public async Task DeleteGroupAsync(
        SqlConnection connection,
        DbTransaction transaction,
        string shopId,
        string groupId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
UPDATE Groups
SET Deleted = 1
WHERE ShopID = @ShopID AND GroupID = @GroupID;
";
        await connection.ExecuteAsync(
            sql,
            new { ShopID = shopId, GroupID = groupId },
            transaction);
    }

    // ─── Users ────────────────────────────────────────────────────────────

    public async Task CreateUserAsync(
        SqlConnection connection,
        DbTransaction transaction,
        UserEntity user,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
INSERT INTO Users
(
    ShopID,
    GroupID,
    UserID,
    SetNumber,
    ChargeType,
    DrinkType,
    SeatName,
    ServiceCharge,
    SetUnitTime
)
VALUES
(
    @ShopID,
    @GroupID,
    @UserID,
    @SetNumber,
    @ChargeType,
    @DrinkType,
    @SeatName,
    @ServiceCharge,
    @SetUnitTime
);
";
        await connection.ExecuteAsync(sql, user, transaction);
    }

    public async Task<List<UserEntity>> GetUsersAsync(
        SqlConnection connection,
        string shopId,
        string groupId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    GroupID,
    UserID,
    SetNumber,
    ChargeType,
    DrinkType,
    SeatName,
    ServiceCharge,
    SetUnitTime
FROM Users
WHERE ShopID = @ShopID AND GroupID = @GroupID
ORDER BY UserID;
";
        var result = await connection.QueryAsync<UserEntity>(
            sql,
            new { ShopID = shopId, GroupID = groupId });
        return result.ToList();
    }

    // ─── Requests ─────────────────────────────────────────────────────────

    public async Task UpsertRequestAsync(
        SqlConnection connection,
        DbTransaction transaction,
        RequestEntity request,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO Requests AS target
USING (SELECT @ShopID AS ShopID, @GroupID AS GroupID, @SetNumber AS SetNumber, @CastName AS CastName) AS source
ON target.ShopID = source.ShopID
   AND target.GroupID = source.GroupID
   AND target.SetNumber = source.SetNumber
   AND target.CastName = source.CastName
WHEN MATCHED THEN
    UPDATE SET RequestCount = @RequestCount
WHEN NOT MATCHED THEN
    INSERT (ShopID, GroupID, SetNumber, CastName, RequestCount)
    VALUES (@ShopID, @GroupID, @SetNumber, @CastName, @RequestCount);
";
        await connection.ExecuteAsync(sql, request, transaction);
    }

    public async Task<List<RequestEntity>> GetRequestsAsync(
        SqlConnection connection,
        string shopId,
        string groupId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    GroupID,
    SetNumber,
    CastName,
    RequestCount
FROM Requests
WHERE ShopID = @ShopID AND GroupID = @GroupID
ORDER BY SetNumber, CastName;
";
        var result = await connection.QueryAsync<RequestEntity>(
            sql,
            new { ShopID = shopId, GroupID = groupId });
        return result.ToList();
    }

    // ─── Works ────────────────────────────────────────────────────────────

    public async Task UpsertWorkAsync(
        SqlConnection connection,
        DbTransaction transaction,
        WorkEntity work,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO Works AS target
USING (SELECT @ShopID AS ShopID, @BusinessDate AS BusinessDate, @CastName AS CastName) AS source
ON target.ShopID = source.ShopID
   AND target.BusinessDate = source.BusinessDate
   AND target.CastName = source.CastName
WHEN MATCHED THEN
    UPDATE SET
        StartDateTime = @StartDateTime,
        EndDateTime = @EndDateTime,
        HourlyWageAddition = @HourlyWageAddition,
        AdvancePayment = @AdvancePayment,
        Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, BusinessDate, CastName, StartDateTime, EndDateTime, HourlyWageAddition, AdvancePayment, Deleted)
    VALUES (@ShopID, @BusinessDate, @CastName, @StartDateTime, @EndDateTime, @HourlyWageAddition, @AdvancePayment, @Deleted);
";
        await connection.ExecuteAsync(sql, work, transaction);
    }

    public async Task<List<WorkEntity>> GetWorksAsync(
        SqlConnection connection,
        string shopId,
        DateTime businessDate,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    BusinessDate,
    CastName,
    StartDateTime,
    EndDateTime,
    HourlyWageAddition,
    AdvancePayment,
    Deleted
FROM Works
WHERE ShopID = @ShopID AND BusinessDate = @BusinessDate
ORDER BY CastName;
";
        var result = await connection.QueryAsync<WorkEntity>(
            sql,
            new { ShopID = shopId, BusinessDate = businessDate });
        return result.ToList();
    }

    // ─── TrainingRemainingTime ─────────────────────────────────────────────

    public async Task UpsertTrainingRemainingTimeAsync(
        SqlConnection connection,
        DbTransaction transaction,
        TrainingRemainingTimeEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO TrainingRemainingTime AS target
USING (SELECT @ShopID AS ShopID, @ClosingYearMonth AS ClosingYearMonth, @CastName AS CastName) AS source
ON target.ShopID = source.ShopID
   AND target.ClosingYearMonth = source.ClosingYearMonth
   AND target.CastName = source.CastName
WHEN MATCHED THEN
    UPDATE SET
        RemainingTrainingHours = @RemainingTrainingHours,
        WorkType = @WorkType,
        SalaryType = @SalaryType,
        Amount = @Amount,
        Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, ClosingYearMonth, CastName, RemainingTrainingHours, WorkType, SalaryType, Amount, Deleted)
    VALUES (@ShopID, @ClosingYearMonth, @CastName, @RemainingTrainingHours, @WorkType, @SalaryType, @Amount, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<TrainingRemainingTimeEntity>> GetTrainingRemainingTimesAsync(
        SqlConnection connection,
        string shopId,
        string closingYearMonth,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    ClosingYearMonth,
    CastName,
    RemainingTrainingHours,
    WorkType,
    SalaryType,
    Amount,
    Deleted
FROM TrainingRemainingTime
WHERE ShopID = @ShopID AND ClosingYearMonth = @ClosingYearMonth
ORDER BY CastName;
";
        var result = await connection.QueryAsync<TrainingRemainingTimeEntity>(
            sql,
            new { ShopID = shopId, ClosingYearMonth = closingYearMonth });
        return result.ToList();
    }

    // ─── PayrollAdjustment ────────────────────────────────────────────────

    public async Task UpsertPayrollAdjustmentAsync(
        SqlConnection connection,
        DbTransaction transaction,
        PayrollAdjustmentEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO PayrollAdjustment AS target
USING (SELECT @ShopID AS ShopID, @CastName AS CastName, @ClosingYearMonth AS ClosingYearMonth) AS source
ON target.ShopID = source.ShopID
   AND target.CastName = source.CastName
   AND target.ClosingYearMonth = source.ClosingYearMonth
WHEN MATCHED THEN
    UPDATE SET Amount = @Amount, Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, CastName, ClosingYearMonth, Amount, Deleted)
    VALUES (@ShopID, @CastName, @ClosingYearMonth, @Amount, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<PayrollAdjustmentEntity>> GetPayrollAdjustmentsAsync(
        SqlConnection connection,
        string shopId,
        string closingYearMonth,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    CastName,
    ClosingYearMonth,
    Amount,
    Deleted
FROM PayrollAdjustment
WHERE ShopID = @ShopID AND ClosingYearMonth = @ClosingYearMonth
ORDER BY CastName;
";
        var result = await connection.QueryAsync<PayrollAdjustmentEntity>(
            sql,
            new { ShopID = shopId, ClosingYearMonth = closingYearMonth });
        return result.ToList();
    }

    // ─── CashInOut ────────────────────────────────────────────────────────

    public async Task CreateCashInOutAsync(
        SqlConnection connection,
        DbTransaction transaction,
        CashInOutEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
INSERT INTO CashInOut
(
    ShopID,
    TransactionDate,
    Category,
    ItemName,
    CashInOutType,
    Amount,
    Memo,
    Deleted
)
VALUES
(
    @ShopID,
    @TransactionDate,
    @Category,
    @ItemName,
    @CashInOutType,
    @Amount,
    @Memo,
    @Deleted
);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<CashInOutEntity>> GetCashInOutsAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    TransactionDate,
    Category,
    ItemName,
    CashInOutType,
    Amount,
    Memo,
    Deleted
FROM CashInOut
WHERE ShopID = @ShopID
ORDER BY TransactionDate DESC;
";
        var result = await connection.QueryAsync<CashInOutEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }

    // ─── RemoteOrders ─────────────────────────────────────────────────────

    public async Task UpsertRemoteOrderAsync(
        SqlConnection connection,
        DbTransaction transaction,
        RemoteOrderEntity entity,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
MERGE INTO RemoteOrders AS target
USING (SELECT @ShopID AS ShopID, @ReceptionDate AS ReceptionDate, @Category AS Category, @SideMenu AS SideMenu, @ProductName AS ProductName) AS source
ON target.ShopID = source.ShopID
   AND target.ReceptionDate = source.ReceptionDate
   AND target.Category = source.Category
   AND target.SideMenu = source.SideMenu
   AND target.ProductName = source.ProductName
WHEN MATCHED THEN
    UPDATE SET
        YearMonth = @YearMonth,
        Amount = @Amount,
        Quantity = @Quantity,
        BackAmount = @BackAmount,
        BackUnit = @BackUnit,
        CastName = @CastName,
        Deleted = @Deleted
WHEN NOT MATCHED THEN
    INSERT (ShopID, ReceptionDate, Category, SideMenu, ProductName, YearMonth, Amount, Quantity, BackAmount, BackUnit, CastName, Deleted)
    VALUES (@ShopID, @ReceptionDate, @Category, @SideMenu, @ProductName, @YearMonth, @Amount, @Quantity, @BackAmount, @BackUnit, @CastName, @Deleted);
";
        await connection.ExecuteAsync(sql, entity, transaction);
    }

    public async Task<List<RemoteOrderEntity>> GetRemoteOrdersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT
    ShopID,
    ReceptionDate,
    Category,
    SideMenu,
    ProductName,
    YearMonth,
    Amount,
    Quantity,
    BackAmount,
    BackUnit,
    CastName,
    Deleted
FROM RemoteOrders
WHERE ShopID = @ShopID
ORDER BY ReceptionDate DESC;
";
        var result = await connection.QueryAsync<RemoteOrderEntity>(
            sql,
            new { ShopID = shopId });
        return result.ToList();
    }
}
