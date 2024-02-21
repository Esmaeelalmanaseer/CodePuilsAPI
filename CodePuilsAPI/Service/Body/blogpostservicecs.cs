using CodePuilsAPI.Data;
using CodePuilsAPI.Models.Domin;
using CodePuilsAPI.Service.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CodePuilsAPI.Service.Body
{
    public class blogpostservicecs:Iblogpost
    {
        private ApplictionDbContext _context;

        public blogpostservicecs(ApplictionDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> createblogpost(BlogPost blogpost)
        {
           await _context.blogPosts.AddAsync(blogpost);
           await _context.SaveChangesAsync();
            return blogpost;
        }

        public async Task<BlogPost?> DeleteBlogpost(Guid id)
        {
            var exisetblogposts = await _context.blogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if(exisetblogposts is null)
            {
                return null;
            }
            _context.Remove(exisetblogposts);
            await _context.SaveChangesAsync();
            return exisetblogposts;
               
        }

        public async Task<BlogPost?> EditBlogpost(BlogPost blogPost)
        {
            var exesctblogpost = await _context.blogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if(exesctblogpost is null)
            {
                return null;
            }
             _context.Entry(exesctblogpost).CurrentValues.SetValues(blogPost);
            //Update CAtegories
             exesctblogpost.Categories=blogPost.Categories;
            await _context.SaveChangesAsync();
            return exesctblogpost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllblogposts()
        {
           return await _context.blogPosts.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<BlogPost> GetById(Guid id)
        {
            return await _context.blogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == id);
           
           
        }

        public async Task<BlogPost?> GetByUrlHandle(string urlHandle)
        {
            return await _context.blogPosts.Include(x => x.Categories).SingleOrDefaultAsync(x => x.UrlHandler == urlHandle);
        }
    }
}
