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

        [HttpGet("orderedbyPoints")]
        public async Task<IActionResult> GetUsersOrderedByPoints()
        {
           
            var users = await _context.Users
              .OrderByDescending(u => u.Points) 
              .Select(u => new
              {
                  u.UserId,
                  u.Name
              })  
              .ToListAsync();

            return Ok(users);
        }
    }
}