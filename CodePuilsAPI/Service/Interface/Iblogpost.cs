using CodePuilsAPI.Models.Domin;
using System.Runtime.CompilerServices;

namespace CodePuilsAPI.Service.Interface
{
    public interface Iblogpost
    {
        Task<BlogPost>createblogpost(BlogPost blogpost);
        Task<IEnumerable<BlogPost>> GetAllblogposts();
        Task<BlogPost> GetById(Guid id);
        Task<BlogPost?> EditBlogpost( BlogPost blogPost);
        Task<BlogPost?> DeleteBlogpost(Guid id);
        Task<BlogPost?> GetByUrlHandle(string urlHandle);
    }
}
