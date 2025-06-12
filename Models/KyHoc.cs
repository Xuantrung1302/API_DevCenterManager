using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Technology_Students_Manages.Models
{
    public class KyHoc
    {
        public string SemesterID { get; set; }
        public string SemesterName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}