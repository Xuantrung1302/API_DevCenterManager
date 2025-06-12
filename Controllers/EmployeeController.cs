using API_Students_Manager.Models;
using API_Technology_Students_Manages.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("thongTinNhanVien")]
        public object DanhSachNhanVien(string employeeID = null, string fullName = null, string position = null)
        {
            object employees = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@EmployeeID", employeeID),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Position", position)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_EMPLOYEE", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                employees = dt;
                return employees;
            }
            return employees;
        }

        [HttpPost]
        [Route("themThongTinNhanVien")]
        public bool ThemThongTinNhanVien([FromBody] NhanVien data)
        {
            bool result = false;

            SqlParameter[] insertParams = {
                new SqlParameter("@EmployeeID", data.EmployeeID),
                new SqlParameter("@FullName", data.FullName),
                new SqlParameter("@PhoneNumber", data.PhoneNumber),
                new SqlParameter("@Email", data.Email),
                new SqlParameter("@Position", data.Position),
                new SqlParameter("@Username", data.Username),
                new SqlParameter("@Password", data.Password)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_EMPLOYEE", insertParams);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinNhanVien")]
        public bool DoiThongTinNhanVien([FromBody] NhanVien data)
        {
            bool result = false;

            SqlParameter[] updateParams = {
                new SqlParameter("@EmployeeID", data.EmployeeID),
                new SqlParameter("@FullName", data.FullName),
                new SqlParameter("@PhoneNumber", data.PhoneNumber),
                new SqlParameter("@Email", data.Email),
                new SqlParameter("@Position", data.Position),
                new SqlParameter("@Username", data.Username),
                new SqlParameter("@Password", data.Password)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_EMPLOYEE", updateParams);
            return result;
        }
    }
}