using Microsoft.AspNetCore.Identity;

namespace WVMS.DAL.Entities
{
    public class AppUsers : IdentityUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
