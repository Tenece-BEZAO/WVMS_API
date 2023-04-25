using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.Shared.Dtos.Request;

namespace WVMS.BLL.ServicesContract
{
    public interface IPaymentService
    {
        Task<TransactionInitializeResponse> MakePayment(PaymentRequestDto request);
    }
}
