using API_Students_Manager.Models;
using API_Technology_Students_Manages.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Teacher")]
    public class TeacherController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("thongTinGiangVien")]
        public object DanhSachGiangVien(string teacherID = null, string fullName = null)
        {
            object teachers = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@TeacherID", teacherID),
                new SqlParameter("@FullName", fullName)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_TEACHER", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                teachers = dt;
                return teachers;
            }
            return teachers;
        }

        [HttpPost]
        [Route("themThongTinGiangVien")]
        public bool ThemThongTinGiangVien([FromBody] GiangVien giangVien)
        {
            bool result = false;

            SqlParameter[] parameters = {
                new SqlParameter("@TeacherID", giangVien.TeacherID),
                new SqlParameter("@FullName", giangVien.FullName),
                new SqlParameter("@PhoneNumber", giangVien.PhoneNumber),
                new SqlParameter("@Address", giangVien.Address),
                new SqlParameter("@Gender", giangVien.Gender),
                new SqlParameter("@Email", giangVien.Email),
                new SqlParameter("@Degree", giangVien.Degree),
                new SqlParameter("@Username", giangVien.Username),
                new SqlParameter("@Password", giangVien.Password)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_TEACHER", parameters);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinGiangVien")]
        public bool DoiThongTinGiangVien([FromBody] GiangVien giangVien)
        {
            bool result = false;

            SqlParameter[] updateParam = {
                new SqlParameter("@TeacherID", giangVien.TeacherID),
                new SqlParameter("@FullName", giangVien.FullName),
                new SqlParameter("@PhoneNumber", giangVien.PhoneNumber),
                new SqlParameter("@Address", giangVien.Address),
                new SqlParameter("@Gender", giangVien.Gender),
                new SqlParameter("@Email", giangVien.Email),
                new SqlParameter("@Degree", giangVien.Degree),
                new SqlParameter("@Username", giangVien.Username),
                new SqlParameter("@Password", giangVien.Password)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_TEACHER", updateParam);
            return result;
        }

        [HttpPost]
        [Route("xoaThongTinGiangVien")]
        public bool XoaThongTinGiangVien(string teacherID, string username = null)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@TeacherID", teacherID),
                new SqlParameter("@Username", username)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_TEACHER", deleteParams);
            return result;
        }

        [HttpGet]
        [Route("thongTinLopDay")]
        public object DanhSachLopGiangVienDay(string teacherID)
        {
            object listClass = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@TeacherID", teacherID)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_CLASS_BY_TEACHER", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                listClass = dt;
                return listClass;
            }
            return listClass;
        }
    }
}