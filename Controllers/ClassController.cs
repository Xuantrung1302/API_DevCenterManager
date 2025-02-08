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
    [RoutePrefix("api/Class")]
    public class ClassController : ApiController
    {
        DBConnect DBConnect = new DBConnect();
        [HttpGet]
        [Route("layLopTheoID")]
        public object LayLop(string maLop = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@maLop", maLop),
            };
            dt = DBConnect.ExecuteQuery("SP_GET_CLASS_ID", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }
        [HttpGet]
        [Route("layDiemLop")]
        public object LayDiemLop(string maLop = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@maLop", maLop),
            };
            dt = DBConnect.ExecuteQuery("SP_GET_CLASS_SCORE_ID", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }
    }
}
