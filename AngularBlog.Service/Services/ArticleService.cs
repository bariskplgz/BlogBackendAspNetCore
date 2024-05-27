using AngularBlog.API;
using AngularBlog.Core.Repositories;
using AngularBlog.Core.Responses;
using AngularBlog.Core.Services;
using AngularBlog.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AngularBlog.Service.Services
{
    public class ArticleService : Service<Article>, IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        
        public ArticleService(IGenericRepository<Article> repository, IUnitOfWork unitOfWork,IArticleRepository articleRepository) : base(repository, unitOfWork)
        {
            _articleRepository = articleRepository;
        }


        public async Task<ApiResponse<ArticleResponse>> GetSingleArticleByIdAsync(int articleId)
        {
            ApiResponse<ArticleResponse> response = new();

            try
            {
                var article = await _articleRepository.GetSingleArticleByIdWithCommentsNCategoryAsync(articleId);

                ArticleResponse articleResponse = new ArticleResponse()
                {
                    Id = article.Id,
                    Title = article.Title,
                    ContentMain = article.ContentMain,
                    ContentSummary = article.ContentSummary,
                    Picture = article.Picture,
                    PublishDate = article.PublishDate,
                    ViewCount = article.ViewCount,
                    Category = new CategoryResponse() { Id = article.Category.Id, Name = article.Category.Name },
                    CommentCount = article.Comments.Count
                };

                response.Data = articleResponse;
                return response;

            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = ex + "Veritabaninda hata olustu.!";
                return response;
            }


        }

        public System.Tuple<IEnumerable<ArticleResponse>, int> ArticlesPagination(IQueryable<Article> query, int page, int pageSize)
        {
            System.Threading.Thread.Sleep(1500);

            int totalCount = query.Count();

            var articlesResponse = query.Skip((pageSize * (page - 1))).Take(pageSize).ToList().Select(x => new ArticleResponse()
            {
                Id = x.Id,
                Title = x.Title,
                ContentMain = x.ContentMain,
                ContentSummary = x.ContentSummary,
                Picture = x.Picture,
                ViewCount = x.ViewCount,
                CommentCount = x.Comments.Count,
                Category = new CategoryResponse() { Id = x.Category.Id, Name = x.Category.Name }
            });

            return new System.Tuple<IEnumerable<ArticleResponse>, int>(articlesResponse, totalCount);
        }
        public IQueryable<Article> GetArticlesWithCategory(int categoryId, int page = 1, int pageSize = 5)
        {
            return _articleRepository.GetArticlesByCategoryWithDetails(categoryId);
        }
    }
}
