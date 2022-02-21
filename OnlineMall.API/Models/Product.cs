namespace OnlineMall.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int RateStar { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Quantily { get; set; }
        public int? Category_Id { get; set; }
        public virtual Category Category { get; set; }
    }
}
