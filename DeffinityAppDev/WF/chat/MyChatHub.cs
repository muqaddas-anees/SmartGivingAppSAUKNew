using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using UserMgt.Entity;

namespace DeffinityAppDev.WF.Admin.chat
{
    public class MyChatHub : Hub
    {   

        //List of the chat members
        public class UserDetail1
        {
            public string ConnectionId { get; set; }
            public string UserName { get; set; }
            public int userid { get; set; }
        }
        public class MessageDetail1
        {

            public string UserName { get; set; }

            public string Message { get; set; }

        }
        static List<UserDetail1> ConnectedUsers = new List<UserDetail1>();
        static List<MessageDetail1> CurrentMessage = new List<MessageDetail1>();



        public void Connect(string userName, int uid)
        {

            var id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                if (ConnectedUsers.Where(i => i.UserName != userName).Count() >= 0)
                {
                    ConnectedUsers.Add(new UserDetail1 { ConnectionId = id, UserName = userName, userid = uid });

                    // send to caller
                    Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage, uid);

                    // send to all except caller client
                    Clients.AllExcept(id).onNewUserConnected(id, userName, uid);

                }

            }


        }

        public int checkuserstatus(int id, string name)
        {
            var count = ConnectedUsers.Count(u => u.userid == id);
            if (count != 0)
            {
                Clients.Caller.userstatusonline(id, name);
            }
            else
            {
                Clients.Caller.userstatusoffline(id, name);
            }
            return count;
        }


        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);
                if (ConnectedUsers.Where(u => u.userid == item.userid).Count() == 0)
                {
                    var id = item.userid.ToString();
                    Clients.All.onUserDisconnected(id, item.UserName);
                }
            }
            return base.OnDisconnected(stopCalled);
        }



        public void SendPrivateMessage(string toUserId, string message, string fromuid)
        {
            string fromUserId = Context.ConnectionId;

            string strfromUserId = (ConnectedUsers.Where(u => u.ConnectionId == Context.ConnectionId).Select(u => u.userid).FirstOrDefault()).ToString();

            int _fromUserId = 0;
            int.TryParse(strfromUserId, out _fromUserId);
            int _toUserId = 0;
            int.TryParse(toUserId, out _toUserId);
            List<UserDetail1> FromUsers = ConnectedUsers.Where(u => u.userid == _fromUserId).ToList();
            List<UserDetail1> ToUsers = ConnectedUsers.Where(x => x.userid == _toUserId).ToList();

            if (FromUsers.Count != 0 && ToUsers.Count() != 0)
            {
                foreach (var ToUser in ToUsers)
                {
                    // send to                                                                                            
                    Clients.Client(ToUser.ConnectionId).sendPrivateMessage(_fromUserId.ToString(), FromUsers[0].UserName, message, fromuid);
                }


                foreach (var FromUser in FromUsers)
                {
                    // send to caller user                                                                                
                    Clients.Client(FromUser.ConnectionId).sendPrivateMessage(_toUserId.ToString(), FromUsers[0].UserName, message, fromuid);
                }

            }

        }
    }
}
