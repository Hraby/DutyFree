using DutyFree.Data;
using DutyFree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace DutyFree.Controllers;

public class ProductsController : Controller
{
    private readonly Database _database;

    public ProductsController()
    {
        _database = new Database();
    }

    public IActionResult Administration()
    {
        var products = _database.GetAllProducts();
        return View(products);
    }

    // [HttpPut]
    // public IActionResult Edit()
    // {
    //     
    // }
    //
    // [HttpPost]
    // public IActionResult Insert()
    // {
    //     
    // }
    //
    // [HttpDelete]
    // public IActionResult Delete()
    // {
    //     
    // }

    // Todo -> actions: [HttpDelete] delete, [HttpPost] edit
}