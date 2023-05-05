using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BackendTestTask.Core;

public class DapperContext
{
    private readonly string? _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqlConnection");
    }
    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
}