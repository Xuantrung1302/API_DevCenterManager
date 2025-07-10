using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace API_Technology_Students_Manages.Controllers
{
    [RoutePrefix("api/VNPay")]
    public class VNPayController : ApiController
    {
        [HttpPost]
        [Route("CreatePaymentUrl")]
        public IHttpActionResult CreatePaymentUrl([FromBody] VNPayRequestModel request)
        {
            try
            {
                string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
                string vnp_TmnCode = "2QXUI4J4";
                string vnp_HashSecret = "SECRETKEY123456789";
                string vnp_Returnurl = "http://localhost:3000/payment-success";

                // Tạo các tham số theo tài liệu VNPAY
                var vnp_Params = new SortedDictionary<string, string>();
                vnp_Params.Add("vnp_Version", "2.1.0");
                vnp_Params.Add("vnp_Command", "pay");
                vnp_Params.Add("vnp_TmnCode", vnp_TmnCode);
                vnp_Params.Add("vnp_Amount", (request.amount * 100).ToString());
                vnp_Params.Add("vnp_CurrCode", "VND");
                vnp_Params.Add("vnp_TxnRef", DateTime.Now.Ticks.ToString());
                vnp_Params.Add("vnp_OrderInfo", request.orderInfo ?? "Thanh toan don hang");
                vnp_Params.Add("vnp_OrderType", "other");
                vnp_Params.Add("vnp_Locale", "vn");
                vnp_Params.Add("vnp_ReturnUrl", vnp_Returnurl);
                vnp_Params.Add("vnp_IpAddr", "127.0.0.1");
                vnp_Params.Add("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

                // Tạo chuỗi dữ liệu để hash
                var hashData = string.Join("&", vnp_Params.Select(kv => kv.Key + "=" + kv.Value));
                string vnp_SecureHash = CreateMD5Hash(hashData + vnp_HashSecret);

                // Tạo URL thanh toán
                string paymentUrl = vnp_Url + "?" + hashData + "&vnp_SecureHash=" + vnp_SecureHash;

                return Ok(new { success = true, paymentUrl });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        // Hàm tạo MD5 hash
        private string CreateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }

    // Model yêu cầu tạo thanh toán
    public class VNPayRequestModel
    {
        public long amount { get; set; }
        public string orderInfo { get; set; }
    }
}