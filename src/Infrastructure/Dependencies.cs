using Infrastructure.Identity;
using Infrastructure.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public class Dependencies
    {
        public static void ConfigureServices(IConfiguration configure, IServiceCollection services)
        {
            services.AddDbContext<WebContext>(c => c.UseSqlServer(configure.GetConnectionString("ContextConnection"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            services.AddDbContext<IdentityWebContext>(c => c.UseSqlServer(configure.GetConnectionString("ContextConnection")));
        }
    }
}