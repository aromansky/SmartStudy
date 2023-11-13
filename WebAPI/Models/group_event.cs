using System.ComponentModel.DataAnnotations;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Models
{
    public class group_event
    {
        [Key]
        public long group_event_id { get; set; }
        public long event_id { get; set; }
        public long group_settings_id { get; set; }
        public required DateTime date_begin { get; set; }
        public required DateTime date_end { get; set; }
    }
}
