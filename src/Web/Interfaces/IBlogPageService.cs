using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IBlogPageService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategorysAsync();
        Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        Task<IEnumerable<BlogViewModel>> GetBlogsByCategoryNameAndPageAsync(string category, int page, int maxBlogsOnPage);
        Task<int> GetCountBlogsByCategory(string category);
        Task<BlogViewModel> GetBlogByIdAsync(int id);
        Task<IEnumerable<BlogMessageViewModel>> GetLastCountBlogMessagesAsync(int count);
        Task<IEnumerable<BlogMessageViewModel>> GetBlogMessagesSortedAsync(BlogViewModel blog);
        Task<int> GetCountAllBlogsAsync();
        Task<string> CreateNewBlogComment(int blogId, int commentId, string userName, string message);
    }
}
