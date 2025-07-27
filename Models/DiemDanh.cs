using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Technology_Students_Manages.Models
{
    public class DiemDanh
    {
        public Guid ClassScheduleID { get; set; }
        public string RecordedBy { get; set; }
        public List<ChiTietDiemDanh> ChiTiet { get; set; }
    }

    public class ChiTietDiemDanh
    {
        public string StudentID { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }
    }

}