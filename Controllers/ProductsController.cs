using Dapper;
using DutyFree.Data;
using DutyFree.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutyFree.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly Database _database;

    public ProductsController(Database database)
    {
        _database = database;
    }

    [HttpGet]
    [Authorize(Policy = "Administrator")]
    public IActionResult Administration()
    {
        IEnumerable<ProductModel> products = _database.GetProducts();
        return View("Administration", new AdminViewModel(){Products = products.ToList()});
    }
    

    public IActionResult Index(string search)
    {
        IEnumerable<ProductModel> products = _database.GetProducts2();

        if (!string.IsNullOrEmpty(search))
        {
            products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        }
        ViewBag.Search = search;
        return View("Index", new AdminViewModel() { Products = products.ToList() });
    }

    [HttpPost]
    public JsonResult Insert(string name, int price, int quantity)
    {
        if(ModelState.IsValid)
        {
            _database.InsertProduct(name, price, quantity);
            return Json(new { success = true, message = "Produkt byl úspěšně přidán do databáze" });
        }
        return Json(new { success = false, message = "Produkt nebyl přidán do databáze" });
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