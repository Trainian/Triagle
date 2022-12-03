using ApplicationCore.Entities.Blog;
using ApplicationCore.Repositories.Administration;
using AutoMapper;
using Web.Interfaces.Administration;
using Web.ViewModels;

namespace Web.Services.Administration
{
    public class BlogMessageAdminService : IBlogMessageAdminService
    {
        public BlogMessageAdminService(IBlogMessageAdminRepository repository, IBlogAdminRepository blogRepository, IMapper mapper, ILogger<BlogMessageAdminService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _blogRepository = blogRepository;
        }

        private IBlogMessageAdminRepository _repository;
        private IMapper _mapper;
        private ILogger<BlogMessageAdminService> _logger;
        private IBlogAdminRepository _blogRepository;

        public async Task<string> CreateBlogMessageAsync(BlogMessageViewModel blogMessage)
        {
            var blogMessageModel = _mapper.Map<BlogMessage>(blogMessage);
            if(blogMessageModel == null)
            {
                _logger.LogWarning("Ошибка преобразования Сообщения Блога {BlogMessage}", blogMessage);
                return "Ошибка создания блога";
            }
            var blogModel = await _blogRepository.GetBlogByIdAsync(blogMessageModel.Blog.Id);
            if(blogModel == null)
            {
                _logger.LogWarning("Не найден блог по Id {Id}", blogMessageModel.Blog.Id);
                return "Ошибка создания блога";
            }
            if (blogMessageModel.ResponseToBlogMessage != null)
            {
                var blogMessageResponse = await _repository.GetAsync(bm => bm.Id == blogMessageModel.ResponseToBlogMessage.Id && bm.ResponseToBlogMessage == null);
                blogMessageModel.ResponseToBlogMessage = blogMessageResponse.FirstOrDefault();
            }
            blogMessageModel.Blog = blogModel;
            blogMessageModel.CreateDateTime = DateTime.Now;
            var result = await _repository.AddAsync(blogMessageModel);
            return result != null ? "Сообщение Блога, успешно создано!" : "Ошибка создания Сообщения Блога";
        }

        public async Task<IEnumerable<BlogMessageViewModel>> GetAllBlogMessagesAsync()
        {
            var blogMessagesListModel = await _repository.GetAllAsync();
            if(blogMessagesListModel == null)
            {
                _logger.LogWarning("Не найдено ни одного Сообщения Блога");
                return new List<BlogMessageViewModel>();
            }
            var blogMessagesListView = _mapper.Map<IEnumerable<BlogMessageViewModel>>(blogMessagesListModel);
            if(blogMessagesListView == null)
            {
                _logger.LogWarning("Ошибка преобразования списка Сообщений Блога {BlogMessagesList}", blogMessagesListModel);
                return new List<BlogMessageViewModel>();
            }
            return blogMessagesListView;
        }

        public async Task<BlogMessageViewModel> GetBlogMessageByIdAsync(int id)
        {
            var blogMessageModel = await _repository.GetByIdAsync(id);
            if (blogMessageModel == null)
            {
                _logger.LogWarning("Не найдено Сообщение Блога по ID {Id}", id);
                return new BlogMessageViewModel();
            }
            var blogMessageView = _mapper.Map<BlogMessageViewModel>(blogMessageModel);
            if (blogMessageView == null)
            {
                _logger.LogWarning("Ошибка преобразования Сообщения Блога {BlogMessage}", blogMessageModel);
                return new BlogMessageViewModel();
            }
            return blogMessageView;
        }

        public async Task<string> RemoveBlogMessageByIdAsync(int id)
        {
            var blogMessageModel = await _repository.GetByIdAsync(id);
            if (blogMessageModel == null)
            {
                _logger.LogWarning("Не найдено Сообщение Блога по ID {Id}", id);
                return "Ошибка удаления Сообщения Блога";
            }

            var responseAnswers = await _repository.GetAsync(bm => bm.ResponseToBlogMessage == blogMessageModel);
            foreach (var item in responseAnswers)
                await _repository.DeleteAsync(item);

            await _repository.DeleteAsync(blogMessageModel);
            return "Успешное удаление Сообщения Блога";
        }

        public async Task<string> UpdateBlogMessageAsync(BlogMessageViewModel blogMessage)
        {
            var blogMessageModel = await _repository.GetByIdAsync(blogMessage.Id);
            if (blogMessageModel == null)
            {
                _logger.LogWarning("Не найдено Сообщение Блога по ID {Id}", blogMessage.Id);
                return "Ошибка обновления Сообщения Блога";
            }
            var blogModel = await _blogRepository.GetBlogByIdAsync(blogMessageModel.Blog.Id);
            if (blogModel == null)
            {
                _logger.LogWarning("Не найден блог по Id {Id}", blogMessageModel.Blog.Id);
                return "Ошибка обновления Сообщения Блога";
            }
            if (blogMessageModel.ResponseToBlogMessage != null)
            {
                var blogMessageResponse = await _repository.GetAsync(bm => bm.Id == blogMessageModel.ResponseToBlogMessage.Id && bm.ResponseToBlogMessage == null);
                blogMessageModel.ResponseToBlogMessage = blogMessageResponse.FirstOrDefault();
            }
            blogMessageModel.Blog = blogModel;
            blogMessageModel.Text = blogMessage.Text;
            blogMessageModel.Visible = blogMessage.Visible;
            await _repository.UpdateAsync(blogMessageModel);
            return "Успешное обновление Сообщения Блога";
        }

        public async Task<IEnumerable<BlogMessageViewModel>> GetBlogMessagesByBlogIdAsync(int blogId)
        {
            var blogMessagesListModel = await _repository.GetBlogMessagesByBlogIdAsync(blogId);
            if (blogMessagesListModel == null)
            {
                _logger.LogWarning("Не найдено ни одного Сообщения Блога");
                return new List<BlogMessageViewModel>();
            }
            var blogMessagesListView = _mapper.Map<IEnumerable<BlogMessageViewModel>>(blogMessagesListModel);
            if (blogMessagesListView == null)
            {
                _logger.LogWarning("Ошибка преобразования списка Сообщений Блога {BlogMessagesList}", blogMessagesListModel);
                return new List<BlogMessageViewModel>();
            }
            return blogMessagesListView;
        }

        public async Task<string> ChangeVisibleAsync(int id)
        {
            var blogMessageModel = await _repository.GetByIdAsync(id);
            if(blogMessageModel.CreateWebUser == null)
            {
                _logger.LogWarning("Не найдено Сообщение Блога по ID {Id}", id);
                return "Ошибка обновления Сообщения Блога";
            }
            blogMessageModel.Visible = !blogMessageModel.Visible;
            await _repository.UpdateAsync(blogMessageModel);
            return "Успешное обновление Сообщения Блога";
        }
    }
}
