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
        public IHttpActionResult DanhSachNhanVien(string search = null, int pageIndex = 1, int pageSize = 30)
        {
            var parameters = new[]
            {
                new SqlParameter("@Search", SqlDbType.NVarChar) { Value = search ?? (object)DBNull.Value },
                new SqlParameter("@PageIndex", SqlDbType.Int) { Value = pageIndex },
                new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize },
            };

            var ds = DBConnect.ExecuteDataset("SP_SELECT_EMPLOYEE", parameters);
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
        [Route("themThongTinNhanVien")]
        public bool ThemThongTinNhanVien([FromBody] NhanVien data)
        {
            bool result = false;

            SqlParameter[] insertParams = {
                new SqlParameter("@EmployeeID", data.EmployeeID),
                new SqlParameter("@FullName", data.FullName),
                new SqlParameter("@PhoneNumber", data.PhoneNumber),
                new SqlParameter("@Gender", data.Gender),
                new SqlParameter("@Address", data.Address),
                new SqlParameter("@Email", data.Email),
                new SqlParameter("@Username", data.Username),
                new SqlParameter("@Password", data.Password),
                new SqlParameter("@Salary", data.Salary)
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
                new SqlParameter("@Gender", data.Gender),
                new SqlParameter("@Address", data.Address),
                new SqlParameter("@Email", data.Email),
                new SqlParameter("@Username", data.Username),
                new SqlParameter("@Password", data.Password),
                new SqlParameter("@Salary", data.Salary)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_EMPLOYEE", updateParams);
            return result;
        }


        [HttpPost]
        [Route("xoaThongTinNhanVien")]
        public bool XoaThongTinNhanVien(string employeeID, string username = null)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@EmployeeID", employeeID),
                new SqlParameter("@Username", username)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_EMPLOYEE", deleteParams);
            return result;
        }
    }
}