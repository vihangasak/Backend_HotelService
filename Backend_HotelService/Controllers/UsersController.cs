using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_HotelService.Data;
using Backend_HotelService.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Backend_HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HotelServiceDbContext _context;

        public UsersController(HotelServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                return await _context.Users
                    .AsNoTracking() // For better performance
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error getting users: {ex.Message}");
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromQuery] string username, [FromQuery] string password)
        {
            Console.WriteLine($"Authentication attempt: Username={username}");
            // Find user by username
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return Ok(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid username or password"
                });
            }

            // Verify password
            bool isPasswordValid = VerifyPassword(password, user.PasswordHash, user.Salt);

            if (!isPasswordValid)
            {
                return Ok(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid username or password"
                });
            }

            // Update last login time
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new LoginResponse
            {
                Success = true,
                UserId = user.UserId,
                roles = "Admin",
                Message = "Authentication successful"
            }); 
        }

        // Add this helper method to your UsersController class
        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // You need to implement your password verification logic here
            // This should match the hashing algorithm you used when creating the user

            // Example implementation using HMACSHA512
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var computedHashString = Convert.ToBase64String(computedHash);

                return computedHashString == storedHash;
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
       

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        public async Task<IActionResult> UpdateUserPasswordHashing()
        {
            // Get all users with empty salt
            var usersToUpdate = await _context.Users
                .Where(u => string.IsNullOrEmpty(u.Salt))
                .ToListAsync();

            int updatedCount = 0;

            foreach (var user in usersToUpdate)
            {
                // Use a default password or a password based on some rule
                // For example, you might use their username as the default password
                string defaultPassword = user.Username ?? "DefaultPassword123";

                // Generate new salt and hash
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    user.Salt = Convert.ToBase64String(hmac.Key);
                    user.PasswordHash = Convert.ToBase64String(
                        hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(defaultPassword))
                    );
                }

                updatedCount++;
            }

            // Save changes if there were updates
            if (updatedCount > 0)
            {
                await _context.SaveChangesAsync();
                return Ok($"Updated password hashing for {updatedCount} users");
            }

            return Ok("No users needed updating");
        }

        [HttpPost("resetpassword/{id}")]
        public async Task<IActionResult> ResetUserPassword(int id, [FromBody] string newPassword)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Generate new salt and hash
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                user.Salt = Convert.ToBase64String(hmac.Key);
                user.PasswordHash = Convert.ToBase64String(
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(newPassword))
                );
            }

            await _context.SaveChangesAsync();
            return Ok("Password reset successfully");
        }
    }
}
