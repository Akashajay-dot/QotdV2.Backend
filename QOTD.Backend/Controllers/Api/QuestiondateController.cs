using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using QOTD.Backend.Models;

namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/questions/update")]
    [ApiController]
    public class QuestionDateController : ControllerBase
    {
        private readonly AppdbContext _context;

        public QuestionDateController(AppdbContext context)
        {
            _context = context;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestionDate([FromBody] QuestionUpdateRequest request)
        {
            var question = await _context.Questions
                .FindAsync(request.QuestionId);

            if (question == null)
            {
                return NotFound();
            }

            if (request.Date == null)
            {
                question.QuestionDate = null;
                question.IsApproved = false;
            }
            else
            {
                question.QuestionDate = request.Date;
                question.IsApproved = true;
            }

            await _context.SaveChangesAsync(); 

            return Ok(question); 
        }
    }

    public class QuestionUpdateRequest
    {
        public int QuestionId { get; set; }
        public DateTime? Date { get; set; }
    }
}
