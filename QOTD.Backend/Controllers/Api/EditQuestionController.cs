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
    public class EditQuestionController : ControllerBase
    {

        private readonly AppdbContext _context;

        public EditQuestionController(AppdbContext context)
        {
            _context = context;
        }


        [HttpPut("{QId}")]
        public async Task<IActionResult> EditQuestion(int QId, [FromBody] QuestionDto2 questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var optionsToRemove = _context.AnswerOptions.Where(ao => ao.QuestionId == QId);
            _context.AnswerOptions.RemoveRange(optionsToRemove);
            await _context.SaveChangesAsync();



            var questionInDb = _context.Questions.SingleOrDefault(q => q.QuestionId == QId);
            if (questionInDb == null)
            {
                return NotFound();
            }

            questionInDb.Question = questionDto.Question;
            questionInDb.CategoryId = questionDto.CategoryId;
            questionInDb.AuthorId = questionDto.AuthorId;

            questionInDb.Point = questionDto.Point;
            questionInDb.HasMultipleAnswers = questionDto.HasMultipleAnswers;
            questionInDb.UpdatedBy = questionDto.UpdatedBy;
            questionInDb.LastUpdatedOn = questionDto.LastUpdatedOn;
            questionInDb.LastUpdatedBy = questionDto.LastUpdatedBy;
            questionInDb.IsActive = true;
            await _context.SaveChangesAsync();

            foreach (var option in questionDto.Answers)
            {
                var answerOption = new AnswerOption
                {
                    QuestionId = QId,
                    Option = option.Text
                };

                _context.AnswerOptions.Add(answerOption);
                await _context.SaveChangesAsync();

                if (option.IsCorrect)
                {
                    var answerKey = new AnswerKey
                    {
                        QuestionId = QId,
                        AnswerOptionId = answerOption.AnswerOptionId
                    };

                    _context.AnswerKeys.Add(answerKey);
                    await _context.SaveChangesAsync();
                }
            }

            return Ok("ok");
        }
    }
    public class QuestionDto2
    {
        public string Question { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

        public int Point { get; set; }
        public bool HasMultipleAnswers { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public List<AnswerDto2> Answers { get; set; }
    }

    public class AnswerDto2
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
