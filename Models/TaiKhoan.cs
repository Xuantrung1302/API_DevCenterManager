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
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        //public ICollection<HocVien> HocViens { get; set; }
        //public ICollection<NhanVien> NhanViens { get; set; }
        //public ICollection<GiangVien> GiangViens { get; set; }
    }

}