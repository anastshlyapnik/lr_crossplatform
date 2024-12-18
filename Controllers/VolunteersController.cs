using System;
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
    public class VolunteersController : ControllerBase
    {
        private readonly Shlyapnikova_lrContext _context;

        public VolunteersController(Shlyapnikova_lrContext context)
        {
            _context = context;
        }

        // GET: api/Volunteers
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Volunteer>>> GetVolunteer()
        {
            return await _context.Volunteer.ToListAsync();
        }

        // GET: api/Volunteers/5
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<Volunteer>> GetVolunteer(int id)
        {
            var volunteer = await _context.Volunteer.FindAsync(id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return volunteer;
        }

        // PUT: api/Volunteers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> PutVolunteer(int id, Volunteer volunteer)
        {
            if (id != volunteer.VolunteerId)
            {
                return BadRequest();
            }

            _context.Entry(volunteer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(id))
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

        // POST: api/Volunteers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Volunteer>> PostVolunteer(Volunteer volunteer)
        {
            _context.Volunteer.Add(volunteer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVolunteer", new { id = volunteer.VolunteerId }, volunteer);
        }

        // DELETE: api/Volunteers/5
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteVolunteer(int id)
        {
            var volunteer = await _context.Volunteer.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            _context.Volunteer.Remove(volunteer);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET: api/Volunteers/Priority/{priority}
        [HttpGet("Priority/{priority}")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Volunteer>>> GetVolunteersByPriority(int priority)
        {
            var volunteers = await _context.Volunteer.Where(v => v.VolunteerPriority == priority).ToListAsync();
            if (volunteers == null || !volunteers.Any())
            {
                return NotFound();
            }

            return volunteers;
        }


        private bool VolunteerExists(int id)
        {
            return _context.Volunteer.Any(e => e.VolunteerId == id);
        }
    }
}
