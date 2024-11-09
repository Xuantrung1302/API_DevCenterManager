using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class LoaiNV
    {
        [Key]
        public string MaLoaiNV { get; set; }
        public string TenLoaiNV { get; set; }

        public ICollection<NhanVien> NhanViens { get; set; }
    }

}