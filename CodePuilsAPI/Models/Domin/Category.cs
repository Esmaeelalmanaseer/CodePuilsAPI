using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodePuilsAPI.Models.Domin
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandler { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
