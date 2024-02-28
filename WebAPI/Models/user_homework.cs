using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebAPI.Models
{
    public class group_homework
    {
        [Key]
        public long user_homework_id { get; set; }
        public long homework_id { get; set; }
        public long group_settings_id { get; set; }
    }
}
