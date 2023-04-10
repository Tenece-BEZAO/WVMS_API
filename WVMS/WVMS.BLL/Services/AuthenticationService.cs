using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WVMS.BLL.ServicesContract;
using WVMS.DAL.Entities;
using WVMS.Shared.Dtos;

namespace WVMS.BLL.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private AppUsers? _user;

        public AuthenticationService(UserManager<AppUsers> userManager, SignInManager<AppUsers> signInManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {

            var userExists = await _userManager.FindByEmailAsync(userForRegistration.Email);

            if (userExists != null)
            {
                throw new Exception("Email already exists");
            }
            // var userResult = _mapper.Map<AppUsers>(userForRegistration);
            var userResult = new AppUsers()
            {
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName,
                UserName = userForRegistration.UserName,
                Email = userForRegistration.Email,
                PasswordHash = userForRegistration.Password,
                PhoneNumber = userForRegistration.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(userResult, userForRegistration.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(userResult, userForRegistration.Roles); 
            }

            return result;
        }

        public async Task<IdentityResult> RegisterVendor(VendorForRegistration vendorForRegistration)
        {
            var userExists = await _userManager.FindByEmailAsync(vendorForRegistration.Email);

            if (userExists != null)
            {
                throw new Exception("Email has been taken");
            }

            var vendorResult = _mapper.Map<AppUsers>(vendorForRegistration);

            var vendor = await _userManager.CreateAsync(vendorResult, vendorForRegistration.Password);

            if (vendor.Succeeded)
            {
                await _userManager.AddToRoleAsync(vendorResult, "Vendor");
            }

            return vendor;
        }

        public async Task<IdentityResult> RegisterCustomer(CustomerForRegistration sellerForRegistration)
        {
            var userExists = await _userManager.FindByEmailAsync(sellerForRegistration.Email);

            if (userExists != null)
            {
                throw new Exception("Email is already taken");
            }

            var sellerResult = _mapper.Map<AppUsers>(sellerForRegistration);

            var seller = await _userManager.CreateAsync(sellerResult, sellerForRegistration.Password);
            if (seller.Succeeded)
            {
                await _userManager.AddToRoleAsync(sellerResult, "Customer");
            }

            return seller;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            var _verifySignIn = await _signInManager.PasswordSignInAsync(userForAuth.UserName, userForAuth.Password, isPersistent: false, lockoutOnFailure: true);
            if (_verifySignIn.IsLockedOut)
            {
                throw new Exception("Sorry! You have been lockedout");
            }

            _user = await _userManager.FindByNameAsync(userForAuth.UserName);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

            if (!result)
            {
                throw new Exception("Invalid email or password");
            }

            return result;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("REPORTAPISECRET") ?? "Fk24632Pz3gyJLYeYqJ6D8qELyNPUubr8vstypCgfMAC8Jyb3B");
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, _user.UserName),
               new Claim(ClaimTypes.Role, "SuperAdmin")
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }


    }
}
