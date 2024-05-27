using AngularBlog.API;
using AngularBlog.Core.Repositories;
using AngularBlog.Core.Responses;
using AngularBlog.Core.Services;
using AngularBlog.Core.UnitOfWorks;
using System.Net;

namespace AngularBlog.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository) : base(repository, unitOfWork)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<Category>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            ApiResponse<Category> response = new();
            try
            {
                var category = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);

                response.Data = category;  
                return response;
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.InternalServerError;
                response.Message = ex + "Veritabaninda hata olustu.!";
                return response;
            }
        }
    }
}
