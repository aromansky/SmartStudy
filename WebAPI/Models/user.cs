using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class user
    {
        [Key]
        public long user_id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Role { get; set; }

    }
}
