using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using QOTD.Backend.Models;

namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/adminApprove")]
    [ApiController]
    public class adminApproveController : ControllerBase
    { 
         private readonly AppdbContext _context;

        public adminApproveController(AppdbContext context)
        {
            _context = context;
        }

        [HttpGet]
    public IActionResult GetUnansweredQuestions()
    {
        var questions = _context.Questions
            .Where(q => q.IsActive )
            .ToList();


        return Ok(questions);
    }
    
    }
}
