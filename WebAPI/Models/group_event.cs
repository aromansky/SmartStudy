using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class group_event
    {
        [Key]
        public long group_event_id { get; set; }
        public long event_id { get; set; }
        public long group_settings_id { get; set; }
    }
}
