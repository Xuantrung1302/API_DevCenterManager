using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using API_Students_Manager.Models;
using API_Technology_Students_Manages.DataAccess;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Students")]
    public class StudentsController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("thongTinHocVien")]
        public object DanhSachHocVien(string studentID = null, string fullName = null, string gender = null, DateTime? enrollmentDateStart = null, DateTime? enrollmentDateEnd = null)
        {
            object students = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] searchParams = {
                new SqlParameter("@StudentID", studentID),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@EnrollmentDateStart", enrollmentDateStart),
                new SqlParameter("@EnrollmentDateEnd", enrollmentDateEnd),
                new SqlParameter("@Gender", gender)
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_STUDENTS", searchParams);

            if (dt?.Rows?.Count > 0)
            {
                students = dt;
                return students;
            }
            return students;
        }

        [HttpPost]
        [Route("themThongTinHocVien")]
        public bool ThemThongTinHocVien([FromBody] HocVien data)
        {
            bool result = false;

            SqlParameter[] insertParams = {
                new SqlParameter("@StudentID", data.StudentID),
                new SqlParameter("@FullName", data.FullName),
                new SqlParameter("@Gender", data.Gender),
                new SqlParameter("@Address", data.Address),
                new SqlParameter("@BirthDate", data.BirthDate),
                new SqlParameter("@PhoneNumber", data.PhoneNumber),
                new SqlParameter("@Email", data.Email),
                new SqlParameter("@EnrollmentDate", data.EnrollmentDate),
                new SqlParameter("@Username", data.Username),
                new SqlParameter("@Password", data.Password)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_STUDENTS", insertParams);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinHocVien")]
        public bool DoiThongTinHocVien([FromBody] HocVien data)
        {
            bool result = false;

            SqlParameter[] updateParams = {
            new SqlParameter("@StudentID", data.StudentID),
            new SqlParameter("@FullName", (object)data.FullName ?? DBNull.Value),
            new SqlParameter("@BirthDate", data.BirthDate == DateTime.MinValue ? DBNull.Value : (object)data.BirthDate),
            new SqlParameter("@Gender", (object)data.Gender ?? DBNull.Value),
            new SqlParameter("@Address", (object)data.Address ?? DBNull.Value),
            new SqlParameter("@PhoneNumber", (object)data.PhoneNumber ?? DBNull.Value),
            new SqlParameter("@Email", (object)data.Email ?? DBNull.Value),
            new SqlParameter("@EnrollmentDate", data.EnrollmentDate == DateTime.MinValue ? DBNull.Value : (object)data.EnrollmentDate),
            new SqlParameter("@Username", (object)data.Username ?? DBNull.Value),
            new SqlParameter("@Password", (object)data.Password ?? DBNull.Value)
        };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_STUDENTS", updateParams);
            return result;
        }

        [HttpPost]
        [Route("xoaThongTinHocVien")]
        public bool XoaThongTinHocVien(string studentID, string maLoaiHV, string username = null)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@StudentID", studentID),
                new SqlParameter("@Username", username),
                new SqlParameter("@MaLoaiHV", maLoaiHV)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_STUDENTS", deleteParams);
            return result;
        }
    }
}