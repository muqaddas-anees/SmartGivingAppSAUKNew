<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ResourceDashboard" Codebehind="ResourceDashboard.aspx.cs" %>
<%@ Register src="controls/DashboardTabs.ascx" tagname="DashboardTabs" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:DashboardTabs ID="DashboardTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Dashboard%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.MyTeam%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 

<div class="form-group">
      <div class="col-md-5">
            <asp:Panel ID="ResourcePanel1" runat="server" width="100%" Height="540px" BorderStyle="Solid" BorderColor="LightSteelBlue" BorderWidth="1px">
                


                <div class="form-group" style="padding-top:5px;">
      <div class="col-md-12">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SelectUser%></label>
           <div class="col-sm-5">
               <asp:DropDownList ID="DDLUsers" runat="server" Width="165px">
               </asp:DropDownList>
            </div>
           <div class="col-sm-3">
               <asp:Button ID="btnAdd" runat="server" SkinID="btnAdd" 
                    onclick="btnAdd_Click" />
               </div>
	</div>
	
</div>


               <asp:Panel ID="Panel1" runat="server" width="100%" Height="500px" >
                <asp:Repeater ID="RepeatInformation" runat="server" 
                    onitemdatabound="RepeatInformation_ItemDataBound" 
                       onitemcommand="RepeatInformation_ItemCommand" >
                 <HeaderTemplate>
               </HeaderTemplate>
               
         <ItemTemplate>
         
               <div style="width:100%;height:120px;cursor:hand;padding:5px;background-color:#f6fafb;border-top:#dbe6e9 1px solid;border-bottom:#dbe6e9 1px solid;text-decoration: none;" onmouseover="style.backgroundColor='WhiteSmoke'" onmouseout="style.backgroundColor='#f6fafb'">
                   <a class="a_underline" href='ResourceDashboardByID.aspx?uid=<%#DataBinder.Eval(Container, "DataItem.ID")%>&uname=<%#DataBinder.Eval(Container, "DataItem.ContractorName")%>' target="frm_setpage">
                       <div class="form-group">
      <div class="col-md-4">
           <%#GetUserImage((string)Eval("ID").ToString())%>
	</div>
	<div class="col-md-6">
         <div  style="width:240px;float:right;padding-left:3px;">
                       <b style="color:Blue"> <%#DataBinder.Eval(Container,"DataItem.ContractorName")%></b>
                       <div style="width:50px;float:right;text-align:right;color:Black;padding-right:100px"><b><%#DataBinder.Eval(Container, "DataItem.OverallUtilisation")%>%</b> </div>
                       <div>&nbsp;</div>
                       <div style="vertical-align:bottom;color:Black;">
                       <br />
                       <b>Mobile: </b><%#DataBinder.Eval(Container, "DataItem.ContactNumber")%><br />
                       <b>Email: </b><%#DataBinder.Eval(Container, "DataItem.EmailAddress")%><br />
                        </div>
                        </div>
	</div>
	<div class="col-md-2">
          <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="DeleteResource"
                                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>'
                                        OnClientClick="return confirm('Do you want to delete the record?');" />
	</div>
</div>
                   
                    
                  
                        </a>
                        </div>
                        
            </ItemTemplate>
            <AlternatingItemTemplate>
               

            <div style="width:100%;height:100px;cursor:hand;padding:5px;text-decoration:none;" onmouseover="style.backgroundColor='WhiteSmoke'" onmouseout="style.backgroundColor=''">
                   <a href='ResourceDashboardByID.aspx?uid=<%#DataBinder.Eval(Container, "DataItem.ID")%>&uname=<%#DataBinder.Eval(Container, "DataItem.ContractorName")%>' target="frm_setpage">

                        <div class="form-group">
      <div class="col-md-4">
          <%#GetUserImage((string)Eval("ID").ToString())%>
	</div>
	<div class="col-md-6">
            <div  style="width:240px;float:right;padding-left:3px;">
                       <b style="color:Blue"> <%#DataBinder.Eval(Container,"DataItem.ContractorName")%></b>
                       <div style="width:50px;float:right;text-align:right;color:Black;padding-right:100px"><b><%#DataBinder.Eval(Container, "DataItem.OverallUtilisation")%>%</b> </div>
                       <div>&nbsp;</div>
                       <div style="vertical-align:bottom;color:Black;">
                       <br />
                       <b>Mobile:  </b><%#DataBinder.Eval(Container, "DataItem.ContactNumber")%><br />
                       <b>Email: </b><%#DataBinder.Eval(Container, "DataItem.EmailAddress")%><br />
                        </div>
                        </div>
	</div>
	<div class="col-md-2">
          <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="DeleteResource"
                                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>'
                                        OnClientClick="return confirm('Do you want to delete the record?');" />
	</div>
</div>
                    
                   
                 
                        
                        </a>
                        </div>
            </AlternatingItemTemplate>
            
            <FooterTemplate>
            </FooterTemplate>
            </asp:Repeater>
            </asp:Panel>
        </asp:Panel>
	</div>
	<div class="col-md-7" style="vertical-align:top;">
          <iframe id="frm_setpage" name="frm_setpage" runat="server" width="100%" frameborder="0" scrolling="no" visible="true"  ></iframe>
	</div>
</div>

<script language="javascript" type="text/javascript">
    function iFrameHeight() {
        if (document.getElementById && !(document.all)) {
            h = document.getElementById("<%=frm_setpage.ClientID%>").contentDocument.body.scrollHeight + 50;
            document.getElementById("<%=frm_setpage.ClientID%>").style.height = h + "px";
        }
        else if (document.all) {
        h = document.frames("<%=frm_setpage.ClientID%>").document.body.scrollHeight + 50;
            //document.all.iframename.style.height = h + "px";
            document.getElementById("<%=frm_setpage.ClientID%>").style.height = h + "px";
        }
    }
    </script>
   
</asp:Content>


