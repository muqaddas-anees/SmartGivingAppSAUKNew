<%@ Page Language="C#" AutoEventWireup="true" Inherits="Training_viewcoursefeedback" Title="View course feedback " Codebehind="viewcoursefeedback.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
      <link href="http://www.deffinity.com/dlite/media//favicon1.ico" rel="shortcut icon" />
<link rel="stylesheet" type="text/css" href="../stylcss/deffinity_frame.css" />
<link rel="stylesheet" type="text/css" href="../stylcss/deffinity_color_scheme.css" />
<link rel="stylesheet" type="text/css" href="../stylcss/deffinity_custom.css" />
<link rel="stylesheet" type="text/css" href="../stylcss/ajaxtabs.css" />
    <style type="text/css">
        .fontSubHeader
        {
            font-weight:bold;
            font-size:15px;
            font-family:Arial;
        }
        .tblCell
        {
           
            font-size:15px;
            font-family:Arial; 
        }
        .style2
        {
            width: 403px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" > 
<Services>

<asp:ServiceReference Path="~/ResourcePlanner_Service.asmx"  />
<%--<asp:ServiceReference Path="~/ProjectTasks.asmx"  />--%>
<asp:ServiceReference Path="~/CustomerSCAutoComplete.asmx" />
</Services>
<Scripts>
<asp:ScriptReference ScriptMode="Release" Path="~/js/Dynamic_styles.js" />
</Scripts>
</asp:ScriptManager>
    <div class="tblCell">
    <table align="center"><tr><td>
        
        <table align="center" class="data_carrier" width="98%" cellpadding="0" cellspacing="0" border="0">
 <tr>    
      <td>
          <h1 class="section2">
              <span>
                  <label id="lblTitle" runat="server">
                  </label>
              </span>
          </h1>
          
      </td>
  </tr>
        
            <tr><td class="p_section2 data_carrier_block" valign="top">
  <div class="tab_subheader" style="width:98%">
                
                    Pharmacy Training Course Feedback Form</div>
                    <div>
                    </div>
                    </td>
            </tr>
            
      
            <tr >
                <td class="p_section2 data_carrier_block" valign="top">
                    <table width="100%">
                      
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style2">
                                <table class="space_r20" width="100%">
                                    <tr>
                                        <td>
                                            Name:</td>
                                        <td>
                                          
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True" ></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Job title:</td>
                                        <td>
                                            <asp:Label ID="lblJobTitle" runat="server" Font-Bold="True" ></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Course title:</td>
                                        <td>
                                            <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date(s) of attendance:</td>
                                        <td>
                                            <asp:Label ID="lblDate" runat="server" Font-Bold="True"></asp:Label>
                                           </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Course organised by:</td>
                                        <td>
                                            <asp:Label ID="lblOrganisedBy" runat="server" Font-Bold="True"></asp:Label>
                                           </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                      
                        
                        <tr>
                            <td>
                                </td>
                            <td>
                                <p align="left">This feedback form is designed to help you plan how you will 
                                put the skills and knowledge you have gained
                                from this course into practice and to evaluate usefullness, Please keep a copy 
                                of this for your own&nbsp; use and as basis for diccuss with your manager.Hand 
                                over a copy of the completed form to the Training Manager.<br /></p>
                            </td>
                            <td>
                                </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td align="right">
                                1.</td>
                            <td>
                                What were your objectives for attending the course?</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                                <td>&nbsp;</td>
                            <td  colspan="4">
                                <asp:Label ID="lblObjectives" runat="server" Font-Bold="True"></asp:Label>
                               </td>
                        </tr>
                        <tr>
                            <td align="right">
                                2.</td>
                            <td>
                                How useful was this course to you?</td>
                            <td>
                                </td>
                            <td>
                                &nbsp;</td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                              <td></td>
                           
                            <td colspan="4">
                                <asp:Label ID="lblUseful" runat="server" Font-Bold="True"></asp:Label>
                               </td>
                        </tr>
                        <tr>
                            <td align="right">
                                3.</td>
                            <td colspan="1">
                                What were your main learning points?</td>
                            <td>
                               </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                        <td></td>
                            <td align="left" colspan="4">
                               <asp:Label ID="lblLearningPoints" runat="server" Font-Bold="True"></asp:Label>
                               </td>
                        </tr>
                        <tr>
                            <td>
                                4</td>
                            <td>
                                Would you recommend this course to your colleagues?</td>
                            <td>
                                </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                                <td>
                               </td>
                            <td colspan="4">
                               <asp:Label ID="lblRecommend" runat="server" Font-Bold="True"></asp:Label>
                               </td>
                        </tr>
                        <tr>
                            <td>
                                5</td>
                            <td>
                               
                                How will you achieve your objects?</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                                <td><table>
                                    <tr>
                                        <td>
                             <asp:Label ID="lblObjects" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;</td>
                            <td>
                                Set out any actions points you have for practising more effectively,relating to 
                                your own personal development or an
                                aspect of service development.If an action plan has been completed during the&nbsp; 
                                course,photocopy and attach it to
                                this form.</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;</td>
                            <td align="center">
                                <asp:GridView ID="grActionPlan" runat="server" AutoGenerateColumns="False" 
                                    Width="568px" >
                                <Columns>
                              <%--  <asp:TemplateField>
                                <ItemTemplate>
                                     <asp:ImageButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit" 
                CommandArgument="<%#Bind('ID')%>"  ToolTip="Edit" ImageUrl="~/media/ico_edit.png" />
                                </ItemTemplate>
                                 <EditItemTemplate>
             
                  <asp:ImageButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                    CommandArgument="<%# Bind('ID')%>"  ImageUrl="~/media/ico_update.png" 
                                              ToolTip="Update" CausesValidation="true" ValidationGroup="Group2"></asp:ImageButton>
                   <asp:ImageButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                     ImageUrl="~/media/ico_cancel.png" ToolTip="Cancel"></asp:ImageButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                       <asp:ImageButton ID="LinkButtonAdd" runat="server" CausesValidation="true" ValidationGroup="Group2" CommandName="AddNew" 
                CommandArgument="<%#Bind('ID')%>" ToolTip="Edit" ImageUrl="~/media/btn_add_new.gif" />
                 <asp:ImageButton ID="LinkButtonAddNew" runat="server" CommandName="AddNew" 
                    CommandArgument="<%# Bind('ID')%>"  ImageUrl="~/media/ico_update.png" 
                                     ToolTip="AddNew" CausesValidation="true" ValidationGroup="Group3"></asp:ImageButton>
                              <asp:ImageButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" CommandName="Clear"
                     ImageUrl="~/media/ico_cancel.png" ToolTip="Cancel"></asp:ImageButton>
                                        </FooterTemplate>
                                </asp:TemplateField>--%>
                                
                               <asp:TemplateField HeaderText="Action Plan">
                               <ItemTemplate>
                                 <asp:Label ID="lblDateOfCourse" runat="server" Text='<%#Bind("ActionPlan") %>' Width="75px"></asp:Label>
                                 </ItemTemplate>
                                <%-- <EditItemTemplate>
                                     <asp:TextBox ID="txtAction" runat="server" Width="150px" Text='<%#Bind("ActionPlan") %>'></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RV1" runat="server" ErrorMessage="Please enter action plan"
                ControlToValidate="txtAction" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                 </EditItemTemplate>--%>
                                <%-- <FooterTemplate>
                                 <asp:TextBox ID="txtActionFooter" runat="server" Width="150px" ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RV7" runat="server" ErrorMessage="Please enter action plan"
                ControlToValidate="txtActionFooter" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                 </FooterTemplate>--%>
                               </asp:TemplateField>
                               
                                  
                               <asp:TemplateField HeaderText="Support required" >
                               <ItemTemplate>
                                 <asp:Label ID="lblSupport" runat="server" Text='<%#Bind("SupportRequired") %>' Width="75px"></asp:Label>
                                 </ItemTemplate>
                                <%-- <EditItemTemplate>
                                     <asp:TextBox ID="txtSupport" runat="server" Width="150px" Text='<%#Bind("SupportRequired") %>'></asp:TextBox>
                                                                       <asp:RequiredFieldValidator ID="RV4" runat="server" ErrorMessage="Please enter support required"
                ControlToValidate="txtSupport" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                 </EditItemTemplate>--%>
                                <%-- <FooterTemplate>
                                 <asp:TextBox ID="txtSupportFooter" runat="server" Width="150px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RV8" runat="server" ErrorMessage="Please enter support required"
                ControlToValidate="txtSupportFooter" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                 </FooterTemplate>--%>
                               </asp:TemplateField>
                               
                                    
                               <asp:TemplateField HeaderText="Timescale" >
                               <ItemTemplate>
                                 <asp:Label ID="lblTimeScale" runat="server" Text='<%#Bind("Timescale") %>' Width="75px"></asp:Label>
                                 </ItemTemplate>
                               <%--  <EditItemTemplate>
                                     <asp:TextBox ID="txtTimeScale" runat="server" Width="150px" Text='<%#Bind("Timescale") %>'></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RV3" runat="server" ErrorMessage="Please enter Timescale"
                ControlToValidate="txtTimeScale" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                 </EditItemTemplate>--%>
                                <%-- <FooterTemplate>
                                 <asp:TextBox ID="txtTimeScaleFooter" runat="server" Width="150px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RV9" runat="server" ErrorMessage="Please enter timescale"
                ControlToValidate="txtTimeScaleFooter" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                 </FooterTemplate>--%>
                               </asp:TemplateField>
                               
                                </Columns>
                                </asp:GridView>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                        <td colspan="3">
                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="center">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
    </td></tr></table>
    
    </div>
    </form>
</body>
</html>
