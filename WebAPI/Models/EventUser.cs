using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [NotMapped] 
    public class EventUser
    {
        public required long user_id { get; set; }
        public required long event_id { get; set; }

    }
}
