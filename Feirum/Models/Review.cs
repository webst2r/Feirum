namespace Feirum.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FairId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
