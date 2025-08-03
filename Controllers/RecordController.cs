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
    [RoutePrefix("api/Record")]
    public class RecordController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("baoCaodanhSachDiemDanh")]
        public object DanhSachDiemDanh(string courseID = null, string classID = null, string subjectID = null)
        {
            object record = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@SubjectID", subjectID),
                new SqlParameter("@CourseID", courseID),
                new SqlParameter("@ClassID", classID)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_ATTENDANCE_RECORD", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                record = dt;
                return record;
            }
            return record;
        }

        [HttpGet]
        [Route("danhSachDiemDanh")]
        public object DanhSachDiemDanh(string scheduleID)
        {
            object record = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@ScheduleID", scheduleID)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_STUDENT_BY_SHCHEDULE", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                record = dt;
                return record;
            }
            return record;
        }

        [HttpPost]
        [Route("themThongTinDiemDanh")]
        public bool ThemThongTinDiemDanh([FromBody] DiemDanh data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] insertParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_ATTENDANCE_RECORD", insertParam);
            return result;
        }

        //[HttpPost]
        //[Route("suaThongTinDiemDanh")]
        //public bool DoiThongTinDiemDanh([FromBody] DiemDanh data)
        //{
        //    bool result = false;
        //    string json = JsonConvert.SerializeObject(data);

        //    SqlParameter[] updateParam = {
        //        new SqlParameter("@AttendanceID", data.AttendanceID),
        //        new SqlParameter("@Status", data.Status),
        //        new SqlParameter("@RecordedBy", data.RecordedBy),
        //        new SqlParameter("@Notes", data.Notes)
        //    };

        //    result = DBConnect.ExecuteNonQuery("SP_UPDATE_ATTENDANCE_RECORD", updateParam);
        //    return result;
        //}
        [HttpPost]
        [Route("xoaThongTinDiemDanh")]
        public bool XoaThongTinDiemDanh(string attendanceID)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@AttendanceID", attendanceID)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_ATTENDANCE_RECORD", deleteParams);
            return result;
        }
    }
}
