using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Technology_Students_Manages.Models
{
    public class DiemDanh
    {
        [Key]
        public Guid AttendanceID { get; set; }
        public Guid Class_ScheID { get; set; }
        public string StudentID { get; set; }
        public int Status { get; set; }
        public DateTime RecordedTime { get; set; }
        public string RecordedBy { get; set; }
        public string Notes { get; set; }
        public bool DELETE_FLG { get; set; }
    }
}