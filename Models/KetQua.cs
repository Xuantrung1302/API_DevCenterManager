using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Technology_Students_Manages.Models
{
    public class KetQua
    {
        public Guid ResultID { get; set; }
        public Guid ExamID { get; set; }
        public string StudentID { get; set; }
        public decimal? Score { get; set; }
        public string Status { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? GradingDate { get; set; }
        public bool? DELETE_FLG { get; set; }
    }
}