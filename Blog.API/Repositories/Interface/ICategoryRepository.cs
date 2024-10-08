using Blog.API.Models.Domain;

namespace Blog.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(
            string? query = null, 
            string? sortBy = null, 
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100);
        Task<Category> GetById(Guid id);
        Task<Category?> CreateAsync(Category category);      
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> DeleteAsync(Guid id);
        Task<int> GetCount();
    }
}
