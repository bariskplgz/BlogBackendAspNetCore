using AngularBlog.API;
using AngularBlog.Core.Responses;

namespace AngularBlog.Core.Services
{
    public interface IArticleService : IService<Article>
    {
        public Task<ApiResponse<ArticleResponse>> GetSingleArticleByIdAsync(int articleId);
        public IQueryable<Article> GetArticlesWithCategory(int categoryId, int page = 1, int pageSize = 5);

        public System.Tuple<IEnumerable<ArticleResponse>, int> ArticlesPagination(IQueryable<Article> query, int page, int pageSize);
    }
}