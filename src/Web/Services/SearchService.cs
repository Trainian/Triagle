using ApplicationCore.Repositories;
using Web.Interfaces;

namespace Web.Services
{
    public class SearchService : ISearchService
    {
        public SearchService(ISearchRepository repository, ILogger<SearchService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        private ISearchRepository _repository { get; set; }
        private ILogger<SearchService> _logger { get; set; }

        public async Task<IDictionary<string, string>> GetElementsByString(string fstring)
        {
            var find = new Dictionary<string, string>();
            var findBlogs = await _repository.GetBlogsNameByStringAsync(fstring);
            var findProjects = await _repository.GetProjectsNameByStringAsync(fstring);

            foreach (var element in findBlogs)
                find.Add(element.Key, element.Value);

            foreach (var element in findProjects)
                find.Add(element.Key, element.Value);

            if (find.Count == 0)
                find.Add("/", "Ничего не найдено");

            return find;
        }
    }
}
