using CodePuilsAPI.Models.Domin;
using CodePuilsAPI.Models.Dtos;
using CodePuilsAPI.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace CodePuilsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private Iblogpost _service;
        private ICetagoryService _categortyservice;

        public BlogPostController(Iblogpost service, ICetagoryService categortyservice)
        {
            _service = service;
            _categortyservice = categortyservice;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBlogposts()
        {
            var allblog = await _service.GetAllblogposts();
            var response = new List<blogpostsrequistDto>();
            foreach (var allresponse in allblog)
            {
                response.Add(new blogpostsrequistDto
                {
                    Author = allresponse.Author,
                    Content = allresponse.Content,
                    Id = allresponse.Id,
                    ShortDescription = allresponse.ShortDescription,
                    Isvisable = allresponse.Isvisable,
                    UrlHandler = allresponse.UrlHandler,
                    Title = allresponse.Title,
                    FeaturedImageUrl = allresponse.FeaturedImageUrl,
                    pulishedDate = allresponse.pulishedDate
                    ,
                    Categores = allresponse.Categories.Select(x => new Categorydto { Id = x.Id, Name = x.Name, UrlHandler = x.UrlHandler }).ToList()
                });


            }
            return Ok(response);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogpostbyId([FromRoute] Guid id)
        {
            var plogposts = await _service.GetById(id);
            if (plogposts == null)
            {
                return NotFound($"Not Found {plogposts}");
            }
            var response = new blogpostsrequistDto
            {
                Author = plogposts.Author,
                Content = plogposts.Content,
                Id = plogposts.Id,
                FeaturedImageUrl = plogposts.FeaturedImageUrl,
                Title = plogposts.Title,
                UrlHandler = plogposts.UrlHandler,
                ShortDescription = plogposts.ShortDescription,
                pulishedDate = plogposts.pulishedDate,
                Isvisable = plogposts.Isvisable,
                Categores = plogposts.Categories.Select(x => new Categorydto { Id = x.Id, Name = x.Name, UrlHandler = x.UrlHandler }).ToList()
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult>GetUrlHandleAsync([FromRoute]string urlHandle)
        {
            var url=await _service.GetByUrlHandle(urlHandle);
            if(url is null)
            {
                return NotFound($"Not Found {url}");
            }
            var response = new blogpostsrequistDto
            {
                Author=url.Author,
                Content = url.Content,
                FeaturedImageUrl=url.FeaturedImageUrl,
                Id = url.Id,
                 Title = url.Title,
                 Isvisable=url.Isvisable,
                 pulishedDate = url.pulishedDate,
                 UrlHandler=url.UrlHandler,
                 ShortDescription= url.ShortDescription,
                 Categores=url.Categories.Select(x => new Categorydto {  Name = x.Name,Id=x.Id,UrlHandler = x.UrlHandler }).ToList()
            };
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult>Createblogpost([FromBody]CreateblogpostDto dto)
        {
            var blogpost = new BlogPost
            {
                Author = dto.Author,
                Content = dto.Content,
                FeaturedImageUrl = dto.FeaturedImageUrl,
                Isvisable = dto.Isvisable,
                pulishedDate = dto.pulishedDate,
                ShortDescription = dto.ShortDescription,
                Title = dto.Title,
                UrlHandler = dto.UrlHandler,
                Categories=new List<Category>()
            };

            foreach(var item in dto.Categores)
              {
                var exisetcategory = await _categortyservice.GetById(item);
                if(exisetcategory is not null)
                {
                    blogpost.Categories.Add(exisetcategory);
                }
              }


             blogpost = await _service.createblogpost(blogpost);

            var respons = new blogpostsrequistDto { Author = blogpost.Author,
                Id = blogpost.Id, UrlHandler = blogpost.UrlHandler, Content = blogpost.Content,
                Isvisable = blogpost.Isvisable, pulishedDate = blogpost.pulishedDate,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                ShortDescription = blogpost.ShortDescription, Title = blogpost.Title,
                Categores = blogpost.Categories.Select(
                    x => new Categorydto { Id = x.Id, Name = x.Name, UrlHandler = x.UrlHandler }).ToList(),
              };

            return Ok(respons);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Editblogposts([FromRoute] Guid id,UpdateBlogpostDto dto)
        {
            //convert Domen
            var blogposts = new BlogPost
            {
                Id = id,
                Author = dto.Author,
                Content = dto.Content,
                FeaturedImageUrl = dto.FeaturedImageUrl,
                Isvisable = dto.Isvisable,
                pulishedDate = dto.pulishedDate,
                ShortDescription = dto.ShortDescription,
                Title = dto.Title,
                UrlHandler = dto.UrlHandler,
                Categories = new List<Category>()
            };
            foreach(var items in dto.Categories)
            {
                var exiistcategory =await _categortyservice.GetById(items);
                if (exiistcategory is not null)
                {
                    blogposts.Categories.Add(exiistcategory);
                }
            }        
           
           var Updateblogpost= await _service.EditBlogpost(blogposts);
            if (Updateblogpost is null)
            {
                return NotFound($"Not found This {Updateblogpost}");
            }

            var response = new blogpostsrequistDto
            {
                Author = blogposts.Author,
                Id = blogposts.Id,
                UrlHandler = blogposts.UrlHandler,
                Content = blogposts.Content,
                Isvisable = blogposts.Isvisable,
                pulishedDate = blogposts.pulishedDate,
                FeaturedImageUrl = blogposts.FeaturedImageUrl,
                ShortDescription = blogposts.ShortDescription,
                Title = blogposts.Title,
                Categores=blogposts.Categories.Select(x=>new Categorydto {Id=x.Id,Name=x.Name,UrlHandler=x.UrlHandler }).ToList()
            };
            return Ok(response);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult>DeleteBlogposts(Guid id)
        {
            var blogposts = await _service.GetById(id);
            if(blogposts is null)
            {
                return NotFound($"Not found this {blogposts.Id}");
            }
            await _service.DeleteBlogpost(blogposts.Id);
            var response = new blogpostsrequistDto()
            {
                Author = blogposts.Author,
                Id = blogposts.Id,
                UrlHandler = blogposts.UrlHandler,
                Content = blogposts.Content,
                Isvisable = blogposts.Isvisable,
                pulishedDate = blogposts.pulishedDate,
                FeaturedImageUrl = blogposts.FeaturedImageUrl,
                ShortDescription = blogposts.ShortDescription,
                Title = blogposts.Title,
                Categores = blogposts.Categories.Select(x => new Categorydto { Id = x.Id, Name = x.Name, UrlHandler = x.UrlHandler }).ToList()
            };
            return Ok(response);

        }
        
    }
}
