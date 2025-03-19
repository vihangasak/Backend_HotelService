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

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<RolePermission> RolePermissions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
        }
    }
}
