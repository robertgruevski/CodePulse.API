using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;

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
	}
}
