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
    public class eventController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public eventController(SmartStudyContext context)
        {
            _context = context;
        }

        // GET: api/event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<@event>>> GetEvent()
        {
          if (_context.@event == null)
          {
              return NotFound();
          }
            return await _context.@event.ToListAsync();
        }

        // GET: api/event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<@event>> GetEvent(long id)
        {
          if (_context.@event == null)
          {
              return NotFound();
          }
            var @event = await _context.@event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/event/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(long id, @event @event)
        {
            if (id != @event.event_id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<@event>> PostEvent(@event @event)
        {
          if (_context.@event == null)
          {
              return Problem("Entity set 'SmartStudyContext.Event'  is null.");
          }
            _context.@event.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.event_id }, @event);
        }

        // DELETE: api/event/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(long id)
        {
            if (_context.@event == null)
            {
                return NotFound();
            }
            var @event = await _context.@event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.@event.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(long id)
        {
            return (_context.@event?.Any(e => e.event_id == id)).GetValueOrDefault();
        }
    }
}
