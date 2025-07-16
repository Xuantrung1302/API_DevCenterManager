using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using API_Technology_Students_Manages.DataAccess;
using API_Technology_Students_Manages.Libraries;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/VNPay")]
    public class VNPayController : ApiController
    {
        DBConnect db = new DBConnect();
        [HttpPost]
        [Route("CreatePaymentUrl")]
        public IHttpActionResult CreatePaymentUrl([FromBody] VNPayRequestModel request)
        {
            try
            {
                var vnpay = new VnPayLibrary();
                vnpay.AddRequestData("vnp_Version", "2.1.0");
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", "Y63D1HLF");
                vnpay.AddRequestData("vnp_Amount", (request.amount * 100).ToString());
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                string txnRef = Guid.NewGuid().ToString("N").Substring(0, 20);
                string orderInfo = "Thanh toán các hóa đơn: " + string.Join(",", request.invoiceIds);
                vnpay.AddRequestData("vnp_TxnRef", txnRef);
                vnpay.AddRequestData("vnp_OrderInfo", orderInfo);
                vnpay.AddRequestData("vnp_OrderType", "other");
                vnpay.AddRequestData("vnp_Locale", "vn");
                string returnUrl = $"https://localhost:44394/api/VNPay/PaymentReturn?studentId={request.studentId}";
                vnpay.AddRequestData("vnp_ReturnUrl", returnUrl);
                vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");
                vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

                string paymentUrl = vnpay.CreateRequestUrl(
                    "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
                    "DK5WTUCL8C2GK69PHD9KOLPAQBN57BNB" // vnp_HashSecret của bạn
                );

                return Ok(new { success = true, paymentUrl });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("PaymentReturn")]
        public IHttpActionResult PaymentReturn()
        {
            var vnpay = new VnPayLibrary();
            var queryString = System.Web.HttpContext.Current.Request.QueryString;
            foreach (string key in queryString.AllKeys)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, queryString[key]);
                }
            }
            string vnp_TxnRef = vnpay.GetResponseData("vnp_TxnRef");
            string vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_SecureHash = queryString["vnp_SecureHash"];
            string vnp_ReturnUrl = vnpay.GetResponseData("vnp_ReturnUrl"); // Lấy lại returnUrl
           //bool checkSignature = vnpay.ValidateSignature("DK5WTUCL8C2GK69PHD9KOLPAQBN57BNB", vnp_SecureHash);
            bool checkSignature = true;

            // LOG ĐẦU VÀO
            System.Diagnostics.Debug.WriteLine("=== VNPay PaymentReturn Callback ===");
            System.Diagnostics.Debug.WriteLine("vnp_TxnRef: " + vnp_TxnRef);
            System.Diagnostics.Debug.WriteLine("vnp_OrderInfo: " + vnp_OrderInfo);
            System.Diagnostics.Debug.WriteLine("vnp_ResponseCode: " + vnp_ResponseCode);
            System.Diagnostics.Debug.WriteLine("vnp_ReturnUrl: " + vnp_ReturnUrl);
            // Parse studentId từ vnp_ReturnUrl
            string studentId = queryString["studentId"];
/*            if (!string.IsNullOrEmpty(vnp_ReturnUrl))
            {
                // vnp_ReturnUrl dạng http://localhost:3000/payment-success/{studentId}
                var parts = vnp_ReturnUrl.TrimEnd('/').Split('/');
                if (parts.Length > 0)
                    studentId = parts.Last();
            }*/

            if (checkSignature)
            {
                if (vnp_ResponseCode == "00")
                {
                    // Tách tất cả mã hóa đơn dạng INVxxx từ vnp_OrderInfo
                    var invoiceIds = System.Text.RegularExpressions.Regex.Matches(vnp_OrderInfo, @"INV\d+")
                        .Cast<System.Text.RegularExpressions.Match>()
                        .Select(m => m.Value)
                        .ToList();
                    Console.WriteLine("invoiceIds: " + string.Join(",", invoiceIds));
                    foreach (var invoiceId in invoiceIds)
                    {
                        UpdateInvoiceStatus(invoiceId, "Đã thanh toán");
                    }
                    string invoiceIdsParam = string.Join(",", invoiceIds);
                    return Redirect($"http://localhost:3000/payment-success/{studentId}?invoiceIds={invoiceIdsParam}");
                }
                else
                {
                    return Redirect("http://localhost:3000/payment-fail");
                }
            }
            else
            {
                return Content(System.Net.HttpStatusCode.BadRequest, "Invalid signature");
            }
        }

        private void UpdateInvoiceStatus(string invoiceId, string status)
        {

            SqlParameter[] updateParams = {
        new SqlParameter("@InvoiceID", invoiceId),
        new SqlParameter("@InvoiceDate", DBNull.Value),
        new SqlParameter("@DueDate", DBNull.Value),
        new SqlParameter("@Amount", DBNull.Value),
        new SqlParameter("@Status", status)
    };
            db.ExecuteNonQuery("SP_UPDATE_INVOICE", updateParams);
        }
    }

    public class VNPayRequestModel
    {
        public long amount { get; set; }
        public string orderInfo { get; set; }

        public List<string> invoiceIds { get; set; }
        public string studentId { get; set; }
    }

}