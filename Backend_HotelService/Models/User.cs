using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_HotelService.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string roles { get; set; }
    }

}
