using CodePuilsAPI.Models.Domin;
using CodePuilsAPI.Models.Dtos;
using CodePuilsAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePuilsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogImageController : ControllerBase
    {

        private readonly IImageService _imageService;

        public BlogImageController(IImageService imageService)
        {
            this._imageService = imageService;
        }
        [HttpGet]
        public async Task<IActionResult>GetAllImage()
        {
            var allImage=await _imageService.GetAllAsync();

            var response = new List<BlogImage>();
            foreach (var item in allImage)
            {
                response.Add(new BlogImage {
                DateCreated = item.DateCreated,
                 FileExtension = item.FileExtension,
                 FileName = item.FileName,
                 Id = item.Id,
                 Title = item.Title,
                 Url = item.Url 
                });
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file
            , [FromForm] string fileName, [FromForm] string title)
        {
            {
                ValidateFileUpload(file);

                if (ModelState.IsValid)
                {
                    // File upload
                    var blogImage = new BlogImage
                    {
                        FileExtension = Path.GetExtension(file.FileName).ToLower(),
                        FileName = fileName,
                        Title = title,
                        DateCreated = DateTime.Now
                    };

                    blogImage = await _imageService.UploadAsync(file, blogImage);

                    // Convert Domain Model to DTO
                    var response = new BlogImageDto
                    {
                        Id = blogImage.Id,
                        Title = blogImage.Title,
                        DateCreated = blogImage.DateCreated,
                        FileExtension = blogImage.FileExtension,
                        FileName = blogImage.FileName,
                        Url = blogImage.Url
                    };

                    return Ok(response);
                }
                return BadRequest();
            }

            }
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }

    }
}
