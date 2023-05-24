using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DutyFree.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using DutyFree.Data;

namespace DutyFree.Controllers;

[Authorize]
public class MyOrdersController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Database _database;

    public MyOrdersController(Database database, IHttpContextAccessor httpContextAccessor)
    {
        _database = database;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        var user = GetCurrentUser();
        IEnumerable<OrderModel> orders = _database.GetOrders(user.UserId);
        return View("Index", new AdminViewModel { Orders = orders.ToList() });
    }

    [HttpDelete]
    public IActionResult Delete(int orderId)
    {
        if (ModelState.IsValid)
        {
            _database.DeleteOrder(orderId);
        }
        return null;
    }
    
    [Authorize(Policy = "Administrator")]
    public IActionResult Administration()
    {
        var user = GetCurrentUser();
        IEnumerable<UserModel> users = _database.GetUsers();
        IEnumerable<OrderModel> orders = _database.GetOrders2();
        return View("Administration", new AdminViewModel() { Users = users.ToList(), Orders = orders.ToList() });
    }

    private UserModel GetCurrentUser()
    {
        var context = _httpContextAccessor.HttpContext;
        var userIdClaim = HttpContext.User.FindFirst("UserId");
        if (int.TryParse(userIdClaim?.Value, out int userId))
        {
            var user = _database.GetUser(userId);
            return user;
        }
        return null;
    }

}