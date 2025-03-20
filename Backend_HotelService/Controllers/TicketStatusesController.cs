using Backend_HotelService.Data;
using Backend_HotelService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketStatusesController : ControllerBase
    {
        private readonly HotelServiceDbContext _context;

        public TicketStatusesController(HotelServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/TicketStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketStatus>>> GetTicketStatuses()
        {
            return await _context.TicketStatuses.ToListAsync();
        }

        // GET: api/TicketStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketStatus>> GetTicketStatus(int id)
        {
            var status = await _context.TicketStatuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        // POST: api/TicketStatuses
        [HttpPost]
        public async Task<ActionResult<TicketStatus>> PostTicketStatus(TicketStatus status)
        {
            if (string.IsNullOrWhiteSpace(status.StatusName))
            {
                return BadRequest("StatusName is required.");
            }

            _context.TicketStatuses.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicketStatus), new { id = status.StatusId }, status);
        }

        // PUT: api/TicketStatuses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketStatus(int id, TicketStatus status)
        {
            if (id != status.StatusId)
            {
                return BadRequest("ID mismatch.");
            }

            if (string.IsNullOrWhiteSpace(status.StatusName))
            {
                return BadRequest("StatusName is required.");
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TicketStatuses.Any(s => s.StatusId == id))
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

        // DELETE: api/TicketStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketStatus(int id)
        {
            var status = await _context.TicketStatuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.TicketStatuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
