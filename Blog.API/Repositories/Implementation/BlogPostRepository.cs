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

        public async Task<IEnumerable<BlogPost>> GetAllAsync(string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100)
        {
            var blogPosts = _context.BlogPosts.Include(c => c.Categories).AsQueryable();

            if (string.IsNullOrWhiteSpace(query) == false)
            {
                blogPosts = blogPosts.Where(x => x.Title.Contains(query));
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "Title", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    blogPosts = isAsc ? blogPosts.OrderBy(x => x.Title) : blogPosts.OrderByDescending(x => x.Title);
                }

                if (string.Equals(sortBy, "Description", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase) ? true : false;

                    blogPosts = isAsc ? blogPosts.OrderBy(x => x.ShortDescription) : blogPosts.OrderByDescending(x => x.ShortDescription);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            blogPosts = blogPosts.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await blogPosts.ToListAsync();


        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _context.BlogPosts.Include(c => c.Categories).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await _context.BlogPosts.Include(c => c.Categories).FirstOrDefaultAsync(b => b.UrlHandle == urlHandle);
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
        public async Task<int> GetCount()
        {
            return await _context.BlogPosts.CountAsync();
        }
    }
}
