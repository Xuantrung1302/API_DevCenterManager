using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using API_Technology_Students_Manages.DataAccess;
using API_Students_Manager.Models;
using Newtonsoft.Json;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Service")]
    public class ServiceController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        // API Account
        [HttpGet]
        [Route("dangNhap")]
        public object DangNhap(string username, string password)
        {
            object account = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] searchParams = {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            dt = DBConnect.ExecuteQuery("SP_LOGIN", searchParams);

            if (dt?.Rows?.Count > 0)
            {
                account = dt;
                return account;
            }
            return account;
        }

        [HttpPost]
        [Route("payroll/addWorkDay")]
        public bool AddWorkDay(string employeeId)
        {
            DBConnect db = new DBConnect();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@RecordDate", DateTime.Today)
            };

            return db.ExecuteNonQuery("SP_INSERT_WORKDAY", parameters);
        }

        [HttpGet]
        [Route("taoIdTuDong")]
        public string TaoIDTuDong(string ngay, string prefix)
        {
            if (string.IsNullOrEmpty(ngay) || string.IsNullOrEmpty(prefix))
            {
                return null; // Hoặc ném ngoại lệ để trả về lỗi 400
            }
            return DBConnect.AutoGenerateId(ngay, prefix);
        }


        [HttpPost]
        [Route("doiMatKhau")]
        public bool DoiMatKhau([FromBody] TaiKhoan data)
        {
            bool result = false;
            string username = data.Username;
            string password = data.Password;
            string role = data.Role;

            SqlParameter[] updateParams = {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password),
                new SqlParameter("@Role",role)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_ACCOUNT", updateParams);
            return result;
        }

        // API INFOR CENTER
        [HttpGet]
        [Route("thongTinTrungTam")]
        public object ThongTinTrungTam()
        {
            object info = new List<object>();
            DataTable dt = new DataTable();
            dt = DBConnect.ExecuteQuery("SP_SELECT_CENTER_INFORMATION");

            if (dt?.Rows?.Count > 0)
            {
                info = dt;
                return info;
            }
            return info;
        }

        [HttpPost]
        [Route("suaThongTinTrungTam")]
        public bool SuaThongTinTrungTam([FromBody] ChiTietTrungTam data)
        {
            bool result = false;
            string centerName = data.CenterName;
            string address = data.Address;
            string phoneNumber = data.PhoneNumber;
            string website = data.Website;
            string email = data.Email;

            SqlParameter[] updateParams = {
                new SqlParameter("@CenterName", centerName),
                new SqlParameter("@Address", address),
                new SqlParameter("@PhoneNumber", phoneNumber),
                new SqlParameter("@Website", website),
                new SqlParameter("@Email", email)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_CENTER_INFORMATION", updateParams);
            return result;
        }
        [HttpPost]
        [Route("xoaTaiKhoan")]
        public bool XoaThongTinTaiKhoan(string username = null)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@Username", username)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_ACCOUNT", deleteParams);
            return result;
        }

        [HttpPost]
        [Route("themTaiKhoan")]
        public bool ThemTaiKhoan([FromBody] TaiKhoan data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] insertParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_ACCOUNT", insertParam);
            return result;
        }
        [HttpGet]
        [Route("thongTinTaiKhoan")]
        public object DanhSachTaiKhoan(string role = null, string useName = null)
        {
            object account = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@Role", role),
                new SqlParameter("@Username", useName)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_ACCOUNT", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                account = dt;
                return account;
            }
            return account;
        }
    }
}