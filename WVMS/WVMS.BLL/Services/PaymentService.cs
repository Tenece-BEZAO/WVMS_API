using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PayStack.Net;
using WVMS.BLL.ServicesContract;
using WVMS.DAL.Entities;
using WVMS.DAL.Interfaces;
using WVMS.Shared.Dtos.Request;

namespace WVMS.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Payment> _paymentRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUsers> _userManager;
        private readonly string token;


        private PayStackApi Paystack { get; set; }


        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration,
            UserManager<AppUsers> userManager)
        {
            _unitOfWork = unitOfWork;
            _paymentRepo = _unitOfWork.GetRepository<Payment>();
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            token = _configuration["Payment:PaystackTestKey"];
            Paystack = new PayStackApi(token);
        }

        public async Task<TransactionInitializeResponse> MakePayment(PaymentRequestDto request)
        {
            try
            {
                AppUsers user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user is null)
                {
                    throw new Exception("user is not found");
                }

                TransactionInitializeRequest createRequest = new()
                {
                    AmountInKobo = (int)request.Amount * 100,
                    Email = request.Email,
                    Currency = "NGN",
                    Reference = Generate().ToString(),
                    CallbackUrl = "https://localhost:5001/api/Payment/verify-payment"
                };

                TransactionInitializeResponse response = Paystack.Transactions.Initialize(createRequest);

                if (response.Status)
                {
                    var payment = new Payment()
                    {
                        Name = request.Name,
                        Amount = request.Amount,
                        PaymentRef = createRequest.Reference,
                        Email = request.Email,
                        Status = false,
                        UserId = request.UserId,
                    };
                    await _paymentRepo.AddAsync(payment);

                    return response;
                }

                throw new Exception("Transaction didn't go through");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private static int Generate()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            return rand.Next(100000000, 999999999);
        }

    }
}
