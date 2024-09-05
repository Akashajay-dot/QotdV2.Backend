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
    public class CheckController : ControllerBase
    {
        private readonly AppdbContext _context;

        public CheckController(AppdbContext context)
        {
            _context = context;
        }

        [HttpGet("Questions/IsCorrect/{userid}/{qid}")]
        public async Task<ActionResult> GetIsCorrect(int userid ,int qid)
        {
            var isCorrect = await _context.UserResponses
                .Where(ur => ur.QuestionId == qid && ur.UserId == userid)
                .Select(ur => ur.IsCorrect)
                .FirstOrDefaultAsync();

           

            return Ok(new { IsCorrect = isCorrect });
        }
    }
}
