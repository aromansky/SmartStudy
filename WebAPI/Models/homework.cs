using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebAPI.Models
{
    public class homework
    {
        [Key]
        public long homework_id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public long? author_id { get; set; }
        public required DateTime date_begin { get; set; }
        public required DateTime date_end { get; set; }
    }
}
