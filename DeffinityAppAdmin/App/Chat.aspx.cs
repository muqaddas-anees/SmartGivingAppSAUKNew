using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMgt.BAL;
using DeffinityManager; // Make sure to include the namespace of your DataContext

namespace PlegitVolunteerss
{
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateChatContacts();
            LoadChatMessages();
        }

        private void PopulateChatContacts()
        {
            int sid = 1; // Example SID value
            List<UserMgt.Entity.Contractor> contractors;
            String adminemail = sessionKeys.UName;


            using (ContractorsBAL contractorsBAL = new ContractorsBAL())
            {
                contractors = ContractorsBAL.Contractor_SelectBySID(sid);
            }

            foreach (var contractor in contractors)
            {
                if (contractor.SID == 1) {
                    continue;
                }
                var userHtml = $@"
<div class='d-flex flex-stack py-4 hover-effect' onclick='startchat({adminemail},{contractor.EmailAddress})'>
    <div class='d-flex align-items-center'>
        <div class='symbol symbol-45px symbol-circle'>
            <span class='symbol-label bg-light-danger text-danger fs-6 fw-bolder'>{contractor.ContractorName.Substring(0, 1)}</span>
            <div class='symbol-badge bg-success start-100 top-100 border-4 h-8px w-8px ms-n2 mt-n2'></div>
        </div>
        <div class='ms-5'>
            <a href='#' class='fs-5 fw-bold text-gray-900 text-hover-primary mb-2' id='contactname'>{contractor.ContractorName}</a>
            <div class='fw-semibold text-muted'>{contractor.LoginName}</div>
        </div>
    </div>
    <div class='d-flex flex-column align-items-end ms-2'>
</div>
</div>";



                kt_chat_contacts_body.InnerHtml += userHtml;
            }
        }

        private void LoadChatMessages()
        {
            using (ChatsDataContext context = new ChatsDataContext())
            {
                var messages = context.Chats.ToList(); // Fetch messages as needed

                // Serialize messages to JSON
                var serializer = new JavaScriptSerializer();
                string jsonMessages = serializer.Serialize(messages);
                String adminemail = sessionKeys.UName;

                // Create a new script element
                var scriptTag = new LiteralControl();
                scriptTag.Text = $@"
                <script type='text/javascript'>
                    var chatMessages = {jsonMessages};
                    console.log(chatMessages);
                    alert({adminemail});
                    // Now 'chatMessages' array is available client-side
                </script>";

                // Add the script tag to the page's Header or Body
                Page.Header.Controls.Add(scriptTag); // Or use Page.Form.Controls.Add(scriptTag) for adding to the body
            }
        }
    }
}
