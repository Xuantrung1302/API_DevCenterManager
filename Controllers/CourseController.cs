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
using System.Windows.Input;
using API_Technology_Students_Manages.Models;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Course")]
    public class CourseController : ApiController
    {
        DBConnect DBConnect = new DBConnect();
        [HttpGet]
        [Route("chiTietChuongTrinhHoc")]
        public object DanhSachChiTietKhoaHoc(string courseId = null)
        {
            object courseDetail = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectparams = {
        new SqlParameter("@CourseID", (object)courseId ?? DBNull.Value)
    };

            dt = DBConnect.ExecuteQuery("SP_SELECT_COURSE_DETAIL", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                courseDetail = dt;
                return courseDetail;
            }

            return courseDetail;
        }
        [HttpGet]
        [Route("danhSachKhoaHoc")]
        public object DanhSachKhoaHoc(string courseId = null, string courseName = null)
        {
            object courseList = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectparams = {
        new SqlParameter("@CourseID", (object)courseId ?? DBNull.Value),
        new SqlParameter("@CourseName", (object)courseName ?? DBNull.Value),
    };

            dt = DBConnect.ExecuteQuery("SP_SELECT_COURSE", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                courseList = dt;
            }

            return courseList;
        }
        [HttpGet]
        [Route("danhSachLopTrongKhoaHoc")]
        public object DanhSachLopTrongKhoaHoc(string courseId = null)
        {
            object classList = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectparams = {
        new SqlParameter("@CourseID", (object)courseId ?? DBNull.Value),
    };

            dt = DBConnect.ExecuteQuery("SP_SELECT_CLASSES_BY_COURSE", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                classList = dt;
            }

            return classList;
        }
        [HttpGet]
        [Route("layDanhSachCacKhoaHocDaHoc")]
        public object LayDanhSachCacKhoaHocDaHoc(string studentID)
        {
            object param = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                 new SqlParameter("@StudentID", studentID)
            };

            dt = DBConnect.ExecuteQuery("SP_GET_COMPLETED_COURSES_BY_STUDENT", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                param = dt;
                return param;
            }

            return param;
        }
        [HttpGet]
        [Route("layKhoaHocTheoHocVien")]
        public object LayKhoaHocTheoHocVien(string studentID)
        {
            object courseDetail = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectparams = {
                  new SqlParameter("@StudentID", studentID)
            };

            dt = DBConnect.ExecuteQuery("SP_GET_COURSES_BY_STUDENT", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                courseDetail = dt;
                return courseDetail;
            }

            return courseDetail;
        }

        [HttpGet]
        [Route("layKhoaHocTheoHocVienForLichThi")]
        public object LayKhoaHocTheoHocVienLichThi(string studentID)
        {
            object courseDetail = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectparams = {
                  new SqlParameter("@StudentID", studentID)
            };

            dt = DBConnect.ExecuteQuery("SP_GET_COURSES_BY_STUDENT_EXAM", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                courseDetail = dt;
                return courseDetail;
            }

            return courseDetail;
        }
        [HttpPost]
        [Route("themKhoaHoc")]
        public bool ThemKhoaHoc([FromBody] List<Course> data)
        {
            bool result = true;

            foreach (var course in data)
            {
                // Tạo bảng tạm cho Subjects
                DataTable subjectsTable = new DataTable();
                subjectsTable.Columns.Add("SubjectID", typeof(string));
                subjectsTable.Columns.Add("SubjectName", typeof(string));
                subjectsTable.Columns.Add("TuitionFee", typeof(decimal));
                subjectsTable.Columns.Add("SemesterID", typeof(string));

                // Thêm các môn học của kỳ 1
                foreach (var subject in course.Semester1.Subjects)
                {
                    subjectsTable.Rows.Add(subject.SubjectID, subject.SubjectName, subject.TuitionFee, course.Semester1.SemesterID);
                }

                // Thêm các môn học của kỳ 2
                foreach (var subject in course.Semester2.Subjects)
                {
                    subjectsTable.Rows.Add(subject.SubjectID, subject.SubjectName, subject.TuitionFee, course.Semester2.SemesterID);
                }

                SqlParameter[] insertParams = {
                    new SqlParameter("@CourseCode", course.CourseCode),
                    new SqlParameter("@CourseName", course.CourseName),
                    new SqlParameter("@IsActive", course.IsActive),
                    new SqlParameter("@Semester1ID", course.Semester1.SemesterID),
                    new SqlParameter("@Semester1Name", course.Semester1.SemesterName),
                    new SqlParameter("@Semester1StartDate", course.Semester1.StartDate),
                    new SqlParameter("@Semester1EndDate", course.Semester1.EndDate),
                    new SqlParameter("@Semester2ID", course.Semester2.SemesterID),
                    new SqlParameter("@Semester2Name", course.Semester2.SemesterName),
                    new SqlParameter("@Semester2StartDate", course.Semester2.StartDate),
                    new SqlParameter("@Semester2EndDate", course.Semester2.EndDate),
                    new SqlParameter("@Subjects", subjectsTable) { SqlDbType = SqlDbType.Structured, TypeName = "dbo.SubjectType" }
                };

                result &= DBConnect.ExecuteNonQuery("SP_INSERT_COURSE_WITH_SEMESTERS_SUBJECTS", insertParams);
            }

            return result;
        }

    }
}
