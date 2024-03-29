using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_FeedbackController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public User_FeedbackController(SmartStudyContext context)
        {
            _context = context;
        }

        // POST: api/group_homework
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<user_feedback>> PostUserFeedback(user_feedback user_feedback)
        {
            if (_context.user_feedback == null)
            {
                return Problem("Entity set 'SmartStudyContext.user_feedback'  is null.");
            }
            _context.user_feedback.Add(user_feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserFeedback", new { id = user_feedback.user_feedback_id }, user_feedback);
        }

        // Удаляет фидбек для пользователя
        // DELETE: api/group_homework/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserFeedback(long id)
        {
            if (_context.user_feedback == null)
            {
                return NotFound();
            }
            var user_feedback = await _context.user_feedback.FindAsync(id);
            if (user_feedback == null)
            {
                return NotFound();
            }

            _context.user_feedback.Remove(user_feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
