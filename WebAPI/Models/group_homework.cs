using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebAPI.Models
{
    public class user_homework
    {
        [Key]
        public long homework_id { get; set; }
        public long user_id { get; set; }
    }
}
