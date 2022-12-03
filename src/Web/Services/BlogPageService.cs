using ApplicationCore.Entities.Blog;
using ApplicationCore.Repositories;
using AutoMapper;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class BlogPageService : IBlogPageService
    {
        public BlogPageService(IBlogRepository repository, IIdentityRepository identity, IMapper mapper, ILogger<BlogPageService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _identity = identity;
        }

        private readonly IBlogRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BlogPageService> _logger;
        private readonly IIdentityRepository _identity;

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategorysAsync()
        {
            var categoryList = await _repository.GetAllCategorysAsync();
            var categoryViewList = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryList);
            return categoryViewList;
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            var categoryView = _mapper.Map<CategoryViewModel>(category);
            return categoryView;
        }

        public async Task<BlogViewModel> GetBlogByIdAsync(int id)
        {
            var blog = await _repository.GetBlogByIdAsync(id);
            var blogView = _mapper.Map<BlogViewModel>(blog);
            return blogView;
        }

        public async Task<IEnumerable<BlogMessageViewModel>> GetBlogMessagesSortedAsync(BlogViewModel blog)
        {
            var messagesSorted = new List<BlogMessageViewModel>();
            var temp = blog.BlogMessages.Where(bm => bm.Visible == true);

            foreach (var message in temp)
            {
                message.CreateWebUserAvatar = await _identity.GetUserAvatarAsync(message.CreateWebUser);
                if (message.ResponseToBlogMessage == null)
                    messagesSorted.Add(message);
                else
                    messagesSorted.FirstOrDefault(m => m.Id == message.ResponseToBlogMessage.Id)?.AnswerMessage?.Add(message);
            }
            return messagesSorted;
        }

        public async Task<IEnumerable<BlogMessageViewModel>> GetLastCountBlogMessagesAsync(int count)
        {
            var blogMessagesList = await _repository.GetLastCountBlogMessagesAsync(count);
            var blogMessagesListView = _mapper.Map<IEnumerable<BlogMessageViewModel>>(blogMessagesList);
            foreach(var message in blogMessagesListView)
            {
                message.CreateWebUserAvatar = await _identity.GetUserAvatarAsync(message.CreateWebUser);
            }
            return blogMessagesListView;
        }

        public async Task<int> GetCountAllBlogsAsync()
        {
            var count = await _repository.GetCountAllBlogsAsync();
            return count;
        }

        public async Task<IEnumerable<BlogViewModel>> GetBlogsByCategoryNameAndPageAsync(string category, int page, int maxBlogsOnPage)
        {
            Category categoryB = await _repository.GetCategoryByNameAsync(category);
            List<Blog> blogs = new List<Blog>();
            if (categoryB == null || category == "Все")
                blogs = (List<Blog>)await _repository.GetAllBlogsAsync();
            else
                blogs = (List<Blog>)await _repository.GetBlogsByCategoryIdAsync(categoryB.Id);

            List<Blog> sortedBlogs = new List<Blog>();
            for (int i = (page - 1) * maxBlogsOnPage; i < page * maxBlogsOnPage; i++)
                if (i < blogs.Count())
                    sortedBlogs.Add(blogs[i]);

            var blogsView = _mapper.Map<IEnumerable<BlogViewModel>>(sortedBlogs);
            return blogsView;
        }

        public async Task<int> GetCountBlogsByCategory(string category)
        {
            int count = 0;
            if (category == null || category == "Все")
                count = await _repository.GetCountAllBlogsAsync();
            else
            {
                Category categoryId = await _repository.GetCategoryByNameAsync(category);
                if (categoryId != null)
                    count = await _repository.GetCountBlogsByCategoryId(categoryId.Id);
            }
            return count;
        }

        public async Task<string> CreateNewBlogComment(int blogId, int commentId, string userName, string message)
        {
            var created = await _repository.CreateNewBlogComment(blogId, commentId, userName, message);
            if (created == false)
                return "Ошибка при создании сообщений, пожалуйста, попробуйте позже";
            else
                return "Сообщение было успешно создано, после проверки модератором, оно будет добавлено";
        }
    }
}
