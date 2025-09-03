using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public CategoriesController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
		{
			var category = new Category
			{
				Name = request.Name,
				UrlHandle = request.UrlHandle
			};

			await _context.Categories.AddAsync(category);

			await _context.SaveChangesAsync();

			return Ok(new CategoryDto()
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			});
		}
	}
}
