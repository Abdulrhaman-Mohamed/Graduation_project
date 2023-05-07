using Repo_Core.Identity_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;

using Repo_Core.Services;
using Microsoft.AspNetCore.Authorization;
using Graduation_project.ViewModel;
using Repo_EF;
using System.IO;
using AutoMapper;

namespace Graduation_project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    // Note : (There are class name posts in identity in models)

    public class BlogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ApplicationDbContext dbContext ;
        public BlogController(IBlogService blogService, IWebHostEnvironment webHostEnvironment , ApplicationDbContext _dbContext,IMapper mapper)
        {
            _blogService = blogService;
            _webHostEnvironment = webHostEnvironment;
            dbContext = _dbContext;
            _mapper = mapper;
        }

        [HttpGet("All-blogs")]
        public IActionResult GetPosts(int page, byte pageSize)
        {
            var posts = _blogService.GetPosts(page, pageSize);
            if (posts == null)
                return NoContent();
            else
             return Ok(posts.ToList());
                      
        }

        [HttpPost("Images")]
        public async Task<IActionResult> CreateAsync([FromForm] PostsDtos strm )
        {
            if(!ModelState.IsValid)
                return BadRequest("There are something lost");

            Posts files = new Posts();
            {
                files.postTitle = strm.postTitle;
                files.postContent = strm.postContent;
                files.postDate = strm.postDate;
                files.UserId = strm.UserId;
            };
            var id = await _blogService.Add(files);

            if(strm.formFile != null )
                await _blogService.SaveImages(strm.formFile, id);

            return Ok();
        }

        //Feedback : Can user add feedback or comment in blogs
        [HttpPost("Feedback")]
        public async Task<IActionResult> Addfeedback([FromBody] FeedbackView feedback)
        {
            var feedback1 = _mapper.Map<Feedback>(feedback);
            var map =await _blogService.Addfeedback(feedback1);
            return Ok(map);
            
        }

        //delete Posts
        [HttpGet("Delete Post")]
        public IActionResult DeletePosts(int postId)
        {
             
            return Ok(_blogService.DeletePosts(postId));
        }


    }
}

