using ApplicationCore.Entities.Identity;

namespace ApplicationCore.Repositories
{
    public interface IIdentityRepository
    {
        Task<WebUser> GetByIdAsync(string id);
        Task<WebUser> GetByNameAsync(string name);
        Task<string> GetUserAvatarAsync(string name);
    }
}
