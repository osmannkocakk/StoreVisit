using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbComtext
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any()) return;

            var users = new List<User>
        {
            new User { Username = "admin", Role = "Admin" },
            new User { Username = "user1", Role = "Standard" }
        };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }

}
