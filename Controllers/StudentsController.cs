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

        //[HttpGet]
        //[Route("thongTinHocVien")]
        //public object DanhSachHocVien()
        //{
        //    object students = new List<object>();
        //    DataTable dt = new DataTable();
        //    SqlParameter[] searchParams = {
        //    };
        //    dt = DBConnect.ExecuteQuery("SP_SELECT_STUDENT", searchParams);

        //    if (dt?.Rows?.Count > 0)
        //    {
        //        students = dt;
        //        return students;
        //    }
        //    return students;
        //}
        [HttpGet]
        [Route("thongTinHocVien")]
        public IHttpActionResult DanhSachHocVien(string search = null, int pageIndex = 1, int pageSize = 30)
        {
            var parameters = new[]
            {
                new SqlParameter("@Search", SqlDbType.NVarChar) { Value = search ?? (object)DBNull.Value },
                new SqlParameter("@PageIndex", SqlDbType.Int) { Value = pageIndex },
                new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize },
            };

            var ds = DBConnect.ExecuteDataset("SP_SELECT_STUDENT", parameters);
            if (ds == null || ds.Tables.Count < 2)
                return Ok(new { totalCount = 0, data = new List<object>() });

            int totalCount = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalCount"]);
            var data = ds.Tables[1];

            return Ok(new
            {
                TotalCount = totalCount,
                Data = data
            });
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

            result = DBConnect.ExecuteNonQuery("SP_INSERT_STUDENT", insertParams);
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
            new SqlParameter("@Password", (object)data.Password ?? DBNull.Value),
            new SqlParameter("@Status", data.Status)
        };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_STUDENT", updateParams);
            return result;
        }

        [HttpPost]
        [Route("xoaThongTinHocVien")]
        public bool XoaThongTinHocVien(string studentID, string username = null)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@StudentID", studentID),
                new SqlParameter("@Username", username)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_STUDENT", deleteParams);
            return result;
        }
        [HttpGet]
        [Route("thongTinHocVienCuaLop")]
        public object DanhSachHocViènCuaLop(string classID)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@ClassID", classID),

            };

            dt = DBConnect.ExecuteQuery("SP_GET_STUDENTS_BY_ONLY_CLASS", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                result = dt;
                return result;
            }
            return result;
        }

        [HttpGet]
        [Route("danhSachHocVienCoHocPhi")]
        public object DanhSachHocVienCoHocPhi(string StudentID = null)
        {
            object result = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@StudentID", StudentID),

            };

            dt = DBConnect.ExecuteQuery("SP_GET_STUDENTS_FOR_INVOICE", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                result = dt;
                return result;
            }
            return result;
        }
    }
}