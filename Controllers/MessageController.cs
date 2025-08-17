using API_Technology_Students_Manages.DataAccess;
using API_Technology_Students_Manages.SignalIR;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

[RoutePrefix("api/Message")]
public class MessageController : ApiController
{
    private readonly DBConnect DBConnect = new DBConnect();
    private readonly IHubContext chatHub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();

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

        if (result)
        {
            // Gửi tin nhắn qua SignalR đến receiver
            chatHub.Clients.Group(message.ReceiverID).receiveMessage(message);
        }

        return result;
    }


    [HttpPost]
    [Route("taoCuocTroChuyen")]
    public bool TaoCuocTroChuyen([FromBody] Message message)
    {
        bool result = false;

        SqlParameter[] parameters = {
            new SqlParameter("@SenderID", message.SenderID),
            new SqlParameter("@ReceiverID", message.ReceiverID),
            new SqlParameter("@MessageContent", (object)message.MessageContent ?? DBNull.Value),
            new SqlParameter("@SentDateTime", message.SentDateTime)
        };

        result = DBConnect.ExecuteNonQuery("SP_CreateConversation", parameters);

        if (result)
        {
            // Gửi tin nhắn qua SignalR đến receiver
            chatHub.Clients.Group(message.ReceiverID).receiveMessage(message);
        }

        return result;
    }

    [HttpGet]
    [Route("GetMessages")]
    public object GetMessages(string senderId, string receiverId)
    {
        try
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@SenderID", senderId),
                new SqlParameter("@ReceiverID", receiverId)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_MESSAGE", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
    [HttpGet]
    [Route("GetAccountsByRole")]
    public object GetAccountsByRole(string roleId, string CurrentUserID)
    {
        try
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@RoleId", roleId),
                new SqlParameter("@CurrentUserID", CurrentUserID)
            };

            dt = DBConnect.ExecuteQuery("SP_SELECT_ACCOUNT_BY_ROLE", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
    [HttpGet]
    [Route("layDanhSachTaiKhoanDaNhanTin")]
    public object LayDanhSachTaiKhoanDaNhanTin(string CurrentUserID)
    {
        try
        {
            object lop = new List<object>();
            DataTable dt = new DataTable();
            SqlParameter[] selectParams = {
                new SqlParameter("@CurrentUserID", CurrentUserID)
            };

            dt = DBConnect.ExecuteQuery("SP_GetChatContactsByUserID", selectParams);

            if (dt?.Rows?.Count > 0)
            {
                lop = dt;
                return lop;
            }
            return lop;
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

}

public class Message
{
    public string MessageID { get; set; }
    public string SenderID { get; set; }
    public string ReceiverID { get; set; }
    public string MessageContent { get; set; }
    public DateTime? SentDateTime { get; set; }
}