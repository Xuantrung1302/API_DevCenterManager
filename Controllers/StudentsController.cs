using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Students_Manager.Models;
using API_Technology_Students_Manages.DataAccess;
using System.Drawing.Printing;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Students")]
    public class StudentsController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        //API Employee
        [HttpGet]
        [Route("thongTinHocVien")]
        public object DanhSachHocVien(string MaHV = null, string TenHV = null, string gioiTinh = null, DateTime? Dstart = null, DateTime? Dend = null)
        {
            object nhanVien = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] searchParams = {
                new SqlParameter("@MaHV",MaHV),
                new SqlParameter("@TenHV",TenHV),
                new SqlParameter("@NgayTiepNhanStart",Dstart),
                new SqlParameter("@NgayTiepNhanEnd",Dend),
                new SqlParameter("@GioiTinhHV",gioiTinh)
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_STUDENTS", searchParams);

            if (dt?.Rows?.Count > 0)
            {
                nhanVien = dt;
                return nhanVien;
            }
            return nhanVien;
        }
        [HttpPost]
        [Route("themThongTinHocVien")]
        public bool ThemThongTinHocVien([FromBody] HocVien data)
        {
            bool result = false;

            SqlParameter[] insertparams = {
                    new SqlParameter("@MaHV", data.MaHV),
                    new SqlParameter("@TenHV", data.TenHV),
                    new SqlParameter("@NgaySinh", data.NgaySinh),
                    new SqlParameter("@GioiTinhHV", data.GioiTinhHV),
                    new SqlParameter("@DiaChi", data.DiaChi),
                    new SqlParameter("@SdtHV", data.SdtHV),
                    new SqlParameter("@EmailHV", data.EmailHV),
                    new SqlParameter("@MaLoaiHV", data.MaLoaiHV),
                    new SqlParameter("@NgayTiepNhan", data.NgayTiepNhan),
                    new SqlParameter("@TenDangNhap", data.TenDangNhap),
                    new SqlParameter("@MatKhau", data.MatKhau)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_STUDENTS", insertparams);

            return result;
        }
        [HttpPost]
        [Route("suaThongTinHocVien")]
        public bool DoiThongTinHocVien([FromBody] HocVien data)
        {
            bool result = false;

            SqlParameter[] updateparams = {
                    new SqlParameter("@MaHV", data.MaHV),
                    new SqlParameter("@TenHV", data.TenHV),
                    new SqlParameter("@NgaySinh", data.NgaySinh),
                    new SqlParameter("@GioiTinhHV", data.GioiTinhHV),
                    new SqlParameter("@DiaChi", data.DiaChi),
                    new SqlParameter("@SdtHV", data.SdtHV),
                    new SqlParameter("@EmailHV", data.EmailHV),
                    new SqlParameter("@MaLoaiHV", data.MaLoaiHV),
                    new SqlParameter("@TenDangNhap", data.TenDangNhap),
                    new SqlParameter("@MatKhau", data.MatKhau)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_STUDENTS", updateparams);

            return result;
        }

        [HttpPost]
        [Route("xoaThongTinHocVien")]
        public bool XoaThongTinHocVien(string maHV, string maLoaiHV, string tenDangNhap= null)
        {
            bool result = false;

            SqlParameter[] deleteparams = {
                    new SqlParameter("@MaHV", maHV),
                    new SqlParameter("@TenDangNhap", tenDangNhap),
                    new SqlParameter("@MaLoaiHV", maLoaiHV)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_STUDENTS", deleteparams);

            return result;
        }
    }
}
