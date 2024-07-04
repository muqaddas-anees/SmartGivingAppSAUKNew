<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_Admin" Codebehind="trAdmin.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
      <td>
          <h1 class="section2">
              <span>
                  <label id="lblTitle" runat="server">
                  </label>
              </span>
          </h1>
          
      </td>
  </tr>
  <tr>    
    <td class="p_section2 data_carrier_block" valign="top">
    <div><asp:Label ID="lblException" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label> </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td style="width:45%" valign="top">

<div class="tab_subheader" style="border-bottom:solid 1px Silver;width:90%;">
Training Categories
</div>
<div>
Category &nbsp; <asp:DropDownList ID="ddlCategory" runat="server" Width="230px" 
        AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList> 
<asp:ImageButton ID="btnCategoryAdd" runat="server" SkinID="ImgAdd" ImageAlign="AbsMiddle"/>&nbsp;
<asp:ImageButton ID="btnCategoryEdit" runat="server" SkinID="ImgEdit" onclick="btnCategoryEdit_Click" ImageAlign="AbsMiddle"/>&nbsp;
<asp:ImageButton ID="btnDeleteCategory" runat="server" SkinID="ImgSymDel" 
        onclick="btnDeleteCategory_Click" OnClientClick="return confirm('Do you want to delete the category?');" />
</div>
<div class="clr"></div>
<div class="tab_subheader" style="border-bottom:solid 1px Silver;width:90%;">
Course Administration
</div>
<div>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td>Course</td><td>
    <asp:DropDownList ID="ddlCourse" runat="server" 
        AutoPostBack="True" 
        onselectedindexchanged="ddlCourse_SelectedIndexChanged" Width="300px"></asp:DropDownList>&nbsp;<asp:ImageButton 
        ID="btnAddCourse" runat="server" SkinID="ImgSymAdd" ImageAlign="AbsMiddle" 
        onclick="btnAddCourse_Click"/>&nbsp;<asp:ImageButton ID="btnCancel" 
        runat="server" SkinID="ImgSymCancel" ImageAlign="AbsMiddle" 
        onclick="btnCancel_Click" /> &nbsp;<asp:ImageButton ID="btnDeleteCourse" 
        runat="server" SkinID="ImgSymDel" 
         OnClientClick="return confirm('Do you want to delete the course?');" 
        onclick="btnDeleteCourse_Click" /></td>
</tr>
<tr>
<td>Course Title</td><td><asp:TextBox ID="txtCourseTitle" runat="server" 
        Width="300px"></asp:TextBox> </td>
</tr>
<tr id="panelType" runat="server" visible="false">
<td>Training Type</td><td>
    <asp:DropDownList ID="ddlTrainingType" runat="server" Width="230px">
    </asp:DropDownList>
    <asp:ImageButton ID="imgBtnAdd" runat="server" SkinID="ImgSymAdd" ImageAlign="Middle" />
    </td>
</tr>
<tr>
<td>Venue</td><td><asp:TextBox ID="txtVenue" runat="server" Width="300px"></asp:TextBox></td>
</tr>
<tr>
<td>Vendor</td><td><asp:DropDownList ID="ddlVendor" runat="server" DataTextField="" 
        DataValueField="" Width="230px"></asp:DropDownList></td>
</tr>
<tr>
<td>Rate</td><td><asp:TextBox ID="txtRate" runat="server" Width="100px"></asp:TextBox></td>
</tr>
<tr>
<td>Duration</td><td><asp:TextBox ID="txtDuration" runat="server" Width="150px"></asp:TextBox></td>
</tr>
<tr>
<td></td><td><asp:ImageButton ID="btnSubmitCourse" runat="server" 
        SkinID="ImgSubmit" onclick="btnSubmitCourse_Click" />&nbsp;<asp:ImageButton 
        ID="btnCancelCourse" runat="server" SkinID="ImgCancel" 
        onclick="btnCancelCourse_Click" /> </td>
</tr>
</table>
<div class="clr"></div>
<div>
<asp:GridView ID="Grid_Category" runat="server" Width="100%" Font-Size="X-Small">
<Columns>
<asp:BoundField DataField="CategoryName" HeaderText="Category" HeaderStyle-CssClass="header_bg_l" />
<asp:BoundField DataField="Title" HeaderText="Course" />
<asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right" HtmlEncode="false" DataFormatString="{0:F2}" />
<asp:BoundField DataField="Duration" HeaderText="Duration" HeaderStyle-CssClass="header_bg_r"/>
</Columns>
</asp:GridView>
</div>
</div>
</td>
<td style="width:5%">&nbsp;</td>
<td style="width:45%" valign="top">
<div class="tab_subheader" style="border-bottom:solid 1px Silver;width:90%;">
Department Requirements
</div>
<div>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td>Department</td><td><asp:DropDownList ID="ddlDepartment" runat="server" 
        AutoPostBack="True" 
        onselectedindexchanged="ddlDepartment_SelectedIndexChanged" Width="230px"></asp:DropDownList> <asp:ImageButton ID="BtnAddDepartment" runat="server" SkinID="ImgAdd" ImageAlign="AbsMiddle"/>&nbsp;
<asp:ImageButton ID="btnEditDepartment" runat="server" SkinID="ImgEdit" 
        onclick="btnEditDepartment_Click" ImageAlign="AbsMiddle"/>&nbsp;
        <asp:ImageButton ID="btnDeleteDepartment" runat="server" SkinID="ImgSymDel" 
        onclick="btnDeleteDepartment_Click" OnClientClick="return confirm('Do you want to delete the department?');"/>
        </td>
</tr>
<tr>
<td>Area</td><td>
    <asp:DropDownList ID="ddlArea" runat="server" Width="230px" AutoPostBack="True" 
        onselectedindexchanged="ddlArea_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:ImageButton ID="imgBtnArea" runat="server" SkinID="ImgSymAdd" />&nbsp;
    <asp:ImageButton ID="btnDeleteArea" runat="server" SkinID="ImgSymDel" 
        onclick="btnDeleteArea_Click" OnClientClick="return confirm('Do you want to delete the area?');" />
    </td>
</tr>
<tr>
<td>Customer</td><td><asp:DropDownList ID="ddlCustomer" runat="server" 
        AutoPostBack="True" 
        onselectedindexchanged="ddlCustomer_SelectedIndexChanged" Width="230px"></asp:DropDownList></td>
</tr>
<tr>
<td>Site</td><td><asp:DropDownList ID="ddlSite" runat="server" Width="230px"></asp:DropDownList></td>
</tr>
<tr>
<td>Course</td><td><asp:DropDownList ID="ddlDepCourse" runat="server" Width="230px" 
        AutoPostBack="True" onselectedindexchanged="ddlDepCourse_SelectedIndexChanged"></asp:DropDownList></td>
</tr>
<tr>
<td>Minimum Number Required</td><td><asp:TextBox ID="txtNumberReq" runat="server"></asp:TextBox> </td>
</tr>
<tr>
<td>Target</td><td><asp:TextBox ID="txtTarget" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td></td>
<td><asp:ImageButton ID="btnDepSubmit" runat="server" SkinID="ImgSubmit" 
        onclick="btnDepSubmit_Click" /> &nbsp;<asp:ImageButton ID="btnDeptCancel" 
        runat="server" SkinID="ImgCancel" onclick="btnDeptCancel_Click" />
        <asp:HiddenField ID="H_DeptoCus" runat="server" Value="0" />
         </td>
</tr>
</table>
</div>
<div class="clr"></div>
<div>
<div class="tab_subheader" style="border-bottom:solid 1px Silver;width:90%;">
Users that Belong to this department
</div>
<div>
<asp:GridView ID="Grid_DepartmentUsers" runat="server" Width="100%" Font-Size="X-Small">
<Columns>
<asp:BoundField DataField="DepartmentName" HeaderText="Department" HeaderStyle-CssClass="header_bg_l" />
<asp:BoundField DataField="AreaName" HeaderText="Area" />
<asp:BoundField DataField="ContractorName" HeaderText="User"   HeaderStyle-CssClass="header_bg_r"/>
</Columns>
</asp:GridView>
</div>
</div>
</td>
</tr>
</table>
<div>
<div><ajaxToolkit:ModalPopupExtender ID="mdlCategory" runat="server" CancelControlID="btnModelCategoryCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="btnCategoryAdd" PopupControlID="pnlCategory" />
                    <asp:Panel ID="pnlCategory" runat="server" BackColor="White"
                    Style="display: none" Width="230px" BorderStyle="Double" BorderColor="LightSteelBlue">
                    <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:96%;">
Category
</div>
                    <table>
                        <tr>
                            <td>
                               <asp:TextBox ID="txtModelCategoryInsert" runat="server" Width="210px"></asp:TextBox>
                               <asp:HiddenField ID="H_Category" runat="server" Value="0" />
                                <asp:Label ID="lblmsgCategory" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="reqval" runat="server" ControlToValidate="txtModelCategoryInsert"
                                    ErrorMessage="Please enter category" ForeColor="Red"
                                    ValidationGroup="Group_Category"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnModelCategoryInsert" runat="server" Text="OK" OnClick="btnModelCategoryInsert_Click" SkinID="ImgSubmit"
                                    ValidationGroup="Group_Category" />
                                    <asp:ImageButton ID="btnModelCategoryCancel" runat="server" Text="Close" SkinID="ImgCancel" />
                            </td>
                           
                        </tr>
                    </table>
                    
                </asp:Panel>
</div>
<div><ajaxToolkit:ModalPopupExtender ID="mdlDepartment" runat="server" CancelControlID="btnModelDepartmentCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="BtnAddDepartment" PopupControlID="pnlDepartment" />
                    <asp:Panel ID="pnlDepartment" runat="server" BackColor="White"
                    Style="display: none" Width="230px" BorderStyle="Double" BorderColor="LightSteelBlue">
                    <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:96%;">
Department
</div>
                    <table>
                        <tr>
                            <td>
                               <asp:TextBox ID="txtModelDepartment" runat="server" Width="210px"></asp:TextBox>
                               <asp:HiddenField ID="H_Department" runat="server" Value="0" />
                                <asp:Label ID="lblMsgDepartment" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtModelDepartment"
                                    ErrorMessage="Please enter department" ForeColor="Red" ValidationGroup="Group_Department"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnModelDepartmentInsert" runat="server" Text="OK" OnClick="btnModelDepartmentInsert_Click" SkinID="ImgSubmit"
                                    ValidationGroup="Group_Department" />
                                    <asp:ImageButton ID="btnModelDepartmentCancel" runat="server" Text="Close" SkinID="ImgCancel" />
                            </td>
                           
                        </tr>
                    </table>
                    
                </asp:Panel>
</div>
<div>
<ajaxToolkit:ModalPopupExtender ID="mdlTrainingType" CancelControlID="imgBtnTrainingTypeCancel" runat="server" BackgroundCssClass="modalBackground"
TargetControlID="imgBtnAdd" PopupControlID="pnlTrainingType"></ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlTrainingType" runat="server"  BackColor="White" style="display:none" Width="230px" 
BorderStyle="Double" BorderColor="LightSteelBlue">
 <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:96%;">
Training Type
</div>

 <table>
                        <tr>
                            <td>
                               <asp:TextBox ID="txtModelTrainingType" runat="server" Width="210px"></asp:TextBox>
                               <asp:HiddenField ID="H_TrainingType" runat="server" Value="0" />
                                <asp:Label ID="lblTrainingType" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtModelTrainingType"
                                    ErrorMessage="Please enter training type" ForeColor="Red" ValidationGroup="Group_Triningtype"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgBtnTrainingTypeOk" runat="server" Text="OK"  SkinID="ImgSubmit"
                                     OnClick="imgBtnTrainingTypeOk_Click" ValidationGroup="Group_Triningtype" />
                                    <asp:ImageButton ID="imgBtnTrainingTypeCancel" runat="server" Text="Close" SkinID="ImgCancel" />
                            </td>
                           
                        </tr>
                    </table>

</asp:Panel>
</div>

<div>
<ajaxToolkit:ModalPopupExtender ID="mdlArea" CancelControlID="imgAreaCancel" runat="server" BackgroundCssClass="modalBackground"
TargetControlID="imgBtnArea" PopupControlID="pnlArea"></ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlArea" runat="server"  BackColor="White" style="display:none" Width="230px" 
BorderStyle="Double" BorderColor="LightSteelBlue">
 <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:96%;">
Area
</div>

 <table>
                        <tr>
                            <td>
                               <asp:TextBox ID="txtArea" runat="server" Width="210px"></asp:TextBox>
                               <asp:HiddenField ID="H_Area" runat="server" Value="0" />
                                <asp:Label ID="lblArea" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtArea"
                                    ErrorMessage="Please enter area" ForeColor="Red" ValidationGroup="Group_Area"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgAreaSubmit" runat="server" Text="OK"  SkinID="ImgSubmit"
                                    OnClick="imgAreaSubmit_Click"  ValidationGroup="Group_Area"/>
                                    <asp:ImageButton ID="imgAreaCancel" runat="server" Text="Close" SkinID="ImgCancel" />
                            </td>
                           
                        </tr>
                    </table>

</asp:Panel>
</div>
</div>
</td>
</tr>
</table>
</asp:Content>


