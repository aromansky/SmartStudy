using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class event_groupController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public event_groupController(SmartStudyContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<group_settings>>> GetEventGroups(long event_id)
        {
            var eventGroups = from group_settings in _context.group_settings
                             join group_event in _context.group_event.Where(x => x.event_id == event_id) on group_settings.group_settings_id equals group_event.group_settings_id
                             select new group_settings
                             {
                                 group_settings_id = group_settings.group_settings_id,
                                 Tutor_id = group_settings.Tutor_id,
                                 Title = group_settings.Title,
                                 Description = group_settings.Description,
                             };
            if (eventGroups == null)
                return NotFound();

            return await eventGroups.ToListAsync();
        }
    }
}
