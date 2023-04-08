using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.DAL.Entities;
using WVMS.Shared.Dtos;

namespace WVMS.BLL.ServicesContract
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(OrderDto order, Guid userID);

        Task<IEnumerable<Order>> GetOrders();

        Task<OrderDto> GetOrder(Guid Id);
    }
}
