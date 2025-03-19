using System;

namespace Backend_HotelService.Models
{
    // Update your User.cs model
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty; // Add default values
        public string? Email { get; set; } // Make nullable
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public string? ProfilePictureUrl { get; set; }
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
