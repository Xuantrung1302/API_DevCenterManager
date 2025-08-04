using API_Students_Manager.Models;
using API_Technology_Students_Manages.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Invoice")]
    public class InvoiceController : ApiController
    {
        DBConnect DBConnect = new DBConnect();

        // GET: api/Invoice/GetByStudentID
        [HttpGet]
        [Route("layThongTinHoaDonTheoMaHocVien")]
        public object GetByStudentID(string studentId = null)
        {
            object invoices = null;

            //object invoices = new object();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@StudentID", (object)studentId ?? DBNull.Value)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_INVOICE", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                invoices = dt;
                return invoices;
            }
            return invoices;
        }

        // POST: api/Invoice/Update
        [HttpPost]
        [Route("suaHoaDon")]
        public bool SuaHoaDon([FromBody] InvoiceModel data)
        {
            bool allSuccess = true;
            foreach (var invoiceId in data.InvoiceID)
            {
                SqlParameter[] updateParams = {
            new SqlParameter("@InvoiceID", invoiceId),
            new SqlParameter("@InvoiceDate", data.InvoiceDate),
            new SqlParameter("@DueDate", data.DueDate),
            new SqlParameter("@Amount", data.Amount),
            new SqlParameter("@Status", data.Status)
        };
                bool result = DBConnect.ExecuteNonQuery("SP_UPDATE_INVOICE", updateParams);
                if (!result) allSuccess = false;
            }
            return allSuccess;
        }
        [HttpPost]
        [Route("themHoaDon")]
        public bool ThemHoaDon([FromBody] InvoiceModel data)
        {
            bool result = false;
            string json = JsonConvert.SerializeObject(data);

            SqlParameter[] insertParam = {
                new SqlParameter("@json", json)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_INVOICE", insertParam);
            return result;
        }
        [HttpPost]
        [Route("xoaHoaDon")]
        public bool XoaHoaDon(string invoiceID)
        {
            bool result = false;

            SqlParameter[] deleteParams = {
                new SqlParameter("@InvoiceID", invoiceID)
            };

            result = DBConnect.ExecuteNonQuery("SP_DELETE_INVOICE", deleteParams);
            return result;
        }
        [HttpGet]
        [Route("kiemTraMaSinhVienTrongHoaDon")]
        public object KiemTraMaSinhVienTrongHoaDon(string studentId)
        {
            SqlParameter[] param = {
        new SqlParameter("@StudentID", (object)studentId ?? DBNull.Value)
         };

            DataTable dt = DBConnect.ExecuteQuery("SP_CHECK_STUDENT_IN_INVOICE", param);

            bool exists = false;
            if (dt != null && dt.Rows.Count > 0)
            {
                exists = Convert.ToInt32(dt.Rows[0]["Exists"]) == 1;
            }

            return new { exists };
        }
        // POST: api/Invoice/CalculateTuition
        /*      [HttpPost]
              [Route("CalculateTuition")]
              public bool CalculateTuition([FromBody] TuitionModel data)
              {
                  bool result = false;

                  SqlParameter[] calcParams = {
                      new SqlParameter("@StudentID", data.StudentID),
                      new SqlParameter("@SemesterID", data.SemesterID)
                  };

                  DataTable dt = DBConnect.ExecuteQuery("SP_CalculateTuitionBySemester", calcParams);
                  result = dt.Rows.Count > 0;

                  return result;
              }
        */
    }

    // Model cho POST Update
    public class InvoiceModel
    {
        public List<string> InvoiceID { get; set; } 
        public string StudentID { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public bool? DeleteFlg { get; set; }
        public string Status { get; set; }
        public Guid? CourseID { get; set; }
    }


    // Model cho POST CalculateTuition
    public class TuitionModel
    {
        public string StudentID { get; set; }
        public string SemesterID { get; set; }
    }
}