namespace DutyFree.Data;

using Dapper;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public class Database
{
    private readonly IConfiguration _configuration;

    public Database(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection GetDbConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}

