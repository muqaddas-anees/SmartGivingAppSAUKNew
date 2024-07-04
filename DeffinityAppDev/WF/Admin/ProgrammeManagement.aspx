<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProgrammeManagement" Title="Programme Management"  Codebehind="ProgrammeManagement.aspx.cs" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.2" Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.2"  Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register src="~/WF/Admin/controls/ProgrammeManagement.ascx" tagname="ProgrammeManagement" tagprefix="uc1" %>
<%@ Register src="controls/ProgramTitle.ascx" tagname="ProgramName" tagprefix="uc2" %>
<%--<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.2" Namespace="Infragistics.WebUI.UltraWebNavigator"
    TagPrefix="ignav" %>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProgrammeManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
       <%= Resources.DeffinityRes.AddEditProgramme%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ProgrammeManagement ID="ProgrammeManagement1" runat="server" />
   
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>    
            
       
    </ProgressTemplate>
    </asp:UpdateProgress>

     <div class="row">
                                <div class="col-md-3"> 
                                    <ignav:UltraWebTree ID="UltraWebTree1" runat="server" Height="500px" LoadOnDemand="Manual"
                CompactRendering="true" Visible="true" OnNodeClicked="UltraWebTree1_NodeClicked"
                WebTreeTarget="ClassicTree" Font-Names="Verdana" Font-Size="8pt" BorderWidth="1"
                BorderStyle="Solid" 
                onnodeselectionchanged="UltraWebTree1_NodeSelectionChanged">
                <SelectedNodeStyle Cursor="Hand" ForeColor="White" BackColor="Gray" CssClass="SelectClass" >
                </SelectedNodeStyle>
                <Images>
                </Images>
                <Padding Top="0px"></Padding>
                <NodeMargins Top="0px"></NodeMargins>
                <NodeStyle ForeColor="Black" />
                <Styles>
                    <ignav:Style Cursor="Hand" ForeColor="Black" BackColor="OldLace" CssClass="HiliteClass">
                    </ignav:Style>
                    <ignav:Style BorderWidth="0px" BorderColor="DarkGray" BorderStyle="Solid" BackColor="Gainsboro"
                        CssClass="Hover">
                        <Padding Top="0px"></Padding>
                    </ignav:Style>
                    <ignav:Style ForeColor="White" BackColor="#333333" CssClass="SelectClass">
                        <Padding Top="0px"></Padding>
                    </ignav:Style>
                </Styles>
            </ignav:UltraWebTree>
                                    </div>
                                  <div class="col-md-9">
                                       <asp:DropDownList ID="DropDownListCustomer" runat="server" Width="250px" CausesValidation="false" AutoPostBack="True"
            OnSelectedIndexChanged="DropDownListCustomer_SelectedIndexChanged" 
                Visible="False">
        </asp:DropDownList>
                                        <asp:UpdatePanel ID="UpdatePanelDdl" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group">
                                  <div class="col-md-12">
                                      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GroupName">
                                        </asp:ValidationSummary>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="GroupName1">
                                        </asp:ValidationSummary>
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="GroupName2">
                                        </asp:ValidationSummary>
                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="GroupName5">
                                        </asp:ValidationSummary>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                            ErrorMessage="<%$ Resources:DeffinityRes,PlsEntervalidemailid%>" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                      
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="GroupName"
                                            ControlToValidate="ddlprogrammowner" ErrorMessage="<%$ Resources:DeffinityRes,SelectOwner%>"
                                            Display="None" InitialValue="Please select..."></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                            ControlToValidate="DropDownListCustomer" ErrorMessage="<%$ Resources:DeffinityRes,PlsSelectPortfolio%>"
                                            Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                      <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                      </div>
                            </div>
                         <div class="form-group">
                              <div class="col-md-12">
                                    <div id="tdProgramme" runat="server" class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.Programme%>
                                    </div>
                                    <div class="col-sm-9 form-inline">
                                         <asp:Panel ID="panleDdl" runat="server" CssClass="form-inline">
                                                        <asp:TextBox ID="txtGroups" runat="server" SkinID="txt_70" Visible="false" >
                                                            </asp:TextBox>
                                                            <asp:DropDownList ID="ddlGroups" runat="server" SkinID="ddl_60" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlGroups_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:Button ID="btnAddNew" OnClick="btnAddNew_Click1" runat="server" SkinID="btnAddNew"
                                                                CausesValidation="False"></asp:Button>
                                                        <asp:Button ID="btnDelete" OnClick="btnDelete_Click1" OnClientClick="return confirm('Do you want to delete the programme?');" runat="server"  SkinID="btnDefault" Text="Delete"
                                                                CausesValidation="False"></asp:Button>
                                                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Visible="false" CausesValidation="False"
                                                                AlternateText="<%$ Resources:DeffinityRes,Cancel%>" SkinID="btnCancel"></asp:Button>
                                                        </asp:Panel>
                                        <br />
                                        <asp:CheckBox ID="chkVisible" runat="server" Visible="false" Text="<%$ Resources:DeffinityRes,Visible%>"
                                                            Checked="true"></asp:CheckBox>
                                    </div>
                                    </div>
                                </div>

                           <div id="divLevel" runat="server" class="form-group" >
                               <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.ThisisaSubProgof%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlPgmlevel2"  AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlPgmlevel2_SelectedIndexChanged" runat="server" SkinID="ddl_80">
                                        </asp:DropDownList>
                                    </div>
                                   </div>
                                </div>
                                 <div id="trProgrammesub" runat="server" class="form-group">
                                     <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.SubProgramme%>
                                    </div>
                                    <div class="col-sm-9">
                                    <asp:Panel ID="Panel1" runat="server">
                                    <asp:DropDownList ID="dropdownSubProgramme" runat="server" SkinID="ddl_80" AutoPostBack="True"
                                    OnSelectedIndexChanged="dropdownSubProgramme_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </asp:Panel>
                                    
                                    </div>
                                         </div>
                                </div>
                         <div id="panelEditProgramName" runat="server" visible="false" class="form-group">
                             <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.ProgrammeName%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtEditProgramName" runat="server" MaxLength="100" SkinID="txt_80"></asp:TextBox>
                                        <div id="pnlOriginalImage" runat="server" style="display: none;">
                                            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                                                <HeaderTemplate>
                                                    <table width="350px" border="0" cellspacing="0" cellpadding="10">
                                                        <tr>
                                                            <td class="tab_header_Bold">
                                                                <%= Resources.DeffinityRes.Programme%>
                                                            </td>
                                                            <td class="tab_header_Bold">
                                                                <%= Resources.DeffinityRes.ProgrammeOwner%>
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr style="height: 22px">
                                                        <td style="background-color: LightBlue">
                                                            <%# DataBinder.Eval(Container.DataItem, "operationsowners")%>
                                                        </td>
                                                        <td style="background-color: Azure">
                                                            <%# DataBinder.Eval(Container.DataItem, "Contractorname")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                       
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommandType="StoredProcedure"
                                            ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="DEFFINITY_GETSUBPROGRAMMES">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlGroups" PropertyName="SelectedValue" DefaultValue="0"
                                                    Name="ProgrammeID" Type="Int32"></asp:ControlParameter>
                                                <%--<asp:SessionParameter DefaultValue="0" SessionField="UID" Type="int32" Name="ProgrammeID" />--%>
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                  </div>
                                </div>
                         <div class="form-group">
                         <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.ProgrammeLevel%>
                                    </div>
                                    <div class="col-sm-9 form-inline">
                                        <asp:RadioButton ID="rdbLevel1" runat="server" AutoPostBack="True" Enabled="false" Checked="false"
                                            GroupName="pgmLevel" OnCheckedChanged="rdbLevel1_CheckedChanged"
                                            Text="Level1" />
                                        <asp:RadioButton ID="rdbLevel2" runat="server" AutoPostBack="True" Enabled="false"  Checked="False"
                                             GroupName="pgmLevel" OnCheckedChanged="rdbLevel2_CheckedChanged"
                                            Text="Level2" />
                                    </div>
                                </div>
                             </div>
                         <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.Owner%><span style="color: red">*</span>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlprogrammowner" runat="server" SkinID="ddl_80" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlprogrammowner_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>   
                             </div>
                        <div class="form-group">
                        <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.Email%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtEmail" runat="server" SkinID="txt_80" CssClass="txt_field"></asp:TextBox>
                                    </div>
                                    </div>
                               </div>
                        <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.CostCentre%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtCostcenter" runat="server" SkinID="txt_80"></asp:TextBox>
                                    </div>
                                    </div>
                            </div>
                             <div class="form-group">     
                        <div  class="col-md-12">
                         <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.MaximumBudget%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtMaxBudget" runat="server" ></asp:TextBox>
                                    </div>
                            </div>
                                 </div>
                        <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.Justification%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtJustification" runat="server" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.DesandCapabilities%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtDescription" runat="server" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.BenefitstotheOrg%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtBenefits" runat="server" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.StrategicFitAlignment%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtStratergic" runat="server" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.VisionStatement%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtvision" runat="server" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.RisksandIssues%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtRisk" runat="server" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                        <%= Resources.DeffinityRes.ResourcesRequired%>
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtResources" runat="server" SkinID="txtMulti" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        <div class="form-group">
                                <div class="col-md-12">
                                    <div class="col-sm-3 control-label">
                                       
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" SkinID="btnSubmit"
                                                ValidationGroup="GroupName"></asp:Button>&nbsp;&nbsp;
                                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" SkinID="btnCancel"
                                                CausesValidation="False"></asp:Button>

                                         <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>  
                                           <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>                                      
                                           <asp:HiddenField ID="HiddenField3" runat="server"></asp:HiddenField> 
                                        </div>
                                    </div>
                            </div>
                      
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ddlGroups" />
                        <asp:PostBackTrigger ControlID="btnClear" />
                        <asp:PostBackTrigger ControlID="btnSubmit" /> 




                   </Triggers>
                </asp:UpdatePanel>
             
                    <asp:Button ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" Visible="false"
                        SkinID="btnDefault" Text="Next" />
                                    </div>
                                 </div>
  
    
</asp:Content>

