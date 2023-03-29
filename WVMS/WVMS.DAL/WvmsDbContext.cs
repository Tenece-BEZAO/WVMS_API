using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WVMS.DAL.Configuration;
using WVMS.DAL.Entities;

namespace WVMS.DAL
{
    public class WvmsDbContext : IdentityDbContext<AppUsers>
    {
        public WvmsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfiguration(new RoleConfiguration());
        }

    }
}
