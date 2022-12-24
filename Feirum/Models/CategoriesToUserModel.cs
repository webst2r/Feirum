namespace Feirum.Models
{
    public class CategoriesToUserModel
    {
        public int UserId { get; set; }
        public ICollection<Categories> Categories { get; set; }
    }
}
