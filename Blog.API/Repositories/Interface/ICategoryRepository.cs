using Blog.API.Models.Domain;

namespace Blog.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> CreateAsync(Category category);  
    }
}
