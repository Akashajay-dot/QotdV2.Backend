using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using QOTD.Backend.Models;

namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly AppdbContext _context;

        public UserDetailsController(AppdbContext context)
        {
            _context = context;
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByGoogleId(int id)
        {
            // Fetch the user from the database using the Google User ID
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound(); // Returns a 404 if the user is not found
            }

            var reputationName = await _context.ReputationMasters
                .Where(r => user.Points >= r.MinPoints && user.Points <= r.UptoPoints)
                .Select(r => r.Badge)
                .FirstOrDefaultAsync();

            return Ok(new
            {
                User = user, // Include all user fields
                ReputationName = reputationName // Include reputation name in the response
            });
        }
    }
}
