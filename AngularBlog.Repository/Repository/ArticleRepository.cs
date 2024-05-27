using AngularBlog.API;
using AngularBlog.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AngularBlog.Repository.Repository
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        public ArticleRepository(AngularBlogDbContext context) : base(context)
        {
        }

        public async Task<Article> GetSingleArticleByIdWithCommentsNCategoryAsync(int articleId)
        {
            return await _context.Articles
                .Include(x => x.Category)
                .Include(y => y.Comments)
                .FirstOrDefaultAsync(z => z.Id == articleId);
        }

        public IQueryable<Article>  GetArticlesByCategoryWithDetails(int categoryId)
        {
            return _context.Articles.Include(x => x.Category).Include(y => y.Comments).Where(z => z.CategoryId == categoryId).OrderByDescending(x => x.PublishDate);
        }

    }
}
