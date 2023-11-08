using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorId.Server.Data;
using BlazorId.Shared;

namespace BlazorId.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryTrainingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryTrainingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoryTrainings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryTraining>>> GetCategoryTrainings()
        {
          if (_context.CategoryTrainings == null)
          {
              return NotFound();
          }
            return await _context.CategoryTrainings.ToListAsync();
        }

        // GET: api/CategoryTrainings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryTraining>>> GetCategoryTraining(int id)
        {
          if (_context.CategoryTrainings == null)
          {
              return NotFound();
          }
            return await _context.CategoryTrainings.Include(trai=>trai.Training).Where(cat=>cat.CategoryId==id).ToListAsync();           
        }

        // PUT: api/CategoryTrainings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryTraining(int id, CategoryTraining categoryTraining)
        {
            if (id != categoryTraining.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoryTraining).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryTrainingExists(id))
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

        // POST: api/CategoryTrainings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryTraining>> PostCategoryTraining(CategoryTraining categoryTraining)
        {
          if (_context.CategoryTrainings == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CategoryTrainings'  is null.");
          }
            _context.CategoryTrainings.Add(categoryTraining);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryTraining", new { id = categoryTraining.Id }, categoryTraining);
        }

        // DELETE: api/CategoryTrainings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryTraining(int id)
        {
            if (_context.CategoryTrainings == null)
            {
                return NotFound();
            }
            var categoryTraining = await _context.CategoryTrainings.FindAsync(id);
            if (categoryTraining == null)
            {
                return NotFound();
            }

            _context.CategoryTrainings.Remove(categoryTraining);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryTrainingExists(int id)
        {
            return (_context.CategoryTrainings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
