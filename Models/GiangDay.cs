using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class GiangDay
    {
        [Key]
        public string MaGV { get; set; }
        public string MaLop { get; set; }

        // Foreign keys
        public GiangVien GiangVien { get; set; }
        public LopHoc LopHoc { get; set; }
    }

}