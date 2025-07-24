using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace API_Technology_Students_Manages.Models
{
    public class ThongBao
    {
        public string NewsID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public string PostedBy { get; set; }

    }
}