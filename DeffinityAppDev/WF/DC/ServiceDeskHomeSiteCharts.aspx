<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_ServiceDeskHomeSiteCharts" Codebehind="ServiceDeskHomeSiteCharts.aspx.cs" %>

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
        <div>
            <div class="form-group row">
        <div class="col-md-12">
           <strong>Search Details</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>



             <div class="row">
        <div class="col-sm-12">
        <div class="card shadow-sm">
            
          <div class="panel-body">
              <div class="row">
                  <div class="form-group row">
          <div class="col-md-12">
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFromdate" ForeColor="Red"
                                        ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid from date format." ValidationGroup="Site" />
              <asp:RegularExpressionValidator runat="server" ControlToValidate="txttodate" ForeColor="Red"
                                        ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid to date format." ValidationGroup="Site" />
	</div>
</div>
                   <div class="form-group row">
      <div class="col-md-3">
          <label class="col-sm-5 control-label">From Date</label>
           <div class="col-md-7 form-inline">
          <asp:TextBox ID="txtFromdate" SkinID="Date" ClientIDMode="Static" MaxLength="10"
                                   runat="server"></asp:TextBox>
                                  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFromdate" ValidChars="0123456789/" />
                               <asp:Label ID="imgDateRaised" runat="server" SkinID="Calender" />
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender7"  runat="server"
                                PopupButtonID="imgDateRaised" TargetControlID="txtFromdate" >
                            </ajaxToolkit:CalendarExtender>
               </div>
          </div>
                        <div class="col-md-3">
                            <label class="col-sm-5 control-label">To Date</label>
                             <div class="col-md-7 form-inline">
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
                            <label class="col-sm-4 control-label">Customer</label>
                             <div class="col-md-8 form-inline">
                            <asp:DropDownList SkinID="ddl_90" ID="ddlCustomer"
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
                        <div class="col-md-2">
                            <asp:Button ID="BtnSearchInsite" runat="server" ClientIDMode="Static"
                               Text="Search" />
          </div>
                       </div>
                 
                 
                     <div class="form-group row">
                           <div class="col-md-6">
                                   <div class="col-sm-12" style="overflow-y:scroll;height:100px; border-radius:5px; border:1px solid #8AC007;padding:10px; width: 300px;display:none">
                                                     <asp:CheckBoxList ID="checklistforsites" runat="server"></asp:CheckBoxList>
                                              </div>
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
        </div>
        </div>
    </div>
           

                <div class="form-group row">
        <div class="col-md-12">
           <strong> <asp:Label ID="Label1" runat="server" ClientIDMode="Static"></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
           <div class="row">
            <div id="bar-site-1" style="height: 440px; width: 100%;"></div>
               </div>
   
            
                <div class="form-group row">
        <div class="col-md-12">
           <strong> <asp:Label ID="Label2" runat="server" ClientIDMode="Static"></asp:Label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
           <div class="row">
             <div id="bar-site-2" style="height: 440px; width: 100%;"></div>
               </div>
               
   
        <div class="form-group row">
        <div class="col-md-12">
           <strong><asp:Label ID="Label3" runat="server" ClientIDMode="Static"></asp:Label> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
            <div class="row">
             <div id="bar-site-3" style="height: 440px; width: 100%;"></div>
                </div>
  

            </div>
         
       
    <%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
    
</asp:Content>


