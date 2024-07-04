<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AdminUsersAnnualLeave" Title="Untitled Page" Codebehind="AdminUsersAnnualLeave.aspx.cs" %>

<%@ Register src="controls/MangeUserTab.ascx" tagname="MangeUserTab" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
 
 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.ManageUsersAnnualLeave%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:LinkButton ID="btngohome" runat="server" SkinID="BtnLinkButton" Text="Return to User Admin"
                                        OnClick="btngohome_Click" CausesValidation="false"><i class="fa fa-arrow-left"></i> Return to User Admin</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
             <div class="col-md-12 form-inline">
                  <%= Resources.DeffinityRes.UserAdminfor%> <asp:Label ID="lblusername" runat='server' Font-Bold="true"></asp:Label>
</div>
</div>
    <uc1:MangeUserTab ID="MangeUserTab1" runat="server" />
    <div class="form-group">
             <div class="col-md-12">
                 <asp:Label ID="Label1" runat="server" EnableViewState="false" />
</div>
</div>
    <div class="form-group">
                                  <div class="col-md-4">
                                       <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.AllowanceDetails%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                                      <div class="form-group">
             <div class="col-md-12">
                  <asp:ValidationSummary ID="valsumadd" runat="server" ForeColor="Red" ValidationGroup="grpadd" />
</div>
</div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.User%></label>
                                      <div class="col-sm-8"><asp:Label ID="lblallowanceusername" Font-Bold="true" runat="server"></asp:Label>
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Leaveallowance%></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtleaves" runat="server" SkinID="Date"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rqdLeaves" runat="server" ControlToValidate="txtleaves"
                                                                Display="None" ValidationGroup="grpadd" ErrorMessage="<%$ Resources:DeffinityRes,Plsenternumberofdaysleave%>"  />
                                                            <asp:CompareValidator ID="cmpvalallowance" runat="server" ControlToValidate="txtleaves"
                                                                Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidleaves%>" Operator="DataTypeCheck"
                                                                Type="Double" ValidationGroup="grpadd"></asp:CompareValidator>
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Year%></label>
                                      <div class="col-sm-8">
                                          <asp:DropDownList ID="ddlyear" runat="server" SkinID="ddl_80">
                                                                <asp:ListItem Text="2010" Value="2010">2010-2011</asp:ListItem>
                                                                <asp:ListItem Text="2011" Value="2011">2011-2012</asp:ListItem>
                                                                <asp:ListItem Text="2012" Value="2012">2012-2013</asp:ListItem>
                                                                <asp:ListItem Text="2013" Value="2013">2013-2014</asp:ListItem>
                                                                <asp:ListItem Text="2014" Value="2014">2014-2015</asp:ListItem>
                                                                <asp:ListItem Text="2015" Value="2015">2015-2016</asp:ListItem>
                                                                <asp:ListItem Text="2016" Value="2016">2016-2017</asp:ListItem>
                                                                <asp:ListItem Text="2017" Value="2017">2017-2018</asp:ListItem>
                                                                <asp:ListItem Text="2018" Value="2018">2018-2019</asp:ListItem>
                                                                <asp:ListItem Text="2019" Value="2019">2019-2020</asp:ListItem>
                                                                <asp:ListItem Text="2020" Value="2020">2020-2021</asp:ListItem>
                                                            </asp:DropDownList>
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.CarriedOver%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtCarriedOver" runat="server" SkinID="Date"></asp:TextBox>
                                                            <asp:CompareValidator ID="cmpCarriedOver" runat="server" ControlToValidate="txtCarriedOver"
                                                                Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,Plsentervalidleaves%>" Operator="DataTypeCheck"
                                                                Type="Double" ValidationGroup="grpadd"></asp:CompareValidator>
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:Button ID="btnsubmit" runat="server" SkinID="btnSubmit" OnClick="btnsubmit_Click"
                                                                ValidationGroup="grpadd" />
                                                            <asp:Button ID="ImageButton2" runat="server" SkinID="btnCancel" />
					</div>
				</div>
                </div>
                                      
                                      
				</div>
 <div class="col-md-4">
     <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%=Resources.DeffinityRes.Approver %> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
     
<div class="form-group">
             <div class="col-md-12">
                  <asp:ValidationSummary ID="valsumadd1" runat="server" ForeColor="Red" ValidationGroup="grpadd1" />
                                                            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" />
</div>
</div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Approver%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlUsersapp" runat="server" AppendDataBoundItems="true" DataSourceID="objManagers"
                                                                DataTextField="Name" DataValueField="ID">
                                                                <asp:ListItem Text="<%$ Resources:DeffinityRes, PleaseSelect%>" Value="0" />
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rqdUsersapp" runat="server" ControlToValidate="ddlUsersapp"
                                                                Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectuser%>"  InitialValue="0" Text="*" ValidationGroup="grpadd1" />
                                                            <asp:ObjectDataSource ID="objManagers" runat="server" SelectMethod="LoadApprover"
                                                                TypeName="DataHelperClass">
                                                             
                                                            </asp:ObjectDataSource>
					</div>
				</div>
</div>
     <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:Button  ID="btnsubmit1" runat="server" OnClick="btnsubmit1_Click" SkinID="btnSubmit"
                                                                ValidationGroup="grpadd1" />
                                                            <asp:Button ID="ImageButton3" runat="server" SkinID="btnCancel" />
					</div>
				</div>
</div>
    
                                      
				</div>
<div class="col-md-4">
    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%= Resources.DeffinityRes.DaysinLieu%></strong>
            <hr class="no-top-margin" />
            </div>
</div>
    <div class="form-group">
             <div class="col-md-12">
                 <asp:ValidationSummary ID="ValidationSummary11" runat="server" ForeColor="Red" ValidationGroup="lieu" />
                                                            <asp:Label ID="lblDayMessg" runat="server" EnableViewState="false" />
</div>
</div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.DaysinLieu%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtDays" runat="server" SkinID="Date"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdays%>" 
                                                             ControlToValidate="txtDays" ValidationGroup="lieu" Display="None"></asp:RequiredFieldValidator>
                                           <ajaxToolkit:FilteredTextBoxExtender ID="txtfilter" runat="server" TargetControlID="txtDays"
                                                                                                                                 ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Year%></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlLieuYear" runat="server" SkinID="ddl_80">
                                                                <asp:ListItem Text="2010" Value="2010">2010-2011</asp:ListItem>
                                                                <asp:ListItem Text="2011" Value="2011">2011-2012</asp:ListItem>
                                                                <asp:ListItem Text="2012" Value="2012">2012-2013</asp:ListItem>
                                                                <asp:ListItem Text="2013" Value="2013">2013-2014</asp:ListItem>
                                                                <asp:ListItem Text="2014" Value="2014">2014-2015</asp:ListItem>
                                                                <asp:ListItem Text="2015" Value="2015">2015-2016</asp:ListItem>
                                                                <asp:ListItem Text="2016" Value="2016">2016-2017</asp:ListItem>
                                                                <asp:ListItem Text="2017" Value="2017">2017-2018</asp:ListItem>
                                                                <asp:ListItem Text="2018" Value="2018">2018-2019</asp:ListItem>
                                                                <asp:ListItem Text="2019" Value="2019">2019-2020</asp:ListItem>
                                                                <asp:ListItem Text="2020" Value="2020">2020-2021</asp:ListItem>
                                                            </asp:DropDownList>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Comments %></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtComments" runat="server" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
					</div>
				</div>
                </div>
    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:Button ID="imgSubmit" runat="server" OnClick="imgSubmit_Click" SkinID="btnSubmit"
                                                                ValidationGroup="lieu" />
                                                            <asp:Button ID="imgCancel" runat="server" SkinID="btnCancel" 
                                                                onclick="imgCancel_Click" />
					</div>
				</div>
                </div>
                                   
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-4">
                                       <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong> <%= Resources.DeffinityRes.UserAllowance%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                                      
                                                    <asp:GridView ID="GrdUserAllowance" runat="server" AutoGenerateColumns="False" DataSourceID="objUserAllowance"
                                                        EmptyDataText="<%$ Resources:DeffinityRes,Nodataavailable%>" >
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes,ID%>"  SortExpression="ID" Visible="false" />
                                                            <asp:BoundField DataField="UserID" HeaderText="<%$ Resources:DeffinityRes,UserID%>"  SortExpression="UserID" Visible="false" />
                                                            <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:DeffinityRes,UserName%>"  SortExpression="UserName"
                                                                Visible="false" />
                                                            <asp:BoundField DataField="LeaveAllowance" HeaderText="<%$ Resources:DeffinityRes,LeaveAllowance%>"  SortExpression="LeaveAllowance"  ItemStyle-HorizontalAlign="Right">
                                                                <HeaderStyle CssClass="header_bg_l" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CarriedOver" HeaderText="<%$ Resources:DeffinityRes,CarriedOver%>"  SortExpression="CarriedOver"  ItemStyle-HorizontalAlign="Right" />
                                                            <asp:BoundField DataField="Available" HeaderText="<%$ Resources:DeffinityRes,Available%>"  SortExpression="Available"  ItemStyle-HorizontalAlign="Right" />
                                                            <asp:TemplateField HeaderText="Year">
                                                                <HeaderStyle CssClass="header_bg_r" />
                                                            <ItemTemplate>
                                                            <asp:Label ID="lblYear" Text='<%# YearFormate(Eval("Year").ToString()) %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="Year" HeaderText="<%$ Resources:DeffinityRes,Year%>"  SortExpression="Year">
                                                                <HeaderStyle CssClass="header_bg_r" />
                                                            </asp:BoundField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                      
				</div>
 <div class="col-md-4">
      <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.UsrandApproverAssociations%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                       
                                                    <asp:GridView ID="gridUserMappings" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                                        DataSourceID="ObjectDataSource1" EmptyDataText="<%$ Resources:DeffinityRes,Noassotionmadethisperson %>"  Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:DeffinityRes,User%>"  SortExpression="UserName">
                                                                <HeaderStyle CssClass="header_bg_l" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ApproverName" HeaderText="<%$ Resources:DeffinityRes,Approver%>"  SortExpression="ApproverName" />
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" SkinID="BtnLinkDelete" Text="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
                                                        SelectMethod="SelectApproverByUser" TypeName="VT.DAL.LeaveApproverHelper">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="getUserId" DefaultValue="0" Name="userId" PropertyName="Value"
                                                                Type="Int32" />
                                                        </SelectParameters>
                                                        <DeleteParameters>
                                                            <asp:Parameter Name="ID" Type="Int32" />
                                                        </DeleteParameters>
                                                    </asp:ObjectDataSource>
                                                    <asp:ObjectDataSource ID="objUserAllowance" runat="server" SelectMethod="SelectAllowanceByUser"
                            TypeName="VT.DAL.LeaveApproverHelper" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="getUserId" Name="UserID" PropertyName='Value' DefaultValue="272"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>              
				</div>
<div class="col-md-4">
     <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong> <%= Resources.DeffinityRes.AvailabledaysinLieu%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                       
                                                    <asp:GridView ID="grdDaysLeave" runat="server" AutoGenerateColumns="False" 
                                                        EmptyDataText="<%$ Resources:DeffinityRes,NodaysinLieu%>" 
                                                       onrowcommand="grdDaysLeave_RowCommand" 
                                                        onrowdeleting="grdDaysLeave_RowDeleting" Width="100%">
                                                        <Columns>
                                                           
                                                            <asp:BoundField DataField="UserName" HeaderText="<%$ Resources:DeffinityRes,User%>"   ItemStyle-HorizontalAlign="Left">
                                                                <HeaderStyle CssClass="header_bg_l" />

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Days" HeaderText="<%$ Resources:DeffinityRes,Days%>"  
                                                                ItemStyle-HorizontalAlign="Right" >
                                                            
                                                            
<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Year">
                                                               <ItemTemplate>
                                                            <asp:Label ID="lblYear2" Text='<%# YearFormate(Eval("Year").ToString()) %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <%--<asp:BoundField DataField="Year" HeaderText="<%$ Resources:DeffinityRes,Year%>" ItemStyle-HorizontalAlign="Right" />--%>
                                                            <asp:BoundField DataField="Comments" HeaderText="<%$ Resources:DeffinityRes,Comments%>"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            </asp:BoundField>
                                                            
                                                            <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="imgDelete" runat="server" CommandArgument='<%#Bind("ID")%>' 
                                                                CommandName="Delete" SkinID="BtnLinkDelete" />
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>                
				</div>
</div>
   
    <asp:HiddenField ID="getUserId" runat="server" />
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>

