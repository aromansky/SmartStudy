using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebAPI.Models
{
    public class user_homework
    {
        [Key]
        public long user_homework_id { get; set; }
        public long homework_id { get; set; }
        public long user_id { get; set; }
    }
}
