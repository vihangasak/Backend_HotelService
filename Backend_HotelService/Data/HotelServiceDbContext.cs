using Microsoft.EntityFrameworkCore;
using Backend_HotelService.Models;

namespace Backend_HotelService.Data
{
    public class HotelServiceDbContext : DbContext
    {
        public HotelServiceDbContext(DbContextOptions<HotelServiceDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var ConnectionString = @"Data Source=DESKTOP-6UMV2RA\SQLEXPRESS2019;Initial Catalog=HotelServiceDB;Persist Security Info=True;User ID=sa;password =pass1234;Connection Timeout=10000;Language =British English;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
