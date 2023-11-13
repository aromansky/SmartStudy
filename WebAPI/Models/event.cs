using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class @event
    {
        [Key]
        public long event_id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
