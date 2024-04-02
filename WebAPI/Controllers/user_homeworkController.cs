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
        // DELETE: api/group_homework/5_1
        [HttpDelete("{homework_id}_{user_id}")]
        public async Task<IActionResult> DeleteUserHomework(long homework_id, long user_id)
        {
            if (_context.user_homework == null)
            {
                return NotFound();
            }
            var user_homework = from _user_homework in _context.user_homework
                                where _user_homework.homework_id == homework_id && _user_homework.user_id == user_id
                                select _user_homework;
            if (user_homework == null)
            {
                return NotFound();
            }

            _context.user_homework.Remove(user_homework.First());
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
