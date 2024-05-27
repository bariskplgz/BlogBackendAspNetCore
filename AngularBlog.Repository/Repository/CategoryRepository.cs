using AngularBlog.API;
using AngularBlog.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AngularBlog.Repository.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AngularBlogDbContext context) : base(context)
        {
        }
        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            // TODO : to be changed
            return await _context.Categories.Include(x => x.Name).Where(x => x.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}
