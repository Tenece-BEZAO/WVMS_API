using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WVMS.DAL.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
               new IdentityRole
               {
                   Name = "Vendor",
                   NormalizedName = "VENDOR",
               },

               new IdentityRole
               {
                   Name = "Customer",
                   NormalizedName = "CUSTOMER",
               },

               new IdentityRole
               {
                   Name = "Admin",
                   NormalizedName = "ADMIN",
               });
        }

    }
}
