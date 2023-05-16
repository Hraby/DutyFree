using DutyFree.Models;
using Microsoft.AspNetCore.Mvc;

namespace DutyFree.Controllers;

public class ProductsController : Controller
{
    private static List<Product> _products = new List<Product>
    {
        new Product { Name = "Pepsi", Price = 38 },
        new Product { Name = "Cola", Price = 29 },
        new Product { Name = "Cola", Price = 29 }
    };
    
    public IActionResult Administration()
    {
        var ViewModel = new AdminViewModel();
        ViewModel.Products = _products;
        return View(ViewModel);
    }

    
}