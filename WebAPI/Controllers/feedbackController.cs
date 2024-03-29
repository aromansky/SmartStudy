using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public FeedbackController(SmartStudyContext context)
        {
            _context = context;
        }


        // GET: api/feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<feedback>> Getfeedback(long id)
        {
          if (_context.feedback == null)
          {
              return NotFound();
          }
            var feedback = await _context.feedback.FindAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        // PUT: api/feedback/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putfeedback(long id, feedback feedback)
        {
            if (id != feedback.feedback_id)
            {
                return BadRequest();
            }

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!feedbackExists(id))
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


        // DELETE: api/feedback/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletefeedback(long id)
        {
            if (_context.feedback == null)
            {
                return NotFound();
            }
            var feedback = await _context.feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.feedback.Remove(feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/feedback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<feedback>> Postfeedback(feedback feedback)
        {
            if (_context.feedback == null)
            {
                return Problem("Entity set 'SmartStudyContext.feedback'  is null.");
            }
            _context.feedback.Add(feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getfeedback", new { id = feedback.feedback_id }, feedback);
        }

        private bool feedbackExists(long id)
        {
            return (_context.feedback?.Any(e => e.feedback_id == id)).GetValueOrDefault();
        }


    }
}
