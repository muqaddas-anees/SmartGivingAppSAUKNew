using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;

/// <summary>
/// Summary description for TimeSheet_methods
/// </summary>
public class TimeSheet_methods
{
    public TimeSheet_methods()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public void FillTextBoxes(DataTable dtVal, System.Web.UI.WebControls.GridView gvTimeSheet)
    //{

    //    int Rows = dtVal.Rows.Count;
    //    int rowID = 0;
    //  int cid=0;
    //    int rowIDTemp = -1;
    //    string txthour = "";
    //    for (int ir = 0; ir < Rows; ir++)
    //    {
  
    //        rowID = ir;

    //        if (rowIDTemp == -1)
    //        {
    //            rowIDTemp = rowID;
    //        }
           
    //        if (dtVal.Rows[ir]["CID"].ToString() != "")
    //        {
    //            cid = Convert.ToInt32(dtVal.Rows[ir]["CID"].ToString());
    //        }
    //        else
    //        {
    //            if (cid == 6)
    //            {
    //                cid = 0;
    //            }
    //            else
    //            {
    //                cid = cid + 1;
    //            }
    //        }
    //        //string hour = dt.Rows[ir]["Hours"].ToString();
    //        txthour = dtVal.Rows[ir]["Hours"].ToString();
    //        System.Web.UI.WebControls.TextBox txtCID = (System.Web.UI.WebControls.TextBox)gvTimeSheet.Rows[rowID].FindControl("txtHours" + cid.ToString());
    //        txthour = txthour.Replace(".", ":");
    //        txtCID.Text = txthour;



    //    }
    //    for (int ir = 0; ir < Rows; ir++)
    //    {
    //        string ID = dtVal.Rows[ir]["ID"].ToString().Trim();
    //        double[] hourColsNew = new double[7];
    //        if (ID != null && ID != "")
    //        {

    //            for (int k = 0; k < 7; k++)
    //            {
    //                System.Web.UI.WebControls.TextBox txtRowID = (System.Web.UI.WebControls.TextBox)gvTimeSheet.Rows[ir].FindControl("txtHours" + k.ToString());
    //                if (txtRowID.Text.Trim() != "00:00")
    //                {
    //                    txtRowID.Text = txtRowID.Text.Trim();

    //                }


    //            }
    //        }
    //    }

    //}
    public bool CalculateTotal(System.Web.UI.WebControls.GridView gvTimeSheet, DateTime eDate1)
    {
        bool flag = true;
        double Hours = 0;

        DateTime tempDate = new DateTime();
        double[] totalHourColsNew = new double[7];
        int gvTimeSheetCount = gvTimeSheet.Rows.Count;
        //Total on each rows
        for (int x = 0; x < gvTimeSheetCount; x++)
        {
            int j = 0;
            DateTime DateEntered;
            DateTime max_Total = new DateTime();
            //DateTime DisplayTotalHour = new DateTime();
            System.Web.UI.WebControls.GridViewRow eachrow = gvTimeSheet.Rows[x];
            double[] hourColsNew = new double[7];
            string Minute = null;
            for (int i = 0; i < 6; i++)
            {
                string id = "txtHours" + (i).ToString();
                string val = ((System.Web.UI.WebControls.TextBox)eachrow.FindControl(id)).Text.Trim();
                //val = val.Replace(":", ".");
                //Hours = Convert.ToDouble(val);
                string[] strColArray = val.Split(':');

                max_Total = max_Total.AddHours(Convert.ToDouble(strColArray[0]));
                max_Total = max_Total.AddMinutes(Convert.ToDouble(strColArray[1]));
                //DateEntered = eDate1.Date.AddDays(j);
                //Minute = strColArray[1].ToString();

            }
            double totalRowTimes;
            if (max_Total.Day > 1)
            {
                totalRowTimes = Convert.ToDouble(max_Total.Hour) + ((max_Total.Day - 1) * 24);
                totalRowTimes = Convert.ToDouble(totalRowTimes.ToString() + "." + max_Total.Minute);
            }
            else
            {
                totalRowTimes = Convert.ToDouble(max_Total.Hour);
                totalRowTimes = Convert.ToDouble(totalRowTimes.ToString() + "." + max_Total.Minute);
            }

            //((System.Web.UI.WebControls.TextBox)eachrow.FindControl("txtTotal")).Text = totalRowTimes.ToString().Replace(".", ":");
            System.Web.UI.WebControls.TextBox txtRowTotalvalue = ((System.Web.UI.WebControls.TextBox)eachrow.FindControl("txtTotal"));
            if (totalRowTimes.ToString().Contains('.'))
            {
                string[] tempTime = totalRowTimes.ToString().Split('.');

                if (tempTime[0].Length < 2)
                {
                    tempTime[0] = "0" + tempTime[0];
                }
                if (tempTime[1].Length < 2)
                {

                    tempTime[1] = tempTime[1] + "0";
                }
                txtRowTotalvalue.Text = tempTime[0] + ":" + tempTime[1];
            }
            else if (totalRowTimes == 0)
            {
                txtRowTotalvalue.Text = "00:00";
            }
            else
            {
                txtRowTotalvalue.Text = totalRowTimes.ToString() + ":00";
            }



        }


        //Total On Footer
        DateTime FooterTotal0 = new DateTime();
        DateTime FooterTotal1 = new DateTime();
        DateTime FooterTotal2 = new DateTime();
        DateTime FooterTotal3 = new DateTime();
        DateTime FooterTotal4 = new DateTime();
        DateTime FooterTotal5 = new DateTime();
        DateTime FooterTotal6 = new DateTime();
        for (int k = 0; k < gvTimeSheetCount; k++)
        {

            System.Web.UI.WebControls.GridViewRow eachrow = gvTimeSheet.Rows[k];
            string id = "txtHours0";
            string val = ((System.Web.UI.WebControls.TextBox)eachrow.FindControl(id)).Text.Trim();
            string[] strArray0 = val.Split(':');
            FooterTotal0 = FooterTotal0.AddHours(Convert.ToDouble(strArray0[0]));
            FooterTotal0 = FooterTotal0.AddMinutes(Convert.ToDouble(strArray0[1]));



            string id1 = "txtHours1";
            string val1 = ((System.Web.UI.WebControls.TextBox)eachrow.FindControl(id1)).Text.Trim();
            string[] strArray1 = val1.Split(':');
            FooterTotal1 = FooterTotal1.AddHours(Convert.ToDouble(strArray1[0]));
            FooterTotal1 = FooterTotal1.AddMinutes(Convert.ToDouble(strArray1[1]));

            string val2 = ((System.Web.UI.WebControls.TextBox)eachrow.FindControl("txtHours2")).Text.Trim();
            string[] strArray2 = val2.Split(':');
            FooterTotal2 = FooterTotal2.AddHours(Convert.ToDouble(strArray2[0]));
            FooterTotal2 = FooterTotal2.AddMinutes(Convert.ToDouble(strArray2[1]));

            string val3 = ((System.Web.UI.WebControls.TextBox)eachrow.FindControl("txtHours3")).Text.Trim();
            string[] strArray3 = val3.Split(':');
            FooterTotal3 = FooterTotal3.AddHours(Convert.ToDouble(strArray3[0]));
            FooterTotal3 = FooterTotal3.AddMinutes(Convert.ToDouble(strArray3[1]));

            string val4 = ((System.Web.UI.WebControls.TextBox)eachrow.FindControl("txtHours4")).Text.Trim();
            string[] strArray4 = val4.Split(':');
            FooterTotal4 = FooterTotal4.AddHours(Convert.ToDouble(strArray4[0]));
            FooterTotal4 = FooterTotal4.AddMinutes(Convert.ToDouble(strArray4[1]));

            string val5 = ((System.Web.UI.WebControls.TextBox)eachrow.FindControl("txtHours5")).Text.Trim();
            string[] strArray5 = val5.Split(':');
            FooterTotal5 = FooterTotal5.AddHours(Convert.ToDouble(strArray5[0]));
            FooterTotal5 = FooterTotal5.AddMinutes(Convert.ToDouble(strArray5[1]));

            string val6 = ((System.Web.UI.WebControls.TextBox)eachrow.FindControl("txtHours6")).Text.Trim();
            string[] strArray6 = val6.Split(':');
            FooterTotal6 = FooterTotal6.AddHours(Convert.ToDouble(strArray6[0]));
            FooterTotal6 = FooterTotal6.AddMinutes(Convert.ToDouble(strArray6[1]));


        }
        for (int m = 0; m < gvTimeSheetCount; m++)
        {
            System.Web.UI.WebControls.GridViewRow eachrow = gvTimeSheet.Rows[m];
            double totalRowTimes0;
            if (FooterTotal0.Day > 1)
            {
                flag = false;
                totalRowTimes0 = Convert.ToDouble(FooterTotal0.Hour) + ((FooterTotal0.Day - 1) * 24);
                totalRowTimes0 = Convert.ToDouble(totalRowTimes0.ToString() + "." + FooterTotal0.Minute);
            }
            else
            {
                totalRowTimes0 = Convert.ToDouble(FooterTotal0.Hour);
                totalRowTimes0 = Convert.ToDouble(totalRowTimes0.ToString() + "." + FooterTotal0.Minute);
            }
            System.Web.UI.WebControls.TextBox txtFooterTotal = (System.Web.UI.WebControls.TextBox)gvTimeSheet.FooterRow.FindControl("txtTotal0");
            //txtFooterTotal.Text = totalRowTimes0.ToString().Replace(".", ":");
            if (totalRowTimes0.ToString().Contains('.'))
            {
                string[] tempTime = totalRowTimes0.ToString().Split('.');

                if (tempTime[0].Length < 2)
                {
                    tempTime[0] = "0" + tempTime[0];
                }
                if (tempTime[1].Length < 2)
                {
                    tempTime[1] = tempTime[1] + "0";
                }
                txtFooterTotal.Text = tempTime[0] + ":" + tempTime[1];
            }
            else if (totalRowTimes0 == 0)
            {
                txtFooterTotal.Text = "00:00";
            }
            else
            {
                txtFooterTotal.Text = totalRowTimes0.ToString() + ":00";
            }


            if (FooterTotal1.Day > 1)
            {
                flag = false;
            }
            double totalRowTimes1;
            if (FooterTotal1.Day > 1)
            {
                totalRowTimes1 = Convert.ToDouble(FooterTotal1.Hour) + ((FooterTotal1.Day - 1) * 24);
                totalRowTimes1 = Convert.ToDouble(totalRowTimes1.ToString() + "." + FooterTotal1.Minute);
            }
            else
            {
                totalRowTimes1 = Convert.ToDouble(FooterTotal1.Hour);
                totalRowTimes1 = Convert.ToDouble(totalRowTimes1.ToString() + "." + FooterTotal1.Minute);
            }
            System.Web.UI.WebControls.TextBox txtFooterTotal1 = (System.Web.UI.WebControls.TextBox)gvTimeSheet.FooterRow.FindControl("txtTotal1");

            if (totalRowTimes1.ToString().Contains('.'))
            {
                string[] tempTime = totalRowTimes1.ToString().Split('.');

                if (tempTime[0].Length < 2)
                {
                    tempTime[0] = "0" + tempTime[0];
                }
                if (tempTime[1].Length < 2)
                {
                    tempTime[1] = tempTime[1] + "0";
                }
                txtFooterTotal1.Text = tempTime[0] + ":" + tempTime[1];
            }
            else if (totalRowTimes1 == 0)
            {
                txtFooterTotal1.Text = "00:00";
            }
            else
            {
                txtFooterTotal1.Text = totalRowTimes1.ToString() + ":00";
            }


            if (FooterTotal2.Day > 1)
            {
                flag = false;
            }
            double totalRowTimes2;
            if (FooterTotal2.Day > 1)
            {
                totalRowTimes2 = Convert.ToDouble(FooterTotal2.Hour) + ((FooterTotal2.Day - 1) * 24);
                totalRowTimes2 = Convert.ToDouble(totalRowTimes2.ToString() + "." + FooterTotal2.Minute);
            }
            else
            {
                totalRowTimes2 = Convert.ToDouble(FooterTotal2.Hour);
                totalRowTimes2 = Convert.ToDouble(totalRowTimes2.ToString() + "." + FooterTotal2.Minute);
            }
            System.Web.UI.WebControls.TextBox txtFooterTotal2 = (System.Web.UI.WebControls.TextBox)gvTimeSheet.FooterRow.FindControl("txtTotal2");
            if (totalRowTimes2.ToString().Contains('.'))
            {
                string[] tempTime = totalRowTimes2.ToString().Split('.');

                if (tempTime[0].Length < 2)
                {
                    tempTime[0] = "0" + tempTime[0];
                }
                if (tempTime[1].Length < 2)
                {
                    tempTime[1] = tempTime[1] + "0";
                }
                txtFooterTotal2.Text = tempTime[0] + ":" + tempTime[1];
            }
            else if (totalRowTimes2 == 0)
            {
                txtFooterTotal2.Text = "00:00";
            }
            else
            {
                txtFooterTotal2.Text = totalRowTimes2.ToString() + ":00";
            }



            if (FooterTotal3.Day > 1)
            {
                flag = false;
            }
            double totalRowTimes3;
            if (FooterTotal3.Day > 1)
            {
                totalRowTimes3 = Convert.ToDouble(FooterTotal3.Hour) + ((FooterTotal3.Day - 1) * 24);
                totalRowTimes3 = Convert.ToDouble(totalRowTimes3.ToString() + "." + FooterTotal3.Minute);
            }
            else
            {
                totalRowTimes3 = Convert.ToDouble(FooterTotal3.Hour);
                totalRowTimes3 = Convert.ToDouble(totalRowTimes3.ToString() + "." + FooterTotal3.Minute);
            }
            System.Web.UI.WebControls.TextBox txtFooterTotal3 = (System.Web.UI.WebControls.TextBox)gvTimeSheet.FooterRow.FindControl("txtTotal3");
            if (totalRowTimes3.ToString().Contains('.'))
            {
                string[] tempTime = totalRowTimes3.ToString().Split('.');

                if (tempTime[0].Length < 2)
                {
                    tempTime[0] = "0" + tempTime[0];
                }
                if (tempTime[1].Length < 2)
                {
                    tempTime[1] = tempTime[1] + "0";
                }
                txtFooterTotal3.Text = tempTime[0] + ":" + tempTime[1];
            }
            else if (totalRowTimes3 == 0)
            {
                txtFooterTotal3.Text = "00:00";
            }
            else
            {
                txtFooterTotal3.Text = totalRowTimes3.ToString() + ":00";
            }




            if (FooterTotal4.Day > 1)
            {
                flag = false;
            }
            double totalRowTimes4;
            if (FooterTotal4.Day > 1)
            {
                totalRowTimes4 = Convert.ToDouble(FooterTotal4.Hour) + ((FooterTotal4.Day - 1) * 24);
                totalRowTimes4 = Convert.ToDouble(totalRowTimes4.ToString() + "." + FooterTotal4.Minute);
            }
            else
            {
                totalRowTimes4 = Convert.ToDouble(FooterTotal4.Hour);
                totalRowTimes4 = Convert.ToDouble(totalRowTimes4.ToString() + "." + FooterTotal4.Minute);
            }
            System.Web.UI.WebControls.TextBox txtFooterTotal4 = (System.Web.UI.WebControls.TextBox)gvTimeSheet.FooterRow.FindControl("txtTotal4");
            if (totalRowTimes4.ToString().Contains('.'))
            {
                string[] tempTime = totalRowTimes4.ToString().Split('.');

                if (tempTime[0].Length < 2)
                {
                    tempTime[0] = "0" + tempTime[0];
                }
                if (tempTime[1].Length < 2)
                {
                    tempTime[1] = tempTime[1] + "0";
                }
                txtFooterTotal4.Text = tempTime[0] + ":" + tempTime[1];
            }
            else if (totalRowTimes4 == 0)
            {
                txtFooterTotal4.Text = "00:00";
            }
            else
            {
                txtFooterTotal4.Text = totalRowTimes4.ToString() + ":00";
            }





            if (FooterTotal5.Day > 1)
            {
                flag = false;
            }
            double totalRowTimes5;
            if (FooterTotal5.Day > 1)
            {
                totalRowTimes5 = Convert.ToDouble(FooterTotal5.Hour) + ((FooterTotal5.Day - 1) * 24);
                totalRowTimes5 = Convert.ToDouble(totalRowTimes5.ToString() + "." + FooterTotal5.Minute);
            }
            else
            {
                totalRowTimes5 = Convert.ToDouble(FooterTotal5.Hour);
                totalRowTimes5 = Convert.ToDouble(totalRowTimes5.ToString() + "." + FooterTotal5.Minute);
            }
            System.Web.UI.WebControls.TextBox txtFooterTotal5 = (System.Web.UI.WebControls.TextBox)gvTimeSheet.FooterRow.FindControl("txtTotal5");
            if (totalRowTimes5.ToString().Contains('.'))
            {
                string[] tempTime = totalRowTimes5.ToString().Split('.');

                if (tempTime[0].Length < 2)
                {
                    tempTime[0] = "0" + tempTime[0];
                }
                if (tempTime[1].Length < 2)
                {
                    tempTime[1] = tempTime[1] + "0";
                }
                txtFooterTotal5.Text = tempTime[0] + ":" + tempTime[1];
            }
            else if (totalRowTimes5 == 0)
            {
                txtFooterTotal5.Text = "00:00";
            }
            else
            {
                txtFooterTotal5.Text = totalRowTimes5.ToString() + ":00";
            }





            if (FooterTotal6.Day > 1)
            {
                flag = false;
            }
            double totalRowTimes6;
            if (FooterTotal6.Day > 1)
            {
                totalRowTimes6 = Convert.ToDouble(FooterTotal6.Hour) + ((FooterTotal6.Day - 1) * 24);
                totalRowTimes6 = Convert.ToDouble(totalRowTimes6.ToString() + "." + FooterTotal6.Minute);
            }
            else
            {
                totalRowTimes6 = Convert.ToDouble(FooterTotal6.Hour);
                totalRowTimes6 = Convert.ToDouble(totalRowTimes6.ToString() + "." + FooterTotal6.Minute);
            }
            System.Web.UI.WebControls.TextBox txtFooterTotal6 = (System.Web.UI.WebControls.TextBox)gvTimeSheet.FooterRow.FindControl("txtTotal6");

            if (totalRowTimes6.ToString().Contains('.'))
            {
                string[] tempTime = totalRowTimes6.ToString().Split('.');

                if (tempTime[0].Length < 2)
                {
                    tempTime[0] = "0" + tempTime[0];
                }
                if (tempTime[1].Length < 2)
                {
                    tempTime[1] = tempTime[1] + "0";
                }
                txtFooterTotal6.Text = tempTime[0] + ":" + tempTime[1];
            }
            else if (totalRowTimes6 == 0)
            {
                txtFooterTotal6.Text = "00:00";
            }
            else
            {
                txtFooterTotal6.Text = totalRowTimes6.ToString() + ":00";
            }


        }


        return flag;
    }
}
