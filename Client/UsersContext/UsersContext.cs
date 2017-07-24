using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.UsersContext
{
    public class HolisUsersContext: IdentityDbContext<HolisUser>
    {
        public HolisUsersContext()
        {
            this.Database.Migrate();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Data Source=users.db");
        }
    }
}
