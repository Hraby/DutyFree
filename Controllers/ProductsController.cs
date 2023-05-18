using Dapper;
using DutyFree.Data;
using DutyFree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace DutyFree.Controllers;

public class ProductsController : Controller
{
    private readonly Database _database;

    public ProductsController(Database database)
    {
        _database = database;
    }

    
    
    public IActionResult Index()
    {
        IEnumerable<ProductModel> products = _database.GetProducts();
        return View("Index", new AdminViewModel() { Products = products.ToList() });
    }

    

    [HttpPost]
    public IActionResult Create(ProductModel product)
    {
        if (ModelState.IsValid)
        {
            string imageFileName = UploadImage(product.Image);

            _database.InsertProduct(product);

            return RedirectToAction("Index");
        }

        return View("Administration");
    }

    [HttpPut]
    public IActionResult Edit(ProductModel product)
    {
        // if (ModelState.IsValid)
        // {
        //     string imageFileName = null;
        //     if (product.Image != null)
        //     {
        //         imageFileName = UploadImage(product.Image);
        //     }
        //
        //     _database.EditProduct(product);
        //
        //     return RedirectToAction("Index");
        // }

        return View("Administration");
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _database.DeleteProduct(id);
        return Ok();
    }

    private string UploadImage(IFormFile image)
    {
        // if (image != null && image.Length > 0)
        // {
        //     var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        //     var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
        //     using (var fileStream = new FileStream(filePath, FileMode.Create))
        //     {
        //         image.CopyTo(fileStream);
        //     }
        //     return fileName;
        // }
        return null;
    }

}