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
        #region LICH THI
        [HttpGet]
        [Route("layDanhSachLichThi")]
        public object LayDanhSachLichThi(string courseID = null, string subjectID = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@CourseID", courseID),
                    new SqlParameter("@SubjectID", subjectID)
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_EXAM_SCHEDULE", selectparams);

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
                    //new SqlParameter("@ExamID", data.ExamID),
                    new SqlParameter("@ClassID", data.ClassID),
                    new SqlParameter("@SubjectID", data.SubjectID ?? (object)DBNull.Value),  
                    new SqlParameter("@ExamName", data.ExamName),
                    new SqlParameter("@ExamType", data.ExamType),
                    new SqlParameter("@ExamDateStart", data.ExamDateStart),
                    new SqlParameter("@ExamDateEnd", data.ExamDateEnd),
                    new SqlParameter("@Room", data.Room),
                    new SqlParameter("@CreatedBy", data.CreatedBy),
                    new SqlParameter("@CreatedDate", data.CreatedDate)
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
                new SqlParameter("@Room", data.Room),
                new SqlParameter("@SubjectID", data.SubjectID ?? (object)DBNull.Value),
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
        [HttpGet]
        [Route("layDanhSachLichThiTheoHocVien")]
        public object LayDanhSachLichThiTheoHocVien(string courseID = null, string studentID = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@CourseID", courseID),
                    new SqlParameter("@StudentID", studentID)
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_EXAM_SCHEDULE_BY_STUDENT", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }
        #endregion
        #region KET QUA
        [HttpGet]
        [Route("layDanhSachKetQua")]
        public object LayDanhSachKetQua(string classID = null, string subjectID = null)
        {
            object ketQua = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@ClassID", classID),
                    new SqlParameter("@SubjectID", subjectID)
            };  
            dt = DBConnect.ExecuteQuery("SP_SELECT_EXAM_RESULT", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                ketQua = dt;
                return ketQua;
            }
            return ketQua;
        }
        [HttpPost]
        [Route("themKetQua")]
        public bool ThemKetQua([FromBody] KetQua data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] insertParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_EXAM_RESULT", insertParam);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinKetQua")]
        public bool DoiThongTinKetQua([FromBody] KetQua data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] updateParam = {
                new SqlParameter("@ResultID", data.ResultID),
                new SqlParameter("@Score", data.Score),
                new SqlParameter("@Status", data.Status),
                new SqlParameter("@EnteredBy", data.EnteredBy),
                new SqlParameter("@GradingDate", data.GradingDate)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_EXAM_RESULT", updateParam);
            return result;
        }
        [HttpPost]
        [Route("xoaKetQua")]
        public bool XoaKetQua(string resultID)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@ResultID", resultID)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_EXAM_RESULT", deleteParams);
            return result;
        }
        [HttpGet]
        [Route("layDiemMon")]
        public object LayDiemMon(string studentID = null, string courseID = null)
        {
            object ketQua = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@StudentID", studentID),
                    new SqlParameter("@CourseID", courseID),
            };
            dt = DBConnect.ExecuteQuery("SP_GET_SUBJECT_SCORES_BY_STUDENT_AND_COURSE");

            if (dt?.Rows?.Count > 0)
            {
                ketQua = dt;
                return ketQua;
            }
            return ketQua;
        }
        #endregion
    }
}
