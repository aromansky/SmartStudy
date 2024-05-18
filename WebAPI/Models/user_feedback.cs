using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebAPI.Models
{
    public class user_feedback
    {
        [Key]
        public long user_feedback_id { get; set; }
        public required long feedback_id { get; set; }
        public required long user_id { get; set; }
    }
}
