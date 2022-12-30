using System.ComponentModel.DataAnnotations;

namespace Feirum.Models
{
    public class FavoriteFair
    {
        [Key]
        public string UserId { get; set; }
        [Key]
        public int FairId { get; set; }
    }
}
