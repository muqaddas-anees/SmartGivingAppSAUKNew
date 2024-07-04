<%@ Page Language="C#" AutoEventWireup="true" Inherits="Training_feedBack" MaintainScrollPositionOnPostback="true" Title="Course feedback" Codebehind="coursefeedback.aspx.cs" %>

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
 
  <table align="center"><tr><td style="width:50%"></td><td>
        
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
                    <div><asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1"  />
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2"  />
                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Group3"  />
                        <asp:Label ID="lblValidation" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
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
                                <table class="space_r20">
                                    <tr>
                                        <td>
                                            Name:</td>
                                        <td>
                                            <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter name"
                                             Display="None" ControlToValidate="txtUserName" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
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
                                            <asp:TextBox ID="txtJobTitle" runat="server" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter job title"
                                             Display="None" ControlToValidate="txtJobTitle" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
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
                                            <asp:TextBox ID="txtCourseTitle" runat="server" Width="200px"></asp:TextBox>
                                            <asp:DropDownList ID="ddlCourseTitle" runat="server" Width="255px" 
                                                Height="17px" Visible="False">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                ControlToValidate="txtCourseTitle" Display="None" 
                                                ErrorMessage="please enter course title" 
                                                ValidationGroup="Group1"></asp:RequiredFieldValidator>
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
                                            <asp:TextBox ID="txtDate" runat="server" Width="150px"></asp:TextBox>
                                       
                                            <asp:Image ID="imgDateIcon" runat="server" SkinID="Calender" 
                                                />
                                            <ajaxToolkit:CalendarExtender CssClass="MyCalendar" runat="server"
                                            id="date" TargetControlID="txtDate" PopupButtonID="imgDateIcon" ></ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ValidationGroup="Group1" ErrorMessage="Please enter date" Display="None"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid date" ControlToValidate="txtDate"
                                         Display="None" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1"></asp:RegularExpressionValidator>
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
                                            <asp:TextBox ID="txtOrgnisedBy" runat="server" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please enter  course organised"
                                             Display="None" ControlToValidate="txtOrgnisedBy" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
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
                                <p>This feedback form is designed to help you plan how you will 
                                put the skills and knowledge you have gained
                                from this course into practice and to evaluate usefullness, Please keep a copy 
                                of this for your own&nbsp; use and as basis for discussion with your manager.Hand 
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
                            <td class="style2">
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
                                <asp:TextBox ID="txtObjectives" runat="server" Width="795px" Height="95px" 
                                    TextMode="MultiLine"></asp:TextBox>
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
                                <asp:TextBox ID="txtUseful" runat="server" Width="795px" Height="95px" 
                                    TextMode="MultiLine"></asp:TextBox>
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
                            <td align="left" colspan="5">
                                <asp:TextBox ID="txtlrningPoints" runat="server" Width="795px" Height="95px" 
                                    TextMode="MultiLine" ></asp:TextBox>
                               </td>
                            <td>
                               </td>
                            <td>
                               </td>
                            <td>
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
                            <td colspan="5">
                                <asp:TextBox ID="txtcolleagues" runat="server" Width="795px" Height="95px" 
                                    TextMode="MultiLine"></asp:TextBox>
                               </td>
                            <td>
                                </td>
                            <td>
                                </td>
                            <td>
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
                                <asp:TextBox ID="txtachieve" runat="server" Width="795px" Height="95px" 
                                    TextMode="MultiLine"></asp:TextBox>
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
                                your own personal development or an<br />
                                aspect of service development.If an action plan has been completed during the&nbsp; 
                                course,photocopy and attach it to
                                <br />
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
                                    Width="700px" onrowcommand="grActionPlan_RowCommand" 
                                    onrowupdating="grActionPlan_RowUpdating" 
                                    onrowcancelingedit="grActionPlan_RowCancelingEdit" 
                                    onrowediting="grActionPlan_RowEditing">
                                <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
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
                                        <%-- <asp:ImageButton ID="LinkButtonAdd" runat="server" CausesValidation="true" ValidationGroup="Group2" CommandName="AddNew" 
                CommandArgument="<%#Bind('ID')%>" ToolTip="Edit" ImageUrl="~/media/btn_add_new.gif" />--%>
                 <asp:ImageButton ID="LinkButtonAddNew" runat="server" CommandName="AddNew" 
                    CommandArgument="<%# Bind('ID')%>"  ImageUrl="~/media/ico_update.png" 
                                     ToolTip="AddNew" CausesValidation="true" ValidationGroup="Group3"></asp:ImageButton>
                              <asp:ImageButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" CommandName="Clear"
                     ImageUrl="~/media/ico_cancel.png" ToolTip="Cancel"></asp:ImageButton>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                               <asp:TemplateField HeaderText="Action Plan">
                               <ItemTemplate>
                                 <asp:Label ID="lblDateOfCourse" runat="server" Text='<%#Bind("ActionPlan") %>' Width="75px"></asp:Label>
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:TextBox ID="txtAction" runat="server" Width="150px" Text='<%#Bind("ActionPlan") %>'></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RV1" runat="server" ErrorMessage="Please enter action plan"
                ControlToValidate="txtAction" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                 </EditItemTemplate>
                                 <FooterTemplate>
                                 <asp:TextBox ID="txtActionFooter" runat="server" Width="150px" ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RV7" runat="server" ErrorMessage="Please enter action plan"
                ControlToValidate="txtActionFooter" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                 </FooterTemplate>
                               </asp:TemplateField>
                               
                                  
                               <asp:TemplateField HeaderText="Support required" >
                               <ItemTemplate>
                                 <asp:Label ID="lblSupport" runat="server" Text='<%#Bind("SupportRequired") %>' Width="75px"></asp:Label>
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:TextBox ID="txtSupport" runat="server" Width="150px" Text='<%#Bind("SupportRequired") %>'></asp:TextBox>
                                                                       <asp:RequiredFieldValidator ID="RV4" runat="server" ErrorMessage="Please enter support required"
                ControlToValidate="txtSupport" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                 </EditItemTemplate>
                                 <FooterTemplate>
                                 <asp:TextBox ID="txtSupportFooter" runat="server" Width="150px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RV8" runat="server" ErrorMessage="Please enter support required"
                ControlToValidate="txtSupportFooter" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                 </FooterTemplate>
                               </asp:TemplateField>
                               
                                    
                               <asp:TemplateField HeaderText="Timescale" HeaderStyle-CssClass="header_bg_r">
                               <ItemTemplate>
                                 <asp:Label ID="lblTimeScale" runat="server" Text='<%#Bind("Timescale") %>' Width="75px"></asp:Label>
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:TextBox ID="txtTimeScale" runat="server" Width="150px" Text='<%#Bind("Timescale") %>'></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RV3" runat="server" ErrorMessage="Please enter Timescale"
                ControlToValidate="txtTimeScale" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                 </EditItemTemplate>
                                 <FooterTemplate>
                                 <asp:TextBox ID="txtTimeScaleFooter" runat="server" Width="150px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RV9" runat="server" ErrorMessage="Please enter timescale"
                ControlToValidate="txtTimeScaleFooter" Display="None" ValidationGroup="Group3"></asp:RequiredFieldValidator>
                                 </FooterTemplate>
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
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="center">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Middle" 
                                    onclick="ImageButton1_Click" SkinID="ImgSubmit" ValidationGroup="Group1" />
                                <asp:HiddenField ID="trid" runat="server" Value="0" />
                                
                            </td>
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
