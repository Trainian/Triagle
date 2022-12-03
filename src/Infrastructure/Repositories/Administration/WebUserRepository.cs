using ApplicationCore.Entities.Identity;
using ApplicationCore.Repositories.Administration;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Administration
{
    public class WebUserRepository : IWebUserRepository
    {
        public WebUserRepository(IdentityWebContext context, UserManager<WebUser> user, ILogger<WebUserRepository> logger)
        {
            _context = context;
            _user = user;
            _logger = logger;
        }

        private IdentityWebContext _context { get; set; }
        private UserManager<WebUser> _user { get; set; }
        private ILogger<WebUserRepository> _logger { get; set; }

        public async Task<bool> AddRoleAsync(string userId, string role)
        {
            var roleCheck = await _context.Roles.FirstAsync(r => r.NormalizedName == role.ToUpper());
            var user = await _context.Users.FirstAsync(u => u.Id == userId);
            if (roleCheck == null || user == null)
            {
                _logger.LogWarning("Не удалось найти пользователя или роль UserId = {UserId}, Role = {Role}", userId, role);
                return false;
            }
            await _user.AddToRoleAsync(user, role);
            return true;
        }

        public async Task<bool> ChangeEmailConfirmedAsync(string userId)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == userId);
            if(user == null)
            {
                _logger.LogWarning("Не удалось найти пользователя UserId = {UserId}", userId);
                return false;
            }
            user.EmailConfirmed = !user.EmailConfirmed;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<string>> GetRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles.Select(r => r.Name).ToList();
        }

        public async Task<string> GetUserRole(string userId)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Не удалось найти пользователя UserId = {UserId}", userId);
                return "";
            }
            var role = await _user.GetRolesAsync(user);
            return role.FirstOrDefault() ?? "";
        }

        public async Task<bool> RemoveRoleAsync(string userId, string role)
        {
            var roleCheck = await _context.Roles.FirstAsync(r => r.NormalizedName == role.ToUpper());
            var user = await _context.Users.FirstAsync(u => u.Id == userId);
            if (roleCheck == null || user == null)
            {
                _logger.LogWarning("Не удалось найти пользователя или роль UserId = {UserId}, Role = {Role}", userId, role);
                return false;
            }
            await _user.RemoveFromRoleAsync(user, role);
            return true;
        }

        public async Task<bool> SetLockoutDateUserAsync(string userId, DateTimeOffset date)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Не удалось найти пользователя по id {UserId}", userId);
                return false;
            }

            await _user.SetLockoutEndDateAsync(user, date);

            return true;
        }

        public async Task<IReadOnlyList<WebUser>> UserListAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
    }
}
