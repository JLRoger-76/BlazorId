using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorId.Server.Data;
using BlazorId.Shared;

namespace BlazorId.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyAttendancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DailyAttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DailyAttendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyAttendance>>> GetDailyAttendances()
        {
          if (_context.DailyAttendances == null)
          {
              return NotFound();
          }
            return await _context.DailyAttendances.ToListAsync();
        }

        // GET: api/DailyAttendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyAttendance>> GetDailyAttendance(int id)
        {
          if (_context.DailyAttendances == null)
          {
              return NotFound();
          }
            var dailyAttendance = await _context.DailyAttendances.FindAsync(id);

            if (dailyAttendance == null)
            {
                return NotFound();
            }

            return dailyAttendance;
        }

        // PUT: api/DailyAttendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyAttendance(int id, DailyAttendance dailyAttendance)
        {
            if (id != dailyAttendance.Id)
            {
                return BadRequest();
            }

            _context.Entry(dailyAttendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyAttendanceExists(id))
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

        // POST: api/DailyAttendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DailyAttendance>> PostDailyAttendance(DailyAttendance dailyAttendance)
        {
          if (_context.DailyAttendances == null)
          {
              return Problem("Entity set 'ApplicationDbContext.DailyAttendances'  is null.");
          }
            _context.DailyAttendances.Add(dailyAttendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailyAttendance", new { id = dailyAttendance.Id }, dailyAttendance);
        }

        // DELETE: api/DailyAttendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyAttendance(int id)
        {
            if (_context.DailyAttendances == null)
            {
                return NotFound();
            }
            var dailyAttendance = await _context.DailyAttendances.FindAsync(id);
            if (dailyAttendance == null)
            {
                return NotFound();
            }

            _context.DailyAttendances.Remove(dailyAttendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DailyAttendanceExists(int id)
        {
            return (_context.DailyAttendances?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
