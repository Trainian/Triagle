using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.TagHelpres
{
    [HtmlTargetElement("pagination")]
    public class PaginationHelper : TagHelper
    {
        [HtmlAttributeName("elements-count")]
        public int Count { get; set; }

        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        [HtmlAttributeName("page-link")]
        public string Url { get; set; }

        [HtmlAttributeName("max-elements-onpage")]
        public int MaxElementsOnPage { get; set; } = 10;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var pages = (byte)Math.Ceiling((decimal)Count / MaxElementsOnPage);

            output.TagName = "ul";
            output.Attributes.SetAttribute("class", "pagination");
            output.Content.AppendHtml($@"<li><a href=""{Url}?pg={(CurrentPage == 1 ? 1 : CurrentPage - 1)}"">left</a></li>");
            for (int i = 1; i <= pages; i++)
            {
                output.Content.AppendHtml($@"<li class=""{(i == CurrentPage ? "active" : "")}""><a href=""{Url}?pg={i}"">{i}</a></li>");
            }
            output.Content.AppendHtml($@"<li><a href=""{Url}?pg={(CurrentPage == pages ? CurrentPage : CurrentPage + 1)}"">right</a></li>");
        }
    }
}
