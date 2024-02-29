using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class groupController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public groupController(SmartStudyContext context)
        {
            _context = context;
        }

        // GET: api/group
        [HttpGet]
        public async Task<ActionResult<IEnumerable<group>>> Getgroup()
        {
          if (_context.group == null)
          {
              return NotFound();
          }
            return await _context.group.ToListAsync();
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
            if (groups is null)
                return NotFound();
            return await groups.ToListAsync();

        }

        // GET: api/group/5
        [HttpGet("{id}")]
        public async Task<ActionResult<group>> Getgroup(long id)
        {
          if (_context.group == null)
          {
              return NotFound();
          }
            var @group = await _context.group.FindAsync(id);

            if (@group == null)
            {
                return NotFound();
            }

            return @group;
        }

        // PUT: api/group/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putgroup(long id, group @group)
        {
            if (id != @group.group_id)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!groupExists(id))
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

        // POST: api/group
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<group>> Postgroup(group @group)
        {
          if (_context.group == null)
          {
              return Problem("Entity set 'SmartStudyContext.group'  is null.");
          }
            _context.group.Add(@group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getgroup", new { id = @group.group_id }, @group);
        }

        // DELETE: api/group/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletegroup(long id)
        {
            if (_context.group == null)
            {
                return NotFound();
            }
            var @group = await _context.group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.group.Remove(@group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool groupExists(long id)
        {
            return (_context.group?.Any(e => e.group_id == id)).GetValueOrDefault();
        }
    }
}
