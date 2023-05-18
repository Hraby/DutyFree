using Microsoft.AspNetCore.Mvc;

namespace DutyFree.Controllers;

public class UsersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}