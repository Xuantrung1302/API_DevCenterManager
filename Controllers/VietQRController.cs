using System.Web.Http;
using System.Web.Http.Cors;
using System.Web;

namespace API_Technology_Students_Manages.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/VietQR")]
    public class VietQRController : ApiController
    {
        [HttpPost]
        [Route("CreateQR")]
        public IHttpActionResult CreateQR([FromBody] VietQRRequestModel request)
        {
            try
            {
                string bankId = request.bankId.ToLower(); 
                string accountNo = request.accountNo;
                string template = "compact2";
                string amount = request.amount.ToString();
                string addInfo = HttpUtility.UrlEncode(request.addInfo);
                string accountName = HttpUtility.UrlEncode(request.accountName);

                string qrImageUrl = $"https://img.vietqr.io/image/{bankId}-{accountNo}-{template}.png?amount={amount}&addInfo={addInfo}&accountName={accountName}";

                return Ok(new { success = true, qrImageUrl = qrImageUrl });
            }
            catch (System.Exception ex)
            {
                return Ok(new { success = false, message = "Lỗi tạo QR: " + ex.Message });
            }
        }
    }

    public class VietQRRequestModel
    {
        public string bankId { get; set; }    
        public string accountNo { get; set; }
        public string accountName { get; set; }
        public long amount { get; set; }
        public string addInfo { get; set; }
    }
}