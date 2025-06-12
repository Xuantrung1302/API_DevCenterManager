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
        public object DanhSachGiangVien(string teacherID = null, string fullName = null, string gender = null)
        {
            object teachers = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@TeacherID", teacherID),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Gender", gender)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_TEACHER", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                teachers = dt;
                return teachers;
            }
            return teachers;
        }

        [HttpPost]
        [Route("themThongTinGiangVien")]
        public bool ThemThongTinGiangVien([FromBody] GiangVien data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] insertParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_TEACHER", insertParam);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinGiangVien")]
        public bool DoiThongTinGiangVien([FromBody] GiangVien data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] updateParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_TEACHER", updateParam);
            return result;
        }
    }
}