using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WVMS.BLL.ServicesContract;
using WVMS.DAL;
using WVMS.DAL.Entities;
using WVMS.DAL.Interfaces;
using WVMS.Shared.Dtos.Response;

namespace WVMS.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<AppUsers> _userRepo;
        private readonly UserManager<AppUsers> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService( IUnitOfWork unitOfWork, UserManager<AppUsers> userManager)
        {
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<AppUsers>();
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUserDto>> GetAllUsers()
        {
            var result = await _userRepo.GetAllAsync();

            if(result == null)
            {
                throw new Exception("Sorry, they are no users!");
            }
            return result.Select(u => new AppUserDto()
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                UserName = u.UserName
            });
        }

        public async Task<AppUserDto> GetUserById(Guid id)
        {

            var user = await _userRepo.GetSingleByAsync(x => x.Id == id.ToString());
            if (user == null) throw new Exception("User not found");
            return new AppUserDto()
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task LockUser(Guid id, int mins)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            var lockoutEnd = DateTimeOffset.Now.AddMinutes(mins);
            

            await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);

        }

        public async Task<IEnumerable<AppUserDto>> GetUserByRole(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName: role);
            if (users == null)
            {
                throw new Exception($"Users with {role} not found");
            }
            return users.Select(u => new AppUserDto() 
            { 
                UserName = u.UserName, 
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            });
        }
    }
}
