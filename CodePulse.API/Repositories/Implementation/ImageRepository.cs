using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Repositories.Implementation
{
	public class ImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly ApplicationDbContext context;

		public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
		{
			this.webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this.context = context;
		}


		public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
		{
			var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
			using var stream = new FileStream(localPath, FileMode.Create);
			await file.CopyToAsync(stream);
			
			var httpRequest = httpContextAccessor.HttpContext.Request;
			var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

			blogImage.Url = urlPath;

			await context.BlogImages.AddAsync(blogImage);
			await context.SaveChangesAsync();

			return blogImage;
		}
	}
}
