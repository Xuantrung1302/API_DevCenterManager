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
using Newtonsoft.Json;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Class")]
    public class ClassController : ApiController
    {
        DBConnect DBConnect = new DBConnect();
        [HttpGet]
        [Route("layLop")]
        public object LayLop(string classID = null, string subjectID = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@ClassID", classID),
                    new SqlParameter("@SubjectID", subjectID)
                    //new SqlParameter("@SemesterStatus", semesterStatus)
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_CLASS", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }
        /*        [HttpGet]
                [Route("layDiemLop")]
                public object LayDiemLop(string maLop = null)
                {
                    object lop = new List<object>();
                    DataTable dt = new DataTable();
                    SqlParameter[] selectparams = {
                            new SqlParameter("@maLop", maLop),
                    };
                    dt = DBConnect.ExecuteQuery("SP_GET_CLASS_SCORE_ID", selectparams);

                    if (dt?.Rows?.Count > 0)
                    {
                        lop = dt;
                        return lop;
                    }
                    return lop;
                }
                [HttpGet]
                [Route("layLopTheoRole")]
                public object LayLopTheoRole(string ma = null)
                {
                    object lop = new List<object>();
                    DataTable dt = new DataTable();
                    SqlParameter[] selectparams = {
                            new SqlParameter("@ma", ma),
                    };
                    dt = DBConnect.ExecuteQuery("SP_GET_CLASS_BY_ROLE", selectparams);

                    if (dt?.Rows?.Count > 0)
                    {
                        lop = dt;
                        return lop;
                    }
                    return lop;
                }
                [HttpGet]
                [Route("layDanhSachHVTheoLop")]
                public object LayDanhSachHVTheoLop(string maLop = null)
                {
                    object lop = new List<object>();
                    DataTable dt = new DataTable();
                    SqlParameter[] selectparams = {
                            new SqlParameter("@maLop", maLop),
                    };
                    dt = DBConnect.ExecuteQuery("SP_GET_STUDENT_LIST_BY_CLASS", selectparams);

                    if (dt?.Rows?.Count > 0)
                    {
                        lop = dt;
                        return lop;
                    }
                    return lop;
                }*/
        [HttpPost]
        [Route("themLop")]
        public bool ThemThongTinGiangVien([FromBody] LopHoc data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] insertParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_CLASS", insertParam);
            return result;
        }

        [HttpPost]
        [Route("suaThongTinLop")]
        public bool DoiThongTinGiangVien([FromBody] LopHoc data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] updateParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_CLASS", updateParam);
            return result;
        }
        [HttpPost]
        [Route("xoaLop")]
        public bool XoaThongTinGiangVien(string classID)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@ClassID", classID)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_CLASS", deleteParams);
            return result;
        }

        [HttpGet]
        [Route("layDanhSachLopKeHoachByMaKy")]
        public object LayDanhSachLopKeHoachByMaKy(string maKy = null)
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectparams = {
                    new SqlParameter("@semesterID", maKy),
            };
            dt = DBConnect.ExecuteQuery("SP_SELECT_CLASS_PLAN", selectparams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }

        [HttpGet]
        [Route("layDanhSachHVDuocThemVaoLop")]
        public object LayDanhSachHVDuocThemVaoLop(string classID)
        {
            object dsHocVien = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectParams = {
             new SqlParameter("@ClassID", classID),
            };

            dt = DBConnect.ExecuteQuery("SP_GET_STUDENT_FOR_ADD_CLASS", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                dsHocVien = dt;
            }

            return dsHocVien;
        }
        [HttpGet]
        [Route("laySoLuongHienTaiVaThieuCuaLop")]
        public object LaySoLuongHienTaiVaThieuCuaLop(string classID)
        {
            object ketQua = new List<object>();
            DataTable dt = new DataTable();

            SqlParameter[] selectParams = {
             new SqlParameter("@ClassID", classID),
            };

            dt = DBConnect.ExecuteQuery("SP_GET_CLASS_STUDENT_COUNT", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                ketQua = dt;
            }

            return ketQua;
        }

        [HttpPost]
        [Route("taoLichHoc")]
        public bool TaoLichHocChoLop(string classID)
        {
            bool result = false;

            SqlParameter[] param = {
                new SqlParameter("@ClassID", classID)
             };

            result = DBConnect.ExecuteNonQuery("SP_GENERATE_CLASS_SCHEDULE", param);
            return result;
        }


    }
}
