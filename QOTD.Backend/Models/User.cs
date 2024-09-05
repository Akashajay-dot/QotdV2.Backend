using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QOTD.Backend.Models
{
    [Table("User")]

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }    
        public string GoogleId { get; set; }
        public string Pic { get; set; }

        public DateTime CreatedON { get; set;}
        public DateTime UpdatedON { get; set;}
        public int Points { get; set;}
        public bool IsAdmin {  get; set;}   

        

    }
}