﻿using API_Students_Manager.Models;
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
    [RoutePrefix("api/Course")]
    public class CourseController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        //API Course
        [HttpGet]
        [Route("thongTinKhoaHoc")]
        public object DanhSachKhoaHoc()
        {
            object khoaHoc = new List<object>();
            DataTable dt = new DataTable();

            dt = DBConnect.ExecuteQuery("SP_SELECT_SEARCH_COURSE");

            if (dt?.Rows?.Count > 0)
            {
                khoaHoc = dt;
                return khoaHoc;
            }
            return khoaHoc;
        }
        [HttpPost]
        [Route("themThongTinKhoaHoc")]
        public bool ThemThongTinKhoaHoc([FromBody] KhoaHoc data)
        {
            bool result = false;

            SqlParameter[] insertparams = {
                    new SqlParameter("@MaKH", data.MaKH),
                    new SqlParameter("@TenKH", data.TenKH),
                    new SqlParameter("@HocPhi", data.HocPhi),
                    new SqlParameter("@HeSoLyThuyet", data.HeSoLyThuyet),
                    new SqlParameter("@HeSoThucHanh", data.HeSoThucHanh),
                    new SqlParameter("@HeSoDuAn", data.HeSoDuAn),
                    new SqlParameter("@HeSoCuoiKy", data.HeSoCuoiKy),
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_COURSE", insertparams);

            return result;
        }
        [HttpPost]
        [Route("suaThongTinKhoaHoc")]
        public bool DoiThongTinKhoaHoc([FromBody] KhoaHoc data)
        {
            bool result = false;

            SqlParameter[] updateparams = {
                    new SqlParameter("@MaKH", data.MaKH),
                    new SqlParameter("@TenKH", data.TenKH),
                    new SqlParameter("@HocPhi", data.HocPhi),
                    new SqlParameter("@HeSoLyThuyet", data.HeSoLyThuyet),
                    new SqlParameter("@HeSoThucHanh", data.HeSoThucHanh),
                    new SqlParameter("@HeSoDuAn", data.HeSoDuAn),
                    new SqlParameter("@HeSoCuoiKy", data.HeSoCuoiKy),
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_COURSE", updateparams);

            return result;
        }

        //[HttpPost]
        //[Route("xoaThongTinHocVien")]
        //public bool XoaThongTinHocVien(string maHV, string maLoaiHV, string tenDangNhap = null)
        //{
        //    bool result = false;

        //    SqlParameter[] deleteparams = {
        //            new SqlParameter("@MaHV", maHV),
        //            new SqlParameter("@TenDangNhap", tenDangNhap),
        //            new SqlParameter("@MaLoaiHV", maLoaiHV)
        //    };

        //    result = DBConnect.ExecuteNonQuery("SP_DELETE_STUDENTS", deleteparams);

        //    return result;
        //}
    }
}