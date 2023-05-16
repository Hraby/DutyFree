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
    
    private static List<ProductModel> _products = new List<ProductModel>
    {
        new ProductModel { Name = "Pepsi", Price = 20, Quantity = 2},
        new ProductModel { Name = "Cola", Price = 20, Quantity = 3 },
        new ProductModel { Name = "Tatranka", Price = 10, Quantity = 5 },
        new ProductModel { Name = "Čokoláda Milka", Price = 40, Quantity = 3 }
    };
    
    public IActionResult Administration()
    {
        var ViewModel = new AdminViewModel();
        ViewModel.Products = _products;
        return View(ViewModel);
    }

    // Todo -> actions: [HttpDelete] delete, [HttpPost] edit
}