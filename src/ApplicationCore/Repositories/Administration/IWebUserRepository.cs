using ApplicationCore.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Administration
{
    public interface IWebUserRepository
    {
        Task<IReadOnlyList<WebUser>> UserListAsync();
        Task<bool> AddRoleAsync (string userId, string role);
        Task<bool> RemoveRoleAsync (string userId, string role);
        Task<string> GetUserRole (string userId);
        Task<IReadOnlyList<string>> GetRolesAsync ();
        Task<bool> SetLockoutDateUserAsync(string userId, DateTimeOffset date);
        Task<bool> ChangeEmailConfirmedAsync(string userId);
    }
}
