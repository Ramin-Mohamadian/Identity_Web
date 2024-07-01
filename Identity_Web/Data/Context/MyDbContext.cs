using Identity_Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Identity_Web.Data.Context
{
    public class MyDbContext : IdentityDbContext<User, Role, string>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<string>>()
                .HasKey(p => new { p.ProviderKey, p.LoginProvider });

            builder.Entity<IdentityUserRole<string>>()
                .HasKey(p => new { p.UserId, p.RoleId });

            builder.Entity<IdentityUserToken<string>>()
                .HasKey(p => new { p.UserId, p.LoginProvider });



            builder.Entity<User>().Ignore(p => p.NormalizedEmail);



        }


    }
}
