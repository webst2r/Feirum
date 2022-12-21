namespace Feirum.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItem> Products { get; set; }

        public Order(List<OrderItem> products)
        {
            Products = products;
        }
    }
}
