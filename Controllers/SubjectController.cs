using API_Students_Manager.Models;
using API_Technology_Students_Manages.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Technology_Students_Manages.Models;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Subject")]
    public class SubjectController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("thongTinMonHoc")]
        public object DanhSachMonHoc(string subjectID = null, string semesterID = null)
        {
            object subject = new List<object>();
            DataTable dt = new DataTable(); 
            SqlParameter[] selectParams = {
                new SqlParameter("@SubjectID", subjectID),
                new SqlParameter("@SemesterID", semesterID)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_SUBJECT", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                subject = dt;
                return subject;
            }
            return subject;
        }

        [HttpPost]
        [Route("themMonHoc")]
        public bool ThemMonHoc([FromBody] MonHoc data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] insertParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_SUBJECT", insertParam);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinMonHoc")]
        public bool DoiThongTinMonHoc([FromBody] MonHoc data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] updateParam = {
                new SqlParameter("@SubjectID", data.SubjectID),
                new SqlParameter("@SemesterID", data.SemesterID),
                new SqlParameter("@SubjectName", data.SubjectName),
                new SqlParameter("@TuitionFee", data.TuitionFee)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_SUBJECT", updateParam);
            return result;
        }
        [HttpPost]
        [Route("xoaMonHoc")]
        public bool XoaMonHoc(string subjectID)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@SubjectID", subjectID)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_SUBJECT", deleteParams);
            return result;
        }
    }
}
