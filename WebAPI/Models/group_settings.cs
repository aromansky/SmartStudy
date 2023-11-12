using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class group_settings
    {
        [Key]
        public long group_settings_id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
