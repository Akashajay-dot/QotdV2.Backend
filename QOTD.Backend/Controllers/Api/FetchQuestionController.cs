using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using QOTD.Backend.Models;


namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FetchQuestionController : ControllerBase
    {
        private readonly AppdbContext _context;

        public FetchQuestionController(AppdbContext context)
        {
            _context = context;
        }


        [HttpGet("{Qid}")]
        public async Task<IActionResult> GetUnansweredQuestions(int Qid)
        {
            var question = await _context.Questions
                .Where(q => q.QuestionId == Qid)
                .Select(q => new
                {
                    Question = q,
                    AnswerOptions = _context.AnswerOptions.Where(a => a.QuestionId == q.QuestionId).ToList(),
                    AnswerKeys = _context.AnswerKeys.Where(k => k.QuestionId == q.QuestionId).ToList(),
                    Category = _context.Categories.Where(k => k.CategoryId == q.CategoryId).ToList(),

                })
                .FirstOrDefaultAsync();

            if (question == null)
            {
                return NotFound();
            }



            return Ok(question);
        }
    }
}
