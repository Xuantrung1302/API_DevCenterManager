using API_Technology_Students_Manages.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
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
            object dstb = new List<object>();
            DataTable dt = new DataTable();
            //SqlParameter[] searchParams = {
            //        new SqlParameter("@TenDangNhap",tenDangNhap),
            //        new SqlParameter("@MatKhau",matKhau)
            //};

            dt = DBConnect.ExecuteQuery("SP_SELECT_NOTICE");

            if (dt?.Rows?.Count > 0)
            {
                dstb = dt;
                return dstb;
            }
            return dstb;
        }
    }
}
