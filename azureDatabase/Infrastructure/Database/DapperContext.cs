using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace azureDatabase.Infrastructure.Database;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly SqlRetryLogicBaseProvider _retryLogicProvider;

    public DapperContext(
        IConfiguration configuration)
    {
        _configuration = configuration;

        // Azure SQL Database（サーバーレス）はアイドル時に自動一時停止し、
        // 再開（オートレジューム）に数十秒～1分程度かかることがある。
        // 再開中の接続は一時エラー（40613等。既定のTransientErrorsに含まれる）を返すため、
        // 指数バックオフで最大5回・間隔上限30秒までリトライし、再開完了を待つ。
        var retryOptions = new SqlRetryLogicOption
        {
            NumberOfTries   = 5,
            DeltaTime       = TimeSpan.FromSeconds(5),
            MaxTimeInterval = TimeSpan.FromSeconds(30),
        };
        _retryLogicProvider = SqlConfigurableRetryFactory.CreateExponentialRetryProvider(retryOptions);

        // オートレジューム直後はクエリ応答が遅くなるため、Dapperの既定コマンドタイムアウトを延長する
        SqlMapper.Settings.CommandTimeout = 60;
    }

    public SqlConnection CreateConnection()
    {
        var connectionString =
            _configuration[
                "SqlConnectionString"];

        return new SqlConnection(connectionString)
        {
            RetryLogicProvider = _retryLogicProvider,
        };
    }
}