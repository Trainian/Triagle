using ApplicationCore.Entities.Blog;
using ApplicationCore.Repositories.Administration;
using AutoMapper;
using Infrastructure.Repositories.Administration;
using Web.Interfaces.Administration;
using Web.ViewModels;

namespace Web.Services.Administration
{
    public class BlogAdminService : IBlogAdminService
    {
        public BlogAdminService(IBlogAdminRepository repository, IMapper mapper, ILogger<BlogAdminService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        private IBlogAdminRepository _repository;
        private IMapper _mapper;
        private ILogger<BlogAdminService> _logger;

        public async Task<string> AddBlogAsync(BlogViewModel blog)
        {
            var blogModel = _mapper.Map<Blog>(blog);
            if(blogModel == null)
            {
                _logger.LogWarning("Не удалось преобразовать Блог из представления {Blog}", blog);
                return "Ошибка добавляения Блога";
            }

            var categoryDb = await _repository.GetCategoryById(blog.Category.Id);
            if (categoryDb == null)
            {
                _logger.LogWarning("Не надена Категория по Id {Id}", blog.Category.Id);
                return "Ошибка обновления Блога";
            }

            blogModel.CreateDateTime = DateTime.Now;
            blogModel.Category = categoryDb;

            var result = await _repository.AddBlogAsync(blogModel);
            return result == true ? "Усешное добавление Блога" : "Ошибка добваления Блога";
        }

        public async Task<BlogViewModel?> GetBlogByIdAsync(int id)
        {
            var blogModel = await _repository.GetBlogByIdAsync(id);

            if(blogModel == null)
                return null;

            var blogView = _mapper.Map<BlogViewModel>(blogModel);
            if (blogView == null)
            {
                _logger.LogWarning("Не удалось преобразовать Блог в представление {Blog}", blogModel);
                return null;
            }

            return blogView;
        }

        public async Task<IEnumerable<BlogViewModel>?> GetBlogListAsync()
        {
            var blogsModel = await _repository.GetBlogListAsync();

            if (blogsModel == null)
                return null;

            var blogsView = _mapper.Map<IEnumerable<BlogViewModel>>(blogsModel);
            if (blogsView == null)
            {
                _logger.LogWarning("Не удалось преобразовать Блоги в представления {Blog}", blogsModel);
                return null;
            }

            return blogsView;
        }

        public async Task<string> RemoveBlogByIdAsync(int id)
        {
            var result = await _repository.RemoveBlogByIdAsync(id);
            return result == true ? "Успешное удаление блога" : "Ошибка удаления блога";
        }

        public async Task<string> UpdateBlogAsync(BlogViewModel blog)
        {
            var blogModel = _mapper.Map<Blog>(blog);
            if(blogModel == null)
            {
                _logger.LogWarning("Ошибка преобразования Блога в модель", blog);
                return "Ошибка обновления блога";
            }

            var blogDB = await _repository.GetBlogByIdAsync(blog.Id);
            if (blogDB == null)
            {
                _logger.LogWarning("Не наден Блог по Id {Id}", blog.Id);
                return "Ошибка обновления блога";
            }

            var categoryDb = await _repository.GetCategoryById(blog.Category.Id);
            if(categoryDb == null)
            {
                _logger.LogWarning("Не надена Категория по Id {Id}", blog.Category.Id);
                return "Ошибка обновления блога";
            }

            blogDB.Title = blogModel.Title;
            blogDB.Text = blogModel.Text;
            blogDB.Category = categoryDb;
            blogDB.Image = blogModel.Image;

            var result = await _repository.UpdateBlogAsync(blogDB);
            return result == true ? "Успешное обновление Блога" : "Ошибка обновления Блога";
        }

        public async Task<IEnumerable<CategoryViewModel>?> GetAllCategoriesAsync()
        {
            var categoriesModel = await _repository.GetAllCategoryListAsync();
            var categoriesView = _mapper.Map<IEnumerable<CategoryViewModel>>(categoriesModel);
            return categoriesView;
        }

        public async Task<CategoryViewModel?> GetCategoryByIdAsync(int id)
        {
            var categoryModel = await _repository.GetCategoryById(id);
            var categoryView = _mapper.Map<CategoryViewModel>(categoryModel);
            return categoryView;
        }
    }
}
