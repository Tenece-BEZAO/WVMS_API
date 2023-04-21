using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVMS.DAL.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public AppUsers User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
