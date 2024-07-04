using PortfolioMgt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace DC.BAL
{
    /// <summary>
    /// Summary description for RequestMailFormat
    /// </summary>
    public class RequestMailFormat
    {
        //public RequestMailFormat()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}
        public static string DeliveryHtmlMailDetails(int callID)
        {
            StringBuilder retString = new StringBuilder();
            using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
            {
                using (PortfolioMgt.DAL.PortfolioDataContext pd = new PortfolioDataContext())
                {
                    var callDetails = dc.CallDetails.Where(o => o.ID == callID).FirstOrDefault();
                    var deliveryDetails = dc.DeliveryInformations.Where(o => o.CallID == callID).FirstOrDefault();
                    var recivedDetails = dc.RecievedInformations.Where(o => o.CallID == callID).FirstOrDefault();
                    var noteslist = dc.Notes.Where(c => c.CallID == callID).ToList();
                    retString.Append(MailHtmlRow("Customer:", pd.ProjectPortfolios.Where(o=>o.ID == callDetails.CompanyID).Select(o=>o.PortFolio).FirstOrDefault()));
                    retString.Append(MailHtmlRow("Site information:", dc.OurSites.Where(o=>o.ID == callDetails.SiteID).Select(o=>o.Name).FirstOrDefault()));
                    retString.Append(MailHtmlRow("Delivery Reference:", callID.ToString()));
                    //retString.Append(MailHtmlRow("Details of the Delivery Request:"));
                    retString.Append(MailHtmlRow("Status of Delivery:", dc.Status.Where(o=>o.ID== callDetails.StatusID).Select(o=>o.Name).FirstOrDefault()));
                    retString.Append(MailHtmlRow("Anticipated Date of Delivery:", deliveryDetails.ArrivalDate.Value.ToShortDateString()));
                    retString.Append(MailHtmlRow("Weight:",   dc.DeliveryItemWeights.Where(o=>o.ID== deliveryDetails.ItemWeight).Select(o=>o.Weight_Value).FirstOrDefault()));
                    retString.Append(MailHtmlRow("Number of Boxes expected:", (deliveryDetails.NumofBoxes.HasValue ? deliveryDetails.NumofBoxes.Value : 0).ToString()));
                    if(recivedDetails != null)
                    {
                        if ((recivedDetails.NumofBoxesRec.HasValue ? recivedDetails.NumofBoxesRec : 0) >0)
                        retString.Append(MailHtmlRow("Number of boxes received:", (recivedDetails.NumofBoxesRec.HasValue?recivedDetails.NumofBoxesRec:0).ToString()));
                        if ((recivedDetails.NumofBoxesCollected.HasValue ? recivedDetails.NumofBoxesCollected : 0) >0)
                        retString.Append(MailHtmlRow("Number of boxes collected:", (recivedDetails.NumofBoxesCollected.HasValue?recivedDetails.NumofBoxesCollected:0).ToString()));
                    }
                    retString.Append(MailHtmlRow("Courier Number:", deliveryDetails.CourierNumber));
                    retString.Append(MailHtmlRow("Courier Company:", deliveryDetails.CourierCompany));
                    retString.Append(MailHtmlRow("Delivery Type:", dc.DeliveryTypes.Where(o=>o.ID == deliveryDetails.DeliveryTypeID).Select(o=>o.Name).FirstOrDefault()));
                    retString.Append(MailHtmlRow("Over 1 Pallet:", (deliveryDetails.Pallet.HasValue?(deliveryDetails.Pallet.Value?"Yes":"No"):"No").ToString()));
                    //retString.Append(MailHtmlRow("Over [kgs]kg Weight:", dc.DeliveryItemWeights.Where(o=>o.ID == deliveryDetails.ItemWeight).Select(o=>o.Weight_Value).FirstOrDefault()));
                    var pcontact = pd.PortfolioContacts.Where(o=>o.ID == callDetails.RequesterID).FirstOrDefault();
                    retString.Append(MailHtmlRow("Contact:", pcontact.Name));
                    retString.Append(MailHtmlRow("Contact Number:", pcontact.Mobile));
                    retString.Append(MailHtmlRow("Description:", deliveryDetails.Description));
                    retString.Append(MailHtmlRow("Notes:", deliveryDetails.Notes ));
                    retString.Append(MailHtmlRow(string.Empty ));
                   
                    retString.Append(MailHtmlRow(GetNotesList(noteslist)));
                    
                }

            }

            return retString.ToString();
        }
        public static string MailHtmlRow(string key, string value)
        {
            return string.Format("<tr><td style='padding-top: 10px;'>{0}</td><td style='padding-top: 10px;'><b>{1}</b></td></tr>", key, value);
        }
        public static string MailHtmlRow(string key)
        {
            return string.Format("<tr><td style='padding-top: 10px;' colspan='2'>{0}</td></tr>", key);
        }
       
        public static string GetNotesList(int callid)
        {
            using (DC.DAL.DCDataContext dc = new DC.DAL.DCDataContext())
            {
               var noteslist = dc.Notes.Where(c => c.CallID == callid).ToList();
               
               return GetNotesList(noteslist);
            }
        }
        public static string GetNotesList(List<DC.Entity.Note> noteslist)
        {
            StringBuilder sbuild = new StringBuilder();
            if (noteslist.Count > 0)
            {
                UserMgt.BAL.ContractorsBAL cCollection = new UserMgt.BAL.ContractorsBAL();
                var uids = noteslist.Select(p => p.LoggedBy).ToArray();
                var usercollection = cCollection.Contractor_SelectAll().Where(p => uids.Contains(p.ID)).ToList();
                sbuild.Append("<table style='width:100%'>");
                sbuild.Append("<tr style='background-color:silver;color:white;text-align:center;font-weight:bold;'>");
                sbuild.Append("<td style='width:50%'>Notes</td><td>Logged by</td><td>Date & Time</td>");
                sbuild.Append("</tr>");
                foreach (DC.Entity.Note n in noteslist)
                {
                    sbuild.Append("<tr>");
                    sbuild.Append(string.Format("<td>{0}</td><td>{1}</td><td>{2}</td>", n.Notes, usercollection.Where(p => p.ID == n.LoggedBy).Select(p => p.ContractorName).FirstOrDefault(), string.Format(Deffinity.systemdefaults.GetStringDateTimeformat(), n.LoggedDate.Value)));

                    sbuild.Append("</tr>");
                }
                sbuild.Append("</table>");
            }

            return sbuild.ToString();
        }
    }

    
}

//namespace DC.Entity
//{
//    public class NotesList
//    {
//        public int ID { get; set; }
//        public string UserName { get; set; }
//        public DateTime? DateTime { get; set; }
//        public string Notes { get; set; }
//    }
//}