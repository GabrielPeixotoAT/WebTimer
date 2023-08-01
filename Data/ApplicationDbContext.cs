using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTimer.Data.Seeders;
using WebTimer.Models;

namespace WebTimer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Status> Status { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}