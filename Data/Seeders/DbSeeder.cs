using Microsoft.EntityFrameworkCore;
using WebTimer.Models;

namespace WebTimer.Data.Seeders
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            SeedStatus(context);
        }

        private static void SeedStatus(ApplicationDbContext context)
        {
            if (!context.Status.Any())
            {
                List<Status> status = new List<Status>
                {
                    new Status
                    {
                        Description = "Working"
                    },
                    new Status
                    {
                        Description = "Project"
                    },
                    new Status
                    {
                        Description = "Studing"
                    },
                    new Status
                    {
                        Description = "Personal"
                    },
                    new Status
                    {
                        Description = "Enterteiment"
                    },
                };

                context.Status.AddRange(status);
                context.SaveChanges();
            }
        }
    }
}
