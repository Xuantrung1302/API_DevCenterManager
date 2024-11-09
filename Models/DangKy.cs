using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class DangKy
    {
        [Key]
        public string MaHV { get; set; }
        public string MaKH { get; set; }
        public string MaPhieu { get; set; }

        // Foreign keys
        public HocVien HocVien { get; set; }
        public KhoaHoc KhoaHoc { get; set; }
        public PhieuGhiDanh PhieuGhiDanh { get; set; }
    }

}