using Repo_Core.Identity_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;
using Repo_Core.Interface;
using Repo_Core.Models;
using Repo_Core.Services;
using Microsoft.AspNetCore.Authorization;
using Graduation_project.ViewModel;

namespace Graduation_project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    // Note : (There are class name posts in identity in models)

    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogController(IBlogService blogService, IWebHostEnvironment webHostEnvironment)
        {
            _blogService = blogService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("allblogs")]
        public IActionResult GetPosts(int page, int pageSize)
        {
            var posts = _blogService.GetPosts(page, pageSize);
            if (posts == null)
                return BadRequest("not found");

            else
            {

                return Ok(posts.ToList());
            }


            //var posts = _blogService.GetPosts( page , pageSize);
            //return Ok(posts.ToList());
            //"Paging data for page no " + page,
        }

        [HttpPost("Images")]
        public async Task<IActionResult> CreateAsync([FromHeader] PostsDtos strm)
        {

            string filepath = "wwwroot/upload/image.png";
            var bytess = Convert.FromBase64String(strm.postImages);
            using (var imageFile = new FileStream(filepath, FileMode.Create))
            {

                imageFile.Write(bytess, 0, bytess.Length);
                imageFile.Flush();
            }


            Posts files = new Posts();
            {
                files.postTitle = strm.postTitle;
                files.postImages = filepath;
                files.postContent = strm.postContent;
                files.postDate = strm.postDate;
                files.UserId = strm.UserId;
            };

            return Ok(_blogService.Add(files));


        }

    }
}

