using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
    public class BlogPostRepository : IRepository<BlogPost>
	{
        private readonly StolovkyDbContext stolovkyDbContext;

        public BlogPostRepository(StolovkyDbContext stolovkyDbContext)
        {
            this.stolovkyDbContext = stolovkyDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await stolovkyDbContext.AddAsync(blogPost);
            await stolovkyDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await stolovkyDbContext.BlogPosts.FindAsync(id);
            if (existingBlogPost != null)
            {
                stolovkyDbContext.BlogPosts.Remove(existingBlogPost);
                await stolovkyDbContext.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await stolovkyDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

		public Task<IEnumerable<BlogPost>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await stolovkyDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

		public async Task<BlogPost?> GetAsync(string urlHandle)
		{
			return await stolovkyDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
		}

		public async Task<BlogPost?> GetAsyncByName(string name)
		{
			return await stolovkyDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Heading == name);
		}

		public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await stolovkyDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlogPost != null)
            {
                existingBlogPost.Id = blogPost.Id;
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDesc = blogPost.ShortDesc;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.FeaturedImage = blogPost.FeaturedImage;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.Visible = blogPost.Visible;
                existingBlogPost.PublishDate = blogPost.PublishDate;
                existingBlogPost.Tags = blogPost.Tags;

                await stolovkyDbContext.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        }
    }
}
