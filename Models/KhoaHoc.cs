using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class KhoaHoc
    {
        [Key]
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string SemesterName { get; set; }
        public string SubjectName { get; set; }
        public decimal Fee { get; set; }
    }

}