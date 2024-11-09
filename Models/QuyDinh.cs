using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class QuyDinh
    {
        [Key]
        public string MaQD { get; set; }
        public string TenQD { get; set; }
        public int GiaTri { get; set; }
    }

}