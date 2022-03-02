namespace OnlineMall.API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int RateStar { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
