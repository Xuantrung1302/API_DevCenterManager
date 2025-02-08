using API_Technology_Students_Manages.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Score")]
    public class ScoreController : ApiController
    {
        DBConnect DBConnect = new DBConnect();
        [HttpGet]
        [Route("thongTinDiem")]
        public object LayDiem(string maHV = null)
        {
            object diem = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@maHv", maHV),
            };
            dt = DBConnect.ExecuteQuery("SP_SHOW_INFOR_OF_STUDENTS", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                diem = dt;
                return diem;
            }
            return diem;
        }
    }
}
