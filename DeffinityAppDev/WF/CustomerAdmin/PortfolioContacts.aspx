<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterTabSub.master"
     EnableEventValidation="false" Inherits="Contacts" Codebehind="PortfolioContacts.aspx.cs" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="controls/PortfolioMenuTab.ascx" TagName="PortfolioMenuTab" TagPrefix="uc1" %>
<%@ Register Src="controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    Customers
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.CustomerContacts%>  <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
    <%-- <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i> Return to <%:sessionKeys.JobsDisplayName %> </asp:HyperLink>--%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts_Section" runat="Server">

     <link rel="stylesheet" href="../../Content/assets/js/select2/select2.css"/>
	<link rel="stylesheet" href="../../Content/assets/js/select2/select2-bootstrap.css"/>
	<link rel="stylesheet" href="../../Content/assets/js/multiselect/css/multi-select.css"/>
    <script src="../../Content/assets/js/select2/select2.min.js"></script>
	<%--<script src="assets/js/jquery-ui/jquery-ui.min.js"></script>--%>
	<%--<script src="../../Content/assets/js/selectboxit/jquery.selectBoxIt.min.js"></script>--%>
	<script src="../../Content/assets/js/tagsinput/bootstrap-tagsinput.min.js"></script>
	<%--<script src="assets/js/typeahead.bundle.js"></script>
	<script src="assets/js/handlebars.min.js"></script>--%>
	<script src="../../Content/assets/js/multiselect/js/jquery.multi-select.js"></script>
     	
     <script type="text/javascript">
         hidetabs();
         $(window).load(function () {
             var i = 1;
             $('#pnlUploadShow').click(function () {
                 if (i == 0) {
                     $('#pnlUpload').hide();
                     $('#pnlUploadShow').html('<b>Show Upload Contacts</b>');

                     i = 1;
                 }
                 else {
                     $('#pnlUpload').show();
                     $('#pnlUploadShow').html('<b>Hide Upload Contacts</b>');
                     i = 0;
                 }
             });
         });
                </script>
    <script type="text/javascript">
        function expandcollapse(obj, row) {
            debugger;
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);
            Close_All(obj);

            if (div.style.display == "none") {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "../../Content/images/minus.gif";
                }
                else {
                    img.src = "../../Content/images/minus.gif";
                }
                img.alt = "Close";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "../../Content/images/plus.gif";
                }
                else {
                    img.src = "../../Content/images/plus.gif";
                }
                img.alt = "Expand";
            }
        }
        function Close_All(obj) {
            var divOld = document.getElementById(obj);
            var getAttribute;
            var str = '';
            var Grid_Table = document.getElementById('<%= GridContactsInfo.ClientID %>');
            for (var row = 1; row < Grid_Table.rows.length - 1; row++) {
                //expandcollapse(Grid_Table, row)

                var imageColapsenm;
                imageColapsenm = Grid_Table.rows[row].cells[0].firstChild.id;
                if (imageColapsenm != 'imageColapse') {
                    //alert(imageColapsenm);
                    if (imageColapsenm != null) {

                        var div = document.getElementById(imageColapsenm);
                        var img = document.getElementById('img' + imageColapsenm);
                        if (divOld != div) {
                            div.style.display = "none";
                            img.src = "../../Content/images/plus.gif";
                            //img.alt = "Expand to show Questionarrie";
                        }
                    }
                }

            }


            return false;
        }


        function expandcollapseUsage(obj, productid, Cid, row) {

            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);
            debugger;
            //alert(img.className);
            if (div != null) {
                if (div.style.display == "none") {
                    div.style.display = "block";
                   // BindUsageInMainGrid(productid, Cid);
                    if (row == 'alt') {
                        img.className = "fa-search-minus";
                        //img.src = "minus.gif";
                    }
                    else {
                        img.className = "fa-search-minus";
                        //img.src = "minus.gif";
                    }
                    img.alt = "Close to view other BOM";
                }
                else {
                    div.style.display = "none";
                    if (row == 'alt') {
                        img.className = "fa-search-plus";
                        // img.src = "plus.gif";
                    }
                    else {
                        img.className = "fa-search-plus";
                        //img.src = "plus.gif";
                    }
                    img.alt = "Expand to show BOM";
                }
            }
        }


        //function BindUsageInMainGrid(productid, Cid) {
        //    $('#TblUsagegrid' + productid).html("");
        //    $("#DivPleaseWaitMsgInUsage" + productid).html("Please wait while data is loaded...");
        //    $("#DivPleaseWaitMsgInUsage" + productid).show();
        //    $.ajax({
        //        url: '/WF/DC/Webservices/DCServices.asmx/DataTableToJsonWithJsonNetForUsageGrid?productid=' + productid + '&&Cid=' + Cid,
        //        type: "POST",
        //        data: "{'productid': '" + productid + "','Cid': '" + Cid + "'}",
        //        dataType: "json",
        //        contentType: 'application/json; charset=utf-8',
        //        async: true,
        //        success: function (data) {
        //            debugger;
        //            if (data.d != "[]") {
        //                var newData = jQuery.parseJSON(data.d);
        //                var trHTML = '';

        //                var Headerstext = Object.keys(newData[0]);

        //                trHTML += '<tr style="background-color:silver;border:thin;color:white;">';
        //                for (var i = 0; i < Headerstext.length; i++) {
        //                    if (Headerstext[i] != '_id') {
        //                        trHTML += '<th>' + Headerstext[i] + '</th>';
        //                    }
        //                }
        //                trHTML += ' </tr>';

        //                $.each(newData, function (i, item) {
        //                    trHTML += '<tr>';
        //                    for (var j = 0; j < Headerstext.length; j++) {
        //                        if (Headerstext[j] != '_id') {

        //                            if (Headerstext[j] == 'Image') 
        //                            {
        //                                var aid = item[Headerstext[j]];
        //                                debugger
        //                                trHTML += '<td><img src="' + item[Headerstext[j]] + '"/></td>';
        //                            }
        //                            else if (item[Headerstext[j]] != null) {
        //                                trHTML += '<td>' + item[Headerstext[j]] + '</td>';
        //                            }
        //                            else {
        //                                trHTML += '<td></td>';
        //                            }
        //                        }
        //                    }
        //                    trHTML += ' </tr>';
        //                });
        //                $('#TblUsagegrid' + productid).append(trHTML);
        //                $("#DivPleaseWaitMsgInUsage" + productid).hide();
        //            }
        //            else {
        //                $('#TblUsagegrid' + productid).append("<tr><td>No data<td></tr>");
        //                $("#DivPleaseWaitMsgInUsage" + productid).hide();
        //            }
        //        }
        //    });
        //}
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .bootstrap-tagsinput {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  width: 200px;
  line-height: 22px;
  cursor: text;
}
        /*.bootstrap-tagsinput0 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}
        .bootstrap-tagsinput1 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}
        .bootstrap-tagsinput2 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}
        .bootstrap-tagsinput3 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}
         .bootstrap-tagsinput4 {
  background-color: #fff;
  border: 1px solid #e4e4e4;
  display: block;
  padding: 4px 6px;
  color: #7d7f7f;
  vertical-align: middle;
  max-width: 100%;
  line-height: 22px;
  cursor: text;
}*/
    </style>
    <div class="form-group row mb-6">
         <asp:Label ID="lblUserDetails" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
                <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label ID="lblError" runat="server" Visible="false" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                <asp:ValidationSummary ID="Valsumgrid" runat="server" ValidationGroup="Group10" ForeColor="Red"
                    DisplayMode="List" Enabled="true" ShowSummary="true" />
        </div>
     <asp:Panel CssClass="form-group row mb-6" ID="pnlUploadShow" runat="server" ClientIDMode="Static" Width="100%" Style="cursor: pointer;
                    text-align: right;display:none;visibility:hidden;">
                    <b>Show Upload Contacts </b>
                </asp:Panel>
                <asp:Panel CssClass="form-group row mb-6" ID="pnlUpload" runat="server" ClientIDMode="Static" Width="100%" Style="display:none;">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"><b> Select a file to upload</b></label>
                                      <div class="col-sm-2"> 
                                          <asp:FileUpload ID="fileUpload" runat="server" />
                                              
					</div>
                                       <div class="col-sm-8 form-inline"> 
                                           <asp:Button ID="imgUpload" SkinID="btnUpload" Text="" runat="server" OnClick="imgUpload_Click" />
                                           <asp:Button ID="btnDownLoadTemplate" runat="server" Text="Download Template"
                                    OnClick="btnDownLoadTemplate_Click" SkinID="btnDefault"> </asp:Button>
                                            <asp:Button ID="btnDownloadData" runat="server" SkinID="btnDefault" Text="Download Data" OnClick="btnDownloadData_Click" CausesValidation="false"  />
                                           </div>
				</div>
                </asp:Panel>
       <ajaxtoolkit:modalpopupextender id="mdlOptions" cancelcontrolid="imgPerClose"
                        runat="server" backgroundcssclass="modalBackground" targetcontrolid="imgpopup"
                        popupcontrolid="pnlOptions">
                    </ajaxtoolkit:modalpopupextender>
                <asp:Image ID="imgpopup" runat="server" style="display:none;" />
                  <asp:Panel ID="pnlOptions" runat="server" Style="width:400px;height: 320px;display:none;" >
                    
                      
                      <div  id="pnlUserDeatils" runat="server">

                          <div class="card shadow-sm">
    <div class="card-header">
        <h3 class="card-title"> Login details </h3>
        <div class="card-toolbar">
           
        </div>
    </div>
    <div class="card-body">
                 <div class="form-group row mb-6">
                              <asp:ValidationSummary DisplayMode="List" ValidationGroup="ul" ID="ulist" runat="server" />
                              </div>
<div class="form-group row mb-6">
         
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RequesterName%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtUsername" runat="server" ReadOnly="true"></asp:TextBox> 
					</div>
				
                </div>
                          <div class="form-group row mb-6">
            
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.LoginName%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtLoginName" runat="server" ReadOnly="true" ></asp:TextBox> 
					</div>
				
                </div>
                          <div class="form-group row mb-6">
          
                                       <label class="col-sm-4 control-label"> Is Active</label>
                                      <div class="col-sm-8"><asp:CheckBox ID="chkUserStatus" runat="server" /> 
					</div>
				
                </div>
                          <div class="form-group row mb-6">
           
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Password%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtPortalPassword" runat="server" TextMode="Password" ></asp:TextBox> 
                                           <asp:RegularExpressionValidator Display ="None" ControlToValidate = "txtPortalPassword" ID="RegularExpressionValidator2" 
                                   ValidationExpression = "^[\s\S]{4,}$" runat="server" ErrorMessage="Please enter minimum 6 characters in password" ValidationGroup="ul"></asp:RegularExpressionValidator>
					</div>
				
                </div>
                          <div class="form-group row mb-6">
             <div class="col-md-12">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8">
                                        
					</div>
				</div>
                </div>
    </div>
    <div class="card-footer">
        <asp:Button ID="btnUpdateDetails" runat="server" ValidationGroup="ul" SkinID="btnSubmit" OnClick="btnUpdateDetails_Click" />
                                          <asp:HiddenField ID="hcontactid" runat="server" Value ="0" />
                                           <asp:Button ID="btnClose" runat="server" CausesValidation="false" SkinID="btnDefault" Text="Close" />
    </div>
</div>

                    
                

                          </div>
                       <div id="pnlLogintoportal" runat="server">

                           <div class="card shadow-sm">
    <div class="card-header">
        <h3 class="card-title">Enable Login to Portal</h3>
        <div class="card-toolbar">
           
        </div>
    </div>
    <div class="card-body">
       <div class="form-group row mb-6">
                                
                                       <label class="col-sm-4 control-label"> Login to Portal</label>
                                      <div class="col-sm-8"><asp:CheckBox ID="ChkLogintoPortal" runat="server" />
					
				</div>
</div>
                           <div class="form-group row mb-6">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>
    </div>

                                <div class="card-footer">
       <asp:Button ID="btnLogintoportal" runat="server" SkinID="btnSubmit" OnClick="btnLogintoportal_Click" /> 
                                        <asp:Button ID="btnloginClose" runat="server" Text="Close" SkinID="btnDefault" OnClick="btnloginClose_Click" CausesValidation="false" />
    </div>
   
</div>

                        

                          
                       </div>
                       <asp:Button SkinID="btnClose" Text="Close" id="imgPerClose" runat="server" style="vertical-align: bottom;display:none" />  
               
            </asp:Panel>
     <asp:Panel ID="PanelGrid" runat="server">
          <div class="form-group row mb-6">
              <div class="col-md-3 form-inline">
                 <asp:TextBox ID="txtSearch" runat="server" placeholder="search"></asp:TextBox>
                      
                 
                  </div>
               <div class="col-md-1">
                   <asp:LinkButton ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click" CausesValidation="false" />
                   </div>
            
              
              <div class="col-md-8 pull-right form-inline" >
                  
                   <asp:Button Visible="false" ID="btnDownloadAddressList" runat="server" SkinID="btnDefault" Text="Download Address List" OnClick="btnDownloadAddressList_Click" CausesValidation="false" style="float:right;margin-left:20px" />
                  <asp:HyperLink ID="btnCampain" runat="server" SkinID="Button" Text="Marketing Campaign" NavigateUrl="~/WF/CustomerAdmin/Campaign/CampaignList.aspx" style="float:right;margin-left:20px" Visible="false" />
                  <asp:HyperLink ID="btnLink" runat="server" SkinID="Button" Text="Add Contact" NavigateUrl="~/WF/CustomerAdmin/ContactDetails.aspx" style="float:right;" /> &nbsp;
                  
                 
                  </div>
         </div>
         <script language="javascript" type="text/javascript">
             function expandcollapseInnerGrid(obj, row) {
                 debugger;
                 var div = document.getElementById(obj);
                 var img = document.getElementById('img' + obj);
                 //   Close_All(obj);
                 //alert(div);
                 //setting width dynamically

                 var MainGrid = document.getElementById("DivInventoryMainGrid");
                 var InnerGrid = document.getElementById(obj);
                 // alert(MainGrid.clientWidth);
                 //alert("Main Grid Width " + MainGrid.style.width + "<br/> Inner Grid Width " + InnerGrid.style.width)
                 if (MainGrid != null && InnerGrid != null) {
                     InnerGrid.style.width = MainGrid.clientWidth - 15 + "px";
                     // alert(InnerGrid.style.width);
                 }
                 if (img != null) {
                     if (div.style.display == "none") {
                         div.style.display = "block";
                         if (row == 'alt') {
                             img.src = "minus.gif";
                         }
                         else {
                             img.src = "minus.gif";
                         }
                         img.alt = "Close";
                     }
                     else {
                         div.style.display = "none";
                         if (row == 'alt') {
                             img.src = "plus.gif";
                         }
                         else {
                             img.src = "plus.gif";
                         }
                         img.alt = "Expand";
                     }
                 }
             }

             </script>
          <div class="table-responsive">
                    <asp:GridView ID="GridContactsInfo" runat="server" Width="120%" DataKeyNames="ID"
                        AutoGenerateColumns="False" OnRowCommand="GridContactsInfo_RowCommand" OnRowEditing="GridContactsInfo_RowEditing"
                        OnRowCancelingEdit="GridContactsInfo_RowCancelingEdit" OnRowUpdating="GridContactsInfo_RowUpdating"
                        OnRowUpdated="GridContactsInfo_RowUpdated" OnRowDeleted="GridContactsInfo_RowDeleted"
                        OnRowDeleting="GridContactsInfo_RowDeleting" OnPageIndexChanging="GridContactsInfo_PageIndexChanging"
                        AllowPaging="True" AllowSorting="false" OnRowDataBound="GridContactsInfo_RowDataBound" PageSize="20">
                        <Columns>
                              <asp:TemplateField Visible="false" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="50px"  ControlStyle-Width="50px" FooterStyle-Width="50px">
             <ItemStyle HorizontalAlign="Center" />
             <ItemTemplate>
                 <a id="imageColapse123" href="javascript:expandcollapseInnerGrid('divInner<%# Eval("id") %>','one');" style="display:none;">
                     <img title='View' id='imgdivInner<%# Eval("ID") %>'
                          alt='Click to show/hide <%# Eval("ID") %>' style="width:9px;border:0px" src='plus.gif' />
                 </a>
               <asp:CheckBox ID="chkContact" runat="server" />
             </ItemTemplate>
            
         </asp:TemplateField>
                             <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/WF/CustomerAdmin/ContactDetails.aspx?ContactID={0}"
                                    Text="View Information"
                                     ControlStyle-CssClass="btn btn-dark"  ItemStyle-Width="10%">
                                </asp:HyperLinkField>

                            <asp:TemplateField ShowHeader="False" Visible="false">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate"></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="BtnLinkCancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" SkinID="BtnLinkEdit"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblid1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                            </asp:TemplateField>
                             <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                     <%--   <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalImage">
                                            <Animations>
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>--%>
                                        <%--<ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalImage" PopDelay="0" PopupPosition="Right" EnableViewState="false"
                                            OffsetY="26" />--%>
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl(Eval("ID").ToString(),ImageManager.ThumbnailSize.MediumSmaller) %>' Width="50px" Height="50px"/>
                                        <%--<div id="pnlOriginalImage" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl(Eval("ID").ToString(),null) %>' CssClass="img-responsive"/>
                                        </div>--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateField>
                            <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# GetUserImage(Eval("ID").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Date Created" SortExpression="DateLogged" ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblidDateCreated" runat="server" Text='<%# Bind("DateLogged","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblidDateCreated" runat="server" Text='<%# Bind("DateLogged","{0:d}") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name"  ItemStyle-Width="8%">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtname" runat="server" Text='<%# Bind("Name") %>' Width="90"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtname"
                                        Display="None" ErrorMessage="Please enter Name" Text="Please enter Name" ValidationGroup="Group10">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title/Position" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txttitle" runat="server" Text='<%# Eval("Title") %>' Width="90"
                                        EnableViewState="false"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email Address" SortExpression="Email"  ItemStyle-Width="8%">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtemail" runat="server" Text='<%# Bind("Email") %>' Width="160"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter vaild email Eg:Smith@Enc.com"
                                        Text="Enter vaild email Eg:Smith@Enc.com" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ControlToValidate="txtemail" Display="None" ValidationGroup="Group10"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtemail"
                                        Display="None" Text="Please enter Email address" ErrorMessage="Please enter Email address"
                                        ValidationGroup="Group10" ForeColor="Red"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblemail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cell Number" SortExpression="Mobile"  ItemStyle-Width="8%">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtcontactnumber" runat="server" Text='<%# Bind("Mobile") %>'
                                        Width="90" MaxLength="16"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="filter_phone" runat="server" TargetControlID="txtcontactnumber"
                                        ValidChars="0123456789+ " />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblcontactnumber" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Properties" Visible="false">
                                <ItemTemplate>
                                    <asp:Button Text='<%# Bind("cnt") %>' ID="btnPcount" runat="server" CssClass="btn btn-info" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" SortExpression="Address1" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtlocation" runat="server" Text='<%# Bind("Location") %>' Width="70"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbllocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Tags" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="350px" HeaderStyle-Width="350px" Visible="false" >
                                <ItemTemplate>
                                    <asp:Literal ID="liTags" runat="server" Text='<%# SetTagCss((string)Eval("Tags") )%>' ></asp:Literal>
                                </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Postcode" SortExpression="Postcode" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPostcode" runat="server" Text='<%# Bind("Postcode") %>' Width="70"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPostcode" runat="server" Text='<%# Eval("Postcode") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Show Assets" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkKeyContact" runat="server" Checked='<%# Eval("Key_Contact") ?? false %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkedit" runat="server" Checked='<%# Eval("Key_Contact") ?? false %>' /></EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notes" SortExpression="Description" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtnotes" runat="server" Text='<%# Bind("Notes") %>' Width="120"
                                        TextMode="MultiLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblnotes" runat="server" Text='<%# Eval("Notes") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Likes/Dislikes" SortExpression="Description" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtlikesdislikes" runat="server" Text='<%# Bind("Likes_Dislikes") %>'
                                        Width="120" TextMode="MultiLine"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbllikesdislikes" runat="server" Text='<%# Eval("Likes_Dislikes") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Hide Contact" Visible="false">
                                 <ItemTemplate>
                                     <asp:CheckBox ID="chkHideContact" runat="server" Checked='<%# Eval("isDisabled") ?? false %>' AutoPostBack="true" OnCheckedChanged="chkHideContact_CheckedChanged" />
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                      <asp:CheckBox ID="chkEditHideContact" runat="server" Checked='<%# Eval("isDisabled") ?? false %>' AutoPostBack="true" OnCheckedChanged="chkHideContact_CheckedChanged" />
                                 </EditItemTemplate>
                                 </asp:TemplateField>
                             <asp:TemplateField Visible="false">
                                 <ItemTemplate>
                                       <asp:LinkButton ID="Linkdocs" runat="server" ForeColor="Green" Font-Bold="true"
                                         CausesValidation="false" CommandName="docs" CommandArgument='<%# Bind("ID") %>' Text="Profile"></asp:LinkButton>
                                     <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ID", "~/WF/CustomerAdmin/ContactDetails.aspx?ContactID={0}") %>' Text="Warranty Docs" Font-Bold="true"  ForeColor="Green"></asp:HyperLink>--%>
                                 </ItemTemplate>
                                 <ItemStyle Font-Bold="True" ForeColor="Green" />
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="Login to Portal" SortExpression="LogintoPortal" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="5%" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLogin" runat="server" AutoPostBack="true" OnCheckedChanged="chkLogin_CheckedChanged"
                                        Checked='<%# Eval("LogintoPortal") ?? false %>' />
                                   
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkLoginedit" runat="server" Checked='<%# Eval("LogintoPortal") ?? false %>' /></EditItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="" SortExpression="LogintoPortal"  ItemStyle-Width="5%" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDetails" runat="server" CommandName="Options" CommandArgument='<%# Bind("ID") %>'>Change Password</asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkLoginedit" runat="server" Checked='<%# Eval("LogintoPortal") ?? false %>' /></EditItemTemplate>
                            </asp:TemplateField>
                           


                           <asp:TemplateField HeaderText="" SortExpression=""  Visible="false">
                                <ItemTemplate>
                                   
                                    <asp:Button ID="btnRemainder" runat="server" CommandName="SetReminder" CommandArgument='<%# Bind("ID") %>' Text="Set Reminder"></asp:Button>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkLoginedit11" runat="server" Checked='<%# Eval("LogintoPortal") ?? false %>' /></EditItemTemplate>
                            </asp:TemplateField>
                              <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/WF/Assets/AssetsAdmin.aspx?PContactId={0}"
                                    Text="Show Products" ControlStyle-ForeColor="Green"  ControlStyle-Font-Bold="true" Visible="false">
                                <ControlStyle Font-Bold="True" ForeColor="Green" />
                                </asp:HyperLinkField>
                             <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate><br />
                                    <asp:LinkButton ID="LinkManageAssets" runat="server" ForeColor="Green" Font-Bold="true"
                                         CausesValidation="false" CommandName="ManageAssets" CommandArgument='<%# Bind("ID") %>' Text="Profile"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Description">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" 
                                        CommandArgument='<%# Eval("ID")%>' SkinID="BtnLinkDelete" ToolTip="Delete" OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                           
                          
                        </Columns>
                    </asp:GridView>
              </div>
                </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mdlExnter" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="lblStorageNew" PopupControlID="pnlStorageNew" CancelControlID="btnPopClose" >
</ajaxToolkit:ModalPopupExtender>
<asp:Label ID="lblStorageNew" runat="server"></asp:Label>
<asp:Panel ID="pnlStorageNew" runat="server" BackColor="White" Style="display:none;"
                       Width="680px" Height="460px" CssClass="panel panel-color panel-info" ScrollBars="None">
    <div class="card-header">
							<h3 class="card-body">Update Maintenance Reminder</h3>
							
							<div class="card-toolbar">
								<%--<a href="#">
									<i class="linecons-cog"></i>
								</a>
								
								<a href="#" data-toggle="panel">
									<span class="collapse-icon">–</span>
									<span class="expand-icon">+</span>
								</a>
								
								<a href="#" data-toggle="reload">
									<i class="fa-rotate-right"></i>
								</a>--%>
								 <asp:LinkButton ID="btnPopClose" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss"/>
								<%--<a href="#" data-toggle="remove">
									×
								</a>--%>
							</div>
						</div>
    <div class="panel-body">

               <div class="form-group row mb-6">
          <div class="col-md-12">
              <asp:ValidationSummary ID="vdSummary" runat="server" ValidationGroup="vd" />
              </div>
                   </div>
    <div class="form-group row mb-6">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Equipment</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtEquipment" runat="server" SkinID="txt_90" Width="10"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEquipment"
                                        Display="None" ErrorMessage="Please enter equipment" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
    
    
    <div class="form-group row mb-6">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Reminder Description</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtReminderDescription" runat="server" SkinID="txt_90"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReminderDescription"
                                        Display="None" ErrorMessage="Please enter reminder description" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
   
    <div class="form-group row mb-6">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Maintenance Type</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlMaintenanceType" runat="server" SkinID="ddl_90"></asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMaintenanceType" InitialValue="0"
                                        Display="None" ErrorMessage="Please enter maintenance type" ValidationGroup="vd"></asp:RequiredFieldValidator>
            </div>
	</div>
</div>
               <div class="form-group row mb-6">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Date Of Reminder</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtDateOfReminder" runat="server" SkinID="Date"></asp:TextBox>
                <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender"  /></div>
              <asp:RequiredFieldValidator ID="rfv_dateRised1" runat="server" ControlToValidate="txtDateOfReminder"
                                        Display="None" ErrorMessage="Please enter date of reminder" ValidationGroup="vd"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorDateReceived" runat="server" ControlToValidate="txtDateOfReminder"
                        ErrorMessage="Please enter valid date" Operator="DataTypeCheck" Type="Date" ValidationGroup="vd" Display="None"></asp:CompareValidator>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                        PopupButtonID="imgbtnenddate6" TargetControlID="txtDateOfReminder" CssClass="MyCalendar">
                    </ajaxToolkit:CalendarExtender>
            </div>
	</div>
                <div class="form-group row mb-6">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">Renewal Amount</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtRenewalAmount" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
                
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRenewalAmount"
                                        Display="None" ErrorMessage="Please enter date of reminder" ValidationGroup="vd"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtRenewalAmount"
                        ErrorMessage="Please enter valid renewal amount" Operator="DataTypeCheck" Type="Double" ValidationGroup="vd" Display="None"></asp:CompareValidator>
                   
            </div>
              </div>
	</div>
     <div class="form-group row mb-6">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
               <asp:HiddenField ID="hbomid" runat="server" Value="0" />
               <asp:Button ID="btnSelect" runat="server" SkinID="btnSubmit" OnClick="btnSelect_OnClick" ValidationGroup="vd" />
              
               </div>
              </div>
         </div>

              </div>
        
</asp:Panel>
    <asp:HiddenField ID="HiddenFiled1" runat="server" />
       <asp:Panel ID="panel1" runat="server" Visible="false">
            <div class="form-group row mb-6">
        <div class="col-md-12 text-bold">
        <strong>  Add Contact </strong>
            <hr class="no-top-margin" />
            </div>
    </div>

            <div class="form-group row mb-6">
                 <asp:ValidationSummary ID="AddNew" runat="server" ValidationGroup="AddContact" />
                    <asp:RequiredFieldValidator ValidationGroup="AddContact" ID="RefName" runat="server"
                        Display="None" ErrorMessage="Please enter Name" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ValidationGroup="AddContact" ID="RefEmail" runat="server"
                        Display="None" ErrorMessage="Please enter Email address" ControlToValidate="TxtEmail"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RevEmail" runat="server" Display="None" ErrorMessage="Please enter valid Email Address"
                        ControlToValidate="TxtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="AddContact"></asp:RegularExpressionValidator>
                </div>

               <div class="form-group row mb-6">
                                  <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Name</label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Title/Position</label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
					</div>
				</div>
</div>
           <div class="form-group row mb-6">
                                  <div class="col-md-6">
                                       <label class="col-sm-3 control-label">Email Address</label>
                                      <div class="col-sm-9"><asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Telephone</label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtTelephone" runat="server" MaxLength="20"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender
                                    ID="filter_phone" runat="server" TargetControlID="txtTelephone" ValidChars="0123456789+ " />
					</div>
				</div>
</div>
           
           <div class="form-group row mb-6">
                               <div class="col-md-6">    
                                       <label class="col-sm-3 control-label"> Address</label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtAddrs1" runat="server" TextMode="MultiLine" Width="250px" Height="50px"></asp:TextBox>
					</div>
				</div> 

                 <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Mobile</label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtMobile" runat="server" MaxLength="20"></asp:TextBox>
				   	</div>
				</div>
 <%--<div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Likes/Dislikes</label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtLikesDislikes" runat="server" TextMode="MultiLine" SkinID="txtMulti"
                                    Height="50px"></asp:TextBox>
					</div>
				</div>--%>
</div>
          
           <div class="form-group row mb-6">
               <div class="col-md-6">
                      <label class="col-sm-3 control-label">Building Name</label>
                      <div class="col-sm-9">
                          <asp:TextBox ID="txtbuildingName" runat="server"></asp:TextBox>
                      </div>
                  </div>
                 <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Notes</label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" SkinID="txtMulti" Height="50px"></asp:TextBox>
					</div>
				</div>
                                  
                                  <%--<div class="col-md-6" style="display:none;">
                                       <label class="col-sm-3 control-label"> Address2</label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtAddrs2" runat="server" TextMode="MultiLine" Width="250px" Height="50px"></asp:TextBox>
					</div>
				</div>--%>
</div>
       <%--    <div class="form-group row mb-6" style="display:none;">
                                  <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Date Of Birth</label>
                                      <div class="col-sm-9 form-inline"> <asp:TextBox ID="txtDateofBirth" runat="server" SkinID="Date"></asp:TextBox><asp:Label
                                    ID="Image2" runat="server" SkinID="Calender" ToolTip="Pick a date" />
                                <asp:RegularExpressionValidator ID="RevDOB" runat="server" ControlToValidate="txtDateofBirth"
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                    ValidationGroup="AddContact" Text="*" ErrorMessage="Please enter valid Date of Birth"></asp:RegularExpressionValidator>
					</div>
				</div>
                                 
</div>--%>
           <div class="form-group row mb-6">

               <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Location</label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
					</div>
				</div>

                                  <div class="col-md-6" style="display:none;">
                                       <label class="col-sm-3 control-label"> Postal Code</label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtPostcode" SkinID="txt_50" runat="server"></asp:TextBox>
					</div>
				</div>
               <div class="col-md-6">
                        <label class="col-sm-3 control-label">Town </label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtTown" runat="server"></asp:TextBox>
                        </div>
                   </div>
            </div>

           <div class="form-group row mb-6">
               <div class="col-md-6">
                          <label class="col-sm-3 control-label">City</label>
                           <div class="col-sm-9">
                               <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                           </div>
                   </div>
                <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Country</label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
					</div>
				</div>

                                  <%--<div class="col-md-6">
                                       <label class="col-sm-3 control-label">Fax</label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
					</div>
				</div>--%>
           </div>
            <div class="form-group row mb-6">
                                  <div class="col-md-6">
                                       <label class="col-sm-3 control-label">Department</label>
                                      <div class="col-sm-9"><asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>
					</div>
				</div>

                   <div class="col-md-6">
                                       <label class="col-sm-3 control-label">Upload Picture</label>
                                      <div class="col-sm-9"><asp:FileUpload ID="ImageFileupload_PFContact" CssClass="control-label" runat="server" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="ImageFileupload_PFContact"
                                    Display="None" ErrorMessage="Select Image only" ValidationExpression="^.*([^\.][\.](([gG][iI][fF])|([Jj][pP][Gg])|([Jj][pP][Ee][Gg])|([Bb][mM][pP])|([Pp][nN][Gg])))"
                                    ValidationGroup="AddContact">File</asp:RegularExpressionValidator>
					</div>
				</div>

 <%--<div class="col-md-6" style="display:none;">
                                       <label class="col-sm-3 control-label"> County</label>
                                      <div class="col-sm-9"> <asp:TextBox ID="txtCounty" runat="server"></asp:TextBox>
					</div>
				</div>--%>
</div>
           <div class="form-group row mb-6" style="display:none;visibility:hidden">

               <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Hide Contact</label>
                                      <div class="col-sm-9"><asp:CheckBox ID="chkHideContact" runat="server" />
					</div>
				</div>
               <div class="col-md-6">
                                       <label class="col-sm-3 control-label"> Show Assets</label>
                                      <div class="col-sm-9"><asp:CheckBox ID="CheckKeyContact" runat="server" />
					</div>
				</div>           
           </div>
           <div class="form-group row mb-6">
                                  
 <div class="col-md-6">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9">
					</div>
				</div>
</div>
           <div class="form-group row mb-6">
                                  <div class="col-md-6">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9"><asp:Button ID="btnUpdate" runat="server" SkinID="btnUpdate" OnClick="btnUpdate_Click"
                            ValidationGroup="AddContact" Visible="false" />
                        <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click"
                            ValidationGroup="AddContact" />
                        &nbsp;<asp:Button ID="btnCanel" runat="server" SkinID="btnCancel" CausesValidation="False"
                            OnClick="btnCanel_Click" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9">
					</div>
				</div>
</div>
                   
                  
                </asp:Panel>
               
               
               
    
  <%--  <ajaxToolkit:CalendarExtender ID="CalendarExtenderDOB" runat="server" TargetControlID="txtDateofBirth"
         CssClass="MyCalendar" PopupButtonID="Image2">
    </ajaxToolkit:CalendarExtender>--%>
  
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
     <script type="text/javascript">
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setStatusBackColor);
         setStatusBackColor();
         function setStatusBackColor() {

             $('.statuscls').each(function () {
                 debugger;
                 var s = $(this).html();
                 if (s == 'Expired')
                     $(this).closest("td").css({ "background-color": "#FF0000", "text-align": "center", "vertical-align": "middle","color": "white","font-weight": "bold" });
                
             });

         }
</script>

      <div class="col-md-4 d-flex d-inline" style="display:none;visibility:hidden;">
                
									<label class="control-label col-sm-2" for="name">Tags</label>
								<div class="col-md-4 form-inline">
                                    	
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
									
									<asp:ListBox CssClass="form-control" runat="server" id="listTags" ClientIDMode="Static" SelectionMode="Multiple" Width="300px">

											
									</asp:ListBox>

                                    
                                    	</div>
							</div>
</asp:Content>
