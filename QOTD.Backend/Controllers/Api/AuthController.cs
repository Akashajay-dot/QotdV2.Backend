
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using QOTD.Backend.Models;



namespace QOTD.Backend.Controllers.Api
{
   
    [Route("api/auth/validate-token")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly AppdbContext _context;

        public AuthController(AppdbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> ValidateToken([FromBody] TokenDto tokenDto)
        {

            if (string.IsNullOrWhiteSpace(tokenDto.credential))
            {
                
                return BadRequest(new { message = "Credential must not be empty" });
            }   
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(tokenDto.credential);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.GoogleId == payload.Subject);

                if (user == null)
                {
                    user = new User
                    {
                        GoogleId = payload.Subject,
                        Name = payload.Name,
                        Pic = payload.Picture,
                        CreatedON = DateTime.UtcNow,
                        UpdatedON = DateTime.UtcNow,
                        

                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    user.UpdatedON = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                   
                   
                }
              


                var orderedUsers = await _context.Users
                    .OrderByDescending(u => u.Points)
                    .ThenBy(u => u.UserId) 
                    .ToListAsync();

                var userRank = orderedUsers
                    .Select((u, index) => new { u.UserId, Rank = index + 1 })
                    .FirstOrDefault(u => u.UserId == user.UserId)?.Rank ?? 0;

                var totalUsers = orderedUsers.Count;

                return base.Ok(new
                {
                   isvalid = true,
                   Payload = payload,
                   UserId = user.UserId,
                   isAdmin = user.IsAdmin,
                   category = _context.Categories,
                    userRank= userRank,
                    totalUsers= totalUsers,

                });
            }
            catch (InvalidJwtException e)
            {
                return Unauthorized(new
                {
                    message = "Invalid token",
                    error = e.Message
                });
            }
            catch (DbUpdateException ex)
            {
                var detailedErrorMessage = ex.InnerException?.InnerException?.Message ?? ex.Message;

                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    message = "An unexpected error occurred while updating the database.",
                    error = detailedErrorMessage
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    message = "An unexpected error occurred.",
                    error = ex.Message,
                    err = tokenDto.credential
                });
            }
        }

       
    }
    public class TokenDto
    {
        public string  credential{ get; set; }
    }

}
