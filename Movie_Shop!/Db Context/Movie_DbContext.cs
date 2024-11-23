using Microsoft.EntityFrameworkCore;
using Movie_Shop_.Models;

namespace Movie_Shop_.Db_Context
{
    public class Movie_DbContext : DbContext 
    {
        public Movie_DbContext(DbContextOptions<Movie_DbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(m => m.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderRow>()
                .Property(o => o.Price)
                .HasPrecision(18, 2);
        }


    }
}
