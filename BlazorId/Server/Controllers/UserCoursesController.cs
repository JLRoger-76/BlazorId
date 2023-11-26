using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorId.Server.Data;
using BlazorId.Shared;

namespace BlazorId.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCourse>>> GetUserCourses()
        {
          if (_context.UserCourses == null)
          {
              return NotFound();
          }
            return await _context.UserCourses.ToListAsync();
        }

        // GET: api/UserCourses/Users/5
        [HttpGet("Users/{id}")]
        public async Task<ActionResult<List<UserCourse>>> GetUserCourse(int id)
        {
            if (_context.UserCourses == null)
            {
                return NotFound();
            }
            var userCourse = await _context.UserCourses.
                Where(u => u.CourseId == id).
                ToListAsync();
            if (userCourse == null)
            {
                return NotFound();
            }
            return userCourse;
        }

        // GET: api/UserCourses/Courses/x@x.com
        [HttpGet("Courses/{name}")]
        public async Task<ActionResult<List<UserCourse>>> GetUserCourse(string name)
        {
            if (_context.UserCourses == null)
            {
                return NotFound();
            }
            var userCourses = await _context.UserCourses.
                Include(u=>u.Course).
                Where(u => u.UserName == name).
                ToListAsync();
            if (userCourses == null)
            {
                return NotFound();
            }
            return userCourses;
        }

        // GET: api/UserCourses/Current/x@x.com
        [HttpGet("Current/{name}")]
        public async Task<ActionResult<UserCourse>> GetCurrentCourse(string name)
        {
            if (_context.UserCourses == null)
            {
                return NotFound();
            }
            var userCourse = await _context.UserCourses.
                Include(u => u.Course).
                Where(u=>u.Course!.StartDate<=DateTime.Today).
                Where(u => u.Course!.EndDate >= DateTime.Today).
                FirstOrDefaultAsync(u => u.UserName == name);
            if (userCourse == null)
            {
                return null;
            }
            return userCourse;
        }

        // GET: api/UserCourses/Params?name= &id=
        [HttpGet("{Params}")]
        public async Task<ActionResult<UserCourse>> GetUserCourse(string name,int id)
        {
          if (_context.UserCourses == null)
          {
              return NotFound();
          }
            var userCourse = await _context.UserCourses.
                Include(u => u.Course).
                FirstOrDefaultAsync(u => u.UserName == name && u.CourseId == id);              
            if (userCourse == null)
            {
                return NotFound();
            }
            return userCourse;
        }

        // PUT: api/UserCourses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCourse(int id, UserCourse userCourse)
        {
            if (id != userCourse.Id)
            {
                return BadRequest();
            }
            _context.Entry(userCourse).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCourseExists(id))
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

        // POST: api/UserCourses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCourse>> PostUserCourse(UserCourse userCourse)
        {
          if (_context.UserCourses == null)
          {
              return Problem("Entity set 'ApplicationDbContext.UserCourses'  is null.");
          }
            _context.UserCourses.Add(userCourse);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserCourse", new { id = userCourse.Id }, userCourse);
        }

        // DELETE: api/UserCourses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCourse(int id)
        {
            if (_context.UserCourses == null)
            {
                return NotFound();
            }
            var userCourse = await _context.UserCourses.FindAsync(id);
            if (userCourse == null)
            {
                return NotFound();
            }
            _context.UserCourses.Remove(userCourse);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserCourseExists(int id)
        {
            return (_context.UserCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
