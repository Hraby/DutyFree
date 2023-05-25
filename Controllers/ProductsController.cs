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
    private readonly IWebHostEnvironment _environment;

    public ProductsController(Database database, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
    {
        _database = database;
        _httpContextAccessor = httpContextAccessor;
        _environment = environment;
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
    public ActionResult Insert(string Name, int Price, int Quantity, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string imagePath = Path.Combine(_environment.WebRootPath, "images", "products", fileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            var user = GetCurrentUser();
            string imageUrl = "/images/products/" + fileName;
            _database.InsertProduct(Name, Price, Quantity, imageUrl, user.UserId);

            return Json(new { success = true, message = "Produkt byl úspěšně vytvořen!" });
        }

        return Json(new { success = false, message = "Chyba při vytváření produktu." });
    }

    [HttpDelete]
    public JsonResult Buy(int productId)
    {
        if (ModelState.IsValid)
        {
            var user = GetCurrentUser();
            var product = _database.GetProduct(productId);
            _database.BuyProduct(productId, user.UserId, product.Name, product.Price);
            return Json(new { Success = true });
        }
        return Json(new { Success = false });
    }

    [HttpPut]
    public ActionResult Edit(int productId, string name, int price, int quantity)
    {
        if (ModelState.IsValid)
        {
            var user = GetCurrentUser();
            _database.EditProduct(user.UserId, productId, name, price, quantity);
        }
        return null;
    }
    

    [HttpDelete]
    public ActionResult Delete(int productId)
    {
        if (ModelState.IsValid)
        {
            var user = GetCurrentUser();
            _database.DeleteProduct(productId, user.UserId);
        }
        return null;
    }

    private UserModel GetCurrentUser()
    {
        var context = _httpContextAccessor.HttpContext;
        var userIdClaim = HttpContext.User.FindFirst("UserId");
        if (int.TryParse(userIdClaim?.Value, out int userId))
        {
            return _database.GetUser(userId);
        }
        return null;
    }
}