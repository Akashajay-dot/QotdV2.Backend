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
    public class PreviousController : ControllerBase
    {
        private readonly AppdbContext _context;

        public PreviousController(AppdbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUnansweredQuestions(int userId)
        {
            var answeredQuestionIds = await _context.UserResponses
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.QuestionId)
                .ToListAsync();

            var today = DateTime.Today;

            // Select questions with dates up to and including today
            var activeApprovedQuestionIds = await _context.Questions
                .Where(q => EF.Functions.DateDiffDay(q.QuestionDate, today) >= 0)
                .Select(q => q.QuestionId)
                .ToListAsync();

            var unansweredQuestionIds = activeApprovedQuestionIds
               .Except(answeredQuestionIds)
               .ToList();

            return Ok(unansweredQuestionIds);
        }
    }
}