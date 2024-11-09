using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Students_Manager.Models
{
    public class ChiTietTrungTam
    {
        [Key]
        public string TenTT { get; set; }
        public string DiaChiTT { get; set; }
        public string SdtTT { get; set; }
        public string Website { get; set; }
        public string EmailTT { get; set; }
    }

}