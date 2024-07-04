using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeffinityManager
{
    public class Tags
    {
        public string ID { set; get; }
        public string Value { set; get; }
    }
        public  class TagsBAL
    {

        public static List<Tags> GetSMSTags()
        {
            List<Tags> list = new List<Tags>();
            list.Add(new Tags() { ID = "username", Value = "User Name" });
            list.Add(new Tags() { ID = "donationurl", Value = "Donation URL" });
            list.Add(new Tags() { ID = "instancename", Value = "Your Company Name" });
            list.Add(new Tags() { ID = "todaydate", Value = "Today" });
            list.Add(new Tags() { ID = "currentyear", Value = "Current Year" });
            list.Add(new Tags() { ID = "currentmonth", Value = "Current Month" });
            list.Add(new Tags() { ID = "currentday", Value = "Current Day" });
            return list;
        }
        public static List<Tags> GetDonationTags()
        {
            List<Tags> list = new List<Tags>();
            list.Add( new Tags() {  ID="todaysdate", Value= "Today’s Date" });
            list.Add(new Tags() { ID = "categorydonationamount", Value = "Donation Category Amount" });
            list.Add(new Tags() { ID = "categorydonationdate", Value = "Donation Category Date" });
            list.Add(new Tags() { ID = "category", Value = "Donation Category" });
            list.Add(new Tags() { ID = "donorfirstname", Value = "Donor First Name" });
            list.Add(new Tags() { ID = "donorsurname", Value = "Donor Surname" });
            list.Add(new Tags() { ID = "donorcompany", Value = "Donor Company Name" });
            list.Add(new Tags() { ID = "logo", Value = "Our Logo" });
            //list.Add(new Tags() { ID = "signature", Value = "Signature" });
            list.Add(new Tags() { ID = "currentyear", Value = "Current Year" });
            list.Add(new Tags() { ID = "currentmonth", Value = "Current Month" });
            list.Add(new Tags() { ID = "fundraiserdate", Value = "Fundraiser Date" });
            list.Add(new Tags() { ID = "fundraisername", Value = "Fundraiser Name" });
            list.Add(new Tags() { ID = "fundraiseramount", Value = "Fundraiser Amount" });
            list.Add(new Tags() { ID = "instancename", Value = "Your Company Name" });

            return list;
        }

        public static List<Tags> GetTemplageTags()
        {
            List<Tags> list = new List<Tags>();
            list.Add(new Tags() { ID = "todaysdate", Value = "Today’s Date" });
            //list.Add(new Tags() { ID = "categorydonationamount", Value = "Donation Category Amount" });
            //list.Add(new Tags() { ID = "categorydonationdate", Value = "Donation Category Date" });
            //list.Add(new Tags() { ID = "category", Value = "Donation Category" });
            list.Add(new Tags() { ID = "donorfirstname", Value = "Donor First Name" });
            list.Add(new Tags() { ID = "donorsurname", Value = "Donor Surname" });
            list.Add(new Tags() { ID = "donorcompany", Value = "Donor Company Name" });
            list.Add(new Tags() { ID = "logo", Value = "Our Logo" });
            //list.Add(new Tags() { ID = "signature", Value = "Signature" });
            list.Add(new Tags() { ID = "currentyear", Value = "Current Year" });
            list.Add(new Tags() { ID = "currentmonth", Value = "Current Month" });
            //list.Add(new Tags() { ID = "fundraiserdate", Value = "Fundraiser Date" });
            //list.Add(new Tags() { ID = "fundraisername", Value = "Fundraiser Name" });
            //list.Add(new Tags() { ID = "fundraiseramount", Value = "Fundraiser Amount" });
            list.Add(new Tags() { ID = "instancename", Value = "Your Company Name" });

            return list;
        }
        public static List<Tags> GetFundrisersTags()
        {
            List<Tags> list = new List<Tags>();
            list.Add(new Tags() { ID = "todaysdate", Value = "Today’s Date" });
            list.Add(new Tags() { ID = "logo", Value = "Our Logo" });
            list.Add(new Tags() { ID = "fundraisername", Value = "Fundraiser Name" });
            list.Add(new Tags() { ID = "fundraiseramount", Value = "Fundraiser Amount" });
            list.Add(new Tags() { ID = "fundraiserdate", Value = "Fundraiser Date" });
            list.Add(new Tags() { ID = "currentyear", Value = "Current Year" });
            list.Add(new Tags() { ID = "currentmonth", Value = "Fundraiser Date" });
            list.Add(new Tags() { ID = "currentmonth", Value = "Fundraiser Date" });
            list.Add(new Tags() { ID = "fundraiserdate", Value = "Current Month" });
            list.Add(new Tags() { ID = "instancename", Value = "Your Company Name" });

            return list;
        }

    }
}
