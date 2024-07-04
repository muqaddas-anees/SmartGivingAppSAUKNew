<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="SetDefaults.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.SetDefaults" %>

<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:AdminTabCtrl runat="server" ID="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Set Logo & Theme
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
	<a id ="link_return" href="~/WF/Admin/PartnersList.aspx" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  Partner list</a>

</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
	  <script language="javascript" type="text/javascript">
         function SelectSingleCheckbox(Chkid) {
             var chkbid = document.getElementById(Chkid);
             var chkList = document.getElementsByTagName("input");
             for (i = 0; i < chkList.length; i++) {
                 if (chkList[i].type == "checkbox" && chkList[i].id != chkbid.id) {
                     chkList[i].checked = false;
                 }
             }
         }
</script>
	<div style="display:none;visibility:hidden;">
      <div class="form-group">
        <div class="col-md-12 text-bold">
 <strong>Logo</strong> 
<hr class="no-top-margin">
	</div>
</div>
    <div class="form-group">
      <div class="col-md-6">
          
                <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="FileUpload1" Display="None" 
                            ErrorMessage="Browse an image file to upload" 
                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w ]*.*))+\.(jpg|JPG|gif|GIF|png|PNG)$" 
                            ValidationGroup="Group10">File</asp:RegularExpressionValidator>
	</div>
	
</div>
    <div class="form-group">
      <div class="col-md-6">
           <asp:Image runat="server" ID="imgLogo"  />
          </div>
        </div>
    <div class="form-group">
      <div class="col-md-6">
           <asp:Button ID="btnSubmit" runat="server" 
                                OnClick="btnSubmit_Click1" 
                                ValidationGroup="Group10" SkinID="btnSubmit" />
          </div>
        </div>

	</div>
	  <div class="form-group" style="display:none;visibility:hidden;">
        <div class="col-md-12 text-bold">
 <strong>Theme</strong> 
<hr class="no-top-margin">
	</div>
</div>
	<script type="text/javascript">
        $(function () {
            //$("[id*=GridView1] input[type=checkbox]").click(function () {
            //    if ($(this).is(":checked")) {
            //        $("[id*=GridView1] input[type=checkbox]").removeAttr("checked");
            //        $(this).attr("checked", "checked");
            //    }
            //});
            $('.subject-list input[type=checkbox]').on('change', function () {
                //alert('test');
                $('.subject-list input[type=checkbox]').not(this).removeAttr('checked', false);
                //$(this).child.attr("checked", "checked");
                //$('.subject-list').removeAttr("checked");
               // $(this).attr("checked", "checked");
            });

        });
</script>
	<script type="text/javascript">
      
    </script>
    <div class="form-group">
        <div class="col-md-8">
			<div>
				<asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
			</div>
			<div style="float:right;">
				<asp:Button ID="btnApplySKin" runat="server" SkinID="btnDefault" Text="Apply" OnClick="btnApplySKin_Click" />
			</div>
            <table id="GridView1" class="table table-hover middle-align">
						<thead>
							<tr>
								<th>Skin Name</th>
								<th width="300">Color Palette</th>
								<th width="300">Apply</th>
							</tr>
						</thead>
						<tbody>
							<tr data-skin="">
								<td>
									<a href="#" class="skin-name-link">Default Skin</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette" data-set-skin="">
										<span style="background-color: #2c2e2f"></span>
										<span style="background-color: #EEE"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #68b828"></span>
										<span style="background-color: #27292a"></span>
										<span style="background-color: #323435"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkDefault" runat="server" CssClass="subject-list" ClientIDMode="Static"  />
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="aero">
								<td>
									<a href="#" class="skin-name-link">Aero</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #558C89"></span>
										<span style="background-color: #ECECEA"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #5F9A97"></span>
										<span style="background-color: #558C89"></span>
										<span style="background-color: #255E5b"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkAero" runat="server" CssClass="subject-list"  />
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="navy">
								<td>
									<a href="#" class="skin-name-link">Navy</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #2c3e50"></span>
										<span style="background-color: #a7bfd6"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #34495e"></span>
										<span style="background-color: #2c3e50"></span>
										<span style="background-color: #ff4e50"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkNavy" runat="server" CssClass="subject-list"  />
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="facebook">
								<td>
									<a href="#" class="skin-name-link">Facebook</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #3b5998"></span>
										<span style="background-color: #8b9dc3"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #4160a0"></span>
										<span style="background-color: #3b5998"></span>
										<span style="background-color: #8b9dc3"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkFacebook" runat="server" CssClass="subject-list" />
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="turquoise">
								<td>
									<a href="#" class="skin-name-link">Turquise</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #16a085"></span>
										<span style="background-color: #96ead9"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #1daf92"></span>
										<span style="background-color: #16a085"></span>
										<span style="background-color: #0f7e68"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkTurquise" runat="server"  CssClass="subject-list"/>
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="lime">
								<td>
									<a href="#" class="skin-name-link">Lime</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #8cc657"></span>
										<span style="background-color: #ffffff"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #95cd62"></span>
										<span style="background-color: #8cc657"></span>
										<span style="background-color: #70a93c"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkLime" runat="server" CssClass="subject-list"/>
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="green">
								<td>
									<a href="#" class="skin-name-link">Green</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #27ae60"></span>
										<span style="background-color: #a2f9c7"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #2fbd6b"></span>
										<span style="background-color: #27ae60"></span>
										<span style="background-color: #1c954f"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkGreen" runat="server"  CssClass="subject-list"/>
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="purple">
								<td>
									<a href="#" class="skin-name-link">Purple</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #795b95"></span>
										<span style="background-color: #c2afd4"></span>
										<span style="background-color: #FFFFFF"></span>
										<span style="background-color: #795b95"></span>
										<span style="background-color: #27ae60"></span>
										<span style="background-color: #5f3d7e"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkPurple" runat="server" CssClass="subject-list" />
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="white">
								<td>
									<a href="#" class="skin-name-link">White</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #FFF"></span>
										<span style="background-color: #666"></span>
										<span style="background-color: #95cd62"></span>
										<span style="background-color: #EEE"></span>
										<span style="background-color: #95cd62"></span>
										<span style="background-color: #555"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkWhite" runat="server"  CssClass="subject-list" />
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="concrete">
								<td>
									<a href="#" class="skin-name-link">Concrete</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #a8aba2"></span>
										<span style="background-color: #666"></span>
										<span style="background-color: #a40f37"></span>
										<span style="background-color: #b8bbb3"></span>
										<span style="background-color: #a40f37"></span>
										<span style="background-color: #323232"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkConcrete" runat="server" CssClass="subject-list" />
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="watermelon">
								<td>
									<a href="#" class="skin-name-link">Watermelon</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #b63131"></span>
										<span style="background-color: #f7b2b2"></span>
										<span style="background-color: #FFF"></span>
										<span style="background-color: #c03737"></span>
										<span style="background-color: #b63131"></span>
										<span style="background-color: #32932e"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkWatermelon" runat="server" CssClass="subject-list" />
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
							<tr data-skin="lemonade">
								<td>
									<a href="#" class="skin-name-link">Lemonade</a>
								</td>
								<td>
									<a href="#" class="skin-color-palette">
										<span style="background-color: #f5c150"></span>
										<span style="background-color: #ffeec9"></span>
										<span style="background-color: #FFF"></span>
										<span style="background-color: #ffcf67"></span>
										<span style="background-color: #f5c150"></span>
										<span style="background-color: #d9a940"></span>
									</a>
								</td>
								<td>
									<asp:CheckBox ID="chkLemonade" runat="server" CssClass="subject-list"/>
									<%--<a href="#" class="btn btn-sm btn-secondary">Try this skin</a>
									<a href="#" class="btn btn-sm btn-white is-login">Login Page</a>--%>
								</td>
							</tr>
						</tbody>
					</table>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
