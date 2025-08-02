using API_Technology_Students_Manages.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace DevEduManager.Controllers
{
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
        private readonly DBConnect DBConnect = new DBConnect();

        [HttpGet]
        [Route("GetMessages")]
        public object GetMessages(int senderId, int receiverId)
        {
            object messages = new List<object>();

            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@SenderID", senderId),
                new SqlParameter("@ReceiverID", receiverId)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_MESSAGE", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                messages = dt;
                return messages;
            }

            return messages;
        }

        [HttpPost]
        [Route("SendMessage")]
        public bool SendMessage([FromBody] Message message)
        {
            bool result = false;

            SqlParameter[] parameters = {
                new SqlParameter("@SenderID", message.SenderID),
                new SqlParameter("@ReceiverID", message.ReceiverID),
                new SqlParameter("@MessageContent", (object)message.MessageContent ?? DBNull.Value),
                new SqlParameter("@SentDateTime", message.SentDateTime ?? DateTime.Now)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_MESSAGE", parameters);

            return result;
        }
    }

    public class Message
    {
        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string MessageContent { get; set; }
        public DateTime? SentDateTime { get; set; }
    }
}