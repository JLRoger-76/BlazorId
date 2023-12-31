﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorId.Server.Data;
using BlazorId.Shared;
using BlazorId.Server.Migrations;

namespace BlazorId.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassroomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Classrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classroom>>> GetClassrooms()
        {
          if (_context.Classrooms == null)
          {
              return NotFound();
          }
            return await _context.Classrooms.Include(c=>c.User).ToListAsync();
        }

        // GET: api/Classrooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classroom>> GetClassroom(int id)
        {
          if (_context.Classrooms == null)
          {
              return NotFound();
          }
            var classroom = await _context.Classrooms.FindAsync(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return classroom;
        }

        // GET: api/Classrooms/Free/5
        [HttpGet("Free/{id}")]
        public async Task<ActionResult<IEnumerable<Classroom>>> GetFreeClassrooms(int id)
        {
            if (_context.Classrooms == null)
            {
                return NotFound();
            }
            var editCourse = await _context.Courses.FindAsync(id);
            if (editCourse == null)
            {
                return NotFound();
            }
            var classrooms= await _context.Classrooms.ToListAsync();
            var courses= await _context.Courses.ToListAsync();
            List<Classroom> freeClassrooms = new List<Classroom>();
            foreach (Classroom classroom in classrooms)
            {
                var isFree = true;

                foreach (Course course in courses)
                {
                    if (course.Classroom.Id == classroom.Id)
                    {
                        if ((course.StartDate >= editCourse.StartDate && course.StartDate <= editCourse.EndDate) ||
                            (course.EndDate >= editCourse.StartDate && course.EndDate <= editCourse.EndDate))
                        {
                            isFree = false;
                            break; // No need to continue checking other courses for this classroom
                        }
                    }
                }

                if (isFree)
                {
                    freeClassrooms.Add(classroom);
                }
            }


            return Ok(freeClassrooms);
        }

        // PUT: api/Classrooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassroom(int id, Classroom classroom)
        {
            if (id != classroom.Id)
            {
                return BadRequest();
            }

            _context.Entry(classroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassroomExists(id))
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

        // POST: api/Classrooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Classroom>> PostClassroom(Classroom classroom)
        {
          if (_context.Classrooms == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Classrooms'  is null.");
          }
            _context.Classrooms.Add(classroom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassroom", new { id = classroom.Id }, classroom);
        }

        // DELETE: api/Classrooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(int id)
        {
            if (_context.Classrooms == null)
            {
                return NotFound();
            }
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }

            _context.Classrooms.Remove(classroom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassroomExists(int id)
        {
            return (_context.Classrooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
