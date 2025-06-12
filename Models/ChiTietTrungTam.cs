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
        public string CenterName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
    }

}