using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Group_HomeworkController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public Group_HomeworkController(SmartStudyContext context)
        {
            _context = context;
        }

        // POST: api/group_homework
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<group_homework>> PostGroupHomework(group_homework group_homework)
        {
            if (_context.group_homework == null)
            {
                return Problem("Entity set 'SmartStudyContext.group_homework'  is null.");
            }
            _context.group_homework.Add(group_homework);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupHomework", new { id = group_homework.group_homework_id }, group_homework);
        }


        // ТРЕБУЕТ ТЕСТИРОВАНИЯ
        // Возвращает дз, которое доступно пользователю
        // GET: api/group_homework/5
        [HttpGet("{id}")]
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


        // Удаляет дз из группы
        // DELETE: api/group_homework/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupHomework(long id)
        {
            if (_context.group_homework == null)
            {
                return NotFound();
            }
            var group_homework = await _context.group_homework.FindAsync(id);
            if (group_homework == null)
            {
                return NotFound();
            }

            _context.group_homework.Remove(group_homework);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
