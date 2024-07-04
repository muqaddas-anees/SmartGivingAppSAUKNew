<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="UpgradePopup.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.UpgradePopup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
	<style>
	ul {
  list-style: none; /* Remove default bullets */
}

ul li::before {
  content: "\2022";  /* Add content: \2022 is the CSS Code/unicode for a bullet */
  color: #40bbea; /* Change the color */
  font-weight: bold; /* If you want it to be bold */
  display: inline-block; /* Needed to add space between the bullet and the text */
  width: 1em; /* Also needed for space (tweak if needed) */
  margin-left: -1em; /* Also needed for space (tweak if needed) */
  font-size:18px;
}
	</style>
	<script>
            if ($("[id*=hpop1]").val() != '0')
				{
                setTimeout(function () { $('#modal-popup').modal('show', { backdrop: 'fade' }); }, $("[id*=hpop1]").val());
            }

            function mysettimeout() {
                setTimeout(function () { $('#modal-popup').modal('show', { backdrop: 'fade' }); }, $("[id*=hpop2]").val());
            }


            window.setInterval(function () {
                
                console.log($("[id*=hpop1]").val());
            }, $("[id*=hpop1]").val());

            jQuery(document).ready(function ($) {
                //var body_classes = public_vars.$body.attr('class').replace(/skin-[a-z]+/i, '');
                // public_vars.$body.attr('class', body_classes).addClass('skin-aero');
                //$("body").addClass($("[id*=hskin]").val());
                $('#btnClosePopup').on('click', function (ev) {
                    ev.preventDefault();
                    mysettimeout();
                });
            });

		</script>
	<asp:HiddenField ID="hpop1" runat="server" ClientIDMode="Static" Value="0" />
		<asp:HiddenField ID="hpop2" runat="server" ClientIDMode="Static" Value="0" />

    	<div class="modal fade" id="modal-popup" aria-hidden="true" data-backdrop="false" style="display: none;">
		<div class="modal-dialog">
			<div class="modal-content panel panel-color panel-info" >
				
				<div class="modal-header" style="text-align:center;font-size:25px">
					
					 <h4 class="modal-title"><span id="modeltitle" style="font-size:28px;"> Upgrade to Enterprise </span> </h4>
				</div>
				
				<div class="modal-body">

					  <div class="form-group row">
      <div class="col-md-12">
		 
		  </div>
						  </div>

				  <div class="form-group row">
      <div class="col-md-12">


		    <div class="form-group row">
      <div class="col-md-5">
		  <img src="https://us.123smartpro.com/WF/UploadData/ThumbNails/00000000-0000-0000-0000-000000000000.png" width="80%"  />

		  </div>

				 <div class="col-md-7">

					 <ul style="font-size:15px">
					<li>VIP support with account manager</li>
<li>One-on-one training</li>
<li>Two-factor authentication</li>
<li>Advance security features</li>
<li>Higher API rate limit</li>
					 </ul>

		  </div>
				</div>

       

          </div>
              </div>
					  <div class="form-group row" style="font-size:18px;">
      <div class="col-md-12">
		For only <b>$<asp:Literal ID="lblprice" runat="server" Text="10.00"></asp:Literal> </b> more a (Month/year)  <input type="checkbox" class="iswitch iswitch-info" checked="checked"/>
		  </div>
						  </div>
					
				</div>
			<div class="modal-footer">
					<button id="btnClosePopup" type="button" class="btn btn-white" data-dismiss="modal">Close</button>
					<asp:Button ClientIDMode="Static" ID="btnUpgarde" runat="server" SkinID="btnDefault" Text="Confirm Upgrade" />
				</div>
			</div>
		</div>
	</div>


</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
