using Microsoft.EntityFrameworkCore;

namespace OnlineMall.API.Models
{
    public partial class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<MoviesToday> MoviesToday { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Seats> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
