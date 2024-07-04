
<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="IPD" Title="IPD" Codebehind="IPD.aspx.cs" %>
<%@ Register Assembly="Flan.FutureControls" Namespace="Flan.FutureControls" TagPrefix="cc3" %>
<%@ Register src="controls/ProjectTabs.ascx" tagname="ProjectTabs" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
  <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.InterProjectDependency%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" />  
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline" ></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript" src="js/overlib.js"></script>

<script type="text/javascript">
<!--
   
    function selectAll(chkVal, idVal)
    {
        var frm = document.forms[0];
        if (idVal.indexOf('chkItem') != -1 && chkVal == true)
        {
                    
            var AllAreSelected = true;
            for (i=0; i<frm.length; i++) 
            {
                if (frm.elements[i].id.indexOf('chkItem') != -1 && frm.elements[i].checked == false)
                { 
                    AllAreSelected = false;
                    break;
                } 
            } 
            if(AllAreSelected == true)
            {
                for (j=0; j<frm.length; j++) 
                {
                    if (frm.elements[j].id.indexOf ('chkHeading') != -1)
                    {
                        frm.elements[j].checked = true;
                        break;
                    }
                }
            }
        } else 
        {
               
            for (i=0; i<frm.length; i++) 
            {
                        
                if (idVal.indexOf ('chkHeading') != -1) 
                {   
                    if(chkVal == true)
                    {
                        frm.elements[i].checked = true; 
                    } else 
                    {
                        frm.elements[i].checked = false; 
                    }
                }else if (idVal.indexOf('chkItem') != -1 && frm.elements[i].checked == false) {
                            
                    for (j=0; j<frm.length; j++) 
                    {
                        if (frm.elements[j].id.indexOf ('chkHeading') != -1) 
                        { 
                            frm.elements[j].checked = false;
                            break; 
                        }
                    }
                }
            }
        }
    }
    -->
</script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>    
    <asp:Label SkinID="Loading" ID="lblloading"  runat="server" />        
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="updatePanel1"><ContentTemplate>
         <div class="form-group">
        <asp:Label id="lblError" runat="Server" ForeColor="Red"></asp:Label> 
             </div>
 <div class="row">
       <div class="col-md-6">
             <div class="form-group">
        <div class="col-md-12">&nbsp;</div>
                 </div>
           <div class="form-group">
        <div class="col-md-12">&nbsp;</div>
                 </div>
           <div class="form-group">
        <div class="col-md-12">&nbsp;</div>
                 </div>
                                       <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong> <%= Resources.DeffinityRes.TasksWithinProject%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
            <asp:Panel id="pnlTasks1" runat="server" ScrollBars="Vertical" Height="400px" BorderStyle="Double" BorderColor="LightSteelBlue" >
        <asp:GridView id="ProjectTasks1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="ID" width="100%">
        <Columns>
             <asp:TemplateField >
                 <ItemTemplate></ItemTemplate>
                 </asp:TemplateField>
        <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
            <ItemTemplate>
                <cc3:RowDragOverlayExtender ID="rowDragOE" runat="server" TargetControlID="Image1" OnRowDrop="rowDragOE_RowDrop" />
                <asp:LinkButton ID="Image1" runat="server" SkinID="BtnLinkDrag" />
            </ItemTemplate>
            <HeaderTemplate>
                <cc3:RowDragOverlayExtender ID="RowDragOverlayExtender2" runat="server" TargetControlID="Image2" OnRowDrop="rowDragOE_RowDrop" />
                <asp:LinkButton ID="Image2" runat="server" SkinID="BtnLinkDrag" />
            </HeaderTemplate>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField> 
<asp:BoundField DataField="id" HeaderText="<%$ Resources:DeffinityRes, id%>" visible="false" ReadOnly="True" InsertVisible="False" SortExpression="id"></asp:BoundField>
<asp:BoundField DataField="itemdescription" HeaderText="<%$ Resources:DeffinityRes, ItemDescription%>" SortExpression="itemdescription"></asp:BoundField>
<asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, StartDate%>" >
                <ItemTemplate>
                        <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("StartDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="15%" />
</asp:TemplateField>
 <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, Completiondate%>" HeaderStyle-CssClass="header_bg_r">
                <ItemTemplate>
                        <asp:Label ID="lblCdate" runat="server" Text='<%# Bind("CompletionDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="15%" />
</asp:TemplateField>

</Columns>

        </asp:GridView> </asp:Panel>
        <asp:SqlDataSource id="SqlDataSource1" runat="server" SelectCommandType="StoredProcedure" SelectCommand="DEFFINITY_IPD_DD_PROJTASKS" ConnectionString="<%$ ConnectionStrings:DBstring %>">
        <SelectParameters>
            <asp:QueryStringParameter QueryStringField="Project" DefaultValue="0" Name="PROJECTREFERENCE" Type="Int32"></asp:QueryStringParameter>
        </SelectParameters>
</asp:SqlDataSource>
                                    </div>
                                <div class="col-md-6">

                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Programme%> </label>
                                      <div class="col-sm-8"><asp:DropDownList id="ddlProjGroups" runat="server" Width="270px" OnSelectedIndexChanged="ddlProjGroups_SelectedIndexChanged"
 AutoPostBack="true" DataTextField="OperationsOwners" DataValueField="ID" DataSourceID="SqlDataSource4">
</asp:DropDownList> 
 <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.SubProgramme%> </label>
                                      <div class="col-sm-8">
                                          
                                           <asp:DropDownList ID="ddlsubprogram" runat="server" Width="270px" DataSourceID="SqlDataSourcesubprogram"
            AutoPostBack="true" OnSelectedIndexChanged="ddlsubprogram_SelectedIndexChanged"
            DataValueField="ID" DataTextField="OPERATIONSOWNERS">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourcesubprogram" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedSubProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlProjGroups" DefaultValue="0" 
                                        Name="PROGRAMMEID" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Project%> </label>
                                      <div class="col-sm-8">
                                            <asp:dropdownlist id="ddlProjects" runat="server" Width="270px" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged" AutoPostBack="True" DataValueField="ProjectReference" DataTextField="ProjectTitle" DataSourceID="SqlDataSource9">
        
        </asp:dropdownlist>
         <asp:SqlDataSource id="SqlDataSource9" runat="server" SelectCommandType="StoredProcedure" SelectCommand="DEFFINITY_CONTRACTORPROJECTS11" ConnectionString="<%$ ConnectionStrings:DBstring %>">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="ContractorID" SessionField="UID" Type="Int32" />
                    <asp:ControlParameter ControlID="ddlProjGroups" DefaultValue="0" Name="Programmer"
              PropertyName="SelectedValue" Type="Int32" />
                <asp:SessionParameter DefaultValue="0" Name="SID" SessionField="SID" Type="Int32" />
                <asp:ControlParameter ControlID="ddlsubprogram" DefaultValue="0" Name="SubProgID"
              PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
					</div>
				</div>
</div>
<asp:Panel id="pnlTasks2" runat="server" ScrollBars="Vertical" Height="400px" BorderStyle="Double" BorderColor="LightSteelBlue" >
<asp:GridView id="ProjectTasks2" runat="server" DataSourceID="SqlDataSource2" AutoGenerateColumns="False" DataKeyNames="id" width="100%">
<Columns>
<asp:TemplateField HeaderStyle-CssClass="header_bg_l" >
                        <ItemTemplate>
                            <cc3:RowDragOverlayExtender ID="RowDragOverlayExtender1" runat="server"  TargetControlID="Image1"
                                OnRowDrop="rowDragOE_RowDrop" />
                            <asp:LinkButton ID="Image1" runat="server" SkinID="BtnLinkDrag"   />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <cc3:RowDragOverlayExtender ID="RowDragOverlayExtender3" runat="server" TargetControlID="Image2"
                                OnRowDrop="rowDragOE_RowDrop" />
                            <asp:LinkButton ID="Image2" runat="server" SkinID="BtnLinkDrag" />
                        </HeaderTemplate>
                        <ItemStyle Width="5%" />
                    </asp:TemplateField> 
<asp:BoundField DataField="id" HeaderText="<%$ Resources:DeffinityRes, id%>" visible="false" ReadOnly="True" InsertVisible="False" SortExpression="id"></asp:BoundField>
<asp:BoundField DataField="itemdescription" HeaderText="<%$ Resources:DeffinityRes, ItemDescription%>" SortExpression="itemdescription"></asp:BoundField>
<asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, StartDate%>">
                <ItemTemplate>
                        <asp:Label ID="lblSdate1" runat="server" Text='<%# Bind("StartDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
</asp:TemplateField>
 <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, CompletionDate%>" HeaderStyle-CssClass="header_bg_r">
                <ItemTemplate>
                        <asp:Label ID="lblCdate1" runat="server" Text='<%# Bind("CompletionDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</asp:Panel>
<asp:SqlDataSource id="SqlDataSource2" runat="server" SelectCommandType="StoredProcedure" SelectCommand="DEFFINITY_IPD_DD_PROJTASKS" ConnectionString="<%$ ConnectionStrings:DBstring %>"><SelectParameters>
<asp:ControlParameter ControlID="ddlProjects" PropertyName="SelectedValue" DefaultValue="0" Name="PROJECTREFERENCE" Type="Int32"></asp:ControlParameter>
</SelectParameters>
</asp:SqlDataSource>
                                    </div>
                                
                                 </div>
        <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>   <%= Resources.DeffinityRes.DependencyMap%></strong>
            <hr class="no-top-margin" />
            </div>
    </div>
        <div class="form-group">
            <div class="col-md-12">
         <asp:LinkButton ID="btnDelete" runat="server" Text="<%$ Resources:DeffinityRes, Delete%>" Font-Bold="true" OnClick="btnDelete_Click"  OnClientClick="return confirm('Do you want to delete the record(s)?');"></asp:LinkButton>   
           </div> </div> 
        <asp:Panel id="Panel2" runat="server" Width="100%" BorderStyle="Double" BorderColor="LightSteelBlue" ScrollBars="Horizontal">
<div style="display: none" >
            <asp:TextBox runat="server" ID="textbox1" />
            <ajaxToolkit:CalendarExtender ID="ceTextBox1" runat="server" TargetControlID="textbox1" />
            </div>
    <asp:GridView ID="GridView1" runat="server"  DataKeyNames="TaskID" GridLines="None"   
            width="100%" AutoGenerateColumns="False"   HorizontalAlign="Left" 
            BorderColor="White" BorderStyle="None" CellPadding="0" CellSpacing="1" 
            OnRowUpdating="GridView1_RowUpdating" OnRowUpdated="GridView1_RowUpdated" 
            OnRowCommand="GridView1_RowCommand" 
            ShowFooter="True" DataSourceID="SqlDataSource3" OnRowDataBound="GridView1_RowDataBound" >
           <RowStyle Font-Size="X-Small" />
            <Columns>  
              
           <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHeading" onclick="selectAll(this.checked,this.id);" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkItem" runat="server" />
                        <asp:HiddenField runat="server" ID ="HID" Value='<%# Bind("ID") %>' /> 
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle  HorizontalAlign="Center" Width="20px"/>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="BtnLinkCancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" SkinID="BtnLinkEdit"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="40px" />
                </asp:TemplateField>
                     
                   <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, ProjectRef %>">
                    <ItemTemplate>                     
                    <%# GetProjectRef()%>
                    </ItemTemplate>
                  <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:TemplateField>
                               
                <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, Task%>">
                    <ItemTemplate>
                     
                    <asp:Label ID="Task" Width="115px" runat="server" Text='<%# Bind("Task") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField  HeaderStyle-Width="10%"  HeaderText="<%$ Resources:DeffinityRes, StartDate%>">
                <ItemTemplate>
                        <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("TaskStartDate","{0:d}") %>' ></asp:Label>
                </ItemTemplate>
                    <EditItemTemplate >
                    <table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>
                  
                        <asp:TextBox ID="txtStartdate" Enabled="<%#CommandField()%>"  runat="server" MaxLength="10" Width="67px" Text='<%# Bind("TaskStartDate","{0:d}") %>'  ></asp:TextBox>
                        </td><td>
                        <asp:Label ID="imgStartDate"  SkinID="Calender" runat="server" visible="True" />
                        <ajaxToolkit:calendarextender  Enabled="<%#CommandField()%>" id="CalendarExtender4" runat="server"  popupbuttonid="imgStartDate"
                           targetcontrolid="txtStartdate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <%--<asp:RequiredFieldValidator ID="sdateval1" runat="server" ErrorMessage="Please enter start date" ControlToValidate="txtStartdate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="Sdateval2" runat="server" ControlToValidate="txtStartdate" ErrorMessage="<%$ Resources:DeffinityRes, PleaseentervalidStartdate%>" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </td></tr></table>
                 </EditItemTemplate> 
                    <HeaderStyle Width="10%" />
                 <ItemStyle  HorizontalAlign="Center" Width="80px"/>               
                </asp:TemplateField>                
                <asp:TemplateField  HeaderStyle-Width="10%"   HeaderText="<%$ Resources:DeffinityRes, EndDate%>">                  
                <ItemTemplate>
                        <asp:Label ID="lblEnddate" runat="server" Text='<%# Bind("TaskCompletiondate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>
                        <asp:TextBox ID="txtEnddate" Enabled="<%#CommandField()%>"  runat="server" Width="67px" MaxLength="10" Text='<%# Bind("TaskCompletiondate","{0:d}") %>'  ></asp:TextBox>
                        </td><td>
                        <asp:Label ID="imgEndDate" runat="server" SkinID="Calender" visible="True" />
                        <ajaxToolkit:calendarextender id="CalendarExtenderEndDate"  Enabled="<%#CommandField()%>" runat="server"  popupbuttonid="imgEndDate"
                           targetcontrolid="txtEnddate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <%--<asp:RequiredFieldValidator ID="Enddateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtEnddate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="Enddateval2" runat="server" ControlToValidate="txtEnddate" ErrorMessage="<%$ Resources:DeffinityRes, Plsentervalidenddate%>" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </td></tr></table>
                    </EditItemTemplate>
                    <HeaderStyle Width="10%" />
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, DependsUpon%>">
                        <ItemTemplate>
                         <asp:LinkButton ID="Image1"  ImageAlign="Middle" runat="server"  SkinID="BtnLinkNext"  />
                     </ItemTemplate>                                          
                    <ItemStyle HorizontalAlign="Center" Width="60px"/>
                   </asp:TemplateField> 
                  
                <asp:TemplateField  HeaderText="">
                    <ItemTemplate>
                    <asp:Label ID="DepOnProject" Width="60px" runat="server" Text='<%# Bind("DependingOnProject") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:TemplateField>    
                    <asp:TemplateField  HeaderText="<%$ Resources:DeffinityRes, Task%>">
                    <ItemTemplate>
                    <asp:Label ID="DepOnTask" Width="90px" runat="server" Text='<%# Bind("DependingOnTask") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:TemplateField>
                
                <asp:TemplateField  HeaderStyle-Width="10%"  HeaderText="<%$ Resources:DeffinityRes, StartDate%>">                  
                <ItemTemplate>
                        <asp:Label ID="lblOnStartdate" runat="server" Text='<%# Bind("OnStartDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>
                        <asp:TextBox ID="txtOnStartdate" Enabled="<%#CommandField()%>"  runat="server" Width="67px" MaxLength="10" Text='<%# Bind("OnStartDate","{0:d}") %>'  ></asp:TextBox>
                        </td><td>
                        <asp:Label ID="imgOnStartdate" runat="server" SkinID="Calender" visible="True" />
                        <ajaxToolkit:calendarextender id="CalendarExtenderOnStartDate"  Enabled="<%#CommandField()%>"  runat="server"  popupbuttonid="imgOnStartdate"
                           targetcontrolid="txtOnStartdate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <%--<asp:RequiredFieldValidator ID="OnStartdateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtOnStartdate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="OnStartdateval2" runat="server" ControlToValidate="txtOnStartdate" ErrorMessage="<%$ Resources:DeffinityRes, EntervalidEnddate%>" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </td></tr></table>
                    </EditItemTemplate>
                    <HeaderStyle Width="10%" />
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField  HeaderStyle-Width="10%"  HeaderText="<%$ Resources:DeffinityRes, EndDate%>">                  
                    <ItemTemplate>
                        <asp:Label ID="lblOnEnddate" runat="server" Text='<%# Bind("OnCompletionDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>
                        <asp:TextBox ID="txtonEnddate" Enabled="<%#CommandField()%>"  runat="server" Width="67px" MaxLength="10" Text='<%# Bind("OnCompletionDate","{0:d}") %>'  ></asp:TextBox>
                        </td><td>
                        <asp:Label ID="ImgOnEndDate"  runat="server" SkinID="Calender" visible="True" />
                        <ajaxToolkit:calendarextender id="CalendarExtenderOnEndDate"  Enabled="<%#CommandField()%>"  runat="server"  popupbuttonid="ImgOnEndDate"
                           targetcontrolid="txtOnEnddate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <%--<asp:RequiredFieldValidator ID="OnEnddateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtonEnddate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="OnEnddateval2" runat="server" ControlToValidate="txtonEnddate" ErrorMessage="<%$ Resources:DeffinityRes, EntervalidEnddate%>" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </td></tr></table>
                    </EditItemTemplate>
                        <HeaderStyle Width="10%" />
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
                     
                <asp:TemplateField Visible="false"  HeaderText="<%$ Resources:DeffinityRes, DependentProject%>">
                    <ItemTemplate>
                    <asp:Label ID="DepProject" Width="60px" runat="server" Text='<%# Bind("DependentProject") %>'  ></asp:Label>                                           
                    </ItemTemplate >
                  <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:TemplateField>    
                    <asp:TemplateField  Visible="false"  HeaderText="<%$ Resources:DeffinityRes, Task%>">
                    <ItemTemplate>
                    <asp:Label ID="DepTask" Width="90px" runat="server" Text='<%# Bind("DependentTask") %>'  ></asp:Label>                                           
                    </ItemTemplate>
                  <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:TemplateField>
                
                <asp:TemplateField  Visible="false"  HeaderText="<%$ Resources:DeffinityRes, StartDate%>">                  
                <ItemTemplate>
                        <asp:Label ID="lblDepStartdate" runat="server" Text='<%# Bind("DepStartDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>
                        <asp:TextBox ID="txtDepStartdate" Enabled="<%#CommandField()%>"  runat="server" Width="67px" MaxLength="10" Text='<%# Bind("DepStartDate","{0:d}") %>'  ></asp:TextBox>
                        </td><td>
                        <asp:Label ID="imgDepStartdate" runat="server" SkinID="Calender" visible="True" />
                        <ajaxToolkit:CalendarExtender id="CalendarExtenderdepStartDate"  Enabled="<%#CommandField()%>" runat="server"  popupbuttonid="imgDepStartdate"
                           targetcontrolid="txtDepStartdate" CssClass="MyCalendar"></ajaxToolkit:CalendarExtender>
                        <%--<asp:RequiredFieldValidator ID="DepStartdateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtDepStartdate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="DepStartdateval2" runat="server" ControlToValidate="txtDepStartdate" ErrorMessage="<%$ Resources:DeffinityRes, EntervalidEnddate%>" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </td></tr></table>
                    </EditItemTemplate>
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField  Visible="false" HeaderText="<%$ Resources:DeffinityRes, EndDate%>" HeaderStyle-CssClass="header_bg_r">                  
                <ItemTemplate>
                        <asp:Label ID="lblDepEnddate" runat="server" Text='<%# Bind("DepCompletionDate","{0:d}") %>' ></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%"><tr><td>
                        <asp:TextBox ID="txtdepEnddate" Enabled="<%#CommandField()%>"  runat="server" Width="67px" MaxLength="10" Text='<%# Bind("DepCompletionDate","{0:d}") %>'  ></asp:TextBox>
                        </td><td>
                        <asp:Label ID="ImgDepEndDate" runat="server" SkinID="Calender" visible="True" />
                        <ajaxToolkit:calendarextender id="CalendarExtenderDepEndDate"  Enabled="<%#CommandField()%>" runat="server"  popupbuttonid="ImgDepEndDate"
                           targetcontrolid="txtdepEnddate" CssClass="MyCalendar"></ajaxToolkit:calendarextender>
                        <%--<asp:RequiredFieldValidator ID="DepEnddateval1" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtdepEnddate" ValidationGroup="GridValid" Display="None" ></asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="DepEnddateval2" runat="server" ControlToValidate="txtdepEnddate" ErrorMessage="<%$ Resources:DeffinityRes,EntervalidEnddate%>" Operator="DataTypeCheck" Type="Date" ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                        </td></tr></table>
                    </EditItemTemplate>
                        <HeaderStyle CssClass="header_bg_r" />
                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                    </asp:TemplateField> 
               
            </Columns>   
   </asp:GridView>
   </asp:Panel>
   <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring%>" SelectCommand="DEFFINITY_IPD_PROJTASKS" SelectCommandType="StoredProcedure" UpdateCommand="DEFFINITY_UPDATE_IPD_TASKS" UpdateCommandType="StoredProcedure" >
        <SelectParameters>
            <asp:QueryStringParameter Type="int32" Name="ProjectReference" QueryStringField="Project" DefaultValue="80" />  
        </SelectParameters>   
        <UpdateParameters>
             <asp:Parameter Name="ID" Type="Int32" DefaultValue="0" />
             <asp:Parameter Name="DepSTARTDATE" Type="DateTime" />
             <asp:Parameter Name="DepENDDATE" Type="DateTime" Size="50"/> 
             <asp:Parameter Name="DepOnSTARTDATE" Type="DateTime" Size="50"/>
             <asp:Parameter Name="DepOnENDDATE" Type="DateTime" Size="50"/> 
        </UpdateParameters>
        </asp:SqlDataSource>

</ContentTemplate></asp:UpdatePanel>
     <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
            $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>
</asp:Content>


