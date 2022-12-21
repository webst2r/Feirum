namespace Feirum.Models
{
    public class Fair
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public List<Product> Products { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<string> ImagesURL { get; set; }

        public Fair(List<string> imagesURL)
        {
            ImagesURL = imagesURL;
        }
    }
}