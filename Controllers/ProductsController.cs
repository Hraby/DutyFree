using Dapper;
using DutyFree.Data;
using DutyFree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace DutyFree.Controllers;

public class ProductsController : Controller
{
    private readonly Database _database;

    public ProductsController(Database database)
    {
        _database = database;
    }
    
    [HttpGet]
    public IActionResult Administration()
    {
        IEnumerable<ProductModel> products = _database.GetProducts();
        return View("Administration", new AdminViewModel(){Products = products.ToList()});
    }


    public IActionResult Index()
    {
        IEnumerable<ProductModel> products = _database.GetProducts();
        return View("Index", new AdminViewModel() { Products = products.ToList() });
    }

    [HttpPost]
    public JsonResult Insert(ProductModel product)
    {
        string name = product.Name;
        int price = product.Price;
        int quantity = product.Quantity;
        _database.InsertProduct(name, price, quantity);

        return Json(new { success = true, message = "Produkt byl úspěšně přidán do databáze" });
    }

    [HttpPut]
    public JsonResult Edit(ProductModel product)
    {
        int id = product.ProductId;
        string name = product.Name;
        int price = product.Price;
        int quantity = product.Quantity;
        _database.EditProduct(id, name, price, quantity);

        return Json(new {success = true, message = "Produkt byl úspěšně editován v databázi"});
    }

    [HttpDelete]
    public JsonResult Delete(ProductModel product)
    {
        int id = product.ProductId;
        _database.DeleteProduct(id);

        return Json(new { success = true, message = "Produkt byl odebrán z databáze" });
    }

}