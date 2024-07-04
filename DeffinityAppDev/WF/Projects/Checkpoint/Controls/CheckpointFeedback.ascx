<%@ Control Language="C#"  AutoEventWireup="true" Inherits="controls_CheckpointFeedback" Codebehind="CheckpointFeedback.ascx.cs" %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/charts") %>
<link href="../Checkpoint/Ratings.css" rel="stylesheet" type="text/css" />

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>--%>
            <div class="form-group">
                    <div class="col-md-12">
                        <strong>Use the following section to give the feedback on the Resources  </strong> 
                         <hr class="no-top-margin" />
                    </div>
            </div>
            <div class="form-group">
                    <div class="col-md-12">
                        <asp:Label id="lblmsg" runat="server" BackColor="White" ForeColor="Green" Visible="False">Feedback saved</asp:Label>
                    </div>
             </div>
            <div class="form-group">
                   <div class="col-md-6">
                         <div class="form-group">
                             <label class="col-sm-3 control-label">Resource</label>
                             <div class="col-sm-9">                          
                                    <asp:DropDownList id="ddlResource" runat="server" ValidationGroup="Group1" SkinID="ddl_70" ClientIDMode="Static"
                                                OnSelectedIndexChanged="ddlResource_SelectedIndexChanged" DataValueField="ID" DataTextField="ContractorName"
                                                DataSourceID="SqlDataSource1" AutoPostBack="True" ></asp:DropDownList>
                                 <asp:RequiredFieldValidator id="R5" runat="server"
                                          ValidationGroup="Group1"  InitialValue="0" ErrorMessage="Select Resource" Display="Dynamic"
                                                                           ControlToValidate="ddlResource"></asp:RequiredFieldValidator>
                             </div>
                         </div>
                         <div class="form-group">
                             <label class="col-sm-3 control-label">Timeliness</label>
                             <div class="col-sm-9">                        
                                  <ajaxToolkit:Rating id="RatingTime" runat="server" WaitingStarCssClass="Saved"
                                                   StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty"
                                                   CurrentRating="0" CssClass="ratingStar"></ajaxToolkit:Rating> 
                             </div>
                         </div>
                         <div class="form-group">
                             <label class="col-sm-3 control-label">Quality of Workmanship</label>
                             <div class="col-sm-9">                          
                                   <ajaxToolkit:Rating id="RatingQuality" runat="server"  WaitingStarCssClass="Saved"
                                                             StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled"
                                                             EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar"></ajaxToolkit:Rating> 
                             </div>
                         </div>
                         <div class="form-group">
                             <label class="col-sm-3 control-label">Value for Money</label>
                             <div class="col-sm-9">                          
                                   <ajaxToolkit:Rating id="RatingMoney" runat="server"  WaitingStarCssClass="Saved" StarCssClass="ratingItem"
                                                                               MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty"
                                                                               CurrentRating="0" CssClass="ratingStar"></ajaxToolkit:Rating> 
                             </div>
                         </div>
                         <div class="form-group">
                             <label class="col-sm-3 control-label">Communication</label>
                             <div class="col-sm-9">
                                   <ajaxToolkit:Rating id="RatingCommunication" runat="server"  WaitingStarCssClass="Saved" 
                                       StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar"></ajaxToolkit:Rating>
                             </div>
                         </div>
                         <div class="form-group">
                              <label class="col-sm-3 control-label"></label>
                             <div class="col-sm-9" style="float:right;">
                                 <asp:Button id="btnSubmitHours" onclick="btnSubmitHours_Click" runat="server" ValidationGroup="Group1"  SkinID="btnSubmit"></asp:Button>
                             </div>
                        </div>
                   </div>
                   <div class="col-md-6">
                       <div id="DivRatingsCharts"></div>
                       <div>
                           <asp:SqlDataSource id="SqlDataSource1" runat="server" SelectCommandType="StoredProcedure" SelectCommand="DEFFINITY_GETCONTRACTORS2P"
                                ConnectionString="<%$ ConnectionStrings:DBstring %>">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="0" Name="Project" QueryStringField="Project"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                       </div>
                   </div>
            </div>
		<%--</ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlResource" />
    </Triggers>
</asp:UpdatePanel>
                                                        
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
           <asp:Label runat="server" SkinID="Loading"></asp:Label>
    </ProgressTemplate>
</asp:UpdateProgress>--%>

