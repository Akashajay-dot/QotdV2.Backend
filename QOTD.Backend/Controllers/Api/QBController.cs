using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using QOTD.Backend.Models;

namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/questions")]
    [ApiController]
    public class QBController : ControllerBase
    {
        private readonly AppdbContext _context;

        public QBController(AppdbContext context)
        {
            _context = context;
        }

        [HttpGet("active/{filter}/{catid}/{searchtxt}")]
        public async Task<IActionResult> GetActiveQuestionIds(string filter, string catid, string searchtxt)
        {
            int? catId = null;
            var today = DateTime.Today;
            var startOfToday = today;

            if (searchtxt != "null")
            {
                var activeQuestionIds = await _context.Questions
                    .Where(q => q.Question.Contains(searchtxt))
                    .Select(q => q.QuestionId)
                    .ToListAsync();

                return Ok(activeQuestionIds);
            }
            else
            {
                if (!string.IsNullOrEmpty(catid) && catid != "null")
                {
                    catId = Convert.ToInt32(catid);
                }

                if (filter == "all")
                {
                    if (!catId.HasValue)
                    {
                        var activeQuestionIds = await _context.Questions
                            .Select(q => q.QuestionId)
                            .ToListAsync();

                        return Ok(activeQuestionIds);
                    }
                    else
                    {
                        var activeQuestionIds = await _context.Questions
                            .Where(q => q.CategoryId == catId)
                            .Select(q => q.QuestionId)
                            .ToListAsync();

                        return Ok(activeQuestionIds);
                    }
                }
                else if (filter == "Published")
                {
                    if (!catId.HasValue)
                    {
                        var activeQuestionIds = await _context.Questions
                            .Where(q => q.IsApproved && q.QuestionDate >= startOfToday)
                            .Select(q => q.QuestionId)
                            .ToListAsync();

                        return Ok(activeQuestionIds);
                    }
                    else
                    {
                        var activeQuestionIds = await _context.Questions
                            .Where(q => q.CategoryId == catId && q.IsApproved && q.QuestionDate >= startOfToday)
                            .Select(q => q.QuestionId)
                            .ToListAsync();

                        return Ok(activeQuestionIds);
                    }
                }
                else if (filter == "unPublished")
                {
                    if (!catId.HasValue)
                    {
                        var activeQuestionIds = await _context.Questions
                            .Where(q => !q.IsApproved)
                            .Select(q => q.QuestionId)
                            .ToListAsync();

                        return Ok(activeQuestionIds);
                    }
                    else
                    {
                        var activeQuestionIds = await _context.Questions
                            .Where(q => q.CategoryId == catId && !q.IsApproved)
                            .Select(q => q.QuestionId)
                            .ToListAsync();

                        return Ok(activeQuestionIds);
                    }
                }
                else if (filter == "previous")
                {
                    if (!catId.HasValue)
                    {
                        var activeQuestionIds = await _context.Questions
                            .Where(q => q.QuestionDate < startOfToday  && q.IsApproved)
                            .Select(q => q.QuestionId)
                            .ToListAsync();

                        return Ok(activeQuestionIds);
                    }
                    else
                    {
                        var activeQuestionIds = await _context.Questions
                            .Where(q => q.CategoryId == catId && q.QuestionDate < startOfToday && q.IsApproved)
                            .Select(q => q.QuestionId)
                            .ToListAsync();

                        return Ok(activeQuestionIds);
                    }
                }

                return Ok("ok");
            }
        }
    }
}