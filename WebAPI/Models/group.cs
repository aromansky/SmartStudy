using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class group
    {
        [Key]
        public long group_id { get; set; }
        public long group_settings_id { get; set; }
        public long user_id { get; set; }
    }
}
