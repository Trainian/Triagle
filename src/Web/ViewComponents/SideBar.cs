using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.ViewComponents
{
    public class SideBar : ViewComponent
    {
        public SideBar(IBlogPageService service, ILogger<SideBar> logger)
        {
            _service = service;
            _logger = logger;
        }

        private IBlogPageService _service { get; set; }
        private ILogger<SideBar> _logger { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(string category)
        {
            SideBarViewModel sideBar;

            var categoryChoised = category;

            var countAllBlogs = await _service.GetCountAllBlogsAsync();

            var categorys = await _service.GetAllCategorysAsync();
            if (categorys == null)
                _logger.LogWarning("Не найдено ни одной категории {categorysList}", categorys);

            var lastBlogMessages = await _service.GetLastCountBlogMessagesAsync(5);
            if (lastBlogMessages == null)
                _logger.LogWarning("Не найдено ни одной категории {lastBlogMessages}", lastBlogMessages);

            sideBar = new SideBarViewModel(categorys, lastBlogMessages, categoryChoised, countAllBlogs);

            return View(sideBar);
        }
    }
}
