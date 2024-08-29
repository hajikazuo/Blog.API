using Blog.API.Models.Domain;

namespace Blog.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);  
    }
}
