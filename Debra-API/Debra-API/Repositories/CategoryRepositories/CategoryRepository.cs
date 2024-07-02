using Debra_API.Data;
using Debra_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Debra_API.Repositories.CategoryRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private AppDBContext _dbContext;

        public CategoryRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int add(TicketDetails category)
        {
            _dbContext.TicketDetails.Add(category);


            return _dbContext.TicketDetails
                .OrderByDescending(t => t.Id)
                .Select(t => t.Id)
                .FirstOrDefault();
        }
    }
}
