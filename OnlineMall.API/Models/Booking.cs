namespace OnlineMall.API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }
        public int? MoviesTodayId { get; set; }
        public virtual MoviesToday MoviesToday { get; set; }
        public int? SeatsId { get; set; }
        public virtual Seats Seats { get; set; }
    }
}
