using Microsoft.AspNetCore.Identity;
using ApplicationCore.Entities.Blog;

namespace ApplicationCore.Entities.Identity;

// Add profile data for application users by adding properties to the WebUser class
public class WebUser : IdentityUser
{
    public string? Avatar { get; set; } = "avatar_01.jpg";
    public List<Blog.Blog>? LikedBlogs { get; set; } = new List<Blog.Blog>();
}

