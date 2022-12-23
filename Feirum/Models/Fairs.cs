using System.ComponentModel.DataAnnotations.Schema;

namespace Feirum.Models
{
    public class Fairs
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        [NotMapped]
        public List<Product>? Products { get; set; }
        [NotMapped]
        public List<string>? ImagesURL { get; set; }

        /*
        public Fairs(int id, string ownerId, int categoryId, string description, bool state, string email, string phone)
        {
            Id = id;
            OwnerId = ownerId;
            CategoryId = categoryId;
            Description= description;
            State = state;
            Products = new List<Product>();
            Email = email;
            Phone = phone;
            ImagesURL= new List<string>();
        }
        */
     
    }
}