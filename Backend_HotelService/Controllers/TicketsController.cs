using Backend_HotelService.Data;
using Backend_HotelService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly HotelServiceDbContext _context;

        public TicketsController(HotelServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets
                .Include(t => t.CreatedBy)
                .Include(t => t.Room)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Department)
                .ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.CreatedBy)
                .Include(t => t.Room)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Department)
                .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket.Title) || string.IsNullOrWhiteSpace(ticket.Description))
            {
                return BadRequest("Title and Description are required.");
            }

            ticket.CreatedAt = DateTime.UtcNow;
            ticket.UpdatedAt = DateTime.UtcNow;

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketId }, ticket);
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return BadRequest("ID mismatch.");
            }

            if (string.IsNullOrWhiteSpace(ticket.Title) || string.IsNullOrWhiteSpace(ticket.Description))
            {
                return BadRequest("Title and Description are required.");
            }

            ticket.UpdatedAt = DateTime.UtcNow;

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tickets.Any(t => t.TicketId == id))
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

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
