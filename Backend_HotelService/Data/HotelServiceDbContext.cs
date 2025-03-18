using Microsoft.EntityFrameworkCore;
using Backend_HotelService.Models;

namespace Backend_HotelService.Data
{
    public class HotelServiceDbContext : DbContext
    {
        public HotelServiceDbContext(DbContextOptions<HotelServiceDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
