using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using QOTD.Backend.Models;

namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountController : ControllerBase
    {

        private readonly AppdbContext _context;
        public CountController(AppdbContext context)
        {
            _context = context;
        }


        [HttpGet("{userId}")]
       
        public async Task<ActionResult> GetQuestioncount(int userId)
        {
            var answeredQuestionIds = await _context.UserResponses
              .Where(ur => ur.UserId == userId)
              .Select(ur => ur.QuestionId)
              .ToListAsync();

            
            var today = DateTime.Today;
            var startOfToday = today; 
            var ApprovedQuestionIds = await _context.Questions
                .Where(q => EF.Functions.DateDiffDay(q.QuestionDate, startOfToday) >= 0)
                .Select(q => q.QuestionId)
                .ToListAsync();


            return Ok(new
            {
                ApprovedQuestionIds,
                answeredQuestionIds
            });
        }

    }
}
