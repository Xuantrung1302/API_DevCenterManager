using API_Technology_Students_Manages.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Notice")]
    public class NoticeController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("danhSachThongBao")]
        public object DanhSachThongBao()
        {
            object notices = new List<object>();
            DataTable dt = new DataTable();
            dt = DBConnect.ExecuteQuery("SP_SELECT_NOTICE");

            if (dt?.Rows?.Count > 0)
            {
                notices = dt;
                return notices;
            }
            return notices;
        }
    }
}