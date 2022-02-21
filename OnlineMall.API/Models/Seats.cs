namespace OnlineMall.API.Models
{
    public class Seats
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Rank { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
