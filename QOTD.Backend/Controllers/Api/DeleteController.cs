
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using QOTD.Backend.Models;
using Microsoft.AspNetCore.Authorization;


namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]


    public class DeleteController : ControllerBase
    {
        private readonly AppdbContext _context;

        public DeleteController(AppdbContext context)
        {
            _context = context;
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(q => q.QuestionId == id);
            if (question == null)
            {
                return NotFound(); 
            }
            var userResponses = await _context.UserResponses.Where(ur => ur.QuestionId == id).ToListAsync();
            _context.UserResponses.RemoveRange(userResponses);

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
