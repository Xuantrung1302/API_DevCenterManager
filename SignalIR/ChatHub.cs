using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace API_Technology_Students_Manages.SignalIR
{
    public class ChatHub : Hub
    {
        public void SendMessage(Message message)
        {
            // Gửi tin nhắn đến người nhận cụ thể
            Clients.User(message.ReceiverID.ToString()).receiveMessage(message);
        }
    }
}