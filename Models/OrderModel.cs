namespace DutyFree.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
