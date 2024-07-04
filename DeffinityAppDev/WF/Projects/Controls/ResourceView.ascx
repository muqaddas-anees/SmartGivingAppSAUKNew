<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ResourceView" Codebehind="ResourceView.ascx.cs" %>

<div class="form-group">
      <div class="col-md-6">
          <div class="form-group">
      <div class="col-md-4">
          <asp:Panel ID="panelUser"  runat="server" BackColor="White"
                     Width="100px" Height="115px" BorderStyle="Solid" BorderWidth="1" BorderColor="LightSteelBlue" >
                     <asp:HyperLink ID="imgUser" runat="server"  Visible="false" ></asp:HyperLink>
                    </asp:Panel>
	</div>
	<div class="col-md-8">
           <asp:Label ID="lblResName" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label> <br />
<div style="width:70px;float:left;font-weight:bold"><label>Address:</label></div> 
<div style="width:200px;float:left;padding-top:5px;">
<asp:Label ID="lblAddress1" runat="server" Width="200px"></asp:Label><br /> 
  <asp:Label ID="lblAddress2" runat="server" Width="200px"></asp:Label><br /> 
  <asp:Label ID="lblTown" runat="server" Width="200px"></asp:Label><br />  
  <asp:Label ID="lblCounty" runat="server" Width="200px"></asp:Label> <br /> 
  <asp:Label ID="lblPostcode" runat="server" Width="200px"></asp:Label> <br /> 
  <asp:Label ID="lbllCountry" runat="server" Width="200px"></asp:Label> <br /> 
</div>
	</div>
	
</div>
          
	</div>
	<div class="col-md-6">
          
<div class="row">
          <div class="col-md-12">
 <strong><asp:Label ID="lblYear" runat="server" ></asp:Label></strong> 
<hr class="no-top-margin" />
	</div>
</div>
        <asp:GridView ID="grdLeave" runat="server" AutoGenerateColumns="false" 
        EmptyDataText="No leave records exist" AllowPaging="True" 
        onpageindexchanging="grdLeave_PageIndexChanging" Width="">
<Columns>
<asp:TemplateField HeaderText="Leave" HeaderStyle-CssClass="header_bg_l">
<ItemTemplate>
<asp:Label ID="lblLeave" runat="server" Text='<%# Bind("AbsenseTypeName") %>'></asp:Label>
</ItemTemplate>

<HeaderStyle CssClass="header_bg_l"></HeaderStyle>

<ItemStyle Width="200px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="From">
<ItemTemplate>
<asp:Label ID="lblFromDate" runat="server" Text='<%# Bind("FromDate","{0:d}") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="70px" HorizontalAlign="Center" />

</asp:TemplateField>
<asp:TemplateField HeaderText="To">
<ItemTemplate>
<asp:Label ID="lblToDate" runat="server" Text='<%# Bind("ToDate","{0:d}") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="70px" HorizontalAlign="Center" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Days">
<ItemTemplate>
<asp:Label ID="lblDays" runat="server" Text='<%# Bind("days") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="40px" HorizontalAlign="Center" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Notes" HeaderStyle-CssClass="header_bg_r">
<ItemTemplate>
<asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>

	</div>
	
</div>

<div class="form-group">
      <div class="col-md-6">
          
<div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.LiveProjects%> </strong> 
<hr class="no-top-margin" />
	</div>
</div>

<asp:GridView ID="grdLProjects" runat="server" AutoGenerateColumns="false" 
        EmptyDataText="No Live Projects" AllowPaging="True" 
        onpageindexchanging="grdLProjects_PageIndexChanging">
<Columns>
<asp:TemplateField HeaderText="Project Reference" HeaderStyle-CssClass="header_bg_l">
<ItemTemplate>
<asp:Label ID="lblLPrjRef" runat="server" Text='<%# Bind("ProjectReferenceWithPrefix") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="70px" />

</asp:TemplateField>
<asp:TemplateField HeaderText="Title">
<ItemTemplate>
<asp:Label ID="lblLPrjTitle" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="250px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="End Date">
<ItemTemplate>
<asp:Label ID="lblLPrjEnd" runat="server" Text='<%# Bind("ProjectEndDate","{0:d}") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="70px" HorizontalAlign="Center"/>
<HeaderStyle CssClass="header_bg_r"></HeaderStyle>
</asp:TemplateField>
</Columns>
</asp:GridView>
          
	</div>
	<div class="col-md-6">
        
<div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.Training%> </strong> 
<hr class="no-top-margin" />
	</div>
</div>

<asp:GridView ID="grdTrainingRecords" runat="server"  DataKeyNames="ID" 
        AllowPaging="True"  PageSize="10" EmptyDataText="No Records Found" 
        onpageindexchanging="grdTrainingRecords_PageIndexChanging" onselectedindexchanging="grdTrainingRecords_SelectedIndexChanging" >
                            <Columns>
                              
                                <asp:TemplateField HeaderText="Training" HeaderStyle-CssClass="header_bg_l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTraining" runat="server" Text='<%# Bind("CourseTitle") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="250px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Start Date"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("DateofCourse","{0:d}")%>' ></asp:Label>
                                    </ItemTemplate>
                                   <ItemStyle Width="70px" HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="End Date" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate","{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                   <ItemStyle Width="70px" HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="header_bg_r" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("StatusName")%>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="70px" HorizontalAlign="Center"/>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
          
	</div>
	
</div>

<div class="form-group">
      <div class="col-md-6">
          
<div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.PendingProjects%> </strong> 
<hr class="no-top-margin" />
	</div>
</div>

<asp:GridView ID="grdPenProjects" runat="server" AutoGenerateColumns="false" 
        EmptyDataText="No Pending Projects" AllowPaging="True"  
        onpageindexchanging="grdPenProjects_PageIndexChanging">
<Columns>
<asp:TemplateField HeaderText="Project Reference" HeaderStyle-CssClass="header_bg_l">
<ItemTemplate>
<asp:Label ID="lblPPrjRef" runat="server" Text='<%# Bind("ProjectReferenceWithPrefix") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="70px" />
<HeaderStyle CssClass="header_bg_l"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Title">
<ItemTemplate>
<asp:Label ID="lblPPrjTitle" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="250px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="End Date">
<ItemTemplate>
<asp:Label ID="lblPPrjEnd" runat="server" Text='<%# Bind("ProjectEndDate","{0:d}") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="70px" HorizontalAlign="Center"/>
<HeaderStyle CssClass="header_bg_r"></HeaderStyle>
</asp:TemplateField>
</Columns>
</asp:GridView>
          
	</div>
	<div class="col-md-6">
        
<div class="row">
          <div class="col-md-12">
 <strong><%= Resources.DeffinityRes.Skills%> </strong> 
<hr class="no-top-margin" />
	</div>
</div>

<asp:GridView ID="grdUserSkills" runat="server" DataKeyNames="ID" EmptyDataText="No Skills exist for this user"  >
                            <Columns>
                                <asp:TemplateField HeaderText="Skills" SortExpression="Skills" HeaderStyle-CssClass="header_bg_l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSkills" runat="server" Text='<%# Bind("Skills") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" />
                                    <HeaderStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Level" SortExpression="SkillLevel">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSkillLevel" runat="server" Text='<%# Bind("SkillLevel") %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes" SortExpression="Notes" HeaderStyle-CssClass="header_bg_r">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes")%>' Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                    <HeaderStyle Width="80px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
          
	</div>
	
</div>

