using System.Security.Claims;
using DutyFree.Data;
using DutyFree.Models;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DutyFree.Controllers;

[Authorize]
public class SecurityController : Controller
{
    private readonly Database _database;

    public SecurityController(Database database)
    {
        _database = database;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        IEnumerable<UserModel> users = _database.GetUsers();

        ClaimsPrincipal claim = HttpContext.User;
        System.Security.Principal.IIdentity? identity = claim.Identity;
        if (identity.IsAuthenticated)
            return RedirectToAction("Index", "Products");

        return View("Login", new AdminViewModel() { Users = users.ToList() });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(int userId)
    {
        var user = _database.GetUser(userId);

        if (user != null)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserId", user.UserId.ToString(), ClaimValueTypes.Integer),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("ImageUrl", user.ImageUrl ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties();
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties).Wait();
            return RedirectToAction("Index", "Products");
        }

        return RedirectToAction("Index", "Products");
    }

    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Security");
    }
}