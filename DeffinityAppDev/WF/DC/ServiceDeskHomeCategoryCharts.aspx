<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_ServiceDeskHomeCategoryCharts" EnableEventValidation="false" Codebehind="ServiceDeskHomeCategoryCharts.aspx.cs" %>

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
        <div class="col-md-12">
        
                  <div class="form-group row">
          <div class="col-md-12">
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFromdate" ForeColor="Red"
                                        ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid from date format." ValidationGroup="Category" />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txttodate" ForeColor="Red"
                                        ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid to date format." ValidationGroup="Category" />
	</div>
</div>

                    <div class="form-group row">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtFromdate" SkinID="Date" ClientIDMode="Static" MaxLength="10"
                                   runat="server"></asp:TextBox>
                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFromdate" ValidChars="0123456789/" />
                            
                              <asp:Label ID="imgDateRaised" runat="server" SkinID="Calender" />
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender7"  runat="server"
                                PopupButtonID="imgDateRaised" TargetControlID="txtFromdate" >
                            </ajaxToolkit:CalendarExtender>
               </div>
          </div>
         <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txttodate" class="form-control" ClientIDMode="Static" MaxLength="10"
                                         SkinID="Date" runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txttodate" ValidChars="0123456789/" />
                            
                                    <asp:Label ID="Img2" runat="server" SkinID="Calender" />
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                PopupButtonID="Img2" TargetControlID="txttodate" >
                            </ajaxToolkit:CalendarExtender>
               </div>
          </div>
       <div class="col-md-4">
           <label class="col-sm-3 control-label">Customer</label>
           <div class="col-sm-9 form-inline">
               <asp:DropDownList SkinID="ddl_90" ID="ddlCustomerCat1"
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
           <label class="col-sm-3 control-label">Type of Request</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlRequestTypeCat" runat="server" Width="180px" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlRequestTypeCat_SelectedIndexChanged">
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="ccdRequestType" runat="server" TargetControlID="ddlRequestTypeCat" ParentControlID="ddlCustomerCat1"
                BehaviorID="ccdType" Category="type" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetRequestTypeByCustomer" LoadingText="[Loading...]" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label">Category</label>
           <div class="col-sm-9" style="overflow-y:scroll;height:100px; border-radius:5px; border:1px solid;padding:10px; width: 220px;border-color:silver;"><asp:CheckBoxList ID="checklistforCategory"  CssClass="col-sm-8 control-label"
                                            ClientIDMode="Static" runat="server"></asp:CheckBoxList>
                 </div>
	</div>
	<div class="col-md-4">
           <asp:Button ID="BtnSearchCat" runat="server" ClientIDMode="Static"
                               Text="Search" />
                                       <asp:DropDownList ID="ddlStatus" runat="server" Visible="false"
                                            ClientIDMode="Static"> </asp:DropDownList>
	</div>
</div>
                 
              
        </div>
    </div>
            <div>
                <div class="form-group row">
        <div class="col-md-12">
           <strong><asp:Label ID="Label1" runat="server" ClientIDMode="Static"></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
            
            <div id="bar-Category-1" style="height: 440px; width: 100%;"></div>
    </div>
            <div>
                <div class="form-group row">
        <div class="col-md-12">
           <strong><asp:Label ID="Label2" runat="server" ClientIDMode="Static"></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
           
            <div id="bar-Category-2" style="height: 440px; width: 100%;"></div>
      </div>
            <div>
                <div class="form-group row">
        <div class="col-md-12">
           <strong><asp:Label ID="Label3" runat="server" ClientIDMode="Static"></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
            
            <div id="bar-Category-3" style="height: 440px; width: 100%;"></div>
                </div>
     
      <%--<div class="row">
        <div class="col-sm-12">
            <div class="card shadow-sm">
                <div class="card-header">
                    <h3 class="card-body">Revenue by Category</h3>
                    <div class="card-toolbar">
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">&ndash;</span>
									<span class="expand-icon">+</span>
								</a>
								<a href="#" data-toggle="remove">
									&times;
								</a>
							</div>
                </div>
                <div class="panel-body">
                    <div id="bar-Category-4" style="height: 440px; width: 100%;"></div>
                </div>
            </div>
        </div>
    </div>--%>






   
   <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
    
</asp:Content>


