using DutyFree.Models;
using Microsoft.Extensions.Options;

namespace DutyFree.Data;

using Dapper;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public class Database
{
    private static string connectionString;

    public static IDbConnection Connection
    {
        get
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            return new SqlConnection(connectionString);
        }
    }
    
    public IEnumerable<ProductModel> GetAllProducts()
    {
        using (var connection = Database.Connection)
        {
            connection.Open();  
            return connection.Query<ProductModel>("ProcProducts", commandType: CommandType.StoredProcedure);
        }
    }

    public void InsertProduct(ProductModel product)
    {
        using (var connection = Database.Connection)
        {
            connection.Open();
            var parameters = new
            {
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                Price = product.Price
            };
            connection.Execute("ProcProductInsert", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public void UpdateProduct(ProductModel product)
    {
        using (var connection = Database.Connection)
        {
            connection.Open();
            var parameters = new
            {
                ProductId = product.ProductId,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                Price = product.Price,
                DateUpdated = DateTime.Now,
                UpdatedBy = product.UpdatedBy
            };
            connection.Execute("ProcProductEdit", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public void DeleteProduct(int productId)
    {
        using (var connection = Database.Connection)
        {
            connection.Open();
            var parameters = new
            {
                ProductId = productId,
                DateUpdated = DateTime.Now,
                UpdatedBy = 0,
                IsDeleted = true
            };
            connection.Execute("ProcProductDelete", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}

