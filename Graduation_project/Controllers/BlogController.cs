using Graduation_project.ViewModel;
using Identity.Model;
using Identity.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Repo_Core;
using System.ComponentModel;
using System.Net;
using System.Security.Policy;
using System.IO;

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

