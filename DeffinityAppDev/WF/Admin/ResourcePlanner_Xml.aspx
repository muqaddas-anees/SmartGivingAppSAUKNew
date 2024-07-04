<%@ Page Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Collections" %>



<% 
	
    
    
    
    
        // set XML as output format for this script.
        Response.ContentType = "text/xml";
    
        #region Decalrations
        string Xml_Resource = string.Empty;
        string Xml_periods = string.Empty;
        string Xml_Shiftcolor = string.Empty;
        string ParentName = string.Empty;
        string EditMode = string.Empty;
        Hashtable ShiftColor = new Hashtable();
        ShiftColor.Add("Black", "#C1C1C1,#1B1B1B,#C1C1C1");
        ShiftColor.Add("Maroon", "#B53117,#a25369,#a25369");
        ShiftColor.Add("Olive", "#F5ADD2,#E53EA3,#F5ADD2");
        ShiftColor.Add("Navy", "#AFAFF4,#3D3DA6,#AFAFF4");
        ShiftColor.Add("Purple", "#D6B9E1,#943FEE,#D6B9E1");
        ShiftColor.Add("Gray", "#E2EAEC,#8C9196,#E2EAEC");
        ShiftColor.Add("Silver", "#EBE9E9,#CCC9D2,#EBE9E9");
        ShiftColor.Add("Red", "#EF847D,#E55552,#F18E85");
        ShiftColor.Add("Green", "#C0EEB4,#53C721,#C0EEB4");
        ShiftColor.Add("Lime", "#94DE79,#46A61B,#C1E779");
        ShiftColor.Add("Yellow", "#F7EF78,#EBD616,#FEF8A2");

        ShiftColor.Add("Aqua", "#65E3E1,#84DEDB,#71F7F5");
        //ShiftColor.Add("Aqua", "#028402,#9db99d,#028402");
        ShiftColor.Add("Blue", "#76ACFF,#6799FF,#93D0FE");
        ShiftColor.Add("White", "#FAFAFA,#DCDADA,#FAFAFA");
        ShiftColor.Add("Teal", "#B0D9DA,#558687,#B0D9DA");
        #endregion
        //Resource Block xml build;
        Xml_Resource = Xml_Resource + "<resources>";

        if (Request.QueryString["fromdate"] != null)
        {
            //CheckLogin(Request.Form["uid"], Request.Form["pwd"]);
        }

        DataSet ds = new DataSet();
        // 1 is static portfolioID
        string sdate = string.Empty;
        string tdate = string.Empty;
        try
        {
            if ((Request.QueryString["fromdate"] != null) && (Request.QueryString["todate"] != null))
            {
                sdate = Request.QueryString["fromdate"].ToString();
                tdate = Request.QueryString["todate"].ToString();
                //CheckLogin(Request.Form["uid"], Request.Form["pwd"]);
            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Reading dates from query string");
        }
        // display team structure
        int displaytype = 1; 
        if (Request.QueryString["view"] != null)
        {
            displaytype = int.Parse(Request.QueryString["view"].ToString());
        }
        ds = ResourcePlanner.SelectTeamData(sessionKeys.PortfolioID, sdate, tdate, displaytype);


        int R_cont = ds.Tables[0].Rows.Count;
        if (R_cont > 0)
        {
            for (int T1_cnt = 0; T1_cnt <= ds.Tables[0].Rows.Count - 1; T1_cnt++)
            {
                if (int.Parse(ds.Tables[0].Rows[T1_cnt]["Level"].ToString()) == 1)
                {
                    
                    Xml_Resource = Xml_Resource + "<resource id='" + ds.Tables[0].Rows[T1_cnt]["ID"].ToString() + "' name='" + ds.Tables[0].Rows[T1_cnt]["Name"].ToString().Trim().Replace("'"," ") + "' /> ";
                    ParentName = ds.Tables[0].Rows[T1_cnt]["ID"].ToString();
                }
                else
                {
                    Xml_Resource = Xml_Resource + "<resource id='" + ds.Tables[0].Rows[T1_cnt]["ID"].ToString() + "' name='" + ds.Tables[0].Rows[T1_cnt]["Name"].ToString().Trim().Replace("'", " ") + "' parent='" + ParentName + "' > ";
                    Xml_Resource = Xml_Resource + "<attributes>";
                    Xml_Resource = Xml_Resource + "<attribute name='TeamName'>" + ds.Tables[0].Rows[T1_cnt]["TeamName"].ToString() + "</attribute>";
                    Xml_Resource = Xml_Resource + "<attribute name='MemberID'>" + ds.Tables[0].Rows[T1_cnt]["TeamID"].ToString() + "</attribute>";
                    Xml_Resource = Xml_Resource + "<attribute name='TeamType'>" + ds.Tables[0].Rows[T1_cnt]["TeamType"].ToString() + "</attribute>";                    
                    Xml_Resource = Xml_Resource + "</attributes>";
                    Xml_Resource = Xml_Resource + "</resource>";                
                }
            }
        }
        Xml_Resource = Xml_Resource + "</resources> ";
        //Period Block xml build;
        Xml_periods = Xml_periods + "<periods>";

        //Response.Write(Xml_Resource);

        R_cont = ds.Tables[1].Rows.Count;
        if (R_cont > 0)
        {
            for (int T2_cnt = 0; T2_cnt <= ds.Tables[1].Rows.Count - 1; T2_cnt++)
            {
                Xml_periods = Xml_periods + "<period id='" + ds.Tables[1].Rows[T2_cnt]["ID"].ToString() + "' resource_id='" + ds.Tables[1].Rows[T2_cnt]["ResourceID"].ToString() + "' start='" + Convert.ToDateTime(ds.Tables[1].Rows[T2_cnt]["FromDate"].ToString()).ToString("yyyy.MM.dd") + "' end='" + Convert.ToDateTime(ds.Tables[1].Rows[T2_cnt]["ToDate"].ToString()).ToString("yyyy.MM.dd") + "' style='" + ds.Tables[1].Rows[T2_cnt]["Color"].ToString() + "' >";
                Xml_periods = Xml_periods + "<attributes>";
                Xml_periods = Xml_periods + "<attribute name='ShiftID'>" + ds.Tables[1].Rows[T2_cnt]["ShiftID"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='ShiftName'>" + ds.Tables[1].Rows[T2_cnt]["ShiftName"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='SiteID'>" + ds.Tables[1].Rows[T2_cnt]["SiteID"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='SiteName'>" + ds.Tables[1].Rows[T2_cnt]["SiteName"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='Notes'>" + ds.Tables[1].Rows[T2_cnt]["Notes"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='Member_ShiftID'>" + ds.Tables[1].Rows[T2_cnt]["Member_ShiftID"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='TeamType'>" + ds.Tables[1].Rows[T2_cnt]["TeamType"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='MemberID'>" + ds.Tables[1].Rows[T2_cnt]["MemberID"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='Date_display'>" + Convert.ToDateTime(ds.Tables[1].Rows[T2_cnt]["FromDate"].ToString()).ToString(Deffinity.systemdefaults.GetDateformat()) + " - " + Convert.ToDateTime(ds.Tables[1].Rows[T2_cnt]["ToDate"].ToString()).AddDays(-1).ToString(Deffinity.systemdefaults.GetDateformat()) + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='Shift_type'>" + "Shift" + "</attribute>";
                Xml_periods = Xml_periods + "</attributes>";
                Xml_periods = Xml_periods + "</period>";
            }
        }
            
    //Read vt table
        R_cont = ds.Tables[2].Rows.Count;
        if (R_cont > 0)
        {
            for (int T2_cnt = 0; T2_cnt <= ds.Tables[2].Rows.Count - 1; T2_cnt++)
            {
                Xml_periods = Xml_periods + "<period id='" + ds.Tables[2].Rows[T2_cnt]["ID"].ToString() + "' resource_id='" + ds.Tables[2].Rows[T2_cnt]["ResourceID"].ToString() + "' start='" + Convert.ToDateTime(ds.Tables[2].Rows[T2_cnt]["FromDate"].ToString()).ToString("yyyy.MM.dd") + "' end='" + Convert.ToDateTime(ds.Tables[2].Rows[T2_cnt]["ToDate"].ToString()).ToString("yyyy.MM.dd") + "' style='" + ds.Tables[2].Rows[T2_cnt]["Color"].ToString() + "' allow_edit='false'>";
                Xml_periods = Xml_periods + "<attributes>";
                Xml_periods = Xml_periods + "<attribute name='ShiftID'>" + ds.Tables[2].Rows[T2_cnt]["AbsentID"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='ShiftName'>" + ds.Tables[2].Rows[T2_cnt]["AbsentName"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='Notes'>" + ds.Tables[2].Rows[T2_cnt]["Notes"].ToString() + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='SiteName'></attribute>";
                Xml_periods = Xml_periods + "<attribute name='Shift_type'>" + "Vacation" + "</attribute>";
                Xml_periods = Xml_periods + "<attribute name='Date_display'>" + Convert.ToDateTime(ds.Tables[2].Rows[T2_cnt]["FromDate"].ToString()).ToString(Deffinity.systemdefaults.GetDateformat()) + " - " + Convert.ToDateTime(ds.Tables[2].Rows[T2_cnt]["ToDate"].ToString()).AddDays(-1).ToString(Deffinity.systemdefaults.GetDateformat()) + "</attribute>";
                Xml_periods = Xml_periods + "</attributes>";
                Xml_periods = Xml_periods + "</period>";
            }
        }
        Xml_periods = Xml_periods + "</periods>";

        //Shift Color Block xml build;

        Xml_Shiftcolor = "<period_styles>";
        string[] color_split;
        IDictionaryEnumerator en;
        en = ShiftColor.GetEnumerator();

        while (en.MoveNext())
        {
            Xml_Shiftcolor = Xml_Shiftcolor + " <period_style name='" + en.Key.ToString() + "'>";
            Xml_Shiftcolor = Xml_Shiftcolor + "<tooltip enabled='True'> <font render_as_html='true'/> <text>    ";
            Xml_Shiftcolor = Xml_Shiftcolor + "<![CDATA[<font face='Verdana' size='10' color='#000000'> <font color='#a25369' size='12'><u><b>{%Shift_type}</b></u></font> \n <font color='#494949'><b>{%Name} </b></font>\n";
            Xml_Shiftcolor = Xml_Shiftcolor + " {%Date_display} ({%Duration}) \n";
            Xml_Shiftcolor = Xml_Shiftcolor + " Shift/Absent type:<font color='#247128'><b>{%ShiftName}</b></font> \n";
            Xml_Shiftcolor = Xml_Shiftcolor + " Site:<font><b>{%SiteName}</b></font> \n";
            Xml_Shiftcolor = Xml_Shiftcolor + " Notes:<font><b>{%Notes}</b></font> \n";
            Xml_Shiftcolor = Xml_Shiftcolor + "</font>]]>";
            Xml_Shiftcolor = Xml_Shiftcolor + "</text></tooltip><bar_style><middle shape='Full'>";
            
            //Xml_Shiftcolor = Xml_Shiftcolor + "<fill enabled='true' type='Solid' color='DarkSeaGreen' /><border enabled='true' color='#656565' />";

            Xml_Shiftcolor = Xml_Shiftcolor + "<fill enabled='true' type='Gradient'>";
            Xml_Shiftcolor = Xml_Shiftcolor + "<gradient angle='-90'>";
            color_split =  en.Value.ToString().Split(',');
            Xml_Shiftcolor = Xml_Shiftcolor + "<key color='" + color_split[0] + "' position='0'/><key color='" + color_split[1] + "' position='0.38'/><key color='" + color_split[2] + "' position='1'/>";
            //<key color='" + color_split[2] + "' position='1'/>
            Xml_Shiftcolor = Xml_Shiftcolor + "</gradient></fill>";

            Xml_Shiftcolor = Xml_Shiftcolor + "</middle><states><hover><middle><border color='#D0D0D0'/></middle></hover><pushed><middle><border enabled='true' thickness='2' type='Solid' color='#2B72AF'/></middle></pushed><selected_normal><middle><border enabled='true' thickness='2' type='Solid' color='#2B72AF'/></middle></selected_normal><selected_hover><middle><border enabled='true' thickness='2' type='Solid' color='#2B72AF'/></middle></selected_hover></states></bar_style></period_style>";
        }
        Xml_Shiftcolor = Xml_Shiftcolor + "</period_styles>";
    
    // add time scale to xml file
        string xml_scale = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(sdate))
            {
                xml_scale = string.Format("<scale show_start='{0}' show_end='{1}'/>", Convert.ToDateTime(sdate).AddMonths(-1).ToString("yyyy.MM.dd"), Convert.ToDateTime(tdate).AddMonths(2).ToString("yyyy.MM.dd"));
            }
            else
            {
                xml_scale = string.Format("<scale show_start='{0}' show_end='{1}'/>", System.DateTime.Now.AddMonths(-1).ToString("yyyy.MM.dd"), System.DateTime.Now.AddMonths(2).ToString("yyyy.MM.dd"));

            }
        }
        catch (Exception ex)
        {
            LogExceptions.LogException(ex.Message, "Scale");
        }
         //<scale show_start="2009/10/01" show_end="2010/01/01"/>
         // check the edit mode
        if (Request.QueryString["editmode"] != null)
        {
            EditMode = "<editing allow_edit='False'/>";
        }
        else
        {
            EditMode = "<editing allow_edit='True'/>";            
        }
    
%>
<anygantt>
  <settings>
 <locale>
    <date_time_format week_starts_from_monday="True">
      <months>
        <names>January,February,March,April,May,June,July,August,September,October,November,December</names>
        <short_names>Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec</short_names>
      </months>
      <time am_string="AM" short_am_string="A" pm_string="PM" short_pm_string="P" />
      <week_days>
        <names>Sunday,Monday,Tuesday,wednesday,Thursday,Friday,Saturday</names>
        <short_names>Su,Mo,Tu,We,Th,Fr,Sa</short_names>
      </week_days>
      <format>      
        <full>%yyyy.%MM.%dd.%HH.%mm.%ss</full>
        <date>%yyyy.%MM.%dd</date>
        <time>%HH.%mm.%ss</time>
      </format>
    </date_time_format>
  </locale>


    <navigation position="Top" size="30">
      <buttons collapse_expand_button="false" align="Far"/>
      <text>Resource Planner</text>
      <font face="Verdana" size="10" bold="true" color="White"/>
      <background>
        <fill type="Gradient">
          <gradient>
            <key color="#B0B0B0" position="0"/>
            <key color="#A0A0A0" position="0.3"/>
            <key color="#999999" position="0.5"/>
            <key color="#A0A0A0" position="0.7"/>
            <key color="#B0B0B0" position="1"/>
          </gradient>
        </fill>
        <border type="Solid" color="#494949"/>
      </background>
    </navigation>
    <background enabled="false"/>
   <% Response.Write(EditMode); %>
  </settings>
  <datagrid width="200" show_tooltip="True">
    <columns>
      <column width="30">
        <header>
          <text>#</text>
        </header>
        <format>{%RowNum}</format>
      </column>
      <column cell_align="LeftLevelPadding" width="170">
        <header>
          <text>Resource</text>
        </header>
        <format>{%Name}</format>
      </column>      
    </columns>
  </datagrid>
  <timeline>
    <% Response.Write(xml_scale);%>
    <plot line_height="26" item_height="24">    
    </plot>
  </timeline>
  <styles>
    <defaults>
      <resource>
        <resource_style>
          <row>
            <fill enabled="false"/>
          </row>
          <row_datagrid>
            <cell>
              <states>
                <hover>
                  <fill type="Solid" color="Gray" opacity="0.1"/>
                </hover>
                <selected_normal>
                  <fill type="Solid" color="Gray" opacity="0.3"/>
                </selected_normal>
                <selected_hover>
                  <fill type="Solid" color="Gray" opacity="0.3"/>
                </selected_hover>
                <pushed>
                  <fill type="Solid" color="Gray" opacity="0.3"/>
                </pushed>
              </states>
            </cell>
            <tooltip enabled="True">
              <font render_as_html="True"/>
              <text>
                <![CDATA[<font face="Verdana" size="10" color="#000000"><font color="#494949"><b>{%Name}</b></font>]]>
              </text>
            </tooltip>
          </row_datagrid>
        </resource_style>
      </resource>
    </defaults>
    <% Response.Write(Xml_Shiftcolor); %> 
    </styles>
   
  <resource_chart>    
    <% Response.Write(Xml_Resource); %>    
    <% Response.Write(Xml_periods); %>   
    
  </resource_chart>
</anygantt>

