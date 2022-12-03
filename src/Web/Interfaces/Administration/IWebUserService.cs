using Web.ViewModels.Administration;

namespace Web.Interfaces.Administration
{
    public interface IWebUserService
    {
        Task<IEnumerable<WebUserViewModel>> UserListAsync();
        Task<IEnumerable<string>> RolesListAsync();
        Task<string> SetRoleAsync(string userId, string role);
        Task<string> EmailConfirmedChangeAsync(string userId);
        Task<string> SetLockoutUserAsync(string userId, DateTimeOffset date);
    }
}
