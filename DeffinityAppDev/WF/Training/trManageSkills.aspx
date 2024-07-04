<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trManageSkills" Codebehind="trManageSkills.aspx.cs" %>

<%@ Register src="~/WF/Training/controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register Src="~/WF/Training/controls/dropdownView.ascx" TagName="DropDownList" TagPrefix="uc2" %>
<%@ Register src="~/WF/Admin/controls/ResourcePlannerTabs.ascx" tagname="ResourcePlannerTabs" tagprefix="uc3" %>
<%@ Register Src="~/WF/Training/controls/SkillManagerSubTabCtrl.ascx" TagName="SkillManagerSubTab"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
     <uc1:TrainingTabs ID="TrainingTabs1" runat="server" Visible="false" />
     <uc3:ResourcePlannerTabs ID="ResourcePlannerTabs1" runat="server" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Training%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <label id="lblTitle" runat="server"> Dashboard
                  </label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_options" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
div#tipDiv {
    padding:4px;
   
    color:#000; font-size:13px; line-height:1.2;
    background-color:#F5F5F5; border:1px solid #667295; 
    width:200px; 
}
</style>
    <style type="text/css">

	.tableBoxOuter {
		width:900px;height:36em; 
		background: #FFFFFF;
		
	}

	.scrolltable  th {
		font-size: 10px;
		font-family: verdana, arial, sans-serif;
		margin:0em;
		padding-top: 4px;
		padding-bottom: 4px;
		padding-right: 0px;
		padding-left: 4px;
		table-layout:fixed;
		white-space:nowrap;
	}

</style>
<script type="text/javascript" src="../js/LockScroll.js"> 
</script>
<script src="../js/dw_event.js" type="text/javascript"></script>
<script src="../js/dw_viewport.js" type="text/javascript"></script>
<script src="../js/dw_tooltip.js" type="text/javascript"></script>
<script src="../js/dw_tooltip_aux.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
            $(document).ready(function () {
                $("#imgLoading").hide();
                $("#lblMsg").hide();
                $('input[type=checkbox]').click(function () {
                    debugger;
                    var s = this.value;
                    if (this.checked == false) {
                        $("input:checkbox[value=" + this.value + "]:checked").attr('checked', false);
                    }
                    else {
                        $("input:checkbox[value=" + this.value + "]:not(:checked)").attr('checked', true);
                    }
                });
                $("#btnshow").on("click", function () {
                    var mchecked = [];
                    var munchecked = [];
                    $("#lblMsg").hide();
                    $("#imgLoading").show();
                    var obj = $("input[type=checkbox]:checked"); // Get all paragraphs
                    var a = $.makeArray(obj); // One way to do it
                    debugger;
                    if(a.length >0)
                    {
                        $.each(a, function (key, eval) {
                            mchecked.push($(eval).val());
                        });
                        
                    }
                    //var mchecked = $("input[type=checkbox]:checked").map(function () {
                    //    return this.value;
                    //}).toArray();

                    var objun = $("input[type=checkbox]:not(:checked)"); // Get all paragraphs
                    var a_un = $.makeArray(objun); // One way to do it
                    debugger;
                    if (a_un.length > 0) {
                        $.each(a_un, function (key, eval) {
                            munchecked.push($(eval).val());
                        });
                       
                    }

                    //var postData = { values: mchecked };
                    debugger;
                    $.ajax({
                        type: "POST",
                        url: "/ServiceMgr.asmx/SaveList",
                        data: "{'data':'" + JSON.stringify($.unique(mchecked)) + "','ucdata':'" + JSON.stringify($.unique(munchecked)) + "'}",
                        contentType: "application/json; charset=utf-8",
                        //data: postData,
                        success: function (data) {
                            debugger;
                            //alert(data.d);
                            $("#imgLoading").hide();
                            $("#lblMsg").show();
                            $('#lblMsg').delay(2500).fadeOut('slow');
                            while (mchecked.length > 0) {
                                mchecked.pop();
                            }
                            while (munchecked.length > 0) {
                                munchecked.pop();
                            }
                            //alert(mchecked.length);
                            //alert(munchecked.length);

                        },
                        dataType: "json",
                        traditional: true
                    });
                });
               

            });
        </script>
    <uc1:SkillManagerSubTab ID="SkillManagerSubTab1" runat="server" />
    <%--<script type="text/javascript" src="../Scripts/jquery-1.9.0.min.js"> 
</script>--%>
    
   <%-- <script src="../js/dw_event.js" type="text/javascript"></script>
<script src="../js/dw_viewport.js" type="text/javascript"></script>
<script src="../js/dw_tooltip.js" type="text/javascript"></script>
<script src="../js/dw_tooltip_aux.js" type="text/javascript"></script>--%>
   
    <table style="width:100%">
            <tr>
                <td style="vertical-align:middle;">
                     <table style="width:50%">
                         <tr>
                             <td> <asp:Panel ID="pnlUser" runat="server" Width="420px" BorderColor="Silver" BorderWidth="1px"
                            Height="100px" ScrollBars="Auto">
                            <asp:CheckBoxList ID="chkUserlist" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel> </td>
                             <td style="vertical-align:middle;"> <asp:Button ID="btnView" runat="server" SkinID="btnView" OnClick="btnView_Click" /> <asp:Button ID="btnViewAll" runat="server" SkinID="btnDefault" Text="View All" OnClick="btnViewAll_Click" /></td>
                             <td style="vertical-align:middle;"></td>
                         </tr>
                    </table>
                </td>
            </tr>
            <tr><td style="">
            <div><asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" ForeColor="Green" Text="Updated successfully" ></asp:Label> </div>
            <div style="float:right;padding-right:80px"><asp:Label id="imgLoading" runat="server" ClientIDMode="Static" SkinID="Loading" /> <asp:Button ID="btnshow" runat="server" SkinID="btnDefault" Text="Show" ClientIDMode="Static" ToolTip="Save Changes" /> </div>
            <br />
            <div class="clr"></div>
        <div>
       
            
       </div>
        </td>
      </tr>
         </table>
     <div class="form-group">
          <asp:Literal ID="pnlDisplayHtml" runat="server" EnableViewState="false"></asp:Literal>
         </div>
</asp:Content>


