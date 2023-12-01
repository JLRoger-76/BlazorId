using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorId.Server.Data;
using BlazorId.Shared;
using Microsoft.AspNetCore.Identity;
using BlazorId.Client.Pages.User;

namespace BlazorId.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCoursesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                Include(u => u.Course).
                Where(u => u.UserName == name).
                ToListAsync();
            if (userCourses == null)
            {
                return NotFound();
            }
            return userCourses;
        }

        


        // GET: api/UserCourses/Teachers/1
        [HttpGet("Teachers/{id}")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetFreeTeachersCourses(int id)
        {
            var freeTeachers = new List<ApplicationUser>();
            if (_context.UserCourses == null)
            {
                return NotFound();
            }
            //get students & teacher in the course:courseID
            var userCourses = await _context.UserCourses.
                Where(u => u.CourseId == id).ToListAsync();
            if (userCourses == null)
            {
                return NotFound();
            }
            //search teacher of the course
            //get all teachers
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            //the edited course
            var editCourse = await _context.Courses.FindAsync(id);
            //courses list from training
            var trainingCourses = await _context.Courses.
                Where(c => c.TrainingId == editCourse.TrainingId).
                ToListAsync();
            //teachers list
            foreach (var teacher in teachers)
            {
                var isFree = true;
                //notes list from teacher
                var teacherCourses = await _context.UserCourses.
                    Where(u => u.UserName == teacher.UserName).
                    ToListAsync();
                //search free teacher in editcourse dates
                foreach (var tc in teacherCourses!)
                {
                    if ((tc.Course.StartDate >= editCourse.StartDate &
                         tc.Course.StartDate <= editCourse.EndDate) ||
                        (tc.Course.EndDate >= editCourse.StartDate &
                         tc.Course.EndDate <= editCourse.EndDate))
                    {
                        isFree = false;
                    }
                }
                if (isFree)
                {
                    // select notes only for defined courses
                    teacher.Appreciation = 0;
                    foreach (Course course in trainingCourses)
                    {
                        var count = 0;
                        foreach (var tc in teacherCourses!)
                        {
                            if (tc.CourseId == course.Id)
                            {
                                if (tc.TeacherAnimation > 0) count++;
                                if (tc.TeacherAnswers > 0) count++;
                                if (tc.TeacherAvailability > 0) count++;
                                if (tc.TeacherExpertise > 0) count++;
                                if (tc.TeacherPedagogy > 0) count++;
                                // sum of notes for the teacher
                                if (count > 0)
                                {
                                    teacher.Appreciation += (
                                    tc.TeacherAnimation +tc.TeacherAnswers +
                                    tc.TeacherAvailability +tc.TeacherExpertise +
                                    tc.TeacherPedagogy) / count;
                                }
                            }
                        }
                    }
                    freeTeachers.Add(teacher);
                }          
            }
            // Sort the best teachers based on Appreciation in descending order
            freeTeachers = freeTeachers
                .OrderByDescending(course => course.Appreciation)
                .Take(5)
                .ToList();

            return freeTeachers;
        }
     
        // GET: api/UserCourses/Params?name= &id=
        [HttpGet("{Params}")]
        public async Task<ActionResult<UserCourse>> GetUserCourse(string name, int id)
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

        // GET: api/UserCourses/CourseTeacher/4
        [HttpGet("CourseTeacher/{id}")]
        public async Task<ActionResult<UserCourse>> GetTeacherCourse(int id)
        {
            if (_context.UserCourses == null)
            {
                return NotFound();
            }

            //get students & teacher in the course:courseID
            var userCourses = await _context.UserCourses.
                Where(u => u.CourseId == id).ToListAsync();
            if (userCourses == null)
            {
                return NotFound();
            }

            ////get all teachers
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            ////search teacher of the course
            foreach (UserCourse usercourse in userCourses)
            {
                foreach (var teacher in teachers)
                {
                    if (teacher.UserName == usercourse.UserName)
                        return usercourse;
                }
            }

            return NoContent();
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
