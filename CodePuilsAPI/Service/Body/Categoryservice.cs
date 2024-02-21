using CodePuilsAPI.Data;
using CodePuilsAPI.Models.Domin;
using CodePuilsAPI.Service.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace CodePuilsAPI.Service.Body
{
    public class Categoryservice : ICetagoryService
    {
        private ApplictionDbContext _context;

        public Categoryservice(ApplictionDbContext context)
        {
            _context = context;
        }

        public async Task<Category> createCatedgory(Category category)
        {
            var creatcategory = await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteItem(Guid id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(x=>x.Id==id);
            if (category is null)
            {
                return null;
            }
            else
            {
                _context.Remove(category);
                _context.SaveChanges();
                return category;
            }
        }

        public async Task<Category?> EditCategory(Category category)
        {
          var categores=await _context.Categories.FirstOrDefaultAsync(x=>x.Id==category.Id);
            if (categores != null)
            {
             _context.Entry(categores).CurrentValues.SetValues(category);
                _context.SaveChanges();
               return categores;

            }
            return null;
            
        }

        public async Task<IEnumerable<Category>> GetAllcategory()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
           return await _context.Categories.SingleOrDefaultAsync(x=>x.Id==id);
          
        }
    }
}
