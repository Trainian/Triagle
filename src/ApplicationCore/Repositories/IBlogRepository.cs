using ApplicationCore.Entities.Blog;
using ApplicationCore.Repositories.Base;

namespace ApplicationCore.Repositories
{
    public interface IBlogRepository : IRepository<Category>
    {
        Task<IReadOnlyList<Category>> GetAllCategorysAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string category);
        Task<IReadOnlyList<Blog>> GetAllBlogsAsync();
        Task<IReadOnlyList<Blog>> GetBlogsByCategoryIdAsync(int category);
        Task<Blog> GetBlogByIdAsync(int id);
        Task<IReadOnlyList<BlogMessage>> GetLastCountBlogMessagesAsync(int count);
        Task<int> GetCountAllBlogsAsync();
        Task<int> GetCountBlogsByCategoryId(int category);
        Task<BlogMessage> GetBlogMessageByIdAsync(int id);
        Task<bool> CreateNewBlogComment(int blogId, int commentId, string userName, string message);
    }
}
