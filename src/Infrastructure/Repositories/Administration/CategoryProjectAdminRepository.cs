using ApplicationCore.Entities.Project;
using ApplicationCore.Repositories.Administration;
using Infrastructure.Repositories.Base;
using Infrastructure.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Administration
{
    public class CategoryProjectAdminRepository : Repository<CategoryP>, ICategoryProjectAdminRepository
    {
        public CategoryProjectAdminRepository(WebContext context) : base(context)
        {
        }
    }
}
