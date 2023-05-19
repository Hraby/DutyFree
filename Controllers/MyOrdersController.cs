using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DutyFree.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace DutyFree.Controllers;

public class MyOrdersController : Controller
{
    private readonly ILogger<HomeController> _logger;

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Administration()
    {
        return View();
    }

}