<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="HC_FormList" Codebehind="FormList.aspx.cs" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Forms%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
  <%= Resources.DeffinityRes.FormList%> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    <script  type="text/javascript">
        $(document).ready(function () {
            $('#navTab').hide();
        });
       </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
         <%-- <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
   
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../../Content/jtable/external/json2.js" type="text/javascript"></script>--%>
    <style type="text/css">
       .ui-dialog{font-size: 85%;}
        a {
            cursor:pointer;
        }
   </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document.body).find("[id$='lblPageHead']").html('Health Check');
        });
      
        function copyPnlLoad(e) {
            
            var rowindex = $(e).closest('tr').index();
            var fvalue = $('#GridFormList tr:eq(' + rowindex + ') td:eq(' + 2 + ') span[id$=lblFormName]').html();
            $('#txtFormname').val(fvalue);
           <%-- $('#pnlCopy').dialog({
                resizable: false,
                height: 220,
                width: 300,
                modal: true,
                open: function (type, data) {

                    $(this).parent().appendTo('form');
                    var el = $(e).prev();
                    var eval = $(el).val();
                    $('#hid').val(eval);
                    __doPostBack('<%=upanelcopy.UniqueID %>', '');
                },
                close: function () {
                    location.reload();
                }
            });--%>
            return false;
        }
      <%--  function MailPnlLoad(e) {
           
           
            //$('#btnSetBack').click(function () {
            $('#pnlMails').dialog({
                resizable: false,
                height: 500,
                width:500,
                modal: true,
                open: function (type, data) {
                   
                    $(this).parent().appendTo('form');
                    var el = $(e).prev();
                    var eval = $(el).val();
                    $('#hid').val(eval);
                    __doPostBack('<%=upanel.UniqueID %>', '');
                    
                }
                ,
                close: function () {
                    $('#hid').val('0');
                    __doPostBack('<%=upanel.UniqueID %>', '');
                }
                
            });
            return false;
            //});
        }--%>
    </script>
     <script type="text/javascript">
        // Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
</script>
    <div class="form-group row" id="pnlCustomers" runat="server" visible="false">
       <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlPortfolio" runat="server"></asp:DropDownList>
            </div>
	</div>
	
	<div class="col-md-6 form-inline">
               <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" /> <asp:Button ID="btnViewAll" runat="server" Text="View All" OnClick="btnViewAll_Click" /> 
       
	</div>
</div>
      <div class="form-group row">
       <div class="col-md-6">
           </div>
           <div class="col-md-6">
                <asp:Button style="float:right;" ID="btnCreateForm" runat="server" Text="Create New Form" ClientIDMode="Static" OnClick="btnCreateForm_Click" CausesValidation="false" />
           </div>
          </div>
     
          
             <asp:GridView ID="GridFormList" runat="server" Width="100%" EmptyDataText="No records available." OnRowCommand="GridFormList_RowCommand1" >
                 <Columns>
                   <%--  <asp:HyperLinkField NavigateUrl="~/HC/FormDesign.aspx?formid={0}" DataNavigateUrlFields="FormID" DataNavigateUrlFormatString="~/HC/FormDesign.aspx?fid={0}" Text="Edit Design" HeaderStyle-CssClass="header_bg_l" />--%>
                      <asp:TemplateField >
                         <ItemTemplate>
                             <asp:LinkButton ID="btnEdit" runat="server" Text="Edit Design" CommandArgument='<%#Bind("FormID") %>' CommandName="Editdesign" ></asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>
                   <%--  <asp:BoundField DataField="CustomerName" HeaderText="Customer" Visible="false" />--%>
                     <asp:TemplateField HeaderText="Form Name"  ItemStyle-Width="40%" >
                         <ItemTemplate>
                             <asp:Label ID="lblFormName" runat="server" Text='<%#Bind("FormName") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                    <%-- <asp:TemplateField Visible="false">
                         <ItemTemplate>
                             <asp:HiddenField ID="txtFormid" runat="server" Value='<%#Bind("FormID") %>' ></asp:HiddenField>
                             <a onclick="MailPnlLoad(this);"><asp:Label SkinID="Mail" runat="server"></asp:Label></a>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField Visible="false" >
                         <ItemTemplate>
                               <asp:HiddenField ID="txtCopy" runat="server" Value='<%#Bind("FormID") %>' ></asp:HiddenField>
                             <a onclick="copyPnlLoad(this);">Copy to Another Customer</a>
                        <asp:LinkButton ID="btnCopy" runat="server" Text="Copy to Another Customer" CommandArgument='<%#Bind("FormID") %>' CommandName="Copy" ></asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>--%>
                      <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="btnPrint" runat="server" Text="View Form and Print" CommandArgument='<%#Bind("FormID") %>' CommandName="Print" ></asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>
                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:LinkButton ID="btnDelete" runat="server" SkinID="BtnLinkDelete" CommandArgument='<%#Bind("FormID") %>' CommandName="Del" OnClientClick="return confirm('Do you want to delete form?');" />
                          </ItemTemplate>
                          </asp:TemplateField>
                 </Columns>

             </asp:GridView>
               <asp:HiddenField ID="hid" runat="server" ClientIDMode="Static" />
                  <%-- <div id="pnlMails" class="Basic dialog pnldialog" style="display:none;" title="Distribution list"> 
                       <asp:UpdateProgress ID="uprogress" runat="server">
                           <ProgressTemplate>
                              <asp:Label ID="lblLoading" runat="server" ClientIDMode="Static" Text="Loading..." ForeColor="Green"></asp:Label>  
                           </ProgressTemplate>
                       </asp:UpdateProgress>
                       <asp:UpdatePanel ID="upanel" runat="server" OnLoad="upanel_Load"> 
                           <ContentTemplate>
                               <asp:Label ID="cdate" runat="server" Text="" ClientIDMode="Static" style="display:none;"></asp:Label>
                                <div>
                <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Green"></asp:Label>
            </div>
                              <asp:ValidationSummary ID="InsertSum1" runat="server" DisplayMode="List" ValidationGroup="InsertSum" Visible="false" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ValidationGroup="UpdateSum" Visible="false" />
      <asp:Label ID="Label1" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
   <%-- <asp:LinkButton ID="btnEmailApply" runat="server" ToolTip="Add email to all form(s)" Text="Add email to all form(s)" ></asp:LinkButton>--%
      <asp:ListView ID="list_mails" runat="server" InsertItemPosition="FirstItem" OnItemCommand="list_mails_ItemCommand" OnItemCanceling="list_mails_ItemCanceling" OnItemEditing="list_mails_ItemEditing">
             <InsertItemTemplate>
            <tr>  
                 <td>
                      <asp:TextBox ID="txtIName" runat="server" ValidationGroup="InsertSum" Width="150px" MaxLength="200"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rqIname" runat="server" ErrorMessage="*" ToolTip="Please enter name" ControlToValidate="txtIName"
                          Display="None" ValidationGroup="InsertSum" Width="225px">*</asp:RequiredFieldValidator>
                 </td>
                 <td>
                  <asp:TextBox ID="txtIEmail" runat="server" ValidationGroup="InsertSum" Width="150px" MaxLength="200"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ToolTip="Please enter email" ControlToValidate="txtIEmail"
                          Display="Dynamic" ValidationGroup="InsertSum" Width="225px">*</asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtIEmail"
                                                        ErrorMessage="*" ToolTip="Please enter valid email" ValidationExpression="\w+([-+.'’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        Display="Dynamic" ValidationGroup="InsertSum">*</asp:RegularExpressionValidator>
                 </td>
                   <td style="width:70px" class="form-inline">
                       <asp:LinkButton  ID="btnAdd" runat="server" CommandName="Add" ValidationGroup="InsertSum" Text="Add" SkinID="BtnLinkAdd" />
                       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" SkinID="BtnLinkCancel" />
                   </td>
                </tr>    
          </InsertItemTemplate>
             <LayoutTemplate>
            <table style="width:100%" class="sec_table" >
                <thead>
                    <tr class="tab_header" style="font-weight:bold;margin:5px 5px 5px 5px;height:30px;">
                        <td>Name</td>
                        <td>Email</td>
                        <td style="width:90px"></td>
                    </tr>
                </thead>
                <tbody>
                    <tr id="ItemPlaceholder" runat="server"></tr>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
              </LayoutTemplate>          
             <ItemTemplate>
              <tr class="even_row">  
                 <td>
                      <asp:Label ID="lblLable" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                 </td>
                   <td>
                      <asp:Label ID="lblType" runat="server" Text='<%# Eval("EmailID")%>'></asp:Label>
                 </td>
                   <td class="form-inline">
                       <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="Edit" Text="Edit" />
                       <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Del" SkinID="BtnLinkDelete"
                            OnClientClick="if (!confirm('do you want delete item?')) return false;" Text="Delete"  />
                   </td>
                </tr>     
          </ItemTemplate>
             <EditItemTemplate>
              <tr>  
                  <td>
                       <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>' Width="175px"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ToolTip="Please enter name"
                            ControlToValidate="txtName" Display="None" ValidationGroup="UpdateSum" Width="225px"></asp:RequiredFieldValidator>
                 </td>
                  <td>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("EmailID") %>' Width="175px"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ToolTip="Please enter email"
                           ControlToValidate="txtName" Display="None" ValidationGroup="UpdateSum" Width="225px"></asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                        ErrorMessage="*" ToolTip="Please enter valid email" ValidationExpression="\w+([-+.'’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        Display="None" ValidationGroup="UpdateSum"  >*</asp:RegularExpressionValidator>
                 </td>
                   <td class="form-inline">
                       <asp:LinkButton ID="btnUpdate" runat="server" SkinID="BtnLinkUpdate" CommandName="UpdateItem"
                            CommandArgument='<%# Eval("ID")%>'
                            Text="Update" ValidationGroup="UpdateSum" />
                       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" Text="Cancel"
                            SkinID="BtnLinkCancel"  />
                   </td>
                </tr>   
          </EditItemTemplate>
      </asp:ListView>
                           </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="imgbtn" EventName="Click"  />
                           </Triggers>
                       </asp:UpdatePanel>
                         <asp:Button runat="server" ID="imgbtn" SkinID="btnSubmit"  OnClick="imgbtn_Click1" Visible="false" />
                       </div>
              
                 <div class="clr"></div>
                <div id="pnlCopy" class="Basic dialog pnldialog" style="display:none;" title="Copy to another customer"> 
                       <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upanelcopy">
                           <ProgressTemplate>
                              <asp:Label ID="lblLoading1" runat="server" ClientIDMode="Static" Text="Loading..." ForeColor="Green"></asp:Label>  
                           </ProgressTemplate>
                       </asp:UpdateProgress>
                       <asp:UpdatePanel ID="upanelcopy" runat="server" OnLoad="upanelcopy_Load"> 
                           <ContentTemplate>
                               <asp:Label id="lblCopymsg" runat="server" ClientIDMode="Static" EnableViewState="false" ForeColor="Green"></asp:Label>
                               <br />
                                <label>Form Name</label><br />
                                <asp:TextBox ID="txtFormname" runat="server" ClientIDMode="Static" MaxLength="100" Width="200px" ></asp:TextBox>
                               <br />
                                 <label>Customer</label><br />
                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px" ClientIDMode="Static">
                                </asp:DropDownList>
                                 <ajaxToolkit:CascadingDropDown ID="ccdCompny" runat="server" TargetControlID="ddlCustomer"
                                Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                                ServiceMethod="GetCompany" LoadingText="[Loading customer...]" BehaviorID="ccdc"
                                ClientIDMode="Static" />
                                <br /> <br />
                               <asp:Button runat="server" ID="btnApply" Text="Apply" OnClick="btnApply_Click"  />
                               
                               </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="btnApply" EventName="Click" />
                           </Triggers>
                           </asp:UpdatePanel>
                    </div>--%>
            
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
          
 <script type="text/javascript">

     //grid_responsive();
     grid_responsive_display();

     $(window).load(function () {


         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });

     });
    </script>
       <script  type="text/javascript">
           $(document).ready(function () {

               sideMenuActive('<%= Resources.DeffinityRes.Forms%>');
        });
       </script>
</asp:Content>

