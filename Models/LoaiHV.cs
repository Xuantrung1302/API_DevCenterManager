using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class LoaiHV
    {
        [Key]
        public string MaLoaiHV { get; set; }
        public string TenLoaiHV { get; set; }

        public ICollection<HocVien> HocViens { get; set; }
    }

}