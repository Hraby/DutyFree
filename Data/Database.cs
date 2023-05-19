using DutyFree.Models;
using Dapper;
using System.Data;
using DutyFree.Controllers;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

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

    public IEnumerable<ProductModel> GetProducts2()
    {
        string query = "select * from dutyfree.dbo.products where Quantity > 0";
        return _connection.Query<ProductModel>(query).ToList();
    }

    public int InsertProduct(string name, int price, int quantity)
    {
        string query = "INSERT INTO dutyfree.dbo.products (DateCreated, CreatedBy, DateUpdated, UpdatedBy, IsDeleted, Name, Price, Quantity, ImageUrl) VALUES (GETDATE(), 1, GETDATE(), 1, 0, @Name, @Price, @Quantity, 0);";
        var par = new { Name = name, Price = price, Quantity = quantity};
        return (int)_connection.ExecuteScalar(query, par);
    }

    public void EditProduct(int id, string name, int price, int quantity)
    {
        string query = "EXEC dbo.ProcProductEdit @ProductId, @Name, @ImageUrl, @Quantity, @Price";
        var par = new { ProductId = id, Name = name, ImageUrl = 0, Quantity = quantity, Price = price };
        _connection.Execute(query, par);
    }

    public int DeleteProduct(int productId)
    {
        string query = "DELETE FROM dutyfree.dbo.Products WHERE ProductId=@ProductId";
        var par = new { ProductId = productId };
        return (int)_connection.ExecuteScalar(query, par);
    }

    public UserModel GetUser(int id)
    {
        string query = "select * from dutyfree.dbo.users where UserId = @UserId";
        var par = new { UserId = id };
        var user = _connection.QuerySingleOrDefault<UserModel>(query, par);
        return user;
    }

    public IEnumerable<UserModel> GetUsers()
    {
        string query = "exec dbo.ProcUsers";
        return _connection.Query<UserModel>(query).ToList();
    }
}

