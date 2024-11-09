using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Technology_Students_Manages.DataAccess;
using API_Students_Manager.Models;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Service")]
    public class ServiceController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        //API Account
        [HttpGet]
        [Route("dangNhap")]
        public object DangNhap(string tenDangNhap, string matKhau)
        {
            object taiKhoan = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] searchParams = {
                    new SqlParameter("@TenDangNhap",tenDangNhap),
                    new SqlParameter("@MatKhau",matKhau)
            };

            dt = DBConnect.ExecuteQuery("SP_LOGIN", searchParams);

            if (dt?.Rows?.Count > 0)
            {
                taiKhoan = dt;
                return taiKhoan;
            }
            return taiKhoan;
        }

        [HttpPost]
        [Route("doiMatKhau")]
        public bool DoiMatKhau([FromBody] TaiKhoan data)
        {
            bool result = false;
            string username = data.TenDangNhap.ToString();
            string password = data.MatKhau.ToString();

            SqlParameter[] updateparams = {
                    new SqlParameter("@TenDangNhap", username),
                    new SqlParameter("@MatKhau", password)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_ACCOUNT", updateparams);
            return result;
        }


        //API INFOR CENTER
        [HttpGet]
        [Route("thongTinTrungTam")]
        public object ThongTinTrungTam()
        {
            object thongTin = new List<object>();
            DataTable dt = new DataTable();
            dt = DBConnect.ExecuteQuery("SP_SELECT_CENTER_INFORMATION");

            if (dt?.Rows?.Count > 0)
            {
                thongTin = dt;
                return thongTin;
            }
            return thongTin;
        }
        [HttpPost]
        [Route("suaThongTinTrungTam")]
        public bool SuaThongTinTrungTam([FromBody] ChiTietTrungTam data)
        {
            bool result = false;
            string tenTT = data.TenTT.ToString();
            string diaChiTT = data.DiaChiTT.ToString();
            string sdtTT = data.SdtTT.ToString();
            string website = data.Website.ToString();
            string emailTT = data.EmailTT.ToString();   
            SqlParameter[] updateparams = {
                    new SqlParameter("@TenTT", tenTT),
                    new SqlParameter("@DiaChiTT", diaChiTT),
                    new SqlParameter("@SdtTT", sdtTT),
                    new SqlParameter("@Website", website),
                    new SqlParameter("@EmailTT", emailTT)

            }; 

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_CENTER_INFORMATION", updateparams);
            return result;
        }

        [HttpGet]
        [Route("taoIdTuDong")]
        public string TaoIDTuDong(string ngay, string ma)
        {
            return DBConnect.AutoGenerateId(ngay, ma);
        }

    }
}
