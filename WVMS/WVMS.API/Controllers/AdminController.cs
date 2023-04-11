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

        /// <summary>
        /// Gets a list of all the users
        /// </summary>
        /// <returns>The user list</returns>
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsers();
            if (users == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            return Ok(users);
        }

        /// <summary>
        /// Gets a paricular user by Id
        /// </summary>
        /// <returns>A user</returns>
        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _adminService.GetUserById(id);
            if (user == null)
                return StatusCode(StatusCodes.Status400BadRequest);

            return Ok(user);
        }

        /// <summary>
        /// Gets a paricular user by Id and locks them out
        /// </summary>
        [HttpPatch]
        [Route("users/{id}/lock/")]
        public async Task<IActionResult> LockUser(Guid id, [FromBody] LockOutDto lockOutDto)
        {
            await _adminService.LockUser(id, lockOutDto.Timespan);
            return Ok("You have been locked out");
        }

        /// <summary>
        /// Gets a user by role
        /// </summary>
        /// <returns>The user</returns>
        [HttpGet]
        [Route("users/role/{role}_role")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            var users = await _adminService.GetUserByRole(role);
            return Ok(users);
        }
    }
}
