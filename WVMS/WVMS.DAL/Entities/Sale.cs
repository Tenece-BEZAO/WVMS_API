using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVMS.DAL.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }

        [ForeignKey(name: "Customer")]
        public int? CustomerId { get; set; }
        public int? ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public Customer? Customer { get; set; }
        public Product? Products { get; set; }

    }
}
