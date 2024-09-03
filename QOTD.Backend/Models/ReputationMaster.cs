using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QOTD.Backend.Models
{
    public class ReputationMaster
    {
        [Key]
        public int ReputationMasterId { get; set; }
        public int MinPoints { get; set; }



        public int UptoPoints { get; set;}

        public string Badge  { get; set; }

    }
}