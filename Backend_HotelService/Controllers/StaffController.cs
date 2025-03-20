using Backend_HotelService.Data;
using Backend_HotelService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly HotelServiceDbContext _context;

        public StaffController(HotelServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            return await _context.Staff
                .Include(s => s.User)
                .ToListAsync();
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            var staff = await _context.Staff
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.StaffId == id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            if (string.IsNullOrWhiteSpace(staff.EmployeeId) || string.IsNullOrWhiteSpace(staff.Position))
            {
                return BadRequest("EmployeeId and Position are required.");
            }

            // Ensure the associated User exists
            var userExists = await _context.Users.AnyAsync(u => u.UserId == staff.UserId);
            if (!userExists)
            {
                return BadRequest("The specified UserId does not exist.");
            }

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStaff), new { id = staff.StaffId }, staff);
        }

        // PUT: api/Staff/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, Staff staff)
        {
            if (id != staff.StaffId)
            {
                return BadRequest("ID mismatch.");
            }

            if (string.IsNullOrWhiteSpace(staff.EmployeeId) || string.IsNullOrWhiteSpace(staff.Position))
            {
                return BadRequest("EmployeeId and Position are required.");
            }

            // Ensure the associated User exists
            var userExists = await _context.Users.AnyAsync(u => u.UserId == staff.UserId);
            if (!userExists)
            {
                return BadRequest("The specified UserId does not exist.");
            }

            _context.Entry(staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Staff.Any(s => s.StaffId == id))
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

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
