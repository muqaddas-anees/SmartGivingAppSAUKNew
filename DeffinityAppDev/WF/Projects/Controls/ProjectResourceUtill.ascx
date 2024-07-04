<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProjectResourceUtill" Codebehind="ProjectResourceUtill.ascx.cs" %>

<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
             HorizontalAlign="Left"  GridLines="None" BorderStyle="None" 
            CellPadding="0" CellSpacing="1" 
             Width="100%" AllowPaging="True" BorderWidth="0px" DataSourceID="Sqlds1" 
            EmptyDataText="No data exist." EnableViewState="false" 
             
             AllowSorting="true"  >        
            <Columns>                
             <asp:BoundField DataField="ContractorID" HeaderText="Resource" Visible="false" />
             <asp:BoundField DataField="ResourceName" HeaderText="Resource" HeaderStyle-CssClass="header_bg_l" SortExpression="ResourceName" />
             <asp:BoundField DataField="SourceTask" HeaderText="Task" SortExpression="SourceTask" />
             <asp:TemplateField ItemStyle-Width="90px">
             <ItemTemplate>
              <asp:Label ID="lbl" runat="server" Text="Clashes with"></asp:Label>
             </ItemTemplate>
             </asp:TemplateField>
            
              <asp:HyperLinkField SortExpression="destprojectref" DataTextField="DestProject" DataNavigateUrlFields="destprojectref" DataNavigateUrlFormatString="~/ProjectOverviewV2.aspx?Project={0}"  HeaderText="Project">

                </asp:HyperLinkField>
             <asp:BoundField DataField="DestTask" HeaderText="Task" />
             <asp:BoundField DataField="DestStartDate" HeaderText="Assigned From " DataFormatString="{0:d}" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="DesEndDate" HeaderText="Assigned Until " DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
             <asp:BoundField DataField="Percentage" HeaderText="% Complete" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
             <asp:BoundField DataField="NextAvilabledate" HeaderText="Next Available Date" DataFormatString="{0:d}" HeaderStyle-CssClass="header_bg_r" ItemStyle-HorizontalAlign="Center" />
</Columns>
</asp:GridView>

        <asp:SqlDataSource ID="Sqlds1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DBstring %>" 
            SelectCommand="ProjectTaskResourceUtil" SelectCommandType="StoredProcedure" >
    <SelectParameters>
    <asp:QueryStringParameter QueryStringField="Project" Name="projectref" Type="Int32" DefaultValue="0" />
    </SelectParameters>
    </asp:SqlDataSource>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 