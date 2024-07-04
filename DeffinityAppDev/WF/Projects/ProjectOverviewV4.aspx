<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectOverviewV4" Codebehind="ProjectOverviewV4.aspx.cs" %>

<%-- --%>
<%--<%@ Register Assembly="Evyatar.Web.Controls" Namespace="Evyatar.Web.Controls" TagPrefix="evy" %>--%>
<%@ Register Src="~/WF/Projects/Controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="~/WF/Projects/Controls/Checkpoint_tabs.ascx" TagName="OpsViewTabs" TagPrefix="uc2" %>
<%@ Register Src="~/WF/Projects/Controls/ProjectOverviewCtrl.ascx" TagName="ProjectOverviewCtrl" TagPrefix="uc2" %>
<%@ Register Src="~/WF/Projects/Controls/ChitChatCtrlHome.ascx" TagName="ChitChatCtrlHome" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.ProjectPlan%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function getQuerystring(key, default_) {

            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href.toLowerCase());
            if (qs == null)
                return default_;
            else
                return qs[1];
        }

        var check1 = 0;
        function hidechatdiv() {
            //link_chatter
            debugger;
            if (check1 == 0) {
                $("#div_chart").hide();
                $("#div_chat").show();
                $("#link_chatter").html("");
                $("#link_chatter").append("<i class='fa-wechat'></i> View Gantt");
                check1 = 1;
            }
            else {
                $("#div_chat").hide();
                $("#div_chart").show();
                $("#link_chatter").html("");
                
                $("#link_chatter").html("<i class='fa-wechat'></i> Chatter");
                check1 = 0;
            }
            return false;

        }
    </script>
   <%-- <script language="javascript" type="text/javascript" src="js/jquery-1.4.2.min.js"></script>--%>
    <script language="javascript" type="text/javascript">

        function CopyResource() {
            ProjectTasks.ProjectTask_CopyResourceByTask_srv(getQuerystring('project'), $('#<%=ddlCopyResourceFrom.ClientID %> OPTION:selected').val(), $('#<%=ddlCopyResourceTo.ClientID %> OPTION:selected').val(), CopyResource_success, CopyResource_fail);
            return false;
        }
        function CopyResource_success() {
            clearmsg_div();
            try {
                getresourcelist_copy_bylistposition();
            }
            catch (err) { }
            addmsg_div('<p>Resource(s) copied successfully</p>');
        }
        function CopyResource_fail() {
            clearmsg_div();
            addmsg_div('<p style="color:Red">Please select a task with resources assigned to it</p>');
        }
        function MoveResource() {
            // alert(getQuerystring('project'));
            ProjectTasks.ProjectTask_MoveResourceByTask_srv(getQuerystring('project'), $('#<%=ddlMoveResourceFrom.ClientID %> OPTION:selected').val(), $('#<%=ddlMoveResourceTo.ClientID %> OPTION:selected').val(), MoveResource_success, MoveResource_fail);
            return false;
        }
        function clearmsg_div() {
            $('#StatusMsg').empty();
        }
        function MoveResource_success() {
            clearmsg_div();

            addmsg_div('<p>Resource(s) moved successfully</p>');
        }
        function addmsg_div(msg) {
            $('#StatusMsg').show();
            $('#StatusMsg').append(msg);
            $('#StatusMsg').delay(3000).fadeOut("slow");
        }

        function Onsuccess_copy(result) {

        }

        function Onsuccess_move(result) {

        }
        function MoveResource_fail() {
            clearmsg_div();
            addmsg_div('<p style="color:Red">Please select a task with resources assigned to it</p>');
        }

        function delayer() {
            window.location.reload();
        }

        function test() {
            $find('model_search_edit1').hide();
            top.frames['iframeMpp'].location = top.frames['iframeMpp'].location;
            return false;
        }
    </script>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mdlPopUpCheclist" BackgroundCssClass="modalBackground"
        runat="server" PopupControlID="pnlCheckList" TargetControlID="btnShowPopup" CancelControlID="imgChecklistCancel"
        ClientIDMode="Static" />
    <%-- <ajaxToolkit:ModalPopupExtender CancelControlID="imgChecklistCancel" ID="mdlPopUpCheclist" runat="server" BehaviorID="imgShowpopup3"
                 PopupControlID="pnlCheckList" TargetControlID="imgShowpopup3" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>--%>
    <asp:Panel ID="pnlCheckList" runat="server" BackColor="White" Style="display: none"
        Width="583px" Height="480px" BorderStyle="Double" ScrollBars="None" BorderColor="LightSteelBlue">
      
        <div class="form-group">
            <div class="col-md-12">
                                       <label class="col-sm-10 control-label"> <asp:Label ID="Label2" runat="server"><strong> Apply Checkpoint healthcheck</strong></asp:Label></label>
                                      <div class="col-sm-2 ">
                                          <div class="pull-right">
                                           <asp:LinkButton ID="imgChecklistCancel" runat="server" SkinID="BtnLinkClose" /></div>
					</div>
				</div>
        </div>
          <hr class="no-top-margin" />
        <div style="margin-left: 5px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <iframe id="iframe5" height="380px" width="570px" src='<%=RetUrl_Checklist() %>'
                        scrolling="Yes" frameborder="0"></iframe>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <div>
     
        <ajaxToolkit:ModalPopupExtender  ID="mpopBOM" runat="server"
            PopupControlID="pnlBOM" TargetControlID="imgItemEdit" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:ModalPopupExtender CancelControlID="imgTasks" ID="mdlPopTaskDocs" runat="server"
            PopupControlID="pnlTaskDocumnets" TargetControlID="Button2" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
         <ajaxToolkit:ModalPopupExtender CancelControlID="imgRAG" ID="mdlPopRAGStatus" runat="server"
            PopupControlID="pnlRAGStatus" TargetControlID="btnRAG" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
      
        <div>
            <div>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                <asp:Label ID="lblErr" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="project" />
            </div>
            <asp:Panel ID="Panel_fileupdload" runat="server" Width="100%">
                <div class="btn_align_right" style="width: 100%">
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                        TargetControlID="PanelCsv" ExpandControlID="PnlTitle" CollapseControlID="PnlTitle"
                        TextLabelID="Lbl1" CollapsedText="Upload MS Project File " ExpandedText="Upload MS Project File "
                        Collapsed="true" SuppressPostBack="true">
                    </ajaxToolkit:CollapsiblePanelExtender>
                    <asp:Panel Width="100%" ID="PnlTitle1" runat="server" HorizontalAlign="Right">
                        <div id="PnlTitle" runat="server">
                            <asp:Label ID="Lbl1" runat="server" Text="Upload MS Project File " Font-Bold="true"
                                ForeColor="Black" Style="cursor: hand;"></asp:Label>
                            <asp:LinkButton SkinID="BtnLinkUpload" runat="server" ID="UploadImg" /></div>
                    </asp:Panel>
                </div>
                <asp:Panel ID="PanelCsv" runat="server" Width="100%">
                        <iframe id="iframe1" height="200px" width="100%" src='<%=RetUrl() %>' scrolling="no"
                            frameborder="0"></iframe>
                </asp:Panel>
            </asp:Panel>
            <div>
                <uc2:ProjectOverviewCtrl ID="ProjectOverviewCtrl1" runat="server" />
            </div>
            <asp:Panel ID="pnlBOM" runat="server" BackColor="White" Style="display: none" Width="720px"
                Height="400px" BorderStyle="Double" ScrollBars="None" BorderColor="LightSteelBlue">

                <div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title"><asp:Label ID="lblTaskName" runat="server">Move Resources</asp:Label></h3>
				<div class="panel-options">
                     <asp:LinkButton ID="ImageButton1" runat="server" SkinID="BtnLinkClose"  />
                    </div>
            </div>
                    <div class="panel-body">

                        <div class="form-group">
                                  <div class="col-md-5">
                                       <label class="col-sm-4 control-label"> From</label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlMoveResourceFrom" runat="server" Width="150">
                            </asp:DropDownList>
					</div>
				</div>
                            <div class="col-md-5">
                                       <label class="col-sm-4 control-label">  To</label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlMoveResourceTo" runat="server" Width="150">
                            </asp:DropDownList>
					</div>
				</div>
                            <div class="col-md-2">
                                 <asp:Button ID="btnMoveResource" runat="server" SkinID="btnUpdate" 
                                ToolTip="Delete all resources from this task and move them to the task entered into the field"
                                OnClientClick="return MoveResource();" />
                            </div>
</div>
                        <div class="form-group">
                             <div id="StatusMsg">
                </div>
                        </div>
 <div class="form-group">
     <div class="col-md-12">
          <asp:Label ID="Label1" runat="server">Copy Resources</asp:Label>
         <hr />
         </div>

     </div>
                        <div class="form-group">
                            <div class="col-md-5">   <label class="col-sm-4 control-label"> From</label>
                                      <div class="col-sm-8">
                                 <asp:DropDownList ID="ddlCopyResourceFrom" runat="server" Width="150">
                            </asp:DropDownList>
                                          </div>
                             </div>
                             <div class="col-md-5">
                                  <label class="col-sm-4 control-label"> To</label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlCopyResourceTo" runat="server" Width="150">
                            </asp:DropDownList>
                                          </div>
                             </div>
                             <div class="col-md-2">
                                  <asp:Button ID="btnCopyResource" runat="server" SkinID="btnUpdate" 
                                ToolTip="Copy resources from this task to the task entered into the field" OnClientClick="return CopyResource();" />
                             </div>
                            </div>
                    </div>
                    </div>
              
               
                <div style="text-align: left">
                    <asp:Button ID="ImgCancel" runat="server" SkinID="btnCancel" 
                        onclick="ImgCancel_Click"  />
                    <asp:Button runat="server" ID="imgItemEdit" Style="display: none" />
                </div>
            </asp:Panel>
            <div>
              
                <div>
                    <asp:Panel ID="pnlTaskDocumnets" runat="server" BackColor="White" Style="display: none"
                        Width="680px" Height="520px" BorderStyle="Double" ScrollBars="None" BorderColor="LightSteelBlue">
                        <div class="form-group">
        <div class="col-md-10">
           <strong>Task Documents</strong> 
            <hr class="no-top-margin" />
            </div>

<div class="col-md-2">
    <asp:LinkButton ID="imgTasks" runat="server" SkinID="BtnLinkClose" />
</div>
    </div>
                           <asp:Button runat="server" ID="Button2" Style="display: none" />
                                    <iframe id="iframe2" height="380px" width="640px" src='<%=RetUrl_Task() %>'
                                         frameborder="0" scrolling="no" style="background-color:white;" ></iframe>
                        <div id="Div2">
                        </div>
                    </asp:Panel>
                </div>
                   <div>
                    <asp:Panel ID="pnlRAGStatus" runat="server" BackColor="White" Style="display: none"
                        Width="600px" Height="450px" BorderStyle="Double" ScrollBars="None" BorderColor="LightSteelBlue">
                        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-10 control-label"><asp:Label ID="lblRAGStatusConfig" runat="server"><strong> RAG Status Configuration</strong></asp:Label></label>
                                      <div class="col-sm-2"> <div class="pull-right"> <asp:LinkButton ID="imgRAG" runat="server" SkinID="BtnLinkClose" /></div>
					</div>
				</div>
                            </div>
                        <hr class="no-top-margin" />
                       <asp:Button runat="server" ID="btnRAG" Style="display: none" />
						<%-- <iframe id="iframeRAG" height="300px" width="570px" src='<%=RetUrl_RAGStatus() %>' scrolling="no"
                                frameborder="0"></iframe>--%>
                    </asp:Panel>
                </div>
            </div>
            <asp:Panel ID="pnluserbuttons" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-12">
                             <asp:LinkButton ID="lnkMoveResources" runat="server" OnClick="lnkMoveResources_Click" ><i class="fa-arrows"></i> Move Resources</asp:LinkButton>&nbsp
                       <asp:LinkButton ID="lnkTaskDocs" runat="server" OnClick="lnkTaskDocs_Click"  ToolTip="Task Documents"><i class="fa-file"></i> Documents

                       </asp:LinkButton>&nbsp
                                 <asp:LinkButton ID="btn_edit_search" runat="server" Text="Resource Search"
                                      OnClick="btn_edit_search_Click"  ToolTip="Resource Search"><i class="fa-search"></i> Resource Search</asp:LinkButton>&nbsp
                                 <asp:LinkButton ID="lnk_checklist" runat="server" Text="Apply Checkpoint Healthcheck" Visible="false"
                                    OnClick="lnk_checklist_Click" CausesValidation="false"  ToolTip="Apply Checkpoint Healthcheck"><i class="fa-list"></i> Apply Checkpoint</asp:LinkButton>&nbsp
                                     <asp:LinkButton ID="lnk_RAGConfig" runat="server" Text="RAG Status configuration"
                                     CausesValidation="false" onclick="lnk_RAGConfig_Click" ToolTip="RAG Status Configurator" Visible="false">
                                         <i class="fa-list"></i> RAG Configurator</asp:LinkButton>
                              <a  href="ProjectTaskJournal.aspx?project=<%=RetPID() %>"><i class="fa-history"></i> Task Journal </a>&nbsp;
                             <a  href="ProjectTaskClash.aspx?project=<%=RetPID() %>"><i class="fa-history"></i> Task Clash</a> &nbsp;
                         <a  onclick="hidechatdiv()" id="link_chatter"><i class="fa-wechat"></i> Chatter </a>
                            </div>
                       
                         <hr />
                    </div>
                </div>
                 <ajaxToolkit:ModalPopupExtender ID="model_search_edit" runat="server" PopupControlID="PanelResource"
                                    TargetControlID="btnResourceSearch" BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
            </asp:Panel>
            <asp:Panel ID="PanelCsv1" runat="server" Width="100%" Style="overflow: hidden">
                <div id="div_chart" style="width: 100%; float: left; margin-left: 4px;">
                    <iframe id="iframeMpp" height="900px" width="99%" src='<%=RetUrl1() %>' scrolling="no"
                        frameborder="1" style="border:medium solid #5fa2dd;border-width:1px"></iframe>
                </div>

              


                <div id="div_chat" style="display: none; vertical-align: top;">
                    <div class="row">
                            <div class="col-md-12">
                                  <iframe id="iframeChitchat" height="900px" width="99%" src='<%=ChitChatUrl() %>' scrolling="no"
                                                              frameborder="1" style="border:medium solid silver;border-width:1px;"></iframe>
                            </div>
                    </div>
                    <div runat="server" id="divChatControl">
                        <%--<uc3:ChitChatCtrlHome ID="ChitChatCtrlHome1" runat="server" />--%>
                    </div>
                </div>

                    </asp:Panel>
          
            <asp:Button ID="btnResourceSearch" runat="server" Style="display: none" />
            <asp:Panel ID="PanelResource" runat="server" Style="display: none" Width="800px"
                Height="480px" BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue">
                <div class="form-group">
<div class="col-md-12">

    <asp:UpdatePanel ID="upIframe" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <iframe id="iframe3" height="440px" width="100%" src='<%=RetUrl_Resource() %>' scrolling="auto"
                            frameborder="0"></iframe>
                    </ContentTemplate>
                </asp:UpdatePanel>

</div>
</div>
                <div class="form-group">
<div class="col-md-12">
      <div class="pull-right">
             
     <asp:Button ID="OkButton2" runat="server" Text="OK" Visible="false" />
                    <asp:Button ID="imgResourceCancel" runat="server" SkinID="btnCancel" Visible="false" />
                    <asp:LinkButton ID="btnClose" runat="server" Text="Close" Font-Bold="true" Visible="false"></asp:LinkButton>
                    <asp:LinkButton ID="btnCloseRes" runat="server" OnClientClick="test();" Text="Close"
                        Font-Bold="true" ></asp:LinkButton>
    </div>   </div>
                    </div>
                <br />
            </asp:Panel>
           
        </div>
    </div>
      <style>
          .ForChitchat {
              margin-bottom: 0px;
          }
      </style>
</asp:Content>

