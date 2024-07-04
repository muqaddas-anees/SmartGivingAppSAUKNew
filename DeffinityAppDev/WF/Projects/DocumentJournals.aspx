<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="DocumentJournals" Codebehind="DocumentJournals.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="sec_header"> <%= Resources.DeffinityRes.DocumentJournals%></div>
<div class="sec_table">
       <div style="width:100%;text-align:right;font-size:85%">
    <a href="#" onclick='javascript:window.close();' ><font color="blue"><b> <%= Resources.DeffinityRes.Close%>  </b></font></a>
        </div>
 <div>
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None" HorizontalAlign="Left" BorderStyle="None" CellPadding="2" CellSpacing="2" 
             Width="685px" EmptyDataText="<%$ Resources:DeffinityRes, JournalDtNotExist%>" EnableViewState="false">                   
                <Columns>                
             <asp:BoundField DataField="DocumentTitle" HeaderText="<%$ Resources:DeffinityRes, DocumentName%>" DataFormatString="{0:d}" HtmlEncode="false">     
             </asp:BoundField> 
              <asp:BoundField DataField="DateAccessed" HeaderText="<%$ Resources:DeffinityRes, DateAccessed%>">     
             </asp:BoundField>                                 
             <asp:BoundField DataField="AccessBy" HeaderText="<%$ Resources:DeffinityRes, AccessBy%>">     
             </asp:BoundField>                                
                </Columns>
        </asp:GridView>
        
    </div>

</div>
</asp:Content>

