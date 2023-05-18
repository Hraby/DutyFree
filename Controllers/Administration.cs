using DutyFree.Data;
using DutyFree.Models;
using Microsoft.AspNetCore.Mvc;

namespace DutyFree.Controllers
{
    public class Administration : Controller
    {
        private readonly Database _database;

        public Administration(Database database)
        {
            _database = database;
        }

        public IActionResult ProductsViewAdmin()
        {
            IEnumerable<ProductModel> products = _database.GetProducts();
            return View("Administration", new AdminViewModel() { Products = products.ToList() });
        }

        public IActionResult OrderViewAdmin()
        {
            return View();
        }

        public IActionResult UserViewAdmin()
        {
            return View();
        }
    }
}
