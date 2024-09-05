using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QOTD.Backend.Models
{
    [Table("UserResponse")]

    public class UserResponse
    {
        [Key]
        public int UserResponseId { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questions  Questions { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int AnswerOptionId { get; set; }
        [ForeignKey("AnswerOptionId")]
        public virtual AnswerOption AnswerOption { get; set; }
        public bool IsCorrect {  get; set; }

    }
}