using Microsoft.AspNetCore.Mvc;
using WVMS.BLL.ServicesContract;
using WVMS.Shared.Dtos;

namespace WVMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <returns>A new order</returns>
        [HttpPost]
        [Route("add-order")]
        public async Task<IActionResult> CreateOrder(OrderDto order, Guid userId)
        {
            var result = await _orderService.CreateOrderAsync(order, userId);

            return Ok(result);
        }

        /// <summary>
        /// Gets lists of all the orders
        /// </summary>
        /// <returns>The order list</returns>
        [HttpGet]
        [Route("get-all-orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }

        /// <summary>
        /// Gets an order by its Id
        /// </summary>
        /// <returns>An order</returns>
        [HttpGet]
        [Route("get-order")]
        public async Task<IActionResult> GetOrder(Guid Id)
        {
            var result = await _orderService.GetOrder(Id);
            return Ok(result);
        }
    }

}
