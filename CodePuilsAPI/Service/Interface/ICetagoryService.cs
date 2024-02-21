using CodePuilsAPI.Models.Domin;

namespace CodePuilsAPI.Service.Interface
{
    public interface ICetagoryService
    {
        Task <Category> createCatedgory (Category category);
        Task<IEnumerable<Category>> GetAllcategory();
        Task<Category?> GetById(Guid id);
        Task<Category?> EditCategory(Category category);
        Task<Category?> DeleteItem(Guid id);
    }
}
