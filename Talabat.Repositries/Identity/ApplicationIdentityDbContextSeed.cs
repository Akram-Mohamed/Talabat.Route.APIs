using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repositries.Identity
{
    public static class ApplicationIdentityDbContextSeed
    {
        public static async Task DataSeedAsync(UserManager<ApplicationUser> userManger)
        {
            if (!userManger.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Akram Mohamed",
                    Email = "AkramMohamed@outlook.com",
                    UserName = "AkramMohamed",
                    PhoneNumber = "01000760300"
                };
                await userManger.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
