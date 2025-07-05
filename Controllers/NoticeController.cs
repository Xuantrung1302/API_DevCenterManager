using API_Students_Manager.Models;
using API_Technology_Students_Manages.DataAccess;
using API_Technology_Students_Manages.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Notice")]
    public class NoticeController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("danhSachThongBao")]
        public object DanhSachThongBao()
        {
            object notices = new List<object>();
            DataTable dt = new DataTable();
            dt = DBConnect.ExecuteQuery("SP_SELECT_NOTICE");

            if (dt?.Rows?.Count > 0)
            {
                notices = dt;
                return notices;
            }
            return notices;
        }
        [HttpPost]
        [Route("themThongBao")]
        public bool ThemThongBao([FromBody] ThongBao data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] insertParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_NOTICE", insertParam);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinThongBao")]
        public bool DoiThongTinThongBao([FromBody] ThongBao data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] updateParam = {
                 new SqlParameter("@NewID", data.NewID),
                 new SqlParameter("@Title", data.Title),
                 new SqlParameter("@Content", data.Content),
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_NOTICE", updateParam);
            return result;
        }
        [HttpPost]
        [Route("xoaThongBao")]
        public bool XoaThongBao(string newID)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@NewID", newID)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_NOTICE", deleteParams);
            return result;
        }
    }
}