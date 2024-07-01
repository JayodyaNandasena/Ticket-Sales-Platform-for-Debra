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

        public int add(Category category)
        {
            _dbContext.Categories.Add(category);


            return _dbContext.Categories
                .OrderByDescending(t => t.Id)
                .Select(t => t.Id)
                .FirstOrDefault();
        }
    }
}
