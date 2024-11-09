using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class GiangVien
    {
        public string MaGV { get; set; }
        public string TenGV { get; set; }
        public string GioiTinhGV { get; set; }
        public string SdtGV { get; set; }
        public string EmailGV { get; set; }

        // Foreign key
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
    }

}