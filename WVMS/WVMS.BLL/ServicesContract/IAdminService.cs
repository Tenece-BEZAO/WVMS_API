using Microsoft.AspNetCore.Identity;
using WVMS.DAL.Entities;
using WVMS.Shared.Dtos.Response;

namespace WVMS.BLL.ServicesContract
{
    public interface IAdminService
    {
        Task<IEnumerable<AppUserDto>> GetAllUsers();
        Task<AppUserDto> GetUserById(Guid id);
        Task LockUser(string id, int mins);

    }
}
