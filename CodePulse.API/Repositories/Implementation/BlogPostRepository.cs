using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly ApplicationDbContext context;

		public BlogPostRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public async Task<BlogPost> CreateAsync(BlogPost blogPost)
		{
			await context.BlogPosts.AddAsync(blogPost);
			await context.SaveChangesAsync();
			return blogPost;
		}

		public async Task<IEnumerable<BlogPost>> GetAllAsync()
		{
			return await context.BlogPosts.Include(x => x.Categories).ToListAsync();
		}
		public async Task<BlogPost?> GetByIdAsync(Guid id)
		{
			return await context.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
		{
			var existingBlogPost = await context.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
			if(existingBlogPost is null)
			{
				return null;
			}

			context.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

			existingBlogPost.Categories = blogPost.Categories;

			await context.SaveChangesAsync();

			return blogPost;
		}
	}
}
