using API_Students_Manager.Models;
using API_Technology_Students_Manages.DataAccess;
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
    [RoutePrefix("api/Semester")]
    public class SemesterController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        // API Semester
        [HttpGet]
        [Route("thongTinKyHoc")]
        public object DanhSachKyHoc(string semesterID = null, string semesterName = null)
        {
            object semesters = new List<object>();
            DataTable dt = new DataTable();

            // Tạo danh sách tham số với xử lý nullable
            SqlParameter[] selectParams = {
                new SqlParameter("@SemesterID", (object)semesterID ?? DBNull.Value),
                new SqlParameter("@SemesterName", (object)semesterName ?? DBNull.Value)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_SEMESTER", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                semesters = dt;
                return semesters;
            }
            return semesters;
        }

        [HttpPost]
        [Route("themThongTinKyHoc")]
        public bool ThemThongTinKyHoc([FromBody] KyHoc data)
        {
            bool result = false;

            SqlParameter[] insertParams = {
                new SqlParameter("@SemesterID", data.SemesterID),
                new SqlParameter("@SemesterName", data.SemesterName),
                new SqlParameter("@StartDate", data.StartDate),
                new SqlParameter("@EndDate", data.EndDate)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_SEMESTER", insertParams);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinKyHoc")]
        public bool SuaThongTinKyHoc([FromBody] KyHoc data)
        {
            bool result = false;

            SqlParameter[] updateParams = {
                new SqlParameter("@SemesterID", data.SemesterID),
                new SqlParameter("@SemesterName", (object)data.SemesterName ?? DBNull.Value),
                new SqlParameter("@StartDate", (object)data.StartDate ?? DBNull.Value),
                new SqlParameter("@EndDate", (object)data.EndDate ?? DBNull.Value)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_SEMESTER", updateParams);
            return result;
        }

        [HttpPost]
        [Route("xoaThongTinKyHoc")]
        public bool XoaThongTinKyHoc(string semesterID)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@SemesterID", semesterID)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_SEMESTER", deleteParams);
            return result;
        }

        [HttpGet]
        [Route("maKyHoc")]
        public object DanhSachMaKyHoc()
        { 
            object semesters = new List<object>();
            DataTable dt = new DataTable();

            dt = DBConnect.ExecuteQuery("SP_SELECT_SEMESTER_ID");

            if (dt?.Rows?.Count > 0)
            {
                semesters = dt;
                return semesters;
            }
            return semesters;
        }
    }
}