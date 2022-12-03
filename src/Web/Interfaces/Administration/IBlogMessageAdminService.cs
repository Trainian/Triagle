using Web.ViewModels;

namespace Web.Interfaces.Administration
{
    public interface IBlogMessageAdminService
    {
        Task<IEnumerable<BlogMessageViewModel>> GetAllBlogMessagesAsync();
        Task<IEnumerable<BlogMessageViewModel>> GetBlogMessagesByBlogIdAsync(int blogId);
        Task<BlogMessageViewModel> GetBlogMessageByIdAsync(int id);
        Task<string> CreateBlogMessageAsync(BlogMessageViewModel blogMessage);
        Task<string> UpdateBlogMessageAsync(BlogMessageViewModel blogMessage);
        Task<string> RemoveBlogMessageByIdAsync(int id);
        Task<string> ChangeVisibleAsync(int id);
    }
}
