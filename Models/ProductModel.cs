using System.ComponentModel.DataAnnotations;

namespace DutyFree.Models;

public class ProductModel
{
    public int ProductId { get; set; }
    public DateTime DateCreate { get; set; }
    public int CreatedBy { get; set; }
    public DateTime DateUpdated { get; set; }
    public int UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
    public IFormFile Image { get; set; }
}