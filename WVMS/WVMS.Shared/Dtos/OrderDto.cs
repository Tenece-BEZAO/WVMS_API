using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVMS.Shared.Dtos
{
    public class OrderDto
    {
        public class Detail
        {
            public Guid ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public DateTime OrderDate { get; set; } = DateTime.Now;
        }

        public IEnumerable<Detail> Details { get; set; }

    }
}
