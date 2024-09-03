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
    [Authorize]
    [Route("api/questions/dates")]
    [ApiController]
    public class DatesetController : ControllerBase
    {
        private readonly AppdbContext _context;

        public DatesetController(AppdbContext context)
        {
            _context = context;
        }

        [HttpGet]
      
        public async Task<ActionResult> GetQuestionDates()
        {
            try
            {
                var questionDates = _context.Questions
                    .Select(q => q.QuestionDate)
                    .Distinct()
                    .OrderBy(date => date)
                    .ToList();

                return Ok(questionDates);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
