using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebAPI.Models
{
    public class feedback
    {
        [Key]
        public long feedback_id { get; set; }
        public long? author_id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
