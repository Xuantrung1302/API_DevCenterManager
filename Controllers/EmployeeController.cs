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

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        //API Employee
        [HttpGet]
        [Route("thongTinNhanVien")]
        public object DanhSachNhanVien(string maNV = null, string tenNV = null, string maLoaiNV = null)
        {
            object nhanVien = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@MaNV", maNV),
                    new SqlParameter("@TenNV", tenNV),
                    new SqlParameter("@TenLoaiNV", maLoaiNV)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_EMPLOYEE", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                nhanVien = dt;
                return nhanVien;
            }
            return nhanVien;
        }

        [HttpPost]
        [Route("themThongTinNhanVien")]
        public bool ThemThongTinNhanVien([FromBody] NhanVien data)
        {
            bool result = false;

            SqlParameter[] insertparams = {
                    new SqlParameter("@MaNV", data.MaNV),
                    new SqlParameter("@TenNV", data.TenNV),
                    new SqlParameter("@SdtNV", data.SdtNV),
                    new SqlParameter("@EmailNV", data.EmailNV),
                    new SqlParameter("@MaLoaiNV", data.MaLoaiNV),
                    new SqlParameter("@TenDangNhap", data.TenDangNhap),
                    new SqlParameter("@MatKhau", data.MatKhau)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_EMPLOYEE", insertparams);

            return result;
        }

        [HttpPost]
        [Route("suaThongTinNhanVien")]
        public bool DoiThongTinNhanVien([FromBody] NhanVien data)
        {
            bool result = false;

            SqlParameter[] updateparams = {
                    new SqlParameter("@MaNV", data.MaNV),
                    new SqlParameter("@TenNV", data.TenNV),
                    new SqlParameter("@SdtNV", data.SdtNV),
                    new SqlParameter("@EmailNV", data.EmailNV),
                    new SqlParameter("@MaLoaiNV", data.MaLoaiNV),
                    new SqlParameter("@TenDangNhap", data.TenDangNhap),
                    new SqlParameter("@MatKhau", data.MatKhau)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_EMPLOYEE", updateparams);
            return result;
        }

    }
}
