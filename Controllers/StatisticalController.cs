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
    [RoutePrefix("api/Statistical")]
    public class StatisticalController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("thongKeSoHocSinhDangKyHocTheoThangTrongNam")]
        public object SoHocSinhDangKy(string year = null)
        {
            object subject = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@Year", year)
            };

            dt = DBConnect.ExecuteQuery("SP_COUNT_ENROLLMENT_BY_MONTH_YEAR", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                subject = dt;
                return subject;
            }
            return subject;
        }

        [HttpGet]
        [Route("thongKeTopNamKhoaHoc")]
        public object TopKhoaHoc(string year = null)
        {
            object subject = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@Year", year)
            };

            dt = DBConnect.ExecuteQuery("SP_TOP_5_COURSE_BY_ENROLLMENT", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                subject = dt;
                return subject;
            }
            return subject;
        }

        [HttpGet]
        [Route("thongKeTyLeTotNghiep")]
        public object TyLeTotNghiep(string year = null)
        {
            object subject = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@Year", year)
            };

            dt = DBConnect.ExecuteQuery("SP_GET_GRADUATION_RATE_BY_YEAR", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                subject = dt;
                return subject;
            }
            return subject;
        }


    }
}
