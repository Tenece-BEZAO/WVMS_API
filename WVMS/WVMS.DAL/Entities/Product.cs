using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVMS.DAL.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        //public  VendorId { get; set; }
        [ForeignKey("AppUsers")]
        public Guid UserId { get; set; }

        public AppUsers User { get; set; }

    }
}
