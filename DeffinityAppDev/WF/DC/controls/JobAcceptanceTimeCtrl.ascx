<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobAcceptanceTimeCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.JobAcceptanceTimeCtrl" %>
<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong><asp:Label ID="lblHeader" runat="server" Text="Job Acceptance Time"></asp:Label></strong>
            <hr class="no-top-margin" />
            </div>
</div>
<asp:UpdateProgress ID="uprogressJobAcceptanceTime" runat="server" AssociatedUpdatePanelID="upnlJobAcceptanceTime">
    <ProgressTemplate>
        <asp:Label SkinID="Loading" runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="upnlJobAcceptanceTime" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
<div class="form-group row">
    <asp:Label ID="lblsuccess" SkinID="GreenBackcolor" EnableViewState="false" runat="server"></asp:Label>
    <asp:Label ID="lblerror" SkinID="RedBackcolor" EnableViewState="false" runat="server"></asp:Label>
    
    </div>

<div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-5 control-label"><asp:Label ID="lblFirstReminderTime" runat="server" Text="1st Reminder to Schedule The Job"></asp:Label></label>
                                      <div class="col-sm-7 form-inline"><asp:TextBox ID="txtFirstReminderTime" runat="server" MaxLength="6" SkinID="Price_100px"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="txtFiltertxtFirstReminderTime" runat="server" ValidChars="0123456789:" TargetControlID="txtFirstReminderTime"></ajaxToolkit:FilteredTextBoxExtender>
					</div>
				</div>
                </div>
                                        <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-5 control-label"><asp:Label ID="lblSecondReminderTime" runat="server" Text="2nd Reminder to Schedule The Job"></asp:Label></label>
                                      <div class="col-sm-7 form-inline"><asp:TextBox ID="txtSecondReminderTime" runat="server" MaxLength="6" SkinID="Price_100px"></asp:TextBox>
                                          <ajaxToolkit:FilteredTextBoxExtender ID="FiltertxtSecondReminderTime" runat="server" ValidChars="0123456789:" TargetControlID="txtSecondReminderTime"></ajaxToolkit:FilteredTextBoxExtender>
					</div>
				</div>
                </div>
                                        <div class="form-group row">
             <div class="col-md-12">
                                       <label class="col-sm-5 control-label"><asp:Label ID="lblBackToPoolTime" runat="server" Text="Time to Schedule the Job Before It Goes Back To The Pool"></asp:Label></label>
                                      <div class="col-sm-7 form-inline"><asp:TextBox ID="txtBackToPoolTime" runat="server" MaxLength="6" SkinID="Price_100px"></asp:TextBox>
                                           <ajaxToolkit:FilteredTextBoxExtender ID="FiltertxtBackToPoolTime" runat="server" ValidChars="0123456789:" TargetControlID="txtBackToPoolTime"></ajaxToolkit:FilteredTextBoxExtender>
					</div>
				</div>
                </div>
<div class="form-group row">
             <div class="col-md-12">
                 <label class="col-sm-5 control-label"></label>
                  <div class="col-sm-7 form-inline">
                     
        <asp:LinkButton ID="imgbtnupdatetime" runat="server" SkinID="btnUpdate" OnClick="imgbtnupdatetime_Click" />
        
                  </div>

                 </div>
    </div>
                                        </ContentTemplate>
    </asp:UpdatePanel>
