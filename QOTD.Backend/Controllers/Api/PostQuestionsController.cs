using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QOTD.Backend.Models;

namespace QOTD.Backend.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostQuestionsController : ControllerBase
    {
        private readonly AppdbContext _context;

        public PostQuestionsController(AppdbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestions(QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == questionDto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid CategoryId");
            }

            var author = await _context.Users.SingleOrDefaultAsync(u => u.UserId == questionDto.AuthorId);
            if (author == null)
            {
                return BadRequest("Invalid AuthorId");
            }

            var question = new Questions
            {
                Question = questionDto.Question,
                CategoryId = questionDto.CategoryId,
                AuthorId = questionDto.AuthorId,
                Point = questionDto.Point,
                HasMultipleAnswers = questionDto.HasMultipleAnswers,
                CreatedBy = questionDto.CreatedBy,
                UpdatedBy = questionDto.UpdatedBy,
                IsApproved = questionDto.IsApproved,
                SnapShot = questionDto.SnapShot,
                LastUpdatedOn = questionDto.LastUpdatedOn,
                LastUpdatedBy = questionDto.LastUpdatedBy,
                IsActive = true,
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            foreach (var option in questionDto.Answers)
            {
                var answerOption = new AnswerOption
                {
                    QuestionId = question.QuestionId,
                    Option = option.Text
                };

                _context.AnswerOptions.Add(answerOption);
                await _context.SaveChangesAsync();

                if (option.IsCorrect)
                {
                    var answerKey = new AnswerKey
                    {
                        QuestionId = question.QuestionId,
                        AnswerOptionId = answerOption.AnswerOptionId
                    };

                    _context.AnswerKeys.Add(answerKey);
                    await _context.SaveChangesAsync();
                }
            }

            return Ok(question);
        }
    }

    public class QuestionDto
    {
        public string Question { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int Point { get; set; }
        public bool HasMultipleAnswers { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsApproved { get; set; }
        public string SnapShot { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

    public class AnswerDto
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}