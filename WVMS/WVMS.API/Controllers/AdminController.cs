using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WVMS.BLL.ServicesContract;
using WVMS.Shared.Dtos.Request;

namespace WVMS.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsers();
            if (users == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            return Ok(users);
        }

        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _adminService.GetUserById(id);
            if (user == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            return Ok(user);
        }

        [HttpPatch]
        [Route("users/{id}/lock/")]
        public async Task<IActionResult> LockUser(Guid id, [FromBody] LockOutDto lockOutDto)
        {
            await _adminService.LockUser(id, lockOutDto.Timespan);
            return Ok("You have been locked out");
        }

        [HttpGet]
        [Route("users/role/{role}_role")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            var users = await _adminService.GetUserByRole(role);
            return Ok(users);
        }
    }
}
