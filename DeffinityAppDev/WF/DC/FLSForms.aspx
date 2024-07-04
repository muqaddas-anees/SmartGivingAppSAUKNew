<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="FLSForms.aspx.cs" Inherits="DeffinityAppDev.WF.DC.FLSFormsV2" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
 <%@ Register Src="~/WF/DC/controls/CustomerOrder.ascx" TagName="CustomerOrder"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <uc2:FlsTab ID="flstab1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <label id="lblTitle" runat="server"></label>  
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
   <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  <%= Resources.DeffinityRes.ServiceDesk%></a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel ID="pnlOrder" runat="server" Width="100%" Visible="false">
    <uc3:CustomerOrder ID="CustomerOrder1" runat="server" Visible="false" />
    
    </asp:Panel>
    <%-- <link rel="stylesheet" href="../stylcss/HCstyle.css"/>--%>
      <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
   <%--  <script src="../Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="jQuery.print.js"></script>
    <link rel="stylesheet" href="../stylcss/ButtonStyle.css"/>
    <script type="text/javascript" src="../Scripts/HCform.js"></script>--%>

   <%--<link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
  <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
  <script src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>--%>

 <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>

<%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
<%: System.Web.Optimization.Styles.Render("~/bundles/formscss") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/forms") %>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var PanelId = GetParameterValues('PID');
            GetTextboxId(PanelId);
        });
        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }
        function GetTextboxId(panelid) {
            var el = $()
            $.ajax({
                url: "../HC/HCWebService.asmx/GetTextBoxId?pid=" + panelid,
                type: "POST",
                data: "{'panelid': '" + panelid + "'}",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.d != '') {
                        debugger;
                        var obj = jQuery.parseJSON(data.d);
                        //debugger;
                        //alert(data.CntlID);
                        //debugger;
                        if (obj != '')
                            debugger;
                        $.each(obj, function () {
                            if ($("#MainContent_MainContent_" + this.CntlID + "").val() == '') {
                                $("#MainContent_MainContent_" + this.CntlID + "").datepicker({ dateFormat: 'dd/mm/yy' });
                            }
                        });
                    }
                },
                error: function (msg) { setMsg(Error); }
            })
        }
        function uploadOurlogo(id) {

            var el = $(e).closest('.td_cls');
            var pid = $(el).attr('id');

            var fileUpload = $('#"+id+"').get(0);
            var files = fileUpload.files;

            var fdata = new FormData();
            for (var i = 0; i < files.length; i++) {
                fdata.append(files[i].name, files[i]);
            }

            var options = {};

            options.url = "WF/Health/HC/HCWebService.asmx/UploadLogo?pid=" + pid;
            options.type = "POST";
            options.data = fdata;
            options.contentType = false;
            options.processData = false;
            options.async = true,
                options.success = function (result) {
                    var obj = result;

                    if (result != '') {
                        setSuccessMsg("Uploaded successfully.");
                        GetControlImageData_our(pid);
                    }
                };
            options.error = function (err) { setErrorMsg('Fail to upload.Please try again.'); };

            $.ajax(options);
            return false;
        }



     
       
            
</script>
   <asp:Panel ID="pnlHealthChecks" runat="server" ScrollBars="None" Width="100%" Height="100%" ClientIDMode="Static">
         <div class="form-group row">
              <div class="col-md-12">
              <div class="pull-right">
         <asp:Button ID="BtnPrint" runat="server" Text="Print" OnClick="BtnPrint_Click" />
                  </div>
                  </div>
             </div>
           <div class="form-group row">
      <div class="col-md-10">
          <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
          </div>
        </div>
          <asp:UpdatePanel ID="updatepanel_additional" runat="server" ClientIDMode="Static" style="padding-left:15px;border-style:groove">
                    <ContentTemplate>
                     <asp:PlaceHolder ID="ph" runat="server" ClientIDMode="Static"></asp:PlaceHolder>
                        <asp:HiddenField ID="hformid" runat="server" Value="0" />
                        </ContentTemplate>
              </asp:UpdatePanel>
        <script language="javascript" type="text/javascript">
            function CheckYes(chkYes, chkNo) {
                chkNo.checked = false;
            }

            function CheckNo(chkYes, chkNo) {
                chkYes.checked = false;
            }
        </script>

      
      
    </asp:Panel>

       <div class="form-group row">
              <div class="col-md-12">
              <div class="pull-right">
     <asp:Button ID="btnSubmitChanges" runat="server" Text="Save"
                    OnClick="btnSubmitChanges_Click" ValidationGroup="Form" />
                  </div>
                  </div>
         </div>
      <script type="text/javascript">
          //apply date 
          applyDatePicker();
      </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
