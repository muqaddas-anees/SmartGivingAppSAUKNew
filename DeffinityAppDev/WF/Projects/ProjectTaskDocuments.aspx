<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="ProjectTaskDocuments" Codebehind="ProjectTaskDocuments.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="form-group form-inline">
       <div class="col-md-12">
        <asp:Label runat="server" ID="lblErrMsg"  Visible="false"></asp:Label>
      <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" runat="server" ValidationGroup="Group1" />
            </div>
      <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Task%>s</label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlltTasks" runat="server"  
                         AutoPostBack="True" onselectedindexchanged="ddlltTasks_SelectedIndexChanged" SkinID="ddl_60">
                            </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlltTasks" runat="server" Display="None"
                      ErrorMessage="Please select task to upload document" InitialValue="0" ValidationGroup="Group1"></asp:RequiredFieldValidator>
					</div>
				</div>
    </div>
    <div class="form-group form-inline">
      <div class="col-md-12">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Document%></label>
          <div class="col-sm-8"> <asp:FileUpload ID="flupTaskDoc" runat="server" />
                             </div>
          </div>
        </div>
    <div class="form-group">
      <div class="col-md-12">
           <div class="col-sm-12">
          <asp:Button ID="imgTaskUpload" runat="server" SkinID="btnUpload"  ValidationGroup="Group1"  
                                 onclick="imgTaskUpload_Click" />
               </div>
          </div>
        </div>
  <div>
                <div class="form-group">
      <div class="col-md-12">  
               <asp:GridView ID="grdTaskDocuments" Width="100%" runat="server"  EmptyDataText="No records found"
                          AutoGenerateColumns="false" 
          onrowcommand="grdTaskDocuments_RowCommand" 
          onrowdeleting="grdTaskDocuments_RowDeleting" SkinID="gv_responsive">
                      <Columns>
                      <asp:TemplateField HeaderText="Name">
                      <ItemStyle Width="35%" />
                      <ItemTemplate>
                      
                          <asp:Label ID="lblTaskName" runat="server" Text='<%#Bind("DocumentName") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="DataSize" DataFormatString="{0:f2}" HeaderText="Size in KB" />
                       <asp:BoundField DataField="UploadDateTime" DataFormatString="{0:d}" HeaderText="Uploaded" HeaderStyle-Width="15%"/>
                        <asp:BoundField DataField="UpdatedBy"  HeaderText="Updated&nbsp;By" HeaderStyle-Width="35%" />
                       <asp:TemplateField HeaderText="" >
                      <ItemStyle Width="5%"  />
                      <ItemTemplate>
                      
                          <asp:LinkButton ID="imgDwnLoad" runat="server" CommandName="Download" CommandArgument='<%# Bind("ID") %>' ToolTip="Attachment click to download"  SkinID="BtnLinkDownload" />
                      </ItemTemplate>
                      </asp:TemplateField>

                       <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="header_bg_r">
                      <ItemStyle Width="15%"  />
                      <ItemTemplate>
                          <asp:LinkButton ID="imgDwnLoad12" runat="server" CommandName="Delete" CommandArgument='<%# Bind("ID") %>' ToolTip="Delete"  SkinID="BtnLinkDelete" />
                      </ItemTemplate>
                      </asp:TemplateField>
                      </Columns>
                      </asp:GridView>
               </div>
                    </div>   
    </div>
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 

</asp:Content>

