using ApplicationCore.Entities.Identity;
using ApplicationCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        public IdentityRepository(UserManager<WebUser> context, ILogger<IdentityRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        private readonly UserManager<WebUser> _context;
        private readonly ILogger<IdentityRepository> _logger;

        public async Task<WebUser> GetByIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                _logger.LogWarning("Пользователь по Id, не найден {User}", user);
            return user!;
        }

        public async Task<WebUser> GetByNameAsync(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);
            if (user == null)
                _logger.LogWarning("Пользователь по Name, не найден {User}", user);
            return user!;
        }

        public async Task<string> GetUserAvatarAsync(string name)
        {
            string avatar;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == name.ToUpper());
            avatar = user?.Avatar ?? "avatar_01.jpg";
            if (user == null)
                _logger.LogWarning("Аватар пользователя не найден {User}", user);
            return avatar;
        }
    }
}
