
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
            // Find the question by its ID
            var question = await _context.Questions.SingleOrDefaultAsync(q => q.QuestionId == id);
            if (question == null)
            {
                return NotFound(); // Return 404 if the question doesn't exist
            }
            var userResponses = await _context.UserResponse.Where(ur => ur.QuestionId == id).ToListAsync();
            _context.UserResponse.RemoveRange(userResponses);

            // Remove the question from the database
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();

            // Return 204 No Content after successful deletion
        }
    }
}
