using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DutyFree.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace DutyFree.Controllers;

[Authorize]
public class MyOrdersController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public IActionResult Index()
    {
        return View();
    }
    
    [Authorize(Policy = "Administrator")]
    public IActionResult Administration()
    {
        return View();
    }

}