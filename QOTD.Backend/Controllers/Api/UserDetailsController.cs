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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByGoogleId(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound(); 
            }

            var reputationName = await _context.ReputationMasters
                .Where(r => user.Points >= r.MinPoints && user.Points <= r.UptoPoints)
                .Select(r => r.Badge)
                .FirstOrDefaultAsync();

            return Ok(new
            {
                User = user,
                ReputationName = reputationName
            });
        }
    }
}
