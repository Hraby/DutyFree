using DutyFree.Models;
using Dapper;
using System.Data;
using DutyFree.Controllers;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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

    public int InsertProduct(string name, int price, int quantity)
    {
        string query = "exec dutyfree.dbo.ProcProductInsert @Name, @ImageUrl, @Quantity, @Price);";
        var par = new { Name = name, ImageUrl = 0,Price = price, Quantity = quantity};
        return _connection.Execute(query, par);
    }

    public void EditProduct(int id, string name, int price, int quantity)
    {
        string query = "EXEC dbo.ProcProductEdit @ProductId, @Name, @ImageUrl, @Quantity, @Price";
        var par = new { ProductId = id, Name = name, ImageUrl = 0, Quantity = quantity, Price = price };
        _connection.Execute(query, par);
    }

    public void DeleteProduct(int productId)
    {
        string query = "EXEC dbo.ProcProductDelete @ProductId";
        var par = new { ProductId = productId };
        _connection.Execute(query, par);
    }
}

