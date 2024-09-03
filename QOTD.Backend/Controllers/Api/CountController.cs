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
            var answeredQuestionIds = await _context.UserResponse
              .Where(ur => ur.UserId == userId)
              .Select(ur => ur.QuestionId)
              .ToListAsync();

            // var unansweredQuestions = _context.Questions
            //   .Where(q => !answeredQuestionIds.Contains(q.QuestionId) && q.IsActive == true && q.IsApproved == true)
            //   .ToList();
            var today = DateTime.Today;
            var startOfToday = today; // Start of today, i.e., 00:00:00

            // Select questions with dates up to and including today
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
