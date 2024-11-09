using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class HocVien
    {
        [Key]
        public string MaHV { get; set; }
        public string TenHV { get; set; }
        public string GioiTinhHV { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SdtHV { get; set; }
        public string EmailHV { get; set; }
        public DateTime NgayTiepNhan { get; set; }

        // Foreign keys
        public string MaLoaiHV { get; set; }
        public LoaiHV LoaiHV { get; set; }

        public string TenDangNhap { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
        public string MatKhau {  get; set; }
    }

}