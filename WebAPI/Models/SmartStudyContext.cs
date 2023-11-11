using Microsoft.EntityFrameworkCore;


namespace WebAPI.Models
{
    public class SmartStudyContext: DbContext
    {
        public SmartStudyContext(DbContextOptions<SmartStudyContext> options): base(options) 
        {
        }
        public DbSet<WebAPI.Models.user> user { get; set; } = default!;
    }
}
