using API_Technology_Students_Manages.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/Notice")]
    public class PayrollController : ApiController
    {

        private readonly DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("GetPayroll")]
        public IHttpActionResult GetPayroll(int employeeId, int teacherId)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] selectParams = {
                    new SqlParameter("@EmployeeID", employeeId),
                    new SqlParameter("@TeacherID", teacherId)
                };

                dt = DBConnect.ExecuteQuery("SP_SELECT_PAYROLL", selectParams);

                if (dt?.Rows?.Count > 0)
                {
                    return Ok(dt);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("AddPayroll")]
        public IHttpActionResult AddPayroll([FromBody] Payroll payroll)
        {
            try
            {
                if (payroll == null || string.IsNullOrWhiteSpace(payroll.EmployeeID?.ToString()) || string.IsNullOrWhiteSpace(payroll.TeacherID?.ToString()))
                {
                    return BadRequest("EmployeeID and TeacherID are required.");
                }

                SqlParameter[] parameters = {
                    new SqlParameter("@EmployeeID", payroll.EmployeeID),
                    new SqlParameter("@TeacherID", payroll.TeacherID),
                    new SqlParameter("@WorkDays", payroll.WorkDays ?? (object)DBNull.Value),
                    new SqlParameter("@TeachingHours", payroll.TeachingHours ?? (object)DBNull.Value),
                    new SqlParameter("@RecordDate", payroll.RecordDate ?? DateTime.Now.Date) // Mặc định ngày hiện tại nếu không có
                };

                bool result = DBConnect.ExecuteNonQuery("SP_INSERT_PAYROLL", parameters);

                if (result)
                {
                    return Ok("Payroll record added successfully.");
                }
                return InternalServerError(new Exception("Failed to add payroll record."));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    public class Payroll
    {
        public int? PayrollID { get; set; }
        public int? EmployeeID { get; set; }
        public int? TeacherID { get; set; }
        public int? WorkDays { get; set; }
        public decimal? TeachingHours { get; set; }
        public DateTime? RecordDate { get; set; }
    }
}