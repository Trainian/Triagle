using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;

namespace Web.Pages
{
    public class SearchModel : PageModel
    {
        public SearchModel(ISearchService service, ILogger<SearchModel> logger)
        {
            _service = service;
            _logger = logger;
        }

        private ISearchService _service { get; set; }
        private ILogger<SearchModel> _logger { get; set; }

        public async Task<IActionResult> OnGetAsync(string fstring)
        {
            var model = await _service.GetElementsByString(fstring);
            return Partial("_Search", model);
        }
    }
}
