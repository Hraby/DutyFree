using Microsoft.Extensions.Options;

namespace DutyFree.Data;

using Dapper;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public class Database
{
    private readonly string _connectionString;
    private IDbConnection Connection => new SqlConnection(_connectionString);
    
    
}

