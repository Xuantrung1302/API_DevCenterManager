using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class LopHoc
    {
        [Key]
        public Guid ClassID { get; set; }

        public string SubjectID { get; set; }

        public string ClassName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Room { get; set; }

        public string TeacherID { get; set; }

        public int MaxSeats { get; set; }

        public string DaysOfWeek { get; set; }

    }
}