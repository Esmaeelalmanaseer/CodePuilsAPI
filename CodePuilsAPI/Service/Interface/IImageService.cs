using CodePuilsAPI.Models.Domin;

namespace CodePuilsAPI.Service.Interface
{
    public interface IImageService
    {
        Task<BlogImage?>UploadAsync(IFormFile file,BlogImage blogImage);
        Task<IEnumerable<BlogImage>>GetAllAsync();
    }
}
