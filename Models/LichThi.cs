using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Technology_Students_Manages.Models
{
    public class LichThi
    {
        [Key]
        public Guid ExamID { get; set; }
        public Guid ClassID { get; set; }
        public string ExamName { get; set; }
        public string ExamType { get; set; }
        public DateTime ExamDateStart { get; set; }
        public DateTime ExamDateEnd { get; set; }
        public string Room { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}