using DutyFree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace DutyFree.Controllers;

public class ProductsController : Controller
{
    public IActionResult Index()
    {
        var ViewModel = new AdminViewModel();
        ViewModel.Products = _products;
        return View(ViewModel);
    }
    
    private static List<Product> _products = new List<Product>
    {
        new Product { Name = "Pepsi", Price = 20, Quantity = 2},
        new Product { Name = "Cola", Price = 20, Quantity = 3 },
        new Product { Name = "Tatranka", Price = 10, Quantity = 5 },
        new Product { Name = "Čokoláda Milka", Price = 40, Quantity = 3 }
    };
    
    public IActionResult Administration()
    {
        var ViewModel = new AdminViewModel();
        ViewModel.Products = _products;
        return View(ViewModel);
    }

    // Todo -> actions: [HttpDelete] delete, [HttpPost] edit
}