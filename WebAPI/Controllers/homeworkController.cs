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

        // PUT: api/homework/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomework(long id, homework homework)
        {
            if (id != homework.homework_id)
            {
                return BadRequest();
            }

            _context.Entry(homework).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeWorkExists(id))
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


        // DELETE: api/homework/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomework(long id)
        {
            if (_context.homework == null)
            {
                return NotFound();
            }
            var homework = await _context.homework.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }

            _context.homework.Remove(homework);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/homework
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<homework>> PostHomework(homework homework)
        {
            if (_context.homework == null)
            {
                return Problem("Entity set 'SmartStudyContext.homework'  is null.");
            }
            _context.homework.Add(homework);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Gethomework", new { id = homework.homework_id }, homework);
        }

        private bool HomeWorkExists(long id)
        {
            return (_context.homework?.Any(e => e.homework_id == id)).GetValueOrDefault();
        }
    }
}
