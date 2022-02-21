namespace OnlineMall.API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public int? Movies_Id { get; set; }
        public virtual Movies Movies { get; set; }
        public int? Seats_Id { get; set; }
        public virtual Seats Seats { get; set; }
    }
}
