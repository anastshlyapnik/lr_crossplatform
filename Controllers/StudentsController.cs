﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shlyapnikova_lr.Data;
using Shlyapnikova_lr.Models;

namespace Shlyapnikova_lr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly Shlyapnikova_lrContext _context;

        public StudentsController(Shlyapnikova_lrContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _context.Student.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET: api/Students/Status/{status}
        [HttpGet("Status/{status}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByStatus(int status)
        {
            var students = await _context.Student.Where(s => s.Status == status).ToListAsync();
            if (students == null || students.Count == 0)
            {
                return NotFound();
            }

            return students;
        }


        // PUT: api/Students/CheckInStart/{id}
        [HttpPut("CheckInStart/{id}")]
        [Authorize]
        public async Task<IActionResult> SetCheckInStart(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.CheckInStart = DateTime.UtcNow; // Записываем текущее время (UTC)
            _context.Entry(student).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(student); // Возвращаем обновлённого студента
        }

        // PUT: api/Students/CheckInEnd/{id}
        [HttpPut("CheckInEnd/{id}")]
        [Authorize]
        public async Task<IActionResult> SetCheckInEnd(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Убедимся, что CheckInStart установлен
            if (!student.CheckInStart.HasValue)
            {
                return BadRequest("CheckInStart is not set. Cannot calculate duration.");
            }

            // Установим текущее время как CheckInEnd
            student.CheckInEnd = DateTime.UtcNow;
            student.Status=3;

            // Рассчитаем разницу между CheckInStart и CheckInEnd
            student.CheckInTime = student.CheckInEnd.Value - student.CheckInStart.Value;

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new
            {
                student.StudentId,
                CheckInStart = student.CheckInStart,
                CheckInEnd = student.CheckInEnd,
                CheckInTime = student.CheckInTime
            }); // Возвращаем обновлённые данные
        }

        // PUT: api/Students/ChangeStatus/5
        [HttpPut("ChangeStatus/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] int newStatus)
        {
            var student = await _context.Student.FindAsync(id);
            //if (student == null)
            //{
            //    return NotFound();
            //}

            student.Status = newStatus;
            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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


        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}
