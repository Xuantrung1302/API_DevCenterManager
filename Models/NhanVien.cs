using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class NhanVien
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string SdtNV { get; set; }
        public string EmailNV { get; set; }

        // Foreign keys
        public string MaLoaiNV { get; set; }
        public LoaiNV LoaiNV { get; set; }

        public string TenDangNhap { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau {  get; set; }
    }

}