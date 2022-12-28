using System.ComponentModel.DataAnnotations.Schema;

namespace Feirum.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}