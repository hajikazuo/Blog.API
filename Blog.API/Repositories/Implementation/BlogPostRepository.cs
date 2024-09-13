using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(c => c.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _context.BlogPosts.Include(c => c.Categories).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _context.BlogPosts.Include(c => c.Categories).FirstOrDefaultAsync(b => b.Id == blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }

            _context.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            existingBlogPost.Categories = blogPost.Categories;

            await _context.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _context.BlogPosts.FirstOrDefaultAsync(b => b.Id == id);

            if (existingBlogPost != null)
            {
                _context.BlogPosts.Remove(existingBlogPost);
                await _context.SaveChangesAsync();
                return existingBlogPost;
            }

            return null;
        }
    }
}
