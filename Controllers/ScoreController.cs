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
    [RoutePrefix("api/Score")]
    public class ScoreController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("thongTinDiem")]
        public object LayDiem(string classID = null)
        {
            object scores = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@ClassID", classID)
            };
            dt = DBConnect.ExecuteQuery("SP_GET_CLASS_SCORE_ID", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                scores = dt;
                return scores;
            }
            return scores;
        }


        [HttpGet]
        [Route("diemHocSinhTheoLop")]
        public object LayDiem(string ClassID = null, string StudentID = null)
        {
            object scores = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@ClassID", ClassID),
                new SqlParameter("@StudentID", StudentID)
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_SCORE_OF_STUDENT_BY_CLASS", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                scores = dt;
                return scores;
            }
            return scores;
        }
        [HttpGet]
        [Route("diemTheoHocVien")]
        public object DiemTheoHocVien(string StudentID = null)
        {
            object scores = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@StudentID", StudentID)
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_SCORE_OF_STUDENT", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                scores = dt;
                return scores;
            }
            return scores;
        }
    }
}