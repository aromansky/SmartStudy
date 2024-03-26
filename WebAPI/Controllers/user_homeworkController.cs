using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_HomeworkController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public User_HomeworkController(SmartStudyContext context)
        {
            _context = context;
        }

        // POST: api/group_homework
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<user_homework>> PostUserHomework(user_homework user_homework)
        {
            if (_context.user_homework == null)
            {
                return Problem("Entity set 'SmartStudyContext.user_homework'  is null.");
            }
            _context.user_homework.Add(user_homework);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserHomework", new { id = user_homework.user_homework_id }, user_homework);
        }

        // Удаляет дз для пользователя
        // DELETE: api/group_homework/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserHomework(long id)
        {
            if (_context.user_homework == null)
            {
                return NotFound();
            }
            var user_homework = await _context.user_homework.FindAsync(id);
            if (user_homework == null)
            {
                return NotFound();
            }

            _context.user_homework.Remove(user_homework);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
