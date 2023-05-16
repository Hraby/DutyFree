using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DutyFree.Models;

namespace DutyFree.Controllers;

public class MyOrders : Controller
{
    private readonly ILogger<MyOrders> _logger;

    public IActionResult MyOrder()
    {
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}