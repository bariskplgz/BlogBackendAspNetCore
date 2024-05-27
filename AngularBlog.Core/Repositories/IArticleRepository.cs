using AngularBlog.API;

namespace AngularBlog.Core.Repositories
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
        Task<Article> GetSingleArticleByIdWithCommentsNCategoryAsync(int articleId);

        IQueryable<Article> GetArticlesByCategoryWithDetails(int categoryId);
    }
}
