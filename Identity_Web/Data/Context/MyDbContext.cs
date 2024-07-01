using Identity_Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity_Web.Data.Context
{
    public class MyDbContext:IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options) 
        {
            
        }


    }
}
