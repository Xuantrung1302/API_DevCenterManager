using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class TaiKhoan
    {
        [Key]
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

        public ICollection<HocVien> HocViens { get; set; }
        public ICollection<NhanVien> NhanViens { get; set; }
        public ICollection<GiangVien> GiangViens { get; set; }
    }

}