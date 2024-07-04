using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using Deffinity.ProgrammeManagers;
using System.Text;
using Deffinity.ProjectTasksManagers;


/// <summary>
/// Summary description for Gantt
/// </summary>
namespace Deffinity.ProjectTasks
{
    public class Gantt
    {

        public static string GetGanttXML(int projectreference, string type)
        {
            string Xml_Tasks, ToolTip, Xml_Connector, EditMode;
            GetTaskData(projectreference, type, out Xml_Tasks, out ToolTip, out  Xml_Connector, out EditMode);
            string retstr = string.Format(@"<anygantt>
  <styles>
    <defaults>
      <summary>
        <task_style>
          <tooltip enabled='true'>
            <font render_as_html='true'/>
           {0}
          </tooltip>
          <row>
            <fill enabled='false'/>
          </row>
          <row_datagrid>
            <cell>
              <font bold='true' size='9'/>
              <states>
                <hover>
                  <font underline='true'/>
                </hover>
                <pushed>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </pushed>
                <selected_normal>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </selected_normal>
                <selected_hover>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </selected_hover>
              </states>
            </cell>
          </row_datagrid>
          <actual>
            <bar_style>
              <labels>
                <label anchor='Right' valign='Center' halign='Far'>
                  <text>*_&%Complete&_*%</text>
                </label>
                <!--
                <label anchor='Left' valign='Center' halign='Near'>
                  <text>*_&%Name&_*</text>
                </label>
                -->
                </labels>
              <middle shape='HalfTop'>
                <border type='Solid' color='#000000'/>
                <fill type='Solid' color='#333333'/>
              </middle>
              <start>
                <marker type='Arrow'>
                  <border type='Solid' color='#000000'/>
                  <fill type='Solid' color='#333333'/>
                </marker>
              </start>
              <end>
                <marker type='Arrow'>
                  <border type='Solid' color='#000000'/>
                  <fill type='Solid' color='#333333'/>
                </marker>
              </end>
              <states>
                <hover>
                  <middle shape='HalfTop'>
                    <border type='Solid' color='#000000'/>
                    <fill type='Solid' color='#777777'/>
                  </middle>
                  <start>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#777777'/>
                    </marker>
                  </start>
                  <end>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#777777'/>
                    </marker>
                  </end>
                </hover>
                <selected_hover>
                  <middle shape='HalfTop'>
                    <border type='Solid' color='#000000'/>
                    <fill type='Solid' color='#E77373'/>
                  </middle>
                  <start>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#E77373'/>
                    </marker>
                  </start>
                  <end>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#E77373'/>
                    </marker>
                  </end>
                </selected_hover>
                <selected_normal>
                  <middle shape='HalfTop'>
                    <border type='Solid' color='#000000'/>
                    <fill type='Solid' color='#E77373'/>
                  </middle>
                  <start>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#E77373'/>
                    </marker>
                  </start>
                  <end>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#E77373'/>
                    </marker>
                  </end>
                </selected_normal>
              </states>
            </bar_style>
          </actual>
          <progress>
            <bar_style>
              <middle shape='ThinBottom'>
                <fill type='Solid' color='#F1C95A'/>
                <border enabled='true' color='#665300'/>
              </middle>
            </bar_style>
          </progress>
        </task_style>
      </summary>
      <task>
        <task_style>
          <tooltip enabled='true'>
            <font render_as_html='true'/>
              {0}
          </tooltip>

          <row>
            <fill enabled='false'/>
          </row>
          <row_datagrid>
            <cell>
              <states>
                <hover>
                  <font underline='true'/>
                </hover>
                <pushed>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </pushed>
                <selected_normal>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </selected_normal>
                <selected_hover>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </selected_hover>
              </states>
            </cell>
          </row_datagrid>
          <actual>
            <bar_style>
              <labels>
                <label anchor='Right' valign='Center' halign='Far'>
                  <text>*_&%Complete&_*%</text>
                </label>
                <!--
                <label anchor='Left' valign='Center' halign='Near'>
                  <text>*_&%Name&_*</text>
                </label>
                -->
              </labels>
              <middle shape='Full'>
                <fill type='Solid' color='#BDD3FC'/>
                <border type='Solid' color='#7A88A6'/>
              </middle>
              <states>
                <hover>
                  <middle shape='Full'>
                    <fill type='Solid' color='#E7EEFC'/>
                    <border type='Solid' color='#7A88A6'/>
                  </middle>
                </hover>
                <selected_hover>
                  <middle shape='Full'>
                    <fill type='Solid' color='#FCBDCD'/>
                    <border type='Solid' color='#A67A83'/>
                  </middle>
                </selected_hover>
                <selected_normal>
                  <middle shape='Full'>
                    <fill type='Solid' color='#FCBDCD'/>
                    <border type='Solid' color='#A67A83'/>
                  </middle>
                </selected_normal>
              </states>
            </bar_style>
          </actual>
          <progress>
            <bar_style>
              <middle shape='ThinCenter'>
                <fill type='Solid' color='#6A68F9'/>
                <border type='Solid' color='#4463A6'/>
              </middle>
              <states>
                <hover>
                  <middle shape='ThinCenter'>
                    <fill type='Solid' color='#9997F9'/>
                    <border type='Solid' color='#4463A6'/>
                  </middle>
                </hover>
                <selected_normal>
                  <middle shape='ThinCenter'>
                    <fill type='Solid' color='#F97968'/>
                    <border type='Solid' color='#A64459'/>
                  </middle>
                </selected_normal>
                <selected_hover>
                  <middle shape='ThinCenter'>
                    <fill type='Solid' color='#F97968'/>
                    <border type='Solid' color='#A64459'/>
                  </middle>
                </selected_hover>
              </states>
            </bar_style>
          </progress>
        </task_style>
      </task>
      <milestone>
        <task_style>
          <tooltip enabled='true'>
            <font render_as_html='true'/>
            <text>
              <![CDATA[<p><font color='#770000' face='Verdana' size='10'><b>*_&
%Name&_*</b></font></p>
<p><font color='#007700' face='Verdana' size='10'><b>*_&%task_startdate&_*</b></font></p>
]]>
            </text>
          </tooltip>
          <actual>
            <bar_style>
              <labels>
                <label anchor='Left' valign='Center' halign='Near'>
                  <text>*_&%Name&_*</text>
                </label>
              </labels>
              <start>
                <marker type='Rhomb'>
                  <border type='Solid' color='Green'/>
                  <fill type='Solid' color='#54D554'/>
                </marker>
              </start>
              <states>
                <hover>
                  <start>
                    <marker type='Rhomb'>
                      <border type='Solid' color='Green'/>
                      <fill type='Solid' color='#8BF78B'/>
                    </marker>
                  </start>
                </hover>
                <selected_hover>
                  <start>
                    <marker type='Rhomb'>
                      <fill type='Solid' color='#F97968'/>
                      <border type='Solid' color='#A64459'/>
                    </marker>
                  </start>
                </selected_hover>
                <selected_normal>
                  <start>
                    <marker type='Rhomb'>
                      <fill type='Solid' color='#F97968'/>
                      <border type='Solid' color='#A64459'/>
                    </marker>
                  </start>
                </selected_normal>
              </states>
            </bar_style>
          </actual>
        </task_style>
      </milestone>
      <connector>
        <connector_style>
          <line type='Solid' color='#595959'/>
        </connector_style>
      </connector>
    </defaults>
  </styles>
  <settings>
  <locale>
 <date_time_format week_starts_from_monday='True'>
  </date_time_format>
  </locale>
    <navigation enabled='false' position='Top' size='30'>
      <buttons collapse_expand_button='true' align='Far'/>
      <text>Custom Elements Styling</text>
      <font face='Verdana' size='10' bold='true' color='White'/>
      <background>
        <fill type='Gradient'>
          <gradient>
            <key color='#B0B0B0' position='0'/>
            <key color='#A0A0A0' position='0.3'/>
            <key color='#999999' position='0.5'/>
            <key color='#A0A0A0' position='0.7'/>
            <key color='#B0B0B0' position='1'/>
          </gradient>
        </fill>
        <border type='Solid' color='#494949'/>
      </background>

    </navigation>
    <background enabled='false'/>
{1}
    
    <context_menu save_as_image='false' version_info='false' print_chart='false' 
about_anygantt='false' />

  </settings>
  <datagrid enabled='true' show_tooltip='False'>
    <columns>
      <column width='30'>
        <header>
          <text>#</text>
        </header>
        <format>*_&%RowNum&_*</format>
      </column>
      <column cell_align='LeftLevelPadding' width='200'>
        <header>
          <text>Task Name</text>
        </header>
        <format>*_&%Name&_*</format>
      </column>
      <column>
        <header>
          <text>Start Date</text>
        </header>
        <format>*_&%task_startdate&_*</format>
      </column>
      <column>
        <header>
          <text>End Date</text>
        </header>
        <format>*_&%task_enddate&_*</format>
      </column>
      <column>
        <header>
          <text>Actual Start Date</text>
        </header>
        <format>*_&%startdate&_**_&dateTimeFormat:%dd/%MM/%yyyy&_*</format>
      </column>
      <column>
        <header>
          <text>Actual End Date</text>
        </header>
        <format>*_&%enddate&_**_&dateTimeFormat:%dd/%MM/%yyyy&_*</format>
      </column>
      <column>
        <header>
          <text>Progress</text>
        </header>
        <format>*_&%Complete&_*%</format>
      </column>
      <column>
        <header>
          <text>Duration</text>
        </header>
        <format>*_&%duration1&_*</format>
      </column>
    </columns>
  </datagrid>
  <timeline>
    <plot line_height='24' item_height='16'>
      <non_working_days show='true'/>
      <non_working_hours show='true'/>
       <grid>
        <vertical>
          <line enabled='true' />
        </vertical>
      </grid>
    </plot>
    <scale show_start='2009.8.15 0.0' show_end='2010.12.28 0.0' lines='3' height='60'>
      <zoom allow_animation='true'/>
      <bottom_line color='#696969'/>
    </scale>
  </timeline>
  <project_chart>
    <auto_summary enabled='True' real_time_update='False' progress_update='True'/>
    {2}
     {3}
  </project_chart>
</anygantt>", ToolTip,EditMode,Xml_Tasks,Xml_Connector);


            return retstr.Replace("*_&", "{").Replace("&_*", "}"); 
        }
        public static void GetTaskData(int projectreference, string type, out string Xml_Tasks, out string ToolTip, out string Xml_Connector, out string EditMode)
        {
           
            //Deffinity.ProgrammeManagers.Admin ad = new Deffinity.ProgrammeManagers.Admin();

            DataSet ds = new DataSet();
            //string key =string.Format("Projecttask_{0}", QueryStringValues.Project);
            //if (BaseCache.Cache_Select(key) == null)
            //{
            //    BaseCache.Cache_Insert(key, pt.ProjectTask_GetGanttchart(QueryStringValues.Project));            
            //}

            ds = ProjectTasksManager.GetChartData(projectreference);



            ToolTip = string.Empty;
            Xml_Connector = string.Empty;
            EditMode = string.Empty;
            Xml_Tasks = "<tasks>";
            int R_cont = ds.Tables[0].Rows.Count;
            if (R_cont > 0)
            {
                try
                {
                    for (int T1_cnt = 0; T1_cnt <= ds.Tables[0].Rows.Count - 1; T1_cnt++)
                    {
                        DateTime sdate = Convert.ToDateTime(string.IsNullOrEmpty(ds.Tables[0].Rows[T1_cnt]["StartDate"].ToString()) ? "01/01/1900" : ds.Tables[0].Rows[T1_cnt]["StartDate"].ToString());
                        DateTime edate = Convert.ToDateTime(string.IsNullOrEmpty(ds.Tables[0].Rows[T1_cnt]["EndDate"].ToString()) ? "01/01/1900" : ds.Tables[0].Rows[T1_cnt]["EndDate"].ToString());
                        ToolTip = "<text> <![CDATA[<font face='Verdana' size='10'><p><font color='#770000' face='Verdana' size='10'><b>{%Name}</b></font></p> \n Start Date: <p><font color='#007700'>{%task_startdate} </font></p> \n End Date:  <p><font color='#007700'>{%task_enddate} </font></p> \n Duration: <p><font color='#0055AA'><b>{%duration1}</b></font></p> \n Complete: <p><font color='#494949'><b>{%Complete}%</b></font></p></font>]]> </text>";
                        if (ds.Tables[0].Rows[T1_cnt]["projectstatus"].ToString() == "2")
                        {
                            ToolTip = "<text> <![CDATA[<font face='Verdana' size='10'><p><font color='#770000' face='Verdana' size='10'><b>{%Name}</b></font></p> \n Actual Start Date: <p><font color='#007700'>{%startdate}{dateTimeFormat:%dd/%MM/%yyyy} </font></p> \n Actual End Date:  <p><font color='#007700'>{%enddate}{dateTimeFormat:%dd/%MM/%yyyy} </font></p> \n Duration: <p><font color='#0055AA'><br/><b>{%duration1}</b></font></p> \n Complete: <p><font color='#494949'><b>{%Complete}%</b></font></p></font>]]> </text>";
                            sdate = Convert.ToDateTime(string.IsNullOrEmpty(ds.Tables[0].Rows[T1_cnt]["ActualStartDate"].ToString()) ? "01/01/1900" : ds.Tables[0].Rows[T1_cnt]["ActualStartDate"].ToString());
                            edate = Convert.ToDateTime(string.IsNullOrEmpty(ds.Tables[0].Rows[T1_cnt]["ActualEndDate"].ToString()) ? "01/01/1900" : ds.Tables[0].Rows[T1_cnt]["ActualEndDate"].ToString());
                        }


                        //check for milestone
                        if (bool.Parse(ds.Tables[0].Rows[T1_cnt]["isMilestone"].ToString()))
                            Xml_Tasks = Xml_Tasks + string.Format("<task id='{0}' name='{1}' parent='{2}' progress='{3}' actual_start='{4}' actual_end1='{5}'>", ds.Tables[0].Rows[T1_cnt]["ListPosition"].ToString(), ds.Tables[0].Rows[T1_cnt]["ItemDescription"].ToString().Replace("''", "&quot;").Replace("'", "&apos;").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;"), ((ds.Tables[0].Rows[T1_cnt]["Parent"].ToString() == "-1") || (ds.Tables[0].Rows[T1_cnt]["Parent"].ToString() == "0")) ? string.Empty : ds.Tables[0].Rows[T1_cnt]["Parent"].ToString(), ds.Tables[0].Rows[T1_cnt]["PercentComplete"].ToString().Trim() + "%", edate.ToString("yyyy.MM.dd") + ".00.00", edate.ToString("yyyy.MM.dd") + ".23.59");
                        else
                            Xml_Tasks = Xml_Tasks + string.Format("<task id='{0}' name='{1}' parent='{2}' progress='{3}' actual_start='{4}' actual_end='{5}'>", ds.Tables[0].Rows[T1_cnt]["ListPosition"].ToString(), ds.Tables[0].Rows[T1_cnt]["ItemDescription"].ToString().Replace("''", "&quot;").Replace("'", "&apos;").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;"), ((ds.Tables[0].Rows[T1_cnt]["Parent"].ToString() == "-1") || (ds.Tables[0].Rows[T1_cnt]["Parent"].ToString() == "0")) ? string.Empty : ds.Tables[0].Rows[T1_cnt]["Parent"].ToString(), ds.Tables[0].Rows[T1_cnt]["PercentComplete"].ToString().Trim() + "%", sdate.ToString("yyyy.MM.dd") + ".00.00", edate.ToString("yyyy.MM.dd") + ".23.59");

                        Xml_Tasks = Xml_Tasks + "<attributes>";
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='task_startdate'>{0}</attribute>", Convert.ToDateTime(string.IsNullOrEmpty(ds.Tables[0].Rows[T1_cnt]["StartDate"].ToString()) ? "01/01/1900" : ds.Tables[0].Rows[T1_cnt]["StartDate"].ToString()).ToString(Deffinity.systemdefaults.GetDateformat()));
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='task_enddate'>{0}</attribute>", Convert.ToDateTime(string.IsNullOrEmpty(ds.Tables[0].Rows[T1_cnt]["EndDate"].ToString()) ? "01/01/1900" : ds.Tables[0].Rows[T1_cnt]["EndDate"].ToString()).ToString(Deffinity.systemdefaults.GetDateformat()));
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='startdate'>{0}</attribute>", string.IsNullOrEmpty(ds.Tables[0].Rows[T1_cnt]["ActualStartDate"].ToString()) ? string.Empty : (ds.Tables[0].Rows[T1_cnt]["ActualStartDate"].ToString().Contains("01/01/1900") ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[T1_cnt]["ActualStartDate"].ToString()).ToString(Deffinity.systemdefaults.GetDateformat())));
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='enddate'>{0}</attribute>", string.IsNullOrEmpty(ds.Tables[0].Rows[T1_cnt]["ActualEndDate"].ToString()) ? string.Empty : (ds.Tables[0].Rows[T1_cnt]["ActualEndDate"].ToString().Contains("01/01/1900") ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[T1_cnt]["ActualEndDate"].ToString()).ToString(Deffinity.systemdefaults.GetDateformat())));
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='rag'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["RAGRequired"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='amberdays'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["AmberDays"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='reddays'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["RedDays"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='amberpercent'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["AmberPercent"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='redpercent'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["RedPercent"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='itemstatus'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["ItemStatus"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='resourceids'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["ResourceIDs"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='ragrequired'>{0}</attribute>", string.IsNullOrEmpty(ds.Tables[0].Rows[T1_cnt]["RAGRequired"].ToString()) ? "N" : ds.Tables[0].Rows[T1_cnt]["RAGRequired"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='notes'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["notes"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='taskid'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["TaskID"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='indentlevel'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["IndentLevel"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='qa'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["QA"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='includeinvaluation'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["IncludeInValuation"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='reoccurRange'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["ReoccurRange"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='dayName'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["DayName"].ToString());
                        int duration = Deffinity.Utility.DateDifference(sdate, edate);
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='duration1'>{0}</attribute>", (duration >= 0 ? duration + 1 : 0).ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='ismilestone'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["isMilestone"].ToString().ToLower());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='categoryid'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["categoryid"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='projectstatus'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["projectstatus"].ToString());
                        Xml_Tasks = Xml_Tasks + string.Format("<attribute name='TeamID'>{0}</attribute>", ds.Tables[0].Rows[T1_cnt]["TeamID"].ToString());
                        Xml_Tasks = Xml_Tasks + "</attributes>";
                        Xml_Tasks = Xml_Tasks + "</task>";
                    }
                }
                catch (Exception ex)
                { LogExceptions.WriteExceptionLog(ex); }
            }
            Xml_Tasks = Xml_Tasks + "</tasks>";


            R_cont = ds.Tables[1].Rows.Count;
            if (R_cont > 0)
            {
                Xml_Connector = "<connectors>";
                for (int T2_cnt = 0; T2_cnt <= ds.Tables[1].Rows.Count - 1; T2_cnt++)
                {
                    Xml_Connector = Xml_Connector + string.Format("<connector type='FinishStart' from='{0}' to='{1}'/>", ds.Tables[1].Rows[T2_cnt]["Task_From"].ToString(), ds.Tables[1].Rows[T2_cnt]["Task_To"].ToString()); ;
                }
                Xml_Connector = Xml_Connector + "</connectors>";
            }
            
            if (!string.IsNullOrEmpty(type))
            {
                EditMode = " <editing allow_edit='flase'><rounding><date unit='Day' step='1'/><progress step='1'/></rounding></editing>";
            }
            else
            {
                int role = 0;
                if (sessionKeys.SID != 1)
                {
                    role = Admin.GetTeamID(sessionKeys.UID);
                    if (role == 3)
                    {
                        EditMode = " <editing allow_edit='flase'><rounding><date unit='Day' step='1'/><progress step='1'/></rounding></editing>";
                    }
                    else
                    {
                        EditMode = " <editing allow_edit='true'><rounding><date unit='Day' step='1'/><progress step='1'/></rounding></editing>";
                    }


                }
                else
                {
                    EditMode = " <editing allow_edit='true'><rounding><date unit='Day' step='1'/><progress step='1'/></rounding></editing>";
                }
            }
        

        }
        string s = string.Format( @"<anygantt>
  <styles>
    <defaults>
      <summary>
        <task_style>
          <tooltip enabled='true'>
            <font render_as_html='true'/>
           {0}
          </tooltip>
          <row>
            <fill enabled='false'/>
          </row>
          <row_datagrid>
            <cell>
              <font bold='true' size='9'/>
              <states>
                <hover>
                  <font underline='true'/>
                </hover>
                <pushed>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </pushed>
                <selected_normal>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </selected_normal>
                <selected_hover>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </selected_hover>
              </states>
            </cell>
          </row_datagrid>
          <actual>
            <bar_style>
              <labels>
                <label anchor='Right' valign='Center' halign='Far'>
                  <text>{%Complete}%</text>
                </label>
                <!--
                <label anchor='Left' valign='Center' halign='Near'>
                  <text>{%Name}</text>
                </label>
                -->
                </labels>
              <middle shape='HalfTop'>
                <border type='Solid' color='#000000'/>
                <fill type='Solid' color='#333333'/>
              </middle>
              <start>
                <marker type='Arrow'>
                  <border type='Solid' color='#000000'/>
                  <fill type='Solid' color='#333333'/>
                </marker>
              </start>
              <end>
                <marker type='Arrow'>
                  <border type='Solid' color='#000000'/>
                  <fill type='Solid' color='#333333'/>
                </marker>
              </end>
              <states>
                <hover>
                  <middle shape='HalfTop'>
                    <border type='Solid' color='#000000'/>
                    <fill type='Solid' color='#777777'/>
                  </middle>
                  <start>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#777777'/>
                    </marker>
                  </start>
                  <end>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#777777'/>
                    </marker>
                  </end>
                </hover>
                <selected_hover>
                  <middle shape='HalfTop'>
                    <border type='Solid' color='#000000'/>
                    <fill type='Solid' color='#E77373'/>
                  </middle>
                  <start>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#E77373'/>
                    </marker>
                  </start>
                  <end>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#E77373'/>
                    </marker>
                  </end>
                </selected_hover>
                <selected_normal>
                  <middle shape='HalfTop'>
                    <border type='Solid' color='#000000'/>
                    <fill type='Solid' color='#E77373'/>
                  </middle>
                  <start>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#E77373'/>
                    </marker>
                  </start>
                  <end>
                    <marker type='Arrow'>
                      <border type='Solid' color='#000000'/>
                      <fill type='Solid' color='#E77373'/>
                    </marker>
                  </end>
                </selected_normal>
              </states>
            </bar_style>
          </actual>
          <progress>
            <bar_style>
              <middle shape='ThinBottom'>
                <fill type='Solid' color='#F1C95A'/>
                <border enabled='true' color='#665300'/>
              </middle>
            </bar_style>
          </progress>
        </task_style>
      </summary>
      <task>
        <task_style>
          <tooltip enabled='true'>
            <font render_as_html='true'/>
              {0}
          </tooltip>

          <row>
            <fill enabled='false'/>
          </row>
          <row_datagrid>
            <cell>
              <states>
                <hover>
                  <font underline='true'/>
                </hover>
                <pushed>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </pushed>
                <selected_normal>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </selected_normal>
                <selected_hover>
                  <fill type='Solid' color='#E77373' opacity='0.5'/>
                </selected_hover>
              </states>
            </cell>
          </row_datagrid>
          <actual>
            <bar_style>
              <labels>
                <label anchor='Right' valign='Center' halign='Far'>
                  <text>{%Complete}%</text>
                </label>
                <!--
                <label anchor='Left' valign='Center' halign='Near'>
                  <text>{%Name}</text>
                </label>
                -->
              </labels>
              <middle shape='Full'>
                <fill type='Solid' color='#BDD3FC'/>
                <border type='Solid' color='#7A88A6'/>
              </middle>
              <states>
                <hover>
                  <middle shape='Full'>
                    <fill type='Solid' color='#E7EEFC'/>
                    <border type='Solid' color='#7A88A6'/>
                  </middle>
                </hover>
                <selected_hover>
                  <middle shape='Full'>
                    <fill type='Solid' color='#FCBDCD'/>
                    <border type='Solid' color='#A67A83'/>
                  </middle>
                </selected_hover>
                <selected_normal>
                  <middle shape='Full'>
                    <fill type='Solid' color='#FCBDCD'/>
                    <border type='Solid' color='#A67A83'/>
                  </middle>
                </selected_normal>
              </states>
            </bar_style>
          </actual>
          <progress>
            <bar_style>
              <middle shape='ThinCenter'>
                <fill type='Solid' color='#6A68F9'/>
                <border type='Solid' color='#4463A6'/>
              </middle>
              <states>
                <hover>
                  <middle shape='ThinCenter'>
                    <fill type='Solid' color='#9997F9'/>
                    <border type='Solid' color='#4463A6'/>
                  </middle>
                </hover>
                <selected_normal>
                  <middle shape='ThinCenter'>
                    <fill type='Solid' color='#F97968'/>
                    <border type='Solid' color='#A64459'/>
                  </middle>
                </selected_normal>
                <selected_hover>
                  <middle shape='ThinCenter'>
                    <fill type='Solid' color='#F97968'/>
                    <border type='Solid' color='#A64459'/>
                  </middle>
                </selected_hover>
              </states>
            </bar_style>
          </progress>
        </task_style>
      </task>
      <milestone>
        <task_style>
          <tooltip enabled='true'>
            <font render_as_html='true'/>
            <text>
              <![CDATA[<p><font color='#770000' face='Verdana' size='10'><b>{%Name}</b></font></p>
<p><font color='#007700' face='Verdana' size='10'><b>{%task_startdate}</b></font></p>
]]>
            </text>
          </tooltip>
          <actual>
            <bar_style>
              <labels>
                <label anchor='Left' valign='Center' halign='Near'>
                  <text>{%Name}</text>
                </label>
              </labels>
              <start>
                <marker type='Rhomb'>
                  <border type='Solid' color='Green'/>
                  <fill type='Solid' color='#54D554'/>
                </marker>
              </start>
              <states>
                <hover>
                  <start>
                    <marker type='Rhomb'>
                      <border type='Solid' color='Green'/>
                      <fill type='Solid' color='#8BF78B'/>
                    </marker>
                  </start>
                </hover>
                <selected_hover>
                  <start>
                    <marker type='Rhomb'>
                      <fill type='Solid' color='#F97968'/>
                      <border type='Solid' color='#A64459'/>
                    </marker>
                  </start>
                </selected_hover>
                <selected_normal>
                  <start>
                    <marker type='Rhomb'>
                      <fill type='Solid' color='#F97968'/>
                      <border type='Solid' color='#A64459'/>
                    </marker>
                  </start>
                </selected_normal>
              </states>
            </bar_style>
          </actual>
        </task_style>
      </milestone>
      <connector>
        <connector_style>
          <line type='Solid' color='#595959'/>
        </connector_style>
      </connector>
    </defaults>
  </styles>
  <settings>
  <locale>
 <date_time_format week_starts_from_monday='True'>
  </date_time_format>
  </locale>
    <navigation enabled='false' position='Top' size='30'>
      <buttons collapse_expand_button='true' align='Far'/>
      <text>Custom Elements Styling</text>
      <font face='Verdana' size='10' bold='true' color='White'/>
      <background>
        <fill type='Gradient'>
          <gradient>
            <key color='#B0B0B0' position='0'/>
            <key color='#A0A0A0' position='0.3'/>
            <key color='#999999' position='0.5'/>
            <key color='#A0A0A0' position='0.7'/>
            <key color='#B0B0B0' position='1'/>
          </gradient>
        </fill>
        <border type='Solid' color='#494949'/>
      </background>

    </navigation>
    <background enabled='false'/>
{1}
    
    <context_menu save_as_image='false' version_info='false' print_chart='false' about_anygantt='false' />

  </settings>
  <datagrid enabled='true' show_tooltip='False'>
    <columns>
      <column width='30'>
        <header>
          <text>#</text>
        </header>
        <format>{%RowNum}</format>
      </column>
      <column cell_align='LeftLevelPadding' width='200'>
        <header>
          <text>Task Name</text>
        </header>
        <format>{%Name}</format>
      </column>
      <column>
        <header>
          <text>Start Date</text>
        </header>
        <format>{%task_startdate}</format>
      </column>
      <column>
        <header>
          <text>End Date</text>
        </header>
        <format>{%task_enddate}</format>
      </column>
      <column>
        <header>
          <text>Actual Start Date</text>
        </header>
        <format>{%startdate}{dateTimeFormat:%dd/%MM/%yyyy}</format>
      </column>
      <column>
        <header>
          <text>Actual End Date</text>
        </header>
        <format>{%enddate}{dateTimeFormat:%dd/%MM/%yyyy}</format>
      </column>
      <column>
        <header>
          <text>Progress</text>
        </header>
        <format>{%Complete}%</format>
      </column>
      <column>
        <header>
          <text>Duration</text>
        </header>
        <format>{%duration1}</format>
      </column>
    </columns>
  </datagrid>
  <timeline>
    <plot line_height='24' item_height='16'>
      <non_working_days show='true'/>
      <non_working_hours show='true'/>
       <grid>
        <vertical>
          <line enabled='true' />
        </vertical>
      </grid>
    </plot>
    <scale show_start='2009.8.15 0.0' show_end='2010.12.28 0.0' lines='3' height='60'>
      <zoom allow_animation='true'/>
      <bottom_line color='#696969'/>
    </scale>
  </timeline>
  <project_chart>
    <auto_summary enabled='True' real_time_update='False' progress_update='True'/>
    {2}
     {3}
  </project_chart>
</anygantt>","","","","");

     
    }
}
