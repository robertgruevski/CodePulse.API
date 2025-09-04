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

		public async Task<Category?> DeleteAsync(Guid id)
		{
			var existingCategory = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if(existingCategory is null)
			{
				return null;
			}
			else
			{
				context.Categories.Remove(existingCategory);
				await context.SaveChangesAsync();
				return existingCategory;
			}
		}

		public async Task<IEnumerable<Category>> GetAllAsync() => await context.Categories.ToListAsync();

		public async Task<Category?> GetById(Guid id) => await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

		public async Task<Category?> UpdateAsync(Category category)
		{
			var exisitingCategory = await context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
			if(exisitingCategory is not null)
			{
				context.Entry(exisitingCategory).CurrentValues.SetValues(category);
				await context.SaveChangesAsync();
				return category;
			}
			return null;
		}
	}
}
