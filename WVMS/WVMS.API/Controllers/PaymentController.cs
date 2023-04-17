using Microsoft.AspNetCore.Mvc;
using WVMS.BLL.ServicesContract;
using WVMS.Shared.Dtos.Request;

namespace WVMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("make-payment")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentRequestDto request)
        {
            var result = await _paymentService.MakePayment(request);

            if (result is null)
            {
                return BadRequest("Unsucessful payment");
            }

            return Ok(result);
        }

    }
}
