using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupHomeWorkController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public GroupHomeWorkController(SmartStudyContext context)
        {
            _context = context;
        }


        // ТРЕБУЕТ ТЕСТИРОВАНИЯ
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
    }
}
