using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QOTD.Backend.Models
{
    public class AnswerKey
    {
        [Key]
        public int  AnswerKeyId {  get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questions Questions { get; set; }
        public int AnswerOptionId { get; set; }
        [ForeignKey("AnswerOptionId")]
        public virtual AnswerOption AnswerOption { get; set; }

    }
}