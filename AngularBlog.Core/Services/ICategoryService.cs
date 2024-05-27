using AngularBlog.API;
using AngularBlog.Core.Responses;

namespace AngularBlog.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        public Task<ApiResponse<Category>> GetSingleCategoryByIdWithProductsAsync(int categoryId);
    }
}
