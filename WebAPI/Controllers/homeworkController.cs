using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeWorkController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public HomeWorkController(SmartStudyContext context)
        {
            _context = context;
        }


        // GET: api/homework/5
        [HttpGet("{id}")]
        public async Task<ActionResult<homework>> GetEvent(long id)
        {
          if (_context.@event == null)
          {
              return NotFound();
          }
            var homework = await _context.homework.FindAsync(id);

            if (homework == null)
            {
                return NotFound();
            }

            return homework;
        }



        // GET: api/event/homework_user-5
        [HttpGet("homework_user-{id}")]
        public async Task<ActionResult<IEnumerable<homework>>> GetHomeWorksWithUsers(long id)
        {
            var homeworks = from homework in _context.homework
                            join user_homework in _context.user_homework on homework.homework_id equals user_homework.homework_id
                            join user in _context.user on user_homework.user_id equals user.user_id
                            select new homework
                            {
                                homework_id = homework.homework_id,
                                Title = homework.Title,
                                Description = homework.Description
                            };
            if (homeworks == null)
                return NotFound();
            return await homeworks.ToListAsync();
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
