using PhysioCenter.Wpf.Domain;
using System.Linq;

namespace PhysioCenter.Wpf.Infrastructure
{
    public static class DataSeeder
    {
        public static void SeedAdmin(AppDbContext db)
        {
            if (!db.Users.Any())
            {
                db.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = "1234",
                    Role = "Admin"
                });

                db.SaveChanges();
            }
        }
    }
}
