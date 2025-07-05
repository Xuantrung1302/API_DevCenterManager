using API_Technology_Students_Manages.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Students_Manager.Models;
using Newtonsoft.Json;
using API_Technology_Students_Manages.Models;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Exam")]
    public class ExamController : ApiController
    {
        DBConnect DBConnect = new DBConnect();
        [HttpGet]
        [Route("layDanhSachLichThi")]
        public object LayDanhSachLichThi(string semesterID = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@SemesterID", semesterID),
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_EXAM_SCHEDULE");

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }
        [HttpPost]
        [Route("themLichThi")]
        public bool ThemLichThi([FromBody] LichThi data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] insertParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_EXAM_SCHEDULE", insertParam);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinLichThi")]
        public bool DoiThongTinLichThi([FromBody] LichThi data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] updateParam = {
                new SqlParameter("@ExamID", data.ExamID),
                new SqlParameter("@ExamName", data.ExamName),
                new SqlParameter("@ExamType", data.ExamType),
                new SqlParameter("@ExamDateStart", data.ExamDateStart),
                new SqlParameter("@ExamDateEnd", data.ExamDateEnd),
                new SqlParameter("@Room", data.Room)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_EXAM_SCHEDULE", updateParam);
            return result;
        }
        [HttpPost]
        [Route("xoaLichThi")]
        public bool XoaLichThi(string examID)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@ExamID", examID)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_EXAM_SCHEDULE", deleteParams);
            return result;
        }
    }
}
