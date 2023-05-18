using DutyFree.Models;
using Dapper;
using System.Data;
using DutyFree.Controllers;

namespace DutyFree.Data;

public class Database
{
    private readonly IDbConnection _connection;

    public Database(IDbConnection connection)
    {
        _connection = connection;
    }

    public IEnumerable<ProductModel> GetProducts()
    {
        string query = "exec dbo.ProcProducts";
        return _connection.Query<ProductModel>(query).ToList();
    }

    public int InsertProduct(ProductModel product)
    {
        string query = "EXEC dbo.ProcProductInsert @Name, @ImageUrl, @Quantity, @Price";
        return _connection.QuerySingle<int>(query, product);
    }

    public void EditProduct(ProductModel product)
    {

        string query = "EXEC dbo.ProcProductEdit @ProductId, @Name, @ImageUrl, @Quantity, @Price";
        _connection.Execute(query, product);
    }

    public void DeleteProduct(int productId)
    {
        string query = "EXEC dbo.ProcProductDelete @ProductId";
        var parameters = new { ProductId = productId };
        _connection.Execute(query, parameters);
    }
}

