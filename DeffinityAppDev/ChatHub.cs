
using DeffinityManager;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;


namespace DeffinityAppDev.App
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
            try
            {
                /*using (var context = new ChatsDataContext())
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
                }*/

                if (UserConnections.TryGetValue(receiverEmail, out string receiverConnectionId))
                {
                    await Clients.Client(receiverConnectionId).receiveMessage(senderEmail, message);
                }
                else
                {
                    // Store the message in a database or message queue for later delivery
                    // e.g., using a message broker like RabbitMQ or Azure Service Bus
                    Console.WriteLine($"Receiver {receiverEmail} is not connected. Storing message for later delivery.");
                    // TO DO: implement message storage and delivery logic
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                // TO DO: implement error handling and logging logic
            }
        }
    }
}


