using ApplicationCore.Entities.Blog;
using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Project;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Web
{
    public class WebContextSeed
    {
        public static async Task SeedAsync(WebContext context)
        {
            if (!await context.Categories.AnyAsync())
            {
                string userCreate = "Trainian";

                await context.Categories.AddRangeAsync(new List<Category>()
                {
                    new Category(){Name = "Работы", Information = "Все работы"},
                    new Category(){Name = "Новости", Information = "Новости сайта"},
                    new Category(){Name = "Акции", Information = "Акции проходимые на сайте"}
                });
                await context.SaveChangesAsync();

                var category = await context.Categories.OrderBy(c => c.Id).FirstAsync();

                await context.Blogs.AddRangeAsync(new List<Blog>()
                {
                    new Blog(){Title = "Блог не информативный", Text = "Блог, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Category = category, CreateDateTime = new DateTime(2022,07,12, 12, 00, 00), CreateWebUser = userCreate,
                        Image = "1.jpg", Likes = new List<WebUser>(), Twits = 0},
                    new Blog(){Title = "Блог не информативный", Text = "Блог, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Category = category, CreateDateTime = new DateTime(2022,07,12, 13, 10, 00), CreateWebUser = userCreate,
                        Image = "2.jpg", Likes = new List<WebUser>(), Twits = 0},
                    new Blog(){Title = "Блог не информативный", Text = "Блог, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Category = category, CreateDateTime = new DateTime(2022,07,13, 07, 10, 30), CreateWebUser = userCreate,
                        Image = "2.jpg", Likes = new List<WebUser>(), Twits = 0},
                });
                await context.SaveChangesAsync();

                var blog2 = await context.Blogs.OrderBy(c => c.Id).FirstAsync(b => b.Id == 2);
                var blog3 = await context.Blogs.OrderBy(c => c.Id).FirstAsync(b => b.Id == 3);

                await context.BlogMessages.AddRangeAsync(new List<BlogMessage>()
                {
                    new BlogMessage(){Text = "Тестовое сообщение пользователя, для дальнейшего удаления", Blog = blog2, CreateDateTime = new DateTime(2022,07,12, 13, 10, 00),
                         CreateWebUser = userCreate, Visible = true},
                    new BlogMessage(){Text = "Тестовое сообщение пользователя, для дальнейшего удаления", Blog = blog2, CreateDateTime = new DateTime(2022,07,12, 13, 10, 00),
                         CreateWebUser = userCreate, Visible = true},
                    new BlogMessage(){Text = "Тестовое сообщение пользователя, для дальнейшего удаления", Blog = blog3, CreateDateTime = new DateTime(2022,07,13, 07, 10, 30),
                         CreateWebUser = userCreate, Visible = true},
                });
                await context.SaveChangesAsync();

                var firstMessage = await context.BlogMessages.OrderBy(c => c.Id).FirstAsync();
                var secontMessage = await context.BlogMessages.FirstOrDefaultAsync(c => c.Id == 2);
                secontMessage.ResponseToBlogMessage = firstMessage;

                await context.SaveChangesAsync();

                await context.Skills.AddRangeAsync(new List<Skill>()
                {
                    new Skill(){Name = "Html", Information = "Html"},
                    new Skill(){Name = "Asp.Net", Information = "Asp.Net"},
                    new Skill(){Name = "JS", Information = "JS"},
                    new Skill(){Name = "CSS", Information = "CSS"}
                });

                await context.SaveChangesAsync();

                await context.CategoriesProject.AddRangeAsync(new List<CategoryP>()
                {
                    new CategoryP(){Name = "Современное", Information = "Современные идеи"},
                    new CategoryP(){Name = "Минимализм", Information = "Минималистические идеи"},
                    new CategoryP(){Name = "Магазин", Information = "Идеи Магазинов"},
                    new CategoryP(){Name = "О себе", Information = "Идеи сайтов О Себе"}
                });

                await context.SaveChangesAsync();

                var categorys1 = await context.CategoriesProject.FirstAsync(c => c.Id == 1);
                var categorys2 = await context.CategoriesProject.FirstAsync(c => c.Id == 2);
                var allSkills = await context.Skills.ToListAsync();

                await context.Projects.AddRangeAsync(new List<Project>()
                {
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2022,07,12, 12, 00, 00),
                        Image = "1.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys1},
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2021,07,12, 12, 00, 00),
                        Image = "2.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys1},
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2020,07,12, 12, 00, 00),
                        Image = "3.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys1},
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2022,06,12, 12, 00, 00),
                        Image = "4.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys1},
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2022,05,12, 12, 00, 00),
                        Image = "5.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys1},
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2022,04,12, 12, 00, 00),
                        Image = "6.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys1},
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2022,03,12, 12, 00, 00),
                        Image = "7.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys2},
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2022,02,12, 12, 00, 00),
                        Image = "8.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys2},
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2022,01,12, 12, 00, 00),
                        Image = "9.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys2},
                    new Project(){ProjectName = "Проект не информативный", CreateWebUser = userCreate, CreateDateTime = new DateTime(2022,07,01, 12, 00, 00),
                        Image = "10.jpg", PreviewLink = "#", Text = "Проект, не содержащей информации необходимые для тестирования и дальнейшего удаления",
                        Skills = allSkills, Category = categorys2},
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
