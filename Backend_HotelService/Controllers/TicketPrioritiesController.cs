using Backend_HotelService.Data;
using Backend_HotelService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketPrioritiesController : ControllerBase
    {
        private readonly HotelServiceDbContext _context;

        public TicketPrioritiesController(HotelServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/TicketPriorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPriority>>> GetTicketPriorities()
        {
            return await _context.TicketPriorities.ToListAsync();
        }

        // GET: api/TicketPriorities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketPriority>> GetTicketPriority(int id)
        {
            var priority = await _context.TicketPriorities.FindAsync(id);
            if (priority == null)
            {
                return NotFound();
            }

            return priority;
        }

        // POST: api/TicketPriorities
        [HttpPost]
        public async Task<ActionResult<TicketPriority>> PostTicketPriority(TicketPriority priority)
        {
            if (string.IsNullOrWhiteSpace(priority.PriorityName))
            {
                return BadRequest("PriorityName is required.");
            }

            _context.TicketPriorities.Add(priority);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicketPriority), new { id = priority.PriorityId }, priority);
        }

        // PUT: api/TicketPriorities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketPriority(int id, TicketPriority priority)
        {
            if (id != priority.PriorityId)
            {
                return BadRequest("ID mismatch.");
            }

            if (string.IsNullOrWhiteSpace(priority.PriorityName))
            {
                return BadRequest("PriorityName is required.");
            }

            _context.Entry(priority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TicketPriorities.Any(p => p.PriorityId == id))
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

        // DELETE: api/TicketPriorities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketPriority(int id)
        {
            var priority = await _context.TicketPriorities.FindAsync(id);
            if (priority == null)
            {
                return NotFound();
            }

            _context.TicketPriorities.Remove(priority);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
