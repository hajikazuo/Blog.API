using Blog.API.Models.Domain;

namespace Blog.API.Repositories.Interface
{

    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost> CreateAsync(BlogPost blogPost);
    }
}
