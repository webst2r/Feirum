namespace Feirum.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int FairId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Image { get; set; }
    }
}
