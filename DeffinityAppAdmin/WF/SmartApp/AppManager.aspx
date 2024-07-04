<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true"
     Inherits="App_App_Manager" EnableEventValidation="false" Codebehind="AppManager.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
     <ul class="tabs_list1" style="float: left;" id="pnltab" runat="server" visible="false">
          <li class="menu_tab" id="link_menu" runat="server"><a href="#"><span>Setup</span></a>
            <ul>
                <li><a id="a1_link" href="../App/AppPermissionManager.aspx">Permission Manager</a></li>
            </ul>
        </li>
     </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
     Create an Smart App    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Smart App 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    
   <script type="text/javascript">
       $(document).ready(function () {
           $('.chkselect').change(function () {
               var id = $(this).children().attr("id");
             
               //alert(id);
               $('.chkselect').each(function () {
                   if (id != $(this).children().attr("id")) {
                       $(this).children().attr("checked", false);
                   }
               });

           });

           $('.AppOption').change(function () {
               var id = $(this).children().attr("id");
             
               //alert(id);
               $('.AppOption').each(function () {
                   if (id != $(this).children().attr("id")) {
                       $(this).children().attr("checked", false);
                   }
               });
           });

           $('#ddlChilfForm').change(function () {
               var pid = $("#ddlForms").val();
               var Cid = $("#ddlChilfForm").val();
               if (pid == Cid)
               {
                   debugger;
                   $("#ddlChilfForm")[0].selectedIndex = 0;
                   alert("Both parent and child forms are same please select different form.");
               }
           });

       });
   </script>
                    <div>
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
                    </div>
                   <div class="col-md-6" id="DivAppOptions" runat="server" visible="false">
                 
                         <div class="form-group">
                                 <div class="col-md-12">
                                      <asp:Label ID="lblOptionsMsg" runat="server" EnableViewState="false"></asp:Label>
                                 </div>
                       </div>
                         <div class="form-group">
                             <div class="col-md-12">
                                 <strong> <asp:Label ID="lblStep1" Text="Is this app:" runat="server"></asp:Label></strong>
                                 <hr class="no-top-margin" />
                             </div>
                        </div>
                         <div class="form-group">
                                 <div class="col-md-12">
                                      <asp:CheckBox ID="CheckboxFlatFileStructure" runat="server" ClientIDMode="Static" CssClass="AppOption" Text="Flat file structure" />
                                 </div>
                       </div>
                         <div class="form-group">
                                 <div class="col-md-12">
                                       (or)
                                 </div>
                       </div>
                         <div class="form-group">
                                 <div class="col-md-12">
                                     <asp:CheckBox ID="CheckBoxParentAndChildRelationShip" runat="server"
                                                                                   ClientIDMode="Static" CssClass="AppOption" Text="Parent and Child relationship" />
                                 </div>
                       </div>
                         <div class="form-group">
                                 <div class="col-md-12">
                                      <asp:Button ID="BtnNext" runat="server" Text="Next" OnClick="BtnNext_Click" />
                                       <asp:Button ID="btnCancelStep1" runat="server" Text="Cancel" OnClick="btnCancelStep1_Click"  />
                                 </div>
                       </div>
                   </div>

                   <div id="DivAppDetails" class="col-md-8" runat="server" visible="false">
                       <div class="form-group">
                           <div class="col-md-12">
                               <strong><asp:Label ID="lblStep2" Text="App Details:" runat="server"></asp:Label> </strong> 
                               <hr class="no-top-margin" />
                           </div>
                       </div>
                       <div class="form-group">
                           <div class="col-md-12">
                                 <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="Create" />
                           </div>
                       </div>
                       <div class="form-group">
                            <div class="col-md-12">
                                <label class="col-sm-3 control-label"> App Name</label>
                                 <div class="col-sm-9">
                                      <asp:TextBox ID="txtAppName" runat="server" SkinID="txt_80"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="req1" runat="server" ValidationGroup="Create" ControlToValidate="txtAppName"
                                                                  ErrorMessage="Please enter App name" Display="None"></asp:RequiredFieldValidator>
                                 </div>
                            </div>
                       </div>
                       <div class="form-group">
                            <div class="col-md-12">
                                <label class="col-sm-3 control-label">  What does this application do?</label>
                                 <div class="col-sm-9">
                                     <asp:TextBox ID="txtapplicationdo" runat="server" SkinID="txt_80"></asp:TextBox>
                                 </div>
                            </div>
                       </div>
                       <div class="form-group">
                            <div class="col-md-12">
                                <label class="col-sm-3 control-label"> Select a Form </label>
                                 <div class="col-sm-9">
                                     <asp:DropDownList ID="ddlForms" SkinID="ddl_80" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Create" ControlToValidate="ddlForms"
                                                                  ErrorMessage="Please select form" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                 </div>
                            </div>
                       </div>
                       <div class="form-group" id="ChildFormddl" runat="server">
                            <div class="col-md-12">
                                <label class="col-sm-3 control-label">  Select child form </label>
                                 <div class="col-sm-9">
                                      <asp:DropDownList ID="ddlChilfForm" SkinID="ddl_80" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                 </div>
                            </div>
                       </div>
                       <div class="form-group">
                            <div class="col-md-12">
                                <label class="col-sm-3 control-label">Select Icon</label>
                                 <div class="col-sm-9">
                                     <asp:Panel ID="PnlIcons" runat="server" Width="250px" BorderStyle="Solid" 
                                         Height="100px" ScrollBars="Vertical" Style="border-width:1px;">
                                                     <div>
                             <ul>
                                <li class="fa-tachometer">
                                  <a>  <asp:CheckBox ID="fa_tachometer" runat="server" CssClass="chkselect" ClientIDMode="Static" /></a>
                                </li>
                                  <li class="fa-tasks">
                                   <a> <asp:CheckBox ID="fa_tasks" runat="server" CssClass="chkselect"  ClientIDMode="Static" /> </a>
                                </li>
                                  <li class="fa-headphones">
                                   <a> <asp:CheckBox ID="fa_headphones" runat="server" CssClass="chkselect"  ClientIDMode="Static" /></a>
                                 </li>
                                 <li class="fa-slideshare">
                                   <a> <asp:CheckBox ID="fa_slideshare" runat="server" CssClass="chkselect"  ClientIDMode="Static" /></a>
                                </li>
                                 <li class="fa-cubes">
                                   <a> <asp:CheckBox ID="fa_cubes" runat="server" CssClass="chkselect"  ClientIDMode="Static" /></a>
                                </li>
                                <li class="fa-cog">
                                 <a> <asp:CheckBox ID="fa_cog" runat="server" CssClass="chkselect"  ClientIDMode="Static" /></a>
                                </li>
                                <li class="fa-suitcase">
                                   <a> <asp:CheckBox ID="fa_suitcase" runat="server" CssClass="chkselect"  ClientIDMode="Static" /></a>
                                </li>
                                <li class="fa-newspaper-o">
                                   <a> <asp:CheckBox ID="fa_newspaper_o" runat="server" CssClass="chkselect"  ClientIDMode="Static" /></a>
                                </li>
                                <li class="fa-users">
                                   <a> <asp:CheckBox ID="fa_users" runat="server" CssClass="chkselect"  ClientIDMode="Static" /></a>
                                </li>
                                <li class="linecons-database">
                                   <a> <asp:CheckBox ID="linecons_database" runat="server" CssClass="chkselect"  ClientIDMode="Static" /></a>
                                </li>
                            </ul>
                        </div>
                                     </asp:Panel>
                                 </div>
                            </div>
                       </div>
                       <div class="form-group">
                            <div class="col-md-12">
                                <label class="col-sm-3 control-label"></label>
                                 <div class="col-sm-9">
                                      <asp:CheckBox ID="chkEngineer" runat="server" ValidationGroup="Create" Checked="true" /> Add to Engineer View<br />
                                      <asp:CheckBox ID="chkCustomer" runat="server" ValidationGroup="Create" Checked="true" /> Add to Customer View
                                 </div>
                            </div>
                       </div>
                       <div class="form-group" style="display:none;">
                            <div class="col-md-12">
                                <label class="col-sm-3 control-label"> Upload file</label>
                                 <div class="col-sm-9">
                                      <asp:FileUpload ID="FileUpload1" runat="server" />
                                 </div>
                            </div>
                       </div>
                       <div class="form-group">
                            <div class="col-md-12">
                                <label class="col-sm-3 control-label"></label>
                                 <div class="col-sm-9">
                                     <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="BtnCreateApp" runat="server" Text="Create App" CssClass="btn btn-orange" ValidationGroup="Create" OnClick="BtnCreateApp_Click" />
                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
                       <asp:Button ID="BtnBack" runat="server" Visible="false" CausesValidation="false" Text="Back" OnClick="BtnBack_Click" />
                       <asp:Label ID="lbltype" runat="server" Visible="false"></asp:Label>
                                 </div>
                            </div>
                       </div>
                  </div>
                   <div>
                      <div style="padding-top:5px;">
                          <asp:Panel ID="pnlApplist" runat="server" Width="100%">

                              <div class="form-group">
                                  <div class="col-md-8">
                                      <strong>List of apps</strong> 
                                      <hr class="no-top-margin" />
                                  </div>
                              </div>
                             <div class="form-group">
                                  <div class="col-md-8">
                                  <asp:Button ID="btnCreateappMain" runat="server" Text="Create Smart App" ValidationGroup="Create" OnClick="btnCreateappMain_Click" /> </div>
                                 <div class="col-md-4">
                                     &nbsp;
                                 </div>
                              </div>
                              <div class="form-group">
                                  <div class="col-md-8">
                                 <asp:GridView ID="GridApps" Width="100%" runat="server" OnRowCommand="GridApps_RowCommand" OnRowDeleting="GridApps_RowDeleting"
                               OnRowEditing="GridApps_RowEditing" OnRowDataBound="GridApps_RowDataBound">
                              <Columns>
                                  <asp:TemplateField HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                      <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" 
                                                                                         CommandArgument='<%#Bind("ID") %>' CommandName="Edit"></asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                      <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                                     HeaderStyle-Width="12%" ItemStyle-CssClass="form-inline" ControlStyle-CssClass="form-inline">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppId" runat="server" Text='<%#Bind("ID") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton SkinID="BtnLinkDownload" ID="LnlDownloadFile" runat="server" Visible="false"
                                                 Text="Download" CommandName="Download" CommandArgument='<%#Bind("ID") %>' ToolTip="Download file" ></asp:LinkButton>
                                            <asp:LinkButton SkinID="BtnLinkDelete" ID="LnlDeletefile" runat="server" Text="Delete"
                                                 CommandName="DeleteFile" CommandArgument='<%#Bind("ID") %>' ToolTip="Delete File"
                                                 OnClientClick="return confirm('Do you want to delete this file?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="App Name">
                                      <ItemTemplate>
                                           <asp:Label ID="LblAppName" runat="server" Text='<%#Bind("AppName") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Description">
                                      <ItemTemplate>
                                            <asp:Label ID="LblNotes" runat="server" Text='<%#Bind("Notes") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Form Name">
                                      <ItemTemplate>
                                           <asp:Label ID="LblFormName" runat="server" Text='<%#Bind("FormName") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                 <%--  <asp:TemplateField HeaderText="Created By">
                                      <ItemTemplate>
                                             <asp:Label ID="LblCreatedBy" runat="server" Text='<%#Bind("CreatedBy") %>'></asp:Label>
                                      </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%--   <asp:TemplateField HeaderText="Created Date">
                                      <ItemTemplate>
                                          <asp:Label ID="LblCreatedDate" runat="server" Text='<%#Bind("CreatedDate") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>--%>
                                  <asp:TemplateField HeaderText="Type" Visible="false">
                                      <ItemTemplate>
                                          <asp:Label ID="lbltype" runat="server" Text='<%#Bind("Type") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Child form" Visible="false">
                                      <ItemTemplate>
                                          <asp:Label ID="lblChildForm" Text='<%#Bind("ChildFormName") %>' runat="server"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                       <ItemTemplate>
                                           <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('Do you want to delete this record?');"
                                                SkinID="BtnLinkDelete" CommandArgument='<%#Bind("ID") %>' CommandName="Delete"></asp:LinkButton>
                                       </ItemTemplate>  
                                   </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                                      </div>
                                  </div>
                          </asp:Panel>
                      </div>

                   </div>
    
</asp:Content>
