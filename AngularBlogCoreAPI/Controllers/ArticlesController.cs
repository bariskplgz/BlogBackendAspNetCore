using AngularBlog.API;
using AngularBlog.Core.Services;
using AngularBlogCoreAPI.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularBlogCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly AngularBlogDbContext _context;
        private readonly IArticleService _articleService;

        public ArticlesController(AngularBlogDbContext context,IArticleService articleService)
        {
            _context = context;
            _articleService = articleService;
        }

        // GET: Article By Id
        [HttpGet("{article_id}")]
        public async Task<IActionResult> GetArticleById(int article_id)
        {
            var article  =  await _articleService.GetSingleArticleByIdAsync(article_id);  
            return Ok(article);
        }

        [HttpGet("{page}/{pageSize}")]
        public IActionResult GetArticle(int page = 1, int pageSize = 5)
        {
            System.Threading.Thread.Sleep(3000);
            try
            {
                IQueryable<Article> query;

                query = _context.Articles.Include(x => x.Category).Include(y => y.Comments).OrderByDescending(z => z.PublishDate);

                int totalCount = query.Count();

                // 5*(1-1) => 0
                //5*(2-1)=>5
                var articlesResponse = query.Skip((pageSize * (page - 1))).Take(5).ToList().Select(x => new ArticleResponse()
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

                var result = new
                {
                    TotalCount = totalCount,
                    Articles = articlesResponse
                };
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //localhost/api/articles/GetArticlesWithCategory/2/1/5
        [HttpGet]
        [Route("GetArticlesWithCategory/{categoryId}/{page}/{pageSize}")]
        public IActionResult GetArticlesByCategory(int categoryId, int page = 1, int pageSize = 5)
        {
            IQueryable<Article> query = _articleService.GetArticlesWithCategory(categoryId, page, pageSize); 

            var queryResult = _articleService.ArticlesPagination(query, page, pageSize);

            var result = new
            {
                TotalCount = queryResult.Item2,
                Articles = queryResult.Item1
            };
            return Ok(result);
        }
    }
}
