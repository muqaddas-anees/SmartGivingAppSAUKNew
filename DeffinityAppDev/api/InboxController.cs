using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserMgt.DAL;
using UserMgt.Entity;
using DeffinityManager;
using System.Web.SessionState;
using System.Web;

namespace DeffinityAppDev.api
{
    public class InboxController : ApiController
    {
        // GET: api/Inbox
       
        public string Get()
        {
            return "value";
        }
        [Route("api/inbox/getlist")]
        [HttpGet]
        public IEnumerable<UserMgt.Entity.InboxMessage> getlist()
        {
            var InboxList = InboxBAL.BindUserInbox(sessionKeys.UID).Take(4).ToList();
            return InboxList;
        }
        // GET: api/Inbox/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Inbox
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Inbox/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Inbox/5
        public void Delete(int id)
        {
        }
    }
}
