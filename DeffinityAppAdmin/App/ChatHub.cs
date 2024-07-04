
using DeffinityManager;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;


namespace DeffinityAppAdmin.App
{
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, string> UserConnections = new ConcurrentDictionary<string, string>();

        public override Task OnConnected()
        {
            string userEmail = Context.QueryString["email"];
            if (!string.IsNullOrEmpty(userEmail))
            {
                UserConnections[userEmail] = Context.ConnectionId;
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userEmail = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(userEmail))
            {
                UserConnections.TryRemove(userEmail, out _);
            }
            return base.OnDisconnected(stopCalled);
        }

        public async Task SendMessage(string senderEmail, string receiverEmail, string message)
        {
            using (var context = new ChatsDataContext())
            {
                var newMessage = new Chat
                {
                    SenderEmail = senderEmail,
                    ReceiverEmail = receiverEmail,
                    Content = message,
                    Timestamp = DateTime.UtcNow
                };
                context.Chats.InsertOnSubmit(newMessage);
                context.SubmitChanges();
            }
            if (UserConnections.TryGetValue(receiverEmail, out string receiverConnectionId))
            {
                await Clients.Client(receiverConnectionId).receiveMessage(senderEmail, message);
            }
            else
            {
                // Handle the case where the receiver is not connected
                // e.g., store the message in a database for later delivery
            }
        }
    }
}


