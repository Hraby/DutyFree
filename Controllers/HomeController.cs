using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DutyFree.Models;

namespace DutyFree.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Login()
    {
        return View();
    }
    
}