using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class event_userController : ControllerBase
    {
        private readonly SmartStudyContext _context;

        public event_userController(SmartStudyContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetEventUsers()
        {
            var eventUsers = from @event in _context.@event
                             join group_event in _context.group_event on @event.event_id equals group_event.event_id
                             join @group in _context.@group on group_event.group_settings_id equals @group.group_settings_id
                             select new EventUser
                             {
                                 user_id = @group.user_id,
                                 event_id = @event.event_id
                             };

            return Ok(eventUsers);
        }
    }
}
