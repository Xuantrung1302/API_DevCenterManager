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
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }
        public int SiSo { get; set; }
        public bool DangMo { get; set; }

        // Foreign key
        public string MaKH { get; set; }
        public KhoaHoc KhoaHoc { get; set; }
    }

}