﻿using CodePulse.API.Models.Domain;
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
		private readonly ICategoryRepository categoryRepository;
		public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
		{
			this.blogPostRepository = blogPostRepository;
			this.categoryRepository = categoryRepository;
		}


		[HttpPost]
		public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
		{
			var blogPost = new BlogPost()
			{
				Title = request.Title,
				ShortDescription = request.ShortDescription,
				Content = request.Content,
				FeaturedImageUrl = request.FeaturedImageUrl,
				UrlHandle = request.UrlHandle,
				PublishedDate = request.PublishedDate,
				Author = request.Author,
				IsVisible = request.IsVisible,
				Categories = new List<Category>()
			};

			foreach (var categoryGuid in request.Categories)
			{
				var existingCategory = await categoryRepository.GetById(categoryGuid);
				if (existingCategory is not null)
				{
					blogPost.Categories.Add(existingCategory);
				}
			}

			blogPost = await blogPostRepository.CreateAsync(blogPost);

			var response = new BlogPostDto()
			{
				Id = blogPost.Id,
				Title = blogPost.Title,
				ShortDescription = blogPost.ShortDescription,
				Content = blogPost.Content,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				UrlHandle = blogPost.UrlHandle,
				PublishedDate = blogPost.PublishedDate,
				Author = blogPost.Author,
				IsVisible = blogPost.IsVisible,
				Categories = blogPost.Categories.Select(x => new CategoryDto
				{
					Id = x.Id,
					Name = x.Name,
					UrlHandle = x.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBlogPosts()
		{
			var blogPosts = await blogPostRepository.GetAllAsync();

			var response = new List<BlogPostDto>();
			foreach (BlogPost blogPost in blogPosts)
			{
				response.Add(new BlogPostDto
				{
					Id = blogPost.Id,
					Title = blogPost.Title,
					ShortDescription = blogPost.ShortDescription,
					Content = blogPost.Content,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					UrlHandle = blogPost.UrlHandle,
					PublishedDate = blogPost.PublishedDate,
					Author = blogPost.Author,
					IsVisible = blogPost.IsVisible,
					Categories = blogPost.Categories.Select(x => new CategoryDto
					{
						Id = x.Id,
						Name = x.Name,
						UrlHandle = x.UrlHandle
					}).ToList()
				});
			}
			return Ok(response);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
		{
			var blogPost = await blogPostRepository.GetByIdAsync(id);

			if (blogPost is null)
			{
				return NotFound();
			}

			var response = new BlogPostDto()
			{
				Id = blogPost.Id,
				Title = blogPost.Title,
				ShortDescription = blogPost.ShortDescription,
				Content = blogPost.Content,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				UrlHandle = blogPost.UrlHandle,
				PublishedDate = blogPost.PublishedDate,
				Author = blogPost.Author,
				IsVisible = blogPost.IsVisible,
				Categories = blogPost.Categories.Select(x => new CategoryDto
				{
					Id = x.Id,
					Name = x.Name,
					UrlHandle = x.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, [FromBody] UpdateBlogPostRequestDto request)
		{
			var blogPost = new BlogPost()
			{
				Id = id,
				Title = request.Title,
				ShortDescription = request.ShortDescription,
				Content = request.Content,
				FeaturedImageUrl = request.FeaturedImageUrl,
				UrlHandle = request.UrlHandle,
				PublishedDate = request.PublishedDate,
				Author = request.Author,
				IsVisible = request.IsVisible,
				Categories = new List<Category>()
			};

			foreach (var categoryGuid in request.Categories)
			{
				var existingCategory = await categoryRepository.GetById(categoryGuid);
				if(existingCategory is not null)
				{
					blogPost.Categories.Add(existingCategory);
				}
			}

			var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);

			if (updatedBlogPost is null)
			{
				return NotFound();
			}

			var response = new BlogPostDto()
			{
				Id = blogPost.Id,
				Title = blogPost.Title,
				ShortDescription = blogPost.ShortDescription,
				Content = blogPost.Content,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				UrlHandle = blogPost.UrlHandle,
				PublishedDate = blogPost.PublishedDate,
				Author = blogPost.Author,
				IsVisible = blogPost.IsVisible,
				Categories = blogPost.Categories.Select(x => new CategoryDto
				{
					Id = x.Id,
					Name = x.Name,
					UrlHandle = x.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}
	}
}
