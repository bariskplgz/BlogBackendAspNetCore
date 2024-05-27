using AngularBlog.Core.Repositories;
using AngularBlog.Core.Responses;
using AngularBlog.Core.Services;
using AngularBlog.Core.UnitOfWorks;
using System.Linq.Expressions;

namespace AngularBlog.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IQueryable<T>> AddRangeAsync(IQueryable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }


        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }

        public async Task<ApiResponse<List<T>>> GetAllAsync()
        {
            ApiResponse<List<T>> response = new()
            {
                Data = await _repository.GetAllAsync()
            };
            return response;
        }

        public async Task<ApiResponse<T>> GetByIdAsync(int id)
        {
            ApiResponse<T> response = new()
            {
                Data = await _repository.GetByIdAsync(id)
            };
            return response;
        }
    }
}