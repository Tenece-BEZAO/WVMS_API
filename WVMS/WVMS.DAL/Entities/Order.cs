using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVMS.DAL.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string? CustomerName { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
