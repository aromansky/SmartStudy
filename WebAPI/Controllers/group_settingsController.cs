using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class group_settingsController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public group_settingsController(SmartStudyContext context)
        {
            _context = context;
        }

        // GET: api/group_settings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<group_settings>>> Getgroup_settings()
        {
          if (_context.group_settings == null)
          {
              return NotFound();
          }
            return await _context.group_settings.ToListAsync();
        }

        // GET: api/group_settings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<group_settings>> Getgroup_settings(long id)
        {
          if (_context.group_settings == null)
          {
              return NotFound();
          }
            var group_settings = await _context.group_settings.FindAsync(id);

            if (group_settings == null)
            {
                return NotFound();
            }

            return group_settings;
        }

        // PUT: api/group_settings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putgroup_settings(long id, group_settings group_settings)
        {
            if (id != group_settings.group_settings_id)
            {
                return BadRequest();
            }

            _context.Entry(group_settings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!group_settingsExists(id))
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

        // POST: api/group_settings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<group_settings>> Postgroup_settings(group_settings group_settings)
        {
          if (_context.group_settings == null)
          {
              return Problem("Entity set 'SmartStudyContext.group_settings'  is null.");
          }
            _context.group_settings.Add(group_settings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getgroup_settings", new { id = group_settings.group_settings_id }, group_settings);
        }

        // DELETE: api/group_settings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletegroup_settings(long id)
        {
            if (_context.group_settings == null)
            {
                return NotFound();
            }
            var group_settings = await _context.group_settings.FindAsync(id);
            if (group_settings == null)
            {
                return NotFound();
            }

            _context.group_settings.Remove(group_settings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool group_settingsExists(long id)
        {
            return (_context.group_settings?.Any(e => e.group_settings_id == id)).GetValueOrDefault();
        }
    }
}
