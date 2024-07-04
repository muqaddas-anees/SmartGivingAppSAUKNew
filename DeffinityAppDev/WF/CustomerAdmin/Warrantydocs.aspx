<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="Warrantydocs.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Warrantydocs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     Warranty Documents
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel id="pnl_profile" runat="server">
    	
				<div class="panel panel-headerless" >
					<div class="panel-body">
			
						<div class="member-form-add-header">
                            <div class="row">
                                 <div class="col-sm-10" style="margin-bottom:5px">
                <asp:Literal ID="lblmsg" runat="server" Text="" EnableViewState="false"></asp:Literal>
                <asp:Literal ID="lblerrmsg" runat="server" Text="" EnableViewState="false"></asp:Literal>
                <asp:ValidationSummary ID="valSum" runat="server" ValidationGroup="add" Visible="false" />
                                     </div>
            </div>
                            <div class="row" id="divPercent" runat="server" visible="false">
                                <div class="col-sm-10">
                                    <p class="bg-info" id="lblinfo" runat="server">
                                        </p>
                                   
                                    <br />
                                    </div>

                            </div>
							<div class="row">
								
								<div class="col-md-10 col-sm-8">
			                       <div class="user-info">
									<div class="user-img">
                                       <asp:Image ID="imguser" runat="server" CssClass="img-responsive img-circle" ImageUrl="~/WF/Admin/ImageHandler.ashx?type=contact&id=0"  ClientIDMode="Static" AlternateText="user-pic"/>
                                      </div>
                                          <div class="user-name">
                                            <asp:Label ID="lblFullName" runat="server" ClientIDMode="Static" ForeColor="Black" Font-Size="X-Large" Text="[Add New Contact]"></asp:Label> <span id="lblUsertype" runat="server"></span>  <br />
                                               <a id="lbtnUpload" style="font-size:small;cursor:pointer;" class="label label-default" title="Upload Image" runat="server">Upload</a>
                                                    <div id="contactupload" style="display:none">
                                        <asp:FileUpload ID="Fileupload_contact" runat="server" /><br />
                                    <asp:Button ID="btnuser" runat="server" SkinID="btnUpload" OnClick="btnuser_Click" ValidationGroup="add" />
                                              <asp:Button ID="btncancel" runat="server" SkinID="btnCancel"/>
                                            </div>
                                          	</div>	
									<br />
                                       <br />
									
			                       </div>
								</div>
                                <div class="col-md-2 col-sm-4 pull-right-sm">
									<div class="action-buttons">
                                        <asp:Button ID="btnupdate"  runat="server" Text="Save Changes" CssClass="btn btn-block btn-secondary" ValidationGroup="add" OnClick="btnupdate_Click" CausesValidation="false"  />
                                        <asp:Button ID="btnreset" runat="server" CssClass="btn btn-block btn-gray" Text="Back to Contact list" OnClick="btnreset_Click" CausesValidation="false"  />
									</div>
								</div>
							</div>
						</div>
						<div class="member-form-inputs">			
							<div class="row">
								<div class="col-md-2">
									<label class="control-label" for="name"><%= Resources.DeffinityRes.Name%></label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtname" runat="server" SkinID="txt_90" MaxLength="250" ReadOnly="true"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="Rf_name" runat="server" ControlToValidate="txtname"
                                                            ErrorMessage="Please enter name" ForeColor="Red" ValidationGroup="add"></asp:RequiredFieldValidator>
								</div>
							</div>
			
							<div class="row">
								<div class="col-md-2">
									<label class="control-label" for="birthdate"><%= Resources.DeffinityRes.EmailAddress%></label>
								</div>
								<div class="col-md-4 form-inline">
		                            	<asp:TextBox ID="txtEmail" runat="server" SkinID="txt_90" MaxLength="500" ReadOnly="true"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfemail" runat="server" ControlToValidate="txtEmail"
                                                            ErrorMessage="Please enter email" ForeColor="Red" ValidationGroup="add"></asp:RequiredFieldValidator>
                                     
								</div>
							</div>
                                  <div class="row">
								<div class="col-md-2">
									<label class="control-label" for="name"><%= Resources.DeffinityRes.Address%></label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtAddress"  SkinID="txt_90" runat="server" MaxLength="1000" ReadOnly="true"></asp:TextBox>
								</div>
                                      <div class="col-sm-4">

                                          </div>
							</div>
                             <div class="" style="display:none;">
									<label class="control-label col-sm-2" for="name">Town</label>
								<div class="col-md-4">
									 <asp:TextBox ID="txtTown" runat="server" SkinID="txt_90"  MaxLength="100" ReadOnly="true"></asp:TextBox>
								</div>
							</div>
                             <div class="row">
									<label class="control-label col-sm-2" for="name"><%= Resources.DeffinityRes.City%></label>
								<div class="col-md-4">
									 <asp:TextBox ID="txtCity" runat="server" SkinID="txt_90"  MaxLength="100" ReadOnly="true"></asp:TextBox>
								</div>
							</div>
                                  <div class="row" >
								<div class="col-md-2">
									<label class="control-label" for="name">Postcode/Zipcode</label>
								</div>
								<div class="col-md-4">
                                     <asp:TextBox ID="txtPostal" SkinID="txt_90" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox>
								</div>
							</div>
                            <div class="" style="display:none;">
								<div class="col-md-2">
									<label class="control-label" for="name">Home Phone</label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtTelephone" SkinID="txt_90" ValidationGroup="update" runat="server" MaxLength="20"></asp:TextBox>
                                  
								</div>
							</div>
                              <div class="" style="display:none;">
								<div class="col-md-2">
									<label class="control-label" for="name">Mobile</label>
								</div>
								<div class="col-md-4">
                                    <asp:TextBox ID="txtmobileno" runat="server" SkinID="txt_90" MaxLength="20"></asp:TextBox>
								</div>
							</div>
                            
                              <div class="row">
          
              <div class="col-md-2">
              <label class="control-label" for="name">Warranty documents</label>
                  </div>
               <div class="col-sm-4">
                    <asp:FileUpload ID="fileupload" runat="server" ClientIDMode="Static" AllowMultiple="true" />
               </div>
            
         
    </div>
    <div class="row" >
          <div class="col-md-12">
               <div class="col-sm-6">
                   <asp:GridView ID="gridfiles" runat="server" AutoGenerateColumns="false" OnRowCommand="gridfiles_RowCommand">
                       <Columns>
                       <asp:BoundField DataField="Text" HeaderText="File Name" Visible="false" />
                       <asp:TemplateField HeaderText="File Name">
                           <ItemTemplate>
                               <asp:LinkButton ID="btndownload" runat="server" Text='<%# Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' CommandName="Download"></asp:LinkButton>
                           </ItemTemplate>
                       </asp:TemplateField>
                      <asp:TemplateField ItemStyle-Width="100px">
                           <ItemTemplate>
                            <%-- <asp:LinkButton ID = "lnkDelete" OnClick = "DeleteFile" CausesValidation="false" 
                                 Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server"></asp:LinkButton>--%>
                     <asp:LinkButton runat="server" ID="lnkDelete" CausesValidation="false" SkinID="BtnLinkDelete"
                               CommandArgument = '<%# Eval("Value") %>'
                          OnClientClick="return confirm('Do you want to delete the record?');" OnClick="DeleteFile"></asp:LinkButton>
                           </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
                </asp:GridView>
               </div>
          </div>
    </div>



                             <asp:HiddenField ID="pstatus" runat="server" Value="" ClientIDMode="Static" />
             <asp:HiddenField ID="sid" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="selectedids" runat="server" Value="" ClientIDMode="Static" />
                            <asp:HiddenField ID="haid" runat="server" Value="0" ClientIDMode="Static" />
    <%--<button id="btn">btn</button>--%>
          
         
 <script type="text/javascript" language="javascript" class="init">

     function getUrlParameter(name) {
         name = name.toLowerCase().replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
         var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
         var results = regex.exec(location.search.toLowerCase());

         return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
     };

   
   
    </script>
           
                      
                             

						</div>
			
					</div>
				</div>
        
    
        </asp:Panel>
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
     <script type="text/javascript">
         $(document).ready(function () {
             $('#MainContent_lbtnUpload').click(
                 function () {
                     $('#contactupload').toggle();
                     $('#MainContent_lbtnUpload').toggle();
                 });
         });
      </script>	
</asp:Content>
