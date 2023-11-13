using Microsoft.EntityFrameworkCore;
using WebAPI.Models;


namespace WebAPI.Models
{
    public class SmartStudyContext: DbContext
    {
        public SmartStudyContext(DbContextOptions<SmartStudyContext> options): base(options) 
        {
        }
        public DbSet<WebAPI.Models.user> user { get; set; } = default!;
        public DbSet<WebAPI.Models.group_settings> group_settings { get; set; } = default!;
        public DbSet<WebAPI.Models.group> group { get; set; } = default!;
        public DbSet<WebAPI.Models.group_event> group_event { get; set; } = default!;
        public DbSet<WebAPI.Models.@event> @event { get; set; } = default!;
    }
}
