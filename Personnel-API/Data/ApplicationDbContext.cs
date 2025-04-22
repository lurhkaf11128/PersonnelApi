using Microsoft.EntityFrameworkCore;
using static Personnel_API.Model.ApiModel;
using System.Collections.Generic;

namespace Personnel_API.Data // <-- Adjust this to your actual project structure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Sales> Sales { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}