using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public userController(SmartStudyContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<user>>> GetUser()
        {
          if (_context.user == null)
          {
              return NotFound();
          }
            return await _context.user.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<user>> GetUser(long id)
        {
          if (_context.user == null)
          {
              return NotFound();
          }
            var user = await _context.user.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // Принимает id группы (group_settings), возвращает пользователей, принадлежащих группе
        // GET: api/users/5
        [HttpGet("group-{id}")]
        public async Task<ActionResult<IEnumerable<user>>> GetUsersInGroup(long id)
        {
            var usersInGroup = from @group in _context.@group
                               join user in _context.user on @group.user_id equals user.user_id
                               where @group.group_settings_id == id
                               select user;

            if (usersInGroup == null)
                return NotFound();

            return await usersInGroup.ToListAsync();
        }


        // Принимает id пользователя, возвращает личное дз, которое ему доступно
        // GET: api/user/users_with_homework-5
        [HttpGet("homework_user-{id}")]
        public async Task<ActionResult<IEnumerable<homework>>> GetHomeWorksForhUser(long id)
        {
            var homeworks = (from homework in _context.homework
                            join user_homework in _context.user_homework on homework.homework_id equals user_homework.homework_id
                            join user in _context.user on user_homework.user_id equals user.user_id
                            where user.user_id == id
                            select homework).Union(from homework in _context.homework
                                                  join group_homework in _context.group_homework on homework.homework_id equals group_homework.homework_id
                                                  join @group in _context.@group on group_homework.group_settings_id equals @group.group_settings_id
                                                  join user in _context.user on @group.user_id equals user.user_id
                                                  where @group.user_id == id
                                                  select homework).Distinct();
            if (homeworks == null)
                return NotFound();
            return await homeworks.ToListAsync();
        }

        // Принимает id дз, возвращает пользователей, которым оно доступно
        // GET: api/user/users_with_homework-5
        [HttpGet("users_with_hw-{id}")]
        public async Task<ActionResult<IEnumerable<user>>> GetUsersWithHomework(long id)
        {
            var homeworks = (from homework in _context.homework
                             join user_homework in _context.user_homework on homework.homework_id equals user_homework.homework_id
                             join user in _context.user on user_homework.user_id equals user.user_id
                             where homework.homework_id == id
                             select user).Distinct();
            if (homeworks == null)
                return NotFound();
            return await homeworks.ToListAsync();
        }


        // Принимает id пользователя, возвращает фидбек, который он получил
        // GET: api/user/users_with_homework-5
        [HttpGet("feedback_user-{id}")]
        public async Task<ActionResult<IEnumerable<feedback>>> GetFeedBackForhUser(long id)
        {
            var feedbacks = (from feedback in _context.feedback
                             join user_feedback in _context.user_feedback on feedback.feedback_id equals user_feedback.feedback_id
                             join user in _context.user on user_feedback.user_id equals user.user_id
                             where user.user_id == id
                             select feedback).Distinct();
            if (feedbacks == null)
                return NotFound();
            return await feedbacks.ToListAsync();
        }


        // Принимает id пользователя, возвращает дз, которое он создал
        // GET: api/user/users_with_homework-5
        [HttpGet("homework_author-{id}")]
        public async Task<ActionResult<IEnumerable<homework>>> GetCreatedHomeWorks(long id)
        {
            var homeworks =  from homework in _context.homework
                             where homework.author_id == id
                             select homework;
            if (homeworks == null)
                return NotFound();
            return await homeworks.ToListAsync();
        }


        // Принимает id пользователя, возвращает фидбек, который он создал
        // GET: api/user/feedback_author-5
        [HttpGet("feedback_author-{id}")]
        public async Task<ActionResult<IEnumerable<feedback>>> GetCreatedFeedbacks(long id)
        {
            var feedbacks = from feedback in _context.feedback
                            where feedback.author_id == id
                            select feedback;
            if (feedbacks == null)
                return NotFound();
            return await feedbacks.ToListAsync();
        }

        // Принимает id пользователя, возвращает групповое дз, которое ему доступно
        // GET: api/user_homework/users_with_homework-5
        [HttpGet("homework_user_from_group-{id}")]
        public async Task<ActionResult<IEnumerable<homework>>> GetHomeWorksForGroup(long id)
        {
            var homeworks = from homework in _context.homework
                            join group_homework in _context.group_homework on homework.homework_id equals group_homework.homework_id
                            join @group in _context.@group on group_homework.group_settings_id equals @group.group_settings_id
                            join user in _context.user on @group.user_id equals user.user_id
                            where user.user_id == id
                            select homework;
            if (homeworks == null)
                return NotFound();
            return await homeworks.ToListAsync();
        }

        // PUT: api/users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, user user)
        {
            if (id != user.user_id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<user>> PostUser(user user)
        {
          if (_context.user == null)
          {
              return Problem("Entity set 'SmartStudyContext.User'  is null.");
          }
            _context.user.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.user_id }, user);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.user == null)
            {
                return NotFound();
            }
            var user = await _context.user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.user.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return (_context.user?.Any(e => e.user_id == id)).GetValueOrDefault();
        }
    }
}
