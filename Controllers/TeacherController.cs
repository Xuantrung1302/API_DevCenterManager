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
using Newtonsoft.Json;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Teacher")]
    public class TeacherController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        //API Teacher
        [HttpGet]
        [Route("thongTinGiangVien")]
        public object DanhSachGiangVien(string maGV = null, string tenGV = null, string gioiTinh = null)
        {
            object giangVien = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@MaGV", maGV),
                    new SqlParameter("@TenGV", tenGV),
                    new SqlParameter("@GioiTinhGV", gioiTinh),
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_TEACHER", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                giangVien = dt;
                return giangVien;
            }
            return giangVien;
        }

        [HttpPost]
        [Route("themThongTinGiangVien")]
        public bool ThemThongTinGiangVien([FromBody] GiangVien data)
        {
            bool result = false;

            string gv = JsonConvert.SerializeObject(data);

            SqlParameter[] insertparam = {
                    new SqlParameter("@json", gv)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_TEACHER", insertparam);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinGiangVien")]
        public bool DoiThongTinGiangVien([FromBody] GiangVien data)
        {
            bool result = false;

            string gv = JsonConvert.SerializeObject(data);

            SqlParameter[] updateparam = {
                    new SqlParameter("@json", gv)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_TEACHER", updateparam);
            return result;
        }
    }
}
