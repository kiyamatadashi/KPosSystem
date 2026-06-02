using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace azureDatabase.Infrastructure.Database;

public class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection CreateConnection()
    {
        var connectionString =
            _configuration[
                "SqlConnectionString"];

        return new SqlConnection(
            connectionString);
    }
}