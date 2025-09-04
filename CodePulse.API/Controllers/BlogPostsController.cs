using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostsController : ControllerBase
	{
		private readonly IBlogPostRepository blogPostRepository;

		public BlogPostsController(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository = blogPostRepository;
		}

		[HttpPost]
		public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
		{
			var blogPost = new BlogPost()
			{
				Title = request.Title,
				ShortDescription = request.ShortDescription,
				Content = request.Content,
				FeatureImageUrl = request.FeatureImageUrl,
				UrlHandle = request.UrlHandle,
				PublishedDate = request.PublishedDate,
				Author = request.Author,
				IsVisible = request.IsVisible
			};
			blogPost = await blogPostRepository.CreateAsync(blogPost);

			var response = new BlogPostDto()
			{
				Id = blogPost.Id,
				Title = blogPost.Title,
				ShortDescription = blogPost.ShortDescription,
				Content = blogPost.Content,
				FeatureImageUrl = blogPost.FeatureImageUrl,
				UrlHandle = blogPost.UrlHandle,
				PublishedDate = blogPost.PublishedDate,
				Author = blogPost.Author,
				IsVisible = blogPost.IsVisible
			};

			return Ok(response);
		}
	}
}
