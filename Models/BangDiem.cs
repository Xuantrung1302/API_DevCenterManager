using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class BangDiem
    {
        [Key]
        public string MaHV { get; set; }
        public string MaLop { get; set; }
        public string MaPhieu { get; set; }
        public int DiemNghe { get; set; }
        public int DiemNoi { get; set; }
        public int DiemDoc { get; set; }
        public int DiemViet { get; set; }

        // Foreign keys
        public HocVien HocVien { get; set; }
        public LopHoc LopHoc { get; set; }
        public PhieuGhiDanh PhieuGhiDanh { get; set; }
    }

}