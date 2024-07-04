<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="OpsApproval" Title="OpsApproval" Codebehind="Checkpoint_Summary.aspx.cs" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CheckPoints%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.CheckpointsSumm%> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
<div class="row">
          <div class="col-md-12">
 <strong> Requests Requiring Approval </strong> 
<hr class="no-top-margin" />
	</div>
</div>
     <asp:Panel ID="Panel1" runat="server" >
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" >
                     
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                         <asp:HyperLinkField HeaderStyle-CssClass="header_bg_l"  
                        DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString= "~/WF/Projects/Checkpoint/CheckpointChecklist.aspx?checkpoint=0&Project={0}" Text="Checkpoint Review">
                                 <ItemStyle Width="100px" />
                                 <HeaderStyle Width="100px" />
                             </asp:HyperLinkField>
                             <asp:HyperLinkField HeaderText="Project Reference" DataTextField="ProjectReferenceWithPrefix" 
                        DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString= "~/WF/Projects/Checkpoint/Checkpoint_Overview.aspx?checkpoint=0&Project={0}">
                                 <ItemStyle Width="100px" />
                                 <HeaderStyle Width="100px" />
                             </asp:HyperLinkField>                                            
                        <asp:BoundField DataField="ProjectTitle" HeaderText="Project Title" >
                            <ItemStyle Width="225px" />
                        </asp:BoundField>
                         <asp:HyperLinkField HeaderText="" Text="Form"
                        DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString= "~/WF/Projects/Checkpoint/ProjectCheckpointForm.aspx?checkpoint=0&Project={0}">
                                 <ItemStyle Width="100px" />
                                 <HeaderStyle Width="100px" />
                             </asp:HyperLinkField>                 
                        <asp:BoundField DataField="OwnerName" HeaderText="Owner">
                           <ItemStyle Width="150px" />
                        </asp:BoundField>                                               
                        <asp:BoundField DataField="SiteName" HeaderText="Site" >
                           <ItemStyle Width="150px" />
                        </asp:BoundField>
                        
                    </Columns>
                </asp:GridView>
    </asp:Panel> 


   
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 
</asp:Content>

