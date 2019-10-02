using Microsoft.EntityFrameworkCore;

namespace DoniAPItest.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Tour> Tours { get; set; }
    }
}
