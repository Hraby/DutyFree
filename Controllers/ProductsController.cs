using Dapper;
using DutyFree.Data;
using DutyFree.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DutyFree.Controllers;

namespace DutyFree.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Database _database;

    public ProductsController(Database database, IHttpContextAccessor httpContextAccessor)
    {
        _database = database;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    [Authorize(Policy = "Administrator")]
    public IActionResult Administration()
    {
        IEnumerable<ProductModel> products = _database.GetProducts();
        return View("Administration", new AdminViewModel(){Products = products.ToList()});
    }

    public IActionResult Index(string search)
    {
        IEnumerable<ProductModel> products = _database.GetProducts2();

        if (!string.IsNullOrEmpty(search))
        {
            products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        }
        ViewBag.Search = search;
        return View("Index", new AdminViewModel() { Products = products.ToList() });
    }

    [HttpPost]
    public ActionResult Insert(string name, int price, int quantity)
    {
        if(ModelState.IsValid)
        {
            _database.InsertProduct(name, price, quantity);
        }

        return null;
    }

    [HttpPost]
    public JsonResult Buy(int productId)
    {
        if (ModelState.IsValid)
        {
            var user = GetCurrentUser();
            var product = _database.GetProduct(productId);
            _database.BuyProduct(productId, user.UserId, product.Name, product.Price);
            return Json(new { success = true, message = productId + " " + user.UserId + " " + product.Name + " " + product.Price });
        }

        return Json(new { success = false, message = "Invalid model state." });
    }

    [HttpPut]
    public ActionResult Edit(int productId, string name, int price, int quantity)
    {
        if (ModelState.IsValid)
        {
            _database.EditProduct(productId, name, price, quantity);
        }

        return Ok();
    }
    

    [HttpDelete]
    public ActionResult Delete(int productId)
    {
        if (ModelState.IsValid)
        {
            _database.DeleteProduct(productId);
        }

        return Ok();
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