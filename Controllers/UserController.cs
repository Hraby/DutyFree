using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DutyFree.Models;
using DutyFree.Controllers;
using DutyFree.Data;
using Microsoft.AspNetCore.Http;

namespace DutyFree.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Database _database;

    public UserController(Database database, IHttpContextAccessor httpContextAccessor)
    {
        _database = database;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        var currentUser = GetCurrentUser();
        IEnumerable<UserModel> users = _database.GetUsers();
        IEnumerable<OrderModel> orders = _database.GetOrders(currentUser.UserId);

        return View("Index",new AdminViewModel { Users = users.ToList(), Orders = orders.ToList(), CurrentUser = currentUser });
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