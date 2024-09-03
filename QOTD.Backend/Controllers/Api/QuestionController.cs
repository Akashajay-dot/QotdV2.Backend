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
    [Route("api/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly AppdbContext _context;

        public QuestionController(AppdbContext context)
        {
            _context = context;
        }

        [HttpGet("get-daily-question/{userId}")]
        public async Task<IActionResult> GetDailyQuestion(int userId)
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            await MarkPreviousDayQuestionAsInactive(yesterday);

            var dailyQuestion = await _context.Questions
                .Where(q => EF.Functions.DateDiffDay(q.QuestionDate, today) == 0 && q.IsActive && q.IsApproved)
                .Select(q => new
                {
                    Question = q,
                    AnswerOptions = _context.AnswerOptions.Where(a => a.QuestionId == q.QuestionId).ToList(),
                    AnswerKeys = _context.AnswerKeys.Where(k => k.QuestionId == q.QuestionId).ToList(),
                })
                .FirstOrDefaultAsync();

            if (dailyQuestion == null)
            {
                return NotFound();
            }

            var userResponse = await _context.UserResponses
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.QuestionId == dailyQuestion.Question.QuestionId);

            if (userResponse != null)
            {
                return NotFound();
            }

            return Ok(dailyQuestion);
        }

        private async Task MarkPreviousDayQuestionAsInactive(DateTime date)
        {
            var previousDayQuestion = await _context.Questions
                .Where(q => EF.Functions.DateDiffDay(q.QuestionDate, date) == 0 && q.IsActive)
                .FirstOrDefaultAsync();

            if (previousDayQuestion != null)
            {
                previousDayQuestion.IsActive = false;
                _context.Entry(previousDayQuestion).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
    }
}