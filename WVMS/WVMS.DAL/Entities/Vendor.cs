using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVMS.DAL.Entities
{
    public class Vendor
    {
        public int VendorId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
