
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace DutyFree.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
