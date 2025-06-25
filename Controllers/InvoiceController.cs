using API_Technology_Students_Manages.DataAccess;
using System;
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
        [Route("GetInvoiceByStudentID")]
        public object GetByStudentID(string studentId = null)
        {
            object invoices = new object();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@StudentID", (object)studentId ?? DBNull.Value)
            };

            dt = DBConnect.ExecuteQuery("SP_GetInvoiceByStudentID", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                invoices = dt;
                return invoices;
            }
            return invoices;
        }

        // POST: api/Invoice/Update
        [HttpPost]
        [Route("UpdateInvoice")]
        public bool Update([FromBody] InvoiceModel data)
        {
            bool result = false;

            SqlParameter[] updateParams = {
                new SqlParameter("@InvoiceID", data.InvoiceID),
                new SqlParameter("@InvoiceDate", data.InvoiceDate),
                new SqlParameter("@Amount", data.Amount),
                new SqlParameter("@Paid", data.Paid)
            };

            result = DBConnect.ExecuteNonQuery("SP_UpdateInvoice", updateParams);
            return result;
        }

        // POST: api/Invoice/CalculateTuition
        [HttpPost]
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
    }

    // Model cho POST Update
    public class InvoiceModel
    {
        public string InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
    }

    // Model cho POST CalculateTuition
    public class TuitionModel
    {
        public string StudentID { get; set; }
        public string SemesterID { get; set; }
    }
}