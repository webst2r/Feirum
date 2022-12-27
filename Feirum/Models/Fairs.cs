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

        public string? Image { get; set; }

        [NotMapped]
        public List<Products>? Products { get; set; }
    }
}