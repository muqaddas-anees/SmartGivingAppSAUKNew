<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="CampaignTemplateTags.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Campaign.CampaignTemplateTags" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
   Campaign 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <asp:Label ID="lblTemplateName" runat="server"></asp:Label>  - Who would you like to send this email to? Select the smart tags 
that you wish you include
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <link rel="stylesheet" href="../../../Content/assets/js/select2/select2.css"/>
	<link rel="stylesheet" href="../../../Content/assets/js/select2/select2-bootstrap.css"/>
	<link rel="stylesheet" href="../../../Content/assets/js/multiselect/css/multi-select.css"/>
    <script src="../../../Content/assets/js/select2/select2.min.js"></script>
	<%--<script src="assets/js/jquery-ui/jquery-ui.min.js"></script>--%>
	<%--<script src="../../../Content/assets/js/selectboxit/jquery.selectBoxIt.min.js"></script>--%>
	<script src="../../../Content/assets/js/tagsinput/bootstrap-tagsinput.min.js"></script>
	<%--<script src="assets/js/typeahead.bundle.js"></script>
	<script src="assets/js/handlebars.min.js"></script>--%>
	<script src="../../../Content/assets/js/multiselect/js/jquery.multi-select.js"></script>

    <div class="row">
									<label class="control-label col-sm-2" for="name">Tags</label>
								<div class="col-md-8 form-inline">
                                    	
									<script type="text/javascript">
                                        jQuery(document).ready(function ($) {
                                            $("#listTags").select2({
                                                placeholder: 'Choose Tags',
                                                allowClear: true
                                            }).on('select2-open', function () {
                                                // Adding Custom Scrollbar
                                                $(this).data('select2').results.addClass('overflow-hidden').perfectScrollbar();
                                            });

                                        });
									</script>
									
									<asp:ListBox CssClass="form-control" runat="server" id="listTags" ClientIDMode="Static" SelectionMode="Multiple" Width="250px">
                                       <%-- <asp:ListItem></asp:ListItem>
											<asp:ListItem>Development</asp:ListItem>
											<asp:ListItem>Designing</asp:ListItem>
											<asp:ListItem>Engineering</asp:ListItem>
											<asp:ListItem>Analysis</asp:ListItem>--%>
											
									</asp:ListBox>
                                    <asp:LinkButton ID="btnAddTagPop" runat="server" SkinID="BtnLinkAdd" style="margin-top:10px;" ToolTip="Create a Tag" Visible="false" />
								</div>
							</div>
     <ajaxtoolkit:modalpopupextender id="mdlWorksheet" cancelcontrolid="imgBtnWorksheetCancel"
                        runat="server" backgroundcssclass="modalBackground" targetcontrolid="btnAddTagPop"
                        popupcontrolid="pnlWorksheet">
                    </ajaxtoolkit:modalpopupextender>
        <asp:Panel ID="pnlWorksheet" runat="server" BackColor="White" Width="330px" ScrollBars="None" CssClass=" card shadow-sm" style="display:none;visibility:hidden;" >
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="LblHeader" runat="server" Text="Add Tag" ></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								<asp:LinkButton ID="LnkReclaimeCancel"
                                                 runat="server" SkinID="BtnLinkCancel" ToolTip="Close"></asp:LinkButton>
								
							</div>
						</div>

        <div class="card-body">
              <div class="form-group row mb-6">
                                      <div class="col-md-10">
                                          <asp:TextBox ID="txtTag" runat="server"></asp:TextBox>
                                          <asp:HiddenField ID="H_tag" runat="server" Value="0" />
                                          <asp:Label ID="lblTag" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTag"
                                                                                      ErrorMessage="Please enter tag" ForeColor="Red"
                                                                                      ValidationGroup="Group_Tag"></asp:RequiredFieldValidator>
                                      </div>
                         </div>
                         <div class="form-group row mb-6">
                                      <div class="col-md-10">
                                          <asp:Button runat="server" ID="imgaddcourse" Style="display: none" />
                                          <asp:Button ID="BtnAddTag" runat="server" Text="OK" SkinID="btnSubmit"
                                                                                       ValidationGroup="Group_Tag" OnClick="BtnAddTag_Click"  />
                                          <asp:Button ID="imgBtnWorksheetCancel" runat="server" Text="Close" SkinID="btnCancel" />
                                      </div>
                         </div>
            </div>


                      

                      
                    </asp:Panel>
     <div class="form-group row mb-6">
         <div class="col-sm-12">
             &nbsp;

             </div>
         </div>
     <div class="form-group row mb-6">
         <div class="col-sm-12">
             &nbsp;

             </div>
         </div>
     <div class="form-group row mb-6">
         <div class="col-sm-12">
             &nbsp;

             </div>
         </div>
     <div class="form-group row mb-6">
         <div class="col-sm-12">
             &nbsp;

             </div>
         </div>
     <div class="form-group row mb-6">
         <div class="col-sm-12">
             &nbsp;

             </div>
         </div>
     <div class="form-group row mb-6">
         <div class="col-sm-2">
                    <asp:Button ID="btnBack" runat="server" ClientIDMode="Static"
                          OnClick="btnBack_Click" Text="Back" SkinID="btnDefault"/>
                   </div>
      <div class="col-sm-8">
          </div>
             <div class="col-sm-2">
                    <asp:Button ID="btnNext" runat="server" ClientIDMode="Static"
                          OnClick="btnSave_Click" Text="Submit"  SkinID="btnNext"/>
                   </div>
        
    </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
