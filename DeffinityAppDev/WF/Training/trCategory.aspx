<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trCategory" Codebehind="trCategory.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register src="controls/TrainingSubTabs.ascx" tagname="TrainingSubTabs" tagprefix="uc2" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.TrainingMgmt%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    <label id="lblTitle" runat="server"></label><br />
       <uc2:trainingsubtabs ID="TrainingSubTabs1" runat="server" />
    </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" Runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 
    <div><asp:Label ID="lblException" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label> </div>    
 <div class="form-group">
         <h3 class="panel-title">
       <label style="font:bold"><%= Resources.DeffinityRes.TrainingDirectory%></label> 
             </h3><hr />
</div>
       <div class="form-group">
            <div class="col-xs-2">
                  <label><%= Resources.DeffinityRes.Category%></label>
            </div>
            <div class="col-xs-10 form-inline">
                 <asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_30"
                             AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList> 
                <asp:Button ID="btnCategoryAdd" runat="server" SkinID="btnAdd" OnClick="btnCategoryAdd_Click" />
           <asp:Button ID="btnCategoryEdit" runat="server" SkinID="btnEdit" onclick="btnCategoryEdit_Click" />
          <asp:Button ID="btnDeleteCategory" runat="server" SkinID="btnDefault" Text="Delete"
        onclick="btnDeleteCategory_Click" OnClientClick="return confirm('Do you want to delete the category?');" />
            </div>
       </div>
                  <div class="form-group">
                        <h3 class="panel-title">
               <label style="font:bold"><%= Resources.DeffinityRes.CourseAdministration%></label> 
                            </h3>  <hr />
                </div>
                     <div class="form-group">
                           <div class="col-xs-2">
                                <label><%= Resources.DeffinityRes.Course%></label>
                          </div>
                           <div class="col-xs-10 form-inline">
                               <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" SkinID="ddl_30"
                                    onselectedindexchanged="ddlCourse_SelectedIndexChanged"></asp:DropDownList>
                                 <asp:Button ID="btnAddCourse" runat="server" SkinID="btnAdd" OnClick="btnAddCourse_Click" />
                                 <asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" onclick="btnCancel_Click" /> 
                                      <asp:Button ID="btnDeleteCourse" runat="server" SkinID="btnDefault" Text="Delete"
                                           OnClientClick="return confirm('Do you want to delete the course?');"
                                                  onclick="btnDeleteCourse_Click" />
                          </div>
                         </div>
                     <div class="form-group">
                           <div class="col-xs-2">
                                <label><%= Resources.DeffinityRes.CourseTitle%></label>
                          </div>
                           <div class="col-xs-10">
                               <asp:TextBox ID="txtCourseTitle" SkinID="txt_30" runat="server"></asp:TextBox>
                          </div>
                          </div>
                     <div class="form-group">
                           <div class="col-xs-2">
                                <label>Course Description</label>
                          </div>
                           <div class="col-xs-10">
                               <asp:TextBox ID="txtCourseDesc" runat="server" SkinID="txt_30" Height="60px" TextMode="MultiLine" ></asp:TextBox>
                          </div>
                     </div>
                      <div class="form-group" style="display:none">
                           <div class="col-xs-2">
                                <label><%= Resources.DeffinityRes.TrainingType%></label>
                          </div>
                           <div class="col-xs-7 form-inline">
                                 <asp:DropDownList ID="ddlTrainingType" SkinID="ddl_30" runat="server"></asp:DropDownList>
                                  <asp:Button ID="imgBtnAdd" runat="server" SkinID="btnAdd" />
                            </div>
                     </div>
                     <div class="form-group">
                           <div class="col-xs-2">
                                <label><%= Resources.DeffinityRes.Requirement%></label>
                          </div>
                           <div class="col-xs-10">
                                <asp:DropDownList SkinID="ddl_30" ID="ddlRequirement" runat="server"></asp:DropDownList>
                          </div>
                      </div>
                    <div class="form-group">
                           <div class="col-xs-2">
                                <label><%= Resources.DeffinityRes.Venue%></label>
                          </div>
                           <div class="col-xs-10">
                               <asp:TextBox ID="txtVenue" SkinID="txt_30" runat="server"></asp:TextBox>
                          </div>
                           </div>
                     <div class="form-group">
                           <div class="col-xs-2">
                                 <label><%= Resources.DeffinityRes.Vendor%></label>
                          </div>
                           <div class="col-xs-10">
                               <asp:DropDownList ID="ddlVendor" runat="server" DataTextField="" DataValueField="" SkinID="ddl_30"></asp:DropDownList>
                          </div>
                         </div>
                     <div class="form-group">
                           <div class="col-xs-2">
                                <label><%= Resources.DeffinityRes.Rate%></label>
                          </div>
                           <div class="col-xs-10">
                               <asp:TextBox ID="txtRate" runat="server" SkinID="txt_30"></asp:TextBox>
                          </div>
                       </div>
                      <div class="form-group">
                             <div class="col-xs-2">
                                 <label><%= Resources.DeffinityRes.Duration%></label>
                          </div>
                           <div class="col-xs-10">
                               <asp:TextBox ID="txtDuration" runat="server" SkinID="txt_30"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                           <div class="col-xs-2">
                                 <label><%= Resources.DeffinityRes.Classifications%></label>
                          </div>
                           <div class="col-xs-10">
                                  <asp:ListBox ID="lstboxClassification" runat="server" Height="80px"
                                      SelectionMode="Multiple" Width="299px"></asp:ListBox>
                              <asp:CheckBox ID="chkTrue" runat="server" Text="Schedule in GAP analysis" />
                          </div>
                            <div class="col-xs-3"></div>
                          </div>
                        <div class="form-group">
                          <div class="col-xs-6">
                               <asp:Button ID="btnSubmitCourse" runat="server" 
                                           SkinID="btnSubmit" onclick="btnSubmitCourse_Click" />&nbsp; 
                                  <asp:Button ID="btnCancelCourse" runat="server" SkinID="btnCancel"
                                              onclick="btnCancelCourse_Click" />
                          </div>
                    </div>
<div>
</div>
<div>
<div>
<asp:GridView ID="Grid_Category" runat="server" Width="100%">
<Columns>
<asp:BoundField DataField="CategoryName" HeaderText="Category" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_bg_l" />
<asp:BoundField DataField="Title" HeaderText="Course" ItemStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="CourseDesc" HeaderText="Course Description" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right" HtmlEncode="false" DataFormatString="{0:F2}" />
<asp:BoundField DataField="Duration" HeaderText="Duration" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="header_bg_r"/>
</Columns>
</asp:GridView>
</div>
</div>


<div><ajaxToolkit:ModalPopupExtender ID="mdlCategory" runat="server" CancelControlID="btnModelCategoryCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="btnCategoryAdd" PopupControlID="pnlCategory" />

                    <asp:Panel ID="pnlCategory" runat="server"  BackColor="White" Style="display: none"
                                                Width="230px" BorderStyle="Double" BorderColor="LightSteelBlue">

                          <div class="form-group">
                                <div class="col-md-12 text-bold">
                                       <strong>   <%= Resources.DeffinityRes.Category%></strong>
                                </div>
                          </div>
                          <div class="form-group">
                                 <div class="col-xs-12">
                                        <div class="form-inline">
                                             <asp:TextBox ID="txtModelCategoryInsert" runat="server" Width="210px"></asp:TextBox>
                                                <asp:HiddenField ID="H_Category" runat="server" Value="0" />
                                                <asp:Label ID="lblmsgCategory" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                  <asp:RequiredFieldValidator ID="reqval" runat="server" ControlToValidate="txtModelCategoryInsert"
                                                                                          ErrorMessage="Please enter category" ForeColor="Red"
                                                                                          ValidationGroup="Group_Category"></asp:RequiredFieldValidator>
                                        </div>
                                 </div>
                          </div>
                          <div class="form-group">
                                  <div class="col-xs-12">
                                      <div class="form-inline">
                                              <asp:Button ID="btnModelCategoryInsert" runat="server" OnClick="btnModelCategoryInsert_Click"
                                                                                      SkinID="btnSubmit" ValidationGroup="Group_Category" />
                                              <asp:Button ID="btnModelCategoryCancel" runat="server" SkinID="btnCancel" />
                                      </div>
                                      </div>
                              </div>
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
 <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
              $(".dropdown-menu li")
            .find("input[type='checkbox']")
            .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>

</asp:Content>


