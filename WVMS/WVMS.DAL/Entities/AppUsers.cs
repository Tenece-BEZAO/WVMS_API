using Microsoft.AspNetCore.Identity;

namespace WVMS.DAL.Entities
{
    public class AppUsers : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();
        public IList<Order> Orders { get; set; } = new List<Order>();
    }
}
