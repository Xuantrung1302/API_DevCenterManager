using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Technology_Students_Manages.Models
{
    public class MonHoc
    {
        [Key] 
        public string SubjectID { get; set; }     
        public string SubjectName { get; set; }   
        public string SemesterID { get; set; }      
        public decimal TuitionFee { get; set; }     
        public bool? DeleteFlag { get; set; }
    }
}