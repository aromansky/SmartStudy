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
