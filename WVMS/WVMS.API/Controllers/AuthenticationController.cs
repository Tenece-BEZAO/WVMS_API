using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WVMS.BLL.ServicesContract;
using WVMS.Shared.Dtos;

namespace WVMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Authenticates an admin
        /// </summary>
        [HttpPost]
        //[Authorize(Policy = "SuperAdminPolicy")]
        public async Task<IActionResult> Create([FromBody] UserForRegistrationDto userForRegistration)
        {

            var result = await _authenticationService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(201);

        }

        /// <summary>
        /// Registers a new vendor
        /// </summary>
        [HttpPost]
        [Route("register/vendor")]
        public async Task<IActionResult> CreateVendor([FromBody] VendorForRegistration vendorForRegistration)
        {
            var result = await _authenticationService.RegisterVendor(vendorForRegistration);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        /// <summary>
        /// Registers a new customer
        /// </summary>
        [HttpPost]
        [Route("register/customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerForRegistration sellerForRegistration)
        {
            var result = await _authenticationService.RegisterCustomer(sellerForRegistration);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        /// <summary>
        /// Logs in a user
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authenticationService.ValidateUser(user))
            {
                return Unauthorized();
            }

            /*var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "SuperAdmin")
            };*/

            return Ok(new { Token = await _authenticationService.CreateToken() });
        }

    }
}
