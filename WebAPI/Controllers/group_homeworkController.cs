using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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


        // Принимает homework_id, возвращает группы (group_settings), которым оно доступно
        // GET: api/group_homework
        [HttpGet("groups_with_homework-{id}")]
        public async Task<ActionResult<IEnumerable<group_settings>>> GetGroupWithHomework(long id)
        {
            var groups = from homework in _context.homework
                         join group_homework in _context.group_homework on homework.homework_id equals group_homework.homework_id
                         join group_settings in _context.group_settings on group_homework.group_settings_id equals group_settings.group_settings_id
                         where homework.homework_id == id
                         select new group_settings
                         {
                             group_settings_id = group_settings.group_settings_id,
                             Tutor_id = group_settings.Tutor_id,
                             Title = group_settings.Title,
                             Description = group_settings.Description

                         };
            if(groups is null)
                return NotFound();
            return await groups.ToListAsync();

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
