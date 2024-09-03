using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using QOTD.Backend.Models;

namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppdbContext _context;

        public UserController(AppdbContext context)
        {
            _context = context;
        }

        // GET: api/users/orderedbyPoints
        [HttpGet("orderedbyPoints")]
        public async Task<IActionResult> GetUsersOrderedByPoints()
        {
            //var userIds = await _context.Users
            //  .OrderByDescending(u => u.Points) // Order users by points in descending order
            // .Select(u => u.UserId)  // Select only the user IDs
            // .ToListAsync();

            //return Ok(userIds); // Return the list of user IDs
            var users = await _context.Users
              .OrderByDescending(u => u.Points) // Order users by points in descending order
              .Select(u => new
              {
                  u.UserId,
                  u.Name
              })  // Select both UserId and Name
              .ToListAsync();

            return Ok(users);
        }
    }
}