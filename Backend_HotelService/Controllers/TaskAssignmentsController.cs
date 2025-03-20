using Backend_HotelService.Data;
using Backend_HotelService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentsController : ControllerBase
    {
        private readonly HotelServiceDbContext _context;

        public TaskAssignmentsController(HotelServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskAssignment>>> GetTaskAssignments()
        {
            return await _context.TaskAssignments
                .Include(ta => ta.Ticket)
                .Include(ta => ta.AssignedToStaff)
                .Include(ta => ta.AssignedToDepartment)
                .Include(ta => ta.AssignedByUser)
                .ToListAsync();
        }

        // GET: api/TaskAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskAssignment>> GetTaskAssignment(int id)
        {
            var taskAssignment = await _context.TaskAssignments
                .Include(ta => ta.Ticket)
                .Include(ta => ta.AssignedToStaff)
                .Include(ta => ta.AssignedToDepartment)
                .Include(ta => ta.AssignedByUser)
                .FirstOrDefaultAsync(ta => ta.AssignmentId == id);

            if (taskAssignment == null)
            {
                return NotFound();
            }

            return taskAssignment;
        }

        // POST: api/TaskAssignments
        [HttpPost]
        public async Task<ActionResult<TaskAssignment>> PostTaskAssignment(TaskAssignment taskAssignment)
        {
            if (taskAssignment.TicketId <= 0 || taskAssignment.AssignedByUserId <= 0)
            {
                return BadRequest("TicketId and AssignedByUserId are required.");
            }

            if (taskAssignment.AssignedToStaffId != null && taskAssignment.AssignedToDepartmentId != null)
            {
                return BadRequest("Only one of AssignedToStaffId or AssignedToDepartmentId can be set.");
            }

            taskAssignment.AssignedAt = DateTime.UtcNow; 
            taskAssignment.IsActive = true;

            _context.TaskAssignments.Add(taskAssignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskAssignment), new { id = taskAssignment.AssignmentId }, taskAssignment);
        }

        // PUT: api/TaskAssignments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskAssignment(int id, TaskAssignment taskAssignment)
        {
            if (id != taskAssignment.AssignmentId)
            {
                return BadRequest("ID mismatch.");
            }

            if (taskAssignment.TicketId <= 0 || taskAssignment.AssignedByUserId <= 0)
            {
                return BadRequest("TicketId and AssignedByUserId are required.");
            }

            if (taskAssignment.AssignedToStaffId != null && taskAssignment.AssignedToDepartmentId != null)
            {
                return BadRequest("Only one of AssignedToStaffId or AssignedToDepartmentId can be set.");
            }

            _context.Entry(taskAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TaskAssignments.Any(ta => ta.AssignmentId == id))
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

        // DELETE: api/TaskAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAssignment(int id)
        {
            var taskAssignment = await _context.TaskAssignments.FindAsync(id);
            if (taskAssignment == null)
            {
                return NotFound();
            }

            _context.TaskAssignments.Remove(taskAssignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
