namespace Web.ViewModels
{
    public class SideBarViewModel
    {
        public SideBarViewModel(IEnumerable<CategoryViewModel> categorys, IEnumerable<BlogMessageViewModel> lastBlogMessages, string categoryChoised, int countAllBlogs)
        {
            Categorys = categorys;
            LastBlogMessages = lastBlogMessages;
            CategoryChoised = categoryChoised;
            CountAllBlogs = countAllBlogs;
        }

        public IEnumerable<CategoryViewModel> Categorys { get; set; }
        public IEnumerable<BlogMessageViewModel> LastBlogMessages { get; set; }
        public string CategoryChoised { get; set; }
        public int CountAllBlogs { get; set; }
    }
}
