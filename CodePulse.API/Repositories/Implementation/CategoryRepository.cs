using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext context;

		public CategoryRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public async Task<Category> CreateAsync(Category category)
		{
			await context.Categories.AddAsync(category);
			await context.SaveChangesAsync();
			return category;
		}
	}
}
