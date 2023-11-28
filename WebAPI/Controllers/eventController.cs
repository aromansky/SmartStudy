using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public EventController(SmartStudyContext context)
        {
            _context = context;
        }

        // GET: api/event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<@event>>> Getevent()
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

        // GET: api/event/author-5
        [HttpGet("author-{id}")]
        public async Task<ActionResult<IEnumerable<@event>>> GetEventsFromAuthor(long id)
        {
            {
                if (_context.@event == null)
                {
                    return NotFound();
                }
                return await _context.@event.Where(x => x.author_id == id).ToListAsync();
            }
        }

        // GET: api/event/user-5
        [HttpGet("user-{id}")]
        public async Task<ActionResult<IEnumerable<@event>>> GetEventsWithUsers(long id)
        {
            var eventUsers = from @event in _context.@event
                             join group_event in _context.group_event on @event.event_id equals group_event.event_id
                             join @group in _context.@group.Where(x => x.user_id == id).Distinct() on group_event.group_settings_id equals @group.group_settings_id
                             select new @event
                             {
                                 event_id = @event.event_id,
                                 author_id = @event.author_id,
                                 Title = @event.Title,
                                 Description = @event.Description,
                                 date_begin = @event.date_begin,
                                 date_end = @event.date_end
                             };
            if (eventUsers == null)
                return NotFound();
            return await eventUsers.ToListAsync();
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

        // POST: api/Event
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<@event>> PostEvent(@event @event)
        {
          if (_context.@event == null)
          {
              return Problem("Entity set 'SmartStudyContext.event'  is null.");
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
