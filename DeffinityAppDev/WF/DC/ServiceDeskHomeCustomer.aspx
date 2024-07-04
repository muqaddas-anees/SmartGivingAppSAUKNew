<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_ServiceDeskHomeCustomer" EnableEventValidation="false" Codebehind="ServiceDeskHomeCustomer.aspx.cs" %>

<%@ Register Src="Controls/ChartsTab.ascx" TagName="ChartTab" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" Runat="Server">
    <%: Resources.DeffinityRes.ServiceDesk %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" Runat="Server">
      <%: Resources.DeffinityRes.Dashboard %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_options" runat="server">
  <asp:HyperLink runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:ChartTab ID="ChartTab1" runat="server" />
     
             <div class="form-group row">
        <div class="col-md-12">
           <strong>Search Details</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>


             <div class="row">
        <div class="col-sm-12">
       
                  <div class="form-group row">
          <div class="col-md-12">
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFromdate" ForeColor="Red"
                                        ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid from date format." ValidationGroup="Site" />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txttodate" ForeColor="Red"
                                        ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid to date format." ValidationGroup="Site" />
	</div>
</div>
                  <div class="form-group row">
      <div class="col-md-4">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.FormDate%></label>
           <div class="col-sm-7 form-inline">
               <asp:TextBox ID="txtFromdate" SkinID="Date" ClientIDMode="Static" MaxLength="10"
                                   runat="server"></asp:TextBox>
                                  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFromdate" ValidChars="0123456789/" />
                               <asp:Label ID="imgDateRaised" runat="server" SkinID="Calender" />
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender7"  runat="server"
                                PopupButtonID="imgDateRaised" TargetControlID="txtFromdate" >
                            </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-7 form-inline">
               <asp:TextBox ID="txttodate" ClientIDMode="Static" MaxLength="10"
                                         SkinID="Date" runat="server"></asp:TextBox>
                                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txttodate" ValidChars="0123456789/" />
                                    <asp:Label ID="Img2" runat="server" SkinID="Calender" />
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                PopupButtonID="Img2" TargetControlID="txttodate" >
                            </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
               <asp:DropDownList  ID="ddlCustomerChart1"
                                                      runat="server" ClientIDMode="Static" DataSourceID="SqlDataSourceTitle2"
                         DataTextField="PortFolio" DataValueField="ID"></asp:DropDownList>
                      <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
</div>
                  <div class="form-group row">
      <div class="col-md-4">
           <label class="col-sm-5 control-label">Type of Request</label>
           <div class="col-sm-7">
               <asp:DropDownList ID="ddlRequestType" runat="server" ClientIDMode="Static">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdRequestType" runat="server" TargetControlID="ddlRequestType" ParentControlID="ddlCustomerChart1"
                BehaviorID="ccdType" Category="type" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetRequestTypeByCustomer" LoadingText="[Loading...]" />
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
	<div class="col-md-4">
          <asp:Button ID="BtnSearchByCustomer" runat="server" ClientIDMode="Static"
                               Text="Search" />
	</div>
</div>
                 
                     <div class="form-group row">
                           <div class="col-md-6">
                           </div>
                            <div class="col-md-3 form-inline pull-right">
                                        <asp:DropDownList ID="ddlStatus" runat="server" Visible="false"
                                            ClientIDMode="Static"> </asp:DropDownList>
                                         
                            </div>
                          <div class="col-md-3">
                              
                          </div>
                     </div>
                 
              
        </div>
    </div>
            <div>
                <div class="form-group row">
        <div class="col-md-12">
           <strong>Summary</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                <div id="divsum" style="background-color:whitesmoke;font-size:small;height:150px"></div>
                <%--<div><label>Total Number of service Request events reported</label>: <asp:Label ID="lblstotal" runat="server" ClientIDMode="Static"></asp:Label> </div>
                <div><label>Total Number of Faults Reported</label>: <asp:Label ID="lblftotal" runat="server" ClientIDMode="Static"></asp:Label></div>--%>
                </div>


             <div>
                 <div class="form-group row">
        <div class="col-md-12">
           <strong>Total Number of Open Incidents</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
           
            <div id="bar-customer-pie-1" style="height: 440px; width: 100%;"></div>
   </div>
           <%-- <div>
             <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 98%;">Total Number of Service Desk Events Reported by Priority</div>
             <div id="bar-customer-sd-priority-2" style="height: 440px; width: 100%;"></div>
                </div>--%>
    <div>
        <div class="form-group row">
        <div class="col-md-12">
           <strong>Total Number of Service Desk Events by Category</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
            
             <div id="bar-customer-category-3" style="height: 440px; width: 100%;"></div>
  </div>
             <%--<div>
             <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 98%;">Total Number of Faults Reported by Priority</div>
             <div id="bar-customer-fault-priority-3" style="height: 440px; width: 100%;"></div>
  </div>
             <div>
             <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 98%;">Total Number of Faults Events by Category</div>
             <div id="bar-customer-fault-cate-3" style="height: 440px; width: 100%;"></div>
  </div>--%>
             <div>
                 <div class="form-group row">
        <div class="col-md-12">
           <strong>Number of Incidents by Engineer</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
             
             <div id="bar-customer-engineer-3" style="height: 440px; width: 100%;"></div>
  </div>
          
    <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
    
</asp:Content>


