<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainFrame.Master" 
         Inherits="Reports_ApproversReport" Codebehind="ApproversReport.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Approvers Report" Font-Size="Large"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <div class="col-xs-4">
           <label class="col-sm-3 control-label">Order&nbsp;By</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlSortOption" runat="server" SkinID="ddl_70">
                                <asp:ListItem Selected="True" Value="1">First Name</asp:ListItem>
                                <asp:ListItem  Value="2">Surname</asp:ListItem>
                </asp:DropDownList>
            </div>
	</div>
     	<div class="col-xs-4 form-inline">
           <label class="col-sm-3 control-label">Status</label>
           <div class="col-sm-9 form-inline">
                 <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_60">
                                 <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                 <asp:ListItem  Value="InActive">InActive</asp:ListItem>
                 </asp:DropDownList>
                   &nbsp; <asp:Button ID="imgviewReport" runat="server" SkinID="btnDefault" Text="View Reports"
                                                                                                   onclick="imgviewReport_Click" />
            </div>
	</div>
	    <div class="col-xs-4">
      &nbsp;&nbsp;
                <asp:LinkButton ID="btnExportExcel" runat="server" Font-Bold="true" 
                            onclick="btnExportExcel_Click" ForeColor="Navy">Excel format 1</asp:LinkButton>
                            &nbsp; &nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" 
                             ForeColor="Navy" onclick="LinkButton1_Click">Excel format 2</asp:LinkButton>
	</div>
    </div>


    <div class="form-group">
        <div class="col-md-12">
           <strong>View Reports </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>


    <div class="form-group">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanel1">
                      <ProgressTemplate>
                              <img src="../media/ico_loading.gif" alt="loading" />
                      </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updatepanel1" runat="server">
            <ContentTemplate>
                <div style="z-index:-1000;">
                    <iframe id="TimesheetSummary" name="TimesheetSummary" runat="server" frameborder="0" width="100%" height="600px" scrolling="auto"></iframe>
                </div>
            </ContentTemplate>
            <Triggers>

            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
