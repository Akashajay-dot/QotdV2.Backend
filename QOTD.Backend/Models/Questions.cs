using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QOTD.Backend.Models
{
    public class Questions
    {
        [Key]
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User User { get; set; }
        
        public int Point {  get; set; }
        public bool HasMultipleAnswers { get; set; }  
        public string CreatedBy {  get; set; }
        public string UpdatedBy { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? QuestionDate { get; set; }
        public string SnapShot { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }




    }
}