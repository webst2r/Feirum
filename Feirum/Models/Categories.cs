using System.ComponentModel.DataAnnotations.Schema;

namespace Feirum.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        public List<Fairs> FairsList { get; set; }
    }
}
