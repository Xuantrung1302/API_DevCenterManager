using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace API_Technology_Students_Manages.SignalIR
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            string userId = Context.QueryString["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                Groups.Add(Context.ConnectionId, userId);
            }
            return base.OnConnected();
        }

        public void SendMessage(Message message)
        {
            Clients.Group(message.ReceiverID).receiveMessage(message);
        }
    }

}