using API_Technology_Students_Manages.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Students_Manager.Models;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Room")]
    public class RoomController : ApiController
    {
        DBConnect DBConnect = new DBConnect();
        [HttpGet]
        [Route("layLop")]
        public object LayLop(string roomName = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@RoomName", roomName)
            };
            dt = DBConnect.ExecuteQuery("SP_GET_ALL_ROOMS", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }

        [HttpPost]
        [Route("themPhong")]
        public bool ThemPhong([FromBody] Room phong)
        {
            bool result = false;

            SqlParameter[] parameters = {
                new SqlParameter("@RoomID", phong.RoomID),
                new SqlParameter("@RoomName", phong.RoomName),
                new SqlParameter("@MaxSeats", phong.MaxSeats)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_ROOM", parameters);
            return result;
        }
    }
    public class Room
    {
        public string RoomID { get; set; }    
        public string RoomName { get; set; }
        public int? MaxSeats { get; set; }
    }
}
