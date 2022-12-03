using ApplicationCore.Repositories.Administration;
using AutoMapper;
using Web.Interfaces.Administration;
using Web.ViewModels.Administration;

namespace Web.Services.Administration
{
    public class WebUserService : IWebUserService
    {
        public WebUserService(IWebUserRepository user, IMapper mapper, ILogger<WebUserService> logger)
        {
            _user = user;
            _mapper = mapper;
            _logger = logger;
        }

        private IWebUserRepository _user;
        private IMapper _mapper;
        private ILogger<WebUserService> _logger;

        public async Task<string> SetRoleAsync(string userId, string role)
        {
            bool enabledRole = false;
            var userRole = await _user.GetUserRole(userId);

            // If User don't have Role, set Role "User"
            if (userRole == "")
            {
                await _user.AddRoleAsync(userId, "User");
                userRole = await _user.GetUserRole(userId);
            }

            var roles = await _user.GetRolesAsync();

            if (userRole == role)
                return "У пользователя уже естm такие Права";

            foreach (var roleInList in roles)
                if (role == roleInList)
                    enabledRole = true;

            if(!enabledRole)
                return "Такой Роли не существует";

            await _user.RemoveRoleAsync(userId, userRole);
            await _user.AddRoleAsync(userId, role);

            var result = await _user.AddRoleAsync(userId, role);
            return result == true ? "Успешное добавление Роли пользователю !" : "Ошибка добавления Роли :(";
        }

        public async Task<string> SetLockoutUserAsync(string userId, DateTimeOffset date)
        {
            var result = await _user.SetLockoutDateUserAsync(userId, date);
            return result == true ? "Успешная установка Блокировки, пользователю !" : "Ошибка установки даты Блокировки, пользователя :(";
        }

        public async Task<IEnumerable<WebUserViewModel>> UserListAsync()
        {
            var usersModel =  await _user.UserListAsync();
            var usersView = _mapper.Map<List<WebUserViewModel>>(usersModel);
            if (usersView == null)
            {
                _logger.LogWarning("Ошибка приобразования пользователей в представление {UsersModel}", usersModel);
                usersView = new List<WebUserViewModel>();
            }
            foreach (var user in usersView)
                user.Role = await _user.GetUserRole(user.Id);

            return usersView;
        }

        public async Task<string> EmailConfirmedChangeAsync(string userId)
        {
            var result = await _user.ChangeEmailConfirmedAsync(userId);
            return result == true ? "Успешное изминение подтверждение почты" : "Ошибка изминения подтверждения Почты :(";
        }

        public async Task<IEnumerable<string>> RolesListAsync()
        {
            var roles = await _user.GetRolesAsync();
            return roles;
        }
    }
}
