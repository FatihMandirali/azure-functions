using Microsoft.EntityFrameworkCore;

namespace Azure.HttpFunctionApp.Model;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    public DbSet<Lesson> Lessons { get; set; }
    
}