using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using QOTD.Backend.Models;

namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostResponseController : ControllerBase
    {
        private readonly AppdbContext _context;

        public PostResponseController(AppdbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostResponseAsync(ResponseDto responseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == responseDto.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (responseDto.IsCorrect)
            {
                user.Points += responseDto.Points;
            }

            foreach (var answerOptionId in responseDto.AnswerOptionId)
            {
                var userResponse = new UserResponse
                {
                    QuestionId = responseDto.QuestionId,
                    UserId = responseDto.UserId,
                    AnswerOptionId = answerOptionId,
                    IsCorrect = responseDto.IsCorrect,
                };

                _context.UserResponse.Add(userResponse);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    public class ResponseDto
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int[] AnswerOptionId { get; set; }
        public bool IsCorrect { get; set; }
        public int Points { get; set; }
    }
}