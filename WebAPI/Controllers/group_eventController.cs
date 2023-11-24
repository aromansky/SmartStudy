using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class group_eventController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public group_eventController(SmartStudyContext context)
        {
            _context = context;
        }

        // GET: api/group_event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<group_event>>> Getgroup_event()
        {
          if (_context.group_event == null)
          {
              return NotFound();
          }
            return await _context.group_event.ToListAsync();
        }

        // GET: api/group_event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<group_event>> Getgroup_event(long id)
        {
          if (_context.group_event == null)
          {
              return NotFound();
          }
            var group_event = await _context.group_event.FindAsync(id);

            if (group_event == null)
            {
                return NotFound();
            }

            return group_event;
        }

        // PUT: api/group_event/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putgroup_event(long id, group_event group_event)
        {
            if (id != group_event.group_event_id)
            {
                return BadRequest();
            }

            _context.Entry(group_event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!group_eventExists(id))
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

        // POST: api/group_event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<group_event>> Postgroup_event(group_event group_event)
        {
          if (_context.group_event == null)
          {
              return Problem("Entity set 'SmartStudyContext.group_event'  is null.");
          }
            _context.group_event.Add(group_event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getgroup_event", new { id = group_event.group_event_id }, group_event);
        }

        // DELETE: api/group_event/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletegroup_event(long id)
        {
            if (_context.group_event == null)
            {
                return NotFound();
            }
            var group_event = await _context.group_event.FindAsync(id);
            if (group_event == null)
            {
                return NotFound();
            }

            _context.group_event.Remove(group_event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool group_eventExists(long id)
        {
            return (_context.group_event?.Any(e => e.group_event_id == id)).GetValueOrDefault();
        }
    }
}
