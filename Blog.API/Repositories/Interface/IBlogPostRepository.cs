using Blog.API.Models.Domain;

namespace Blog.API.Repositories.Interface
{

    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> CreateAsync(BlogPost blogPost);
    }
}
