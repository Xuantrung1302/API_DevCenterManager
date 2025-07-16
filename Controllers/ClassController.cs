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

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Class")]
    public class ClassController : ApiController
    {
        DBConnect DBConnect = new DBConnect();
        [HttpGet]
        [Route("layLop")]
        public object LayLop(string className = null, string courseID = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@ClassName", className),
                    new SqlParameter("@CourseID", courseID)
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_CLASS", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }
        /*        [HttpGet]
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
                [HttpGet]
                [Route("layLopTheoRole")]
                public object LayLopTheoRole(string ma = null)
                {
                    object lop = new List<object>();
                    DataTable dt = new DataTable();
                    SqlParameter[] selectparams = {
                            new SqlParameter("@ma", ma),
                    };
                    dt = DBConnect.ExecuteQuery("SP_GET_CLASS_BY_ROLE", selectparams);

                    if (dt?.Rows?.Count > 0)
                    {
                        lop = dt;
                        return lop;
                    }
                    return lop;
                }
                [HttpGet]
                [Route("layDanhSachHVTheoLop")]
                public object LayDanhSachHVTheoLop(string maLop = null)
                {
                    object lop = new List<object>();
                    DataTable dt = new DataTable();
                    SqlParameter[] selectparams = {
                            new SqlParameter("@maLop", maLop),
                    };
                    dt = DBConnect.ExecuteQuery("SP_GET_STUDENT_LIST_BY_CLASS", selectparams);

                    if (dt?.Rows?.Count > 0)
                    {
                        lop = dt;
                        return lop;
                    }
                    return lop;
                }*/
        [HttpPost]
        [Route("themLop")]
        public bool ThemThongTinLop([FromBody] LopHoc data)
        {
            bool result = false;


            SqlParameter[] insertparams = {
                    new SqlParameter("@CourseID", data.CourseID),
                    new SqlParameter("@ClassName", data.ClassName),
                    new SqlParameter("@StartTime", data.StartTime),
                    new SqlParameter("@EndTime", data.EndTime),
                    new SqlParameter("@Room", data.Room),
                    new SqlParameter("@TeacherID", data.TeacherID),
                    new SqlParameter("@MaxSeats", data.MaxSeats),
                    new SqlParameter("@DaysOfWeek", data.DaysOfWeek)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_CLASS", insertparams);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinLop")]
        public bool DoiThongTinLopHoc([FromBody] LopHoc data)
        {
            bool result = false;


            SqlParameter[] updateparams = {
                    new SqlParameter("@ClassID", data.ClassID),
                    new SqlParameter("@CourseID", data.CourseID),
                    new SqlParameter("@ClassName", data.ClassName),
                    new SqlParameter("@StartTime", data.StartTime),
                    new SqlParameter("@EndTime", data.EndTime),
                    new SqlParameter("@Room", data.Room),
                    new SqlParameter("@TeacherID", data.TeacherID),
                    new SqlParameter("@MaxSeats", data.MaxSeats),
                    new SqlParameter("@DaysOfWeek", data.DaysOfWeek),
                    new SqlParameter("@StudentCount", data.StudentCount),
                    new SqlParameter("@Status", data.Status),
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_CLASS", updateparams);
            return result;
        }
        [HttpPost]
        [Route("xoaLop")]
        public bool XoaThongTinGiangVien(string classID)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@ClassID", classID)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_CLASS", deleteParams);
            return result;
        }

        [HttpGet]
        [Route("layDanhSachLopKeHoachByMaKy")]
        public object LayDanhSachLopKeHoachByMaKy(string maKy = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@semesterID", maKy),
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_CLASS_PLAN", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }

        [HttpGet]
        [Route("layDanhSachHVDuocThemVaoLop")]
        public object LayDanhSachHVDuocThemVaoLop(string classID)
        {
            object dsHocVien = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectParams = {
             new SqlParameter("@ClassID", classID),
            };

            dt = DBConnect.ExecuteQuery("SP_GET_STUDENT_FOR_ADD_CLASS", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                dsHocVien = dt;
            }

            return dsHocVien;
        }
        [HttpGet]
        [Route("laySoLuongHienTaiVaThieuCuaLop")]
        public object LaySoLuongHienTaiVaThieuCuaLop(string classID)
        {
            object ketQua = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectParams = {
             new SqlParameter("@ClassID", classID),
            };

            dt = DBConnect.ExecuteQuery("SP_GET_CLASS_STUDENT_COUNT", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                ketQua = dt;
            }

            return ketQua;
        }

        [HttpPost]
        [Route("taoLichHoc")]
        public bool TaoLichHocChoLop(string classID)
        {
            bool result = false;

            SqlParameter[] param = {
                new SqlParameter("@ClassID", classID)
             };

            result = DBConnect.ExecuteNonQuery("SP_GENERATE_CLASS_SCHEDULE", param);
            return result;
        }
        [HttpGet]
        [Route("layDanhSachLichHoc")]
        public object GetSchedule(string courseID, string classID = null, string semesterID = null)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectParams = {
                new SqlParameter("@CourseID", courseID),
                new SqlParameter("@ClassID", classID),
                new SqlParameter("@SemesterID", semesterID)
            };

            dt = DBConnect.ExecuteQuery("SP_GET_CLASS_SCHEDULE", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                result = dt;
                return result;
            }
            return result;
        }

        [HttpGet]
        [Route("getScheduleByUsername")]
        public object GetScheduleByUsername(string username, string code = null, string semesterID = null)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectParams = {
                new SqlParameter("@Username", username),
                new SqlParameter("@Code", code),
                new SqlParameter("@SemesterID", semesterID)
            };

            dt = DBConnect.ExecuteQuery("SP_GET_SCHEDULE_BY_USERNAME", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                result = dt;
                return result;
            }
            return result;
        }
        [HttpGet]
        [Route("layDanhSachSinhVienTheoLop")]
        public object LayDanhSachSinhVienTheoLop(string classID)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] param = {
        new SqlParameter("@ClassID", classID)
    };

            dt = DBConnect.ExecuteQuery("SP_GET_STUDENTS_BY_CLASS", param);

            if (dt?.Rows?.Count > 0)
            {
                result = dt;
            }
            return result;
        }
        [HttpGet]
        [Route("layDanhSachLopDaHocTheoSinhVien")]
        public object LayDanhSachLopDaHocTheoSinhVien(string studentID)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] param = {
        new SqlParameter("@StudentID", studentID)
    };

            dt = DBConnect.ExecuteQuery("SP_GET_CLASSES_BY_STUDENT", param);

            if (dt?.Rows?.Count > 0)
            {
                result = dt;
            }
            return result;
        }

        [HttpPost]
        [Route("themSinhVienVaoLop")]
        public bool ThemSinhVienVaoLoplment([FromBody] ClassEnrollment data)
        {
            bool result = false;

            SqlParameter[] insertParams = {
                new SqlParameter("@StudentID", data.StudentID),
                new SqlParameter("@ClassID", data.ClassID),
                new SqlParameter("@EnrollmentDate", (object)data.EnrollmentDate ?? DBNull.Value),
                new SqlParameter("@ApprovedBy", (object)data.ApprovedBy ?? DBNull.Value),
                new SqlParameter("@ApprovalDate", (object)data.ApprovalDate ?? DBNull.Value),
                new SqlParameter("@CompletionStatus", (object)data.CompletionStatus ?? DBNull.Value),
                new SqlParameter("@CompletionDate", (object)data.CompletionDate ?? DBNull.Value)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_CLASS_ENROLLMENT", insertParams);
            return result;
        }

        [HttpGet]
        [Route("layGiangVienChoLop")]
        public object LayGiangVienChoLop(string classID)
        {
            object teachers = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                 new SqlParameter("@ClassID", classID)
            };

            dt = DBConnect.ExecuteQuery("SP_GET_TEACHERS_FOR_ADD_CLASS", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                teachers = dt;
                return teachers;
            }

            return teachers;
        }
        [HttpPost]
        [Route("themGiangVienVaoLop")]
        public bool ThemGiangVienVaoLop(string teacherID, string classID)
        {
            bool result = false;

            SqlParameter[] insertParams = {
                new SqlParameter("@ClassID", teacherID),
                new SqlParameter("@TeacherID", classID)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_TEACHER_TO_CLASS", insertParams);
            return result;
        }



    }
}
public class ClassEnrollment
{
    public string StudentID { get; set; }
    public Guid ClassID { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    public string ApprovedBy { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public string CompletionStatus { get; set; }
    public DateTime? CompletionDate { get; set; }
}
