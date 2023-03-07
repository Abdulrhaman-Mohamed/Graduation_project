using Identity;
using Identity.Migrations;
using Identity.Model;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Feedback = Identity.Model.Feedback;
using Graduation_project.ViewModel;
using Repo_Core.Models;
using AutoMapper;

namespace Graduation_project.Controllers
{
    public class BlogController : Controller
    {
        
        private readonly IEditting _editting;
        private readonly UserDbcontext _dbcontext;
        private readonly IMapper _mapper;
        public BlogController(IEditting editting, UserDbcontext dbcontext, IMapper mapper)
        {
            _editting = editting;
            _dbcontext = dbcontext;
            _mapper = mapper;
           
        }


        //Edit Setting of User (Not Fixed) 
        [HttpPut("Editting")]
        public async Task  <IActionResult> Editting([FromBody] Editting info)
        {
           var map= _mapper.Map<ApplicationUser>(info);
           await _editting.Editting(map);

            return Ok();
        }
        //Feedback : Can user add feedback or comment in blogs
        [HttpPost("Feedback")]
        public IActionResult Addfeedback([FromBody] FeedbackView feedback)
        {
            var feedback1 = _mapper.Map<Feedback>(feedback);
            return Ok(_editting.Addfeedback(feedback1));
        }






            // Note : (There are class name posts in identity in models)


            //blogs : return groups of blogs with pagination



            //Create Blog  : user can add posts / blog of some content



            





        }
}
