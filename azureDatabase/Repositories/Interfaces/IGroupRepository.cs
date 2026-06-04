using System.Data.Common;

using Microsoft.Data.SqlClient;

using azureDatabase.Models.Entities.Groups;

namespace azureDatabase.Repositories.Interfaces;

public interface IGroupRepository
{
    // Groups
    Task CreateGroupAsync(
        SqlConnection connection,
        DbTransaction transaction,
        GroupEntity group,
        CancellationToken cancellationToken = default);

    Task<List<GroupEntity>> GetGroupsAsync(
        SqlConnection connection,
        CancellationToken cancellationToken = default);

    Task DeleteGroupAsync(
        SqlConnection connection,
        DbTransaction transaction,
        string shopId,
        string groupId,
        CancellationToken cancellationToken = default);

    // Users
    Task CreateUserAsync(
        SqlConnection connection,
        DbTransaction transaction,
        UserEntity user,
        CancellationToken cancellationToken = default);

    Task<List<UserEntity>> GetUsersAsync(
        SqlConnection connection,
        string shopId,
        string groupId,
        CancellationToken cancellationToken = default);

    // Requests
    Task UpsertRequestAsync(
        SqlConnection connection,
        DbTransaction transaction,
        RequestEntity request,
        CancellationToken cancellationToken = default);

    Task<List<RequestEntity>> GetRequestsAsync(
        SqlConnection connection,
        string shopId,
        string groupId,
        CancellationToken cancellationToken = default);

    // Works
    Task UpsertWorkAsync(
        SqlConnection connection,
        DbTransaction transaction,
        WorkEntity work,
        CancellationToken cancellationToken = default);

    Task<List<WorkEntity>> GetWorksAsync(
        SqlConnection connection,
        string shopId,
        DateTime businessDate,
        CancellationToken cancellationToken = default);

    // TrainingRemainingTime
    Task UpsertTrainingRemainingTimeAsync(
        SqlConnection connection,
        DbTransaction transaction,
        TrainingRemainingTimeEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<TrainingRemainingTimeEntity>> GetTrainingRemainingTimesAsync(
        SqlConnection connection,
        string shopId,
        string closingYearMonth,
        CancellationToken cancellationToken = default);

    // PayrollAdjustment
    Task UpsertPayrollAdjustmentAsync(
        SqlConnection connection,
        DbTransaction transaction,
        PayrollAdjustmentEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<PayrollAdjustmentEntity>> GetPayrollAdjustmentsAsync(
        SqlConnection connection,
        string shopId,
        string closingYearMonth,
        CancellationToken cancellationToken = default);

    // CashInOut
    Task CreateCashInOutAsync(
        SqlConnection connection,
        DbTransaction transaction,
        CashInOutEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<CashInOutEntity>> GetCashInOutsAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);

    // RemoteOrders
    Task UpsertRemoteOrderAsync(
        SqlConnection connection,
        DbTransaction transaction,
        RemoteOrderEntity entity,
        CancellationToken cancellationToken = default);

    Task<List<RemoteOrderEntity>> GetRemoteOrdersAsync(
        SqlConnection connection,
        string shopId,
        CancellationToken cancellationToken = default);
}
