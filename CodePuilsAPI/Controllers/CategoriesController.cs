using CodePuilsAPI.Data;
using CodePuilsAPI.Models.Domin;
using CodePuilsAPI.Models.Dtos;
using CodePuilsAPI.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.VisualBasic;

namespace CodePuilsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICetagoryService _service;

        public CategoriesController(ICetagoryService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult>GetCategory()
        {
            var category=await _service.GetAllcategory();

            var responce =new List<Categorydto>();
            //map dto category
            foreach(var categoryitem in category)
            {
                responce.Add(new Categorydto
                {
                    Id = categoryitem.Id,
                    Name = categoryitem.Name,
                    UrlHandler = categoryitem.UrlHandler,
                }); 
            }
            return Ok(responce);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult>GetCategoryById([FromRoute]Guid id)
        {
           var category=await _service.GetById(id);

            if (category is null)
                return NotFound($"Not Found {category}");

            var responce = new Categorydto { Id = category.Id, Name = category.Name, UrlHandler = category.UrlHandler };
            
            return Ok(responce);

        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult>CreateCategory([FromBody]CreateCategoryDto dto)
        {
            //map dto domin model
            Category category = new()
            {
                Name = dto.Name,
                UrlHandler = dto.UrlHandler,
            };
           await _service.createCatedgory(category);

            //domin model to dto
            Categorydto response = new()
            {
                Id=category.Id,
                 Name = category.Name,
                 UrlHandler = category.UrlHandler,
            };

            return Ok(response);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult>EditCategory([FromRoute]Guid id,CategoryUdateDto reques)
        {
            var category=new Category {Id=id, Name=reques.Name, UrlHandler = reques.UrlHandler}; 
            var newcategory=await _service.EditCategory(category);
            if(newcategory != null)
            {
                var response=new Categorydto { Id=newcategory.Id, Name=newcategory.Name,UrlHandler = newcategory.UrlHandler};

            return Ok(response);
            }
            return BadRequest();
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult>DeleteItem([FromRoute] Guid id)
        {
          var category=await _service.DeleteItem(id);

            if(category is null)
            {
                return NotFound($"Not Found this {category.Id}");
            }

            var response = new Categorydto { Id = category.Id, Name = category.Name, UrlHandler = category.UrlHandler };
            return Ok(response);
        }
         
    }
}
