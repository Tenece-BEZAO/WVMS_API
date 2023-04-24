using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVMS.DAL.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        //public int VendorId { get; set; }
        public string? ReviewText { get; set; }
        public decimal Rating { get; set; }
        public Customer? Customer { get; set; }
        //public Vendor? Vendor { get; set; }
    }
}
