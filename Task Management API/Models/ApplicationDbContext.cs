using Microsoft.EntityFrameworkCore;

namespace Task_Management_API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Task> Tasks { get; set; }
    }
}
