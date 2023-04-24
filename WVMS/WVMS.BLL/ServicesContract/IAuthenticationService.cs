using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVMS.Shared.Dtos;

namespace WVMS.BLL.ServicesContract
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);

        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);

        Task<IdentityResult> RegisterCustomer(CustomerForRegistration sellerForRegistration);

        Task<IdentityResult> RegisterVendor(VendorForRegistration vendorForRegistration);

        Task<string> CreateToken();
    }
}
