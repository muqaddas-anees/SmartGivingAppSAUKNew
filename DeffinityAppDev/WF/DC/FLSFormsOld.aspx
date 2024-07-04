<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="FLSFormsOld.aspx.cs" Inherits="DeffinityAppDev.WF.DC.FLSForms" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
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
    <div class="form-group row" id="pnlForms" runat="server" visible="false">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Forms</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlForms" runat="server"></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <asp:Button ID="btnApply" runat="server" SkinID="btnApply" OnClick="btnApply_Click" />
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-10">
          <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
          </div>
        </div>
    
<asp:Panel ID="pnlCustomFields" runat="server">
<%--<div class="row">
          <div class="col-md-12 form-inline">
 <strong>Custom fields <%--<%=Resources.DeffinityRes.CustomFieldsfor %>  </strong> 
<hr class="no-top-margin" />
	</div>
</div>--%>
   <asp:Label ID="lblCustomFiledCustomer" runat="server" Visible="false"></asp:Label>
    
    <%-- <asp:ValidationSummary ID="VS2" runat="server" ValidationGroup="Custom" DisplayMode="BulletList" />--%>
    <div class="form-group row">
      <div class="col-md-10">

           <div>
        <asp:UpdatePanel ID="updatepanel_additional" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                <asp:HiddenField ID="hcount" runat="server" ClientIDMode="Static" Value="0" />
                 <asp:HiddenField ID="hnext" runat="server" ClientIDMode="Static" Value="0" />
                <script type="text/javascript">
                    //var rval = parseInt($('#hcount').val());
                   
                    //hideAll();
                    //loadFirst();
                    
                    //function hideAll() {
                    //    $(window).load(function () {
                    //        // executes when complete page is fully loaded, including all frames, objects and images
                    //        //alert($('#hcount').val());
                            
                    //        for (i = 0; i < rval; i++) {
                    //            //alert(i);
                    //            $(".tr" + i.toString()).hide();
                    //        }

                    //        $('#imgCustomFieldUpdate').hide();
                    //    });
                    //}
                    
                    //function loadFirst() {
                    //    //hideAll();
                    //    $(window).load(function () {
                    //        //var rval = parseInt($('#hcount').val());
                    //    if (rval > 0) {
                    //        var cnt = 0;//parseInt($('#hnext').val());
                    //        $(".tr" + cnt).show();
                    //    }
                    //    if (rval - 1 == cnt) {
                    //        $('#imgCustomFieldUpdate').show();
                    //        $('#btnLoad').hide();
                    //    }
                    //    });
                    //}
                    //function loadNext()
                    //{
                    //    //hideAll();
                    //    debugger;
                    //   // $(window).load(function () {
                    //    //var rval = parseInt($('#hcount').val());
                    //    var cnt = parseInt($('#hnext').val());
                    //    if (rval > cnt)
                    //    {
                    //        for (i = 0; i < rval; i++) {
                    //            //alert(i);
                    //            $(".tr" + i.toString()).hide();
                    //        }
                    //        cnt = cnt + 1;
                    //        $(".tr" + cnt).show();
                    //        $('#hnext').val(cnt);

                    //        if(rval-1 == cnt)
                    //        {
                    //            $('#imgCustomFieldUpdate').show();
                    //            $('#btnLoad').hide();
                    //        }
                           
                    //    }
                    //  //  });
                    //    return false;
                    //}
                </script>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="imgCustomFieldUpdate" />
            </Triggers>
        </asp:UpdatePanel>
  </div>
    
<div class="form-group row">
     
	<div class="col-md-10 pull-right">
           <div id="div1">
               <a id="btnLoad" onclick="loadNext();" class="btn btn-secondary" style="float:right;display:none;visibility:hidden;">Next</a>
              <%-- <asp:Button ID="btnNext" runat="server" ClientIDMode="Static" SkinID="btnNext" OnClientClick="#();" />--%>
                    <asp:Button ID="imgCustomFieldUpdate" runat="server" SkinID="btnSave" ValidationGroup="Custom"
                        OnClick="imgCustomFieldUpdate_Click" ClientIDMode="Static"  style="float:right;" />
                </div>
	</div>
	
</div>


          </div>
        </div>
  
    
</asp:Panel>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
