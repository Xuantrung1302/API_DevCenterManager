using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Technology_Students_Manages.Models
{
    public class Course
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public bool IsActive { get; set; }
        public Semester Semester1 { get; set; }
        public Semester Semester2 { get; set; }
    }

    public class Semester
    {
        public string SemesterID { get; set; }
        public string SemesterName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Subject> Subjects { get; set; }
    }

    public class Subject
    {
        public string SubjectID { get; set; }
        public string SubjectName { get; set; }
        public decimal TuitionFee { get; set; }
        public string SemesterID { get; set; }
    }
}