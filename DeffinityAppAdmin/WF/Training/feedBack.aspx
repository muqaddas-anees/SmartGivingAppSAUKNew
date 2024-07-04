<%@ Page Language="C#" AutoEventWireup="true" Inherits="Training_trfeedBack" MaintainScrollPositionOnPostback="true" Codebehind="feedBack.aspx.cs" %>

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
        .style3
        {
            width: 100%;
        }
        .style6
        {
            font-weight: bold;
            font-size: 15px;
            font-family: Arial;
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
       
    <asp:UpdatePanel ID="updatePanel" runat="server">
    <ContentTemplate>
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
                
                    General Course Evaluation Form</div>
                    <div><asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1"  /></div>
                    </td>
            </tr>
            
      
            <tr >
                <td class="p_section2 data_carrier_block" valign="top">
                    <table width="50%">
                      
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style6">
                                Programme Details:</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                      
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style2">
                                <table class="style3">
                                    <tr>
                                        <td class="tblCell">
                                            Programme title:</td>
                                        <td>
                                            <asp:TextBox ID="txtProgrammTitle" runat="server" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter programme title"
                                             Display="None" ControlToValidate="txtProgrammTitle" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Facilitators name:</td>
                                        <td>
                                            <asp:TextBox ID="txtFacilitators" runat="server" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter Facilitators name"
                                             Display="None" ControlToValidate="txtFacilitators" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtDate" runat="server" Width="150px"></asp:TextBox>
                                       
                                            <asp:Image ID="imgDateIcon" runat="server" SkinID="Calender" 
                                                />
                                            <ajaxToolkit:CalendarExtender CssClass="MyCalendar" runat="server"
                                            id="date" TargetControlID="txtDate" PopupButtonID="imgDateIcon" ></ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ValidationGroup="Group1" ErrorMessage="Please enter date" Display="None"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid date" ControlToValidate="txtDate"
                                         Display="None" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1"></asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Location:</td>
                                        <td>
                                            <asp:TextBox ID="txtLocation" runat="server" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter location"
                                             Display="None" ControlToValidate="txtLocation" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            </td>
                                        <td>
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
                        </tr>
                      
                        <tr>
                            <td>
                               </td>
                            <td class="style6">
                                Personal Details:</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                      
                        <tr>
                            <td>
                               </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            Name:</td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter name"
                                             Display="None" ControlToValidate="txtName" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                           </td>
                                        <td>
                                           </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Job title:</td>
                                        <td>
                                            <asp:TextBox ID="txtJobTitle" runat="server" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please enter job title"
                                             Display="None" ControlToValidate="txtJobTitle" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            Department:</td>
                                        <td>
                                            <asp:TextBox ID="txtDepartment" runat="server" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please enter department"
                                             Display="None" ControlToValidate="txtDepartment" ValidationGroup="Group1" ></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                               </td>
                            <td>
                                </td>
                        </tr>
                      
                        <tr>
                            <td>
                                </td>
                            <td>
                                Programme evaluation:</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td align="right">
                                1.</td>
                            <td class="style2">
                                What part of the programme did you find most useful?</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                                <td>&nbsp;</td>
                            <td  colspan="3">
                                <asp:TextBox ID="txtMostUseful" runat="server" Width="795px" Height="95px" 
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
                                What part of the programme did you find least useful?</td>
                            <td>
                                </td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                              <td></td>
                           
                            <td colspan="3">
                                <asp:TextBox ID="txtLeastUseful" runat="server" Width="795px" Height="95px" 
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
                                What would you like to see added to the programme so it can meet your needs?</td>
                            <td>
                               </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                        <td></td>
                            <td align="left" colspan="4">
                                <asp:TextBox ID="txtAddNeeds3" runat="server" Width="795px" Height="95px" 
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
                            <td align="right">
                                4.</td>
                            <td class="style2">
                                How would you rate the programme overall?</td>
                            <td>
                                </td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                            <td>
                               </td>
                            <td colspan="3">
                          <table>
                                    <tr>
                                        <td>
                                            Poor</td>
                                        <td>
                                           <ajaxToolkit:Rating id="RatingPerformance" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="6" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
                        </ajaxToolkit:Rating></td>
                                        <td>
                                            Excellent</td>
                                    </tr>
                                </table>
 </td>
                           
                            <td>
                                </td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td>
                                Please state fully why you have given the above ratings?</td>
                            <td>
                                </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                                <td>
                               </td>
                            <td colspan="4">
                                <asp:TextBox ID="txtRateComments" runat="server" Width="795px" Height="95px" 
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
                                </td>
                            <td class="style6">
                                Trainer evaluation:</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td align="right">
                                1.</td>
                            <td>
                                Please rate the trainer on the following aspects:</td>
                            <td>
                              </td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                            <td>
                             
                                </td>
                            <td>
                             
                                
                                <table>
                                    <tr>
                                        <td width="250px">
                                             Knowledge of the subject</td>
                                        <td align="right">
                                           <ajaxToolkit:Rating id="RatingKnowledge" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
                        </ajaxToolkit:Rating></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                               </td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                            <td>
                               </td>
                            <td>
                                  
                                   <table>
                                       <tr>
                                           <td width="250px">
                                              Obvious preparation</td>
                                           <td align="right">
                                              <ajaxToolkit:Rating id="RatingObviousPrep" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar" >
        
                        </ajaxToolkit:Rating></td>
                                       </tr>
                                   </table>
                            </td>
                            <td>
                              </td>
                            <td>
                              </td>
                        </tr>
                        <tr>
                            <td>
                               </td>
                            <td>
                              
                                <table>
                                    <tr>
                                        <td width="250px">
                                            Style and delivery</td>
                                        <td align="right">
                                            <ajaxToolkit:Rating id="RatingStyle" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
                        </ajaxToolkit:Rating> </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                               </td>
                            <td>
                             </td>
                        </tr>
                        <tr>
                            <td>
                               </td>
                            <td>
                                
                                <table>
                                    <tr>
                                        <td width="250px">
                                            Responsiveness to group</td>
                                        <td align="right" width="250px">
                                        <ajaxToolkit:Rating id="RatingResponsiveness" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
                        </ajaxToolkit:Rating></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                               </td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td>
                               
                                <table>
                                    <tr>
                                        <td width="250px">
                                         Producing a good learning climate</td>
                                        <td align="right">
                                            <ajaxToolkit:Rating id="RatingLrnClimate" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
                        </ajaxToolkit:Rating></td>
                                    </tr>
                                </table>
                            </td>
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
                                           Any other&nbsp; comments:</td>
                                        <td>
                                <asp:TextBox ID="txtAnyOtherComments" runat="server" Width="717px" Height="95px" 
                                    TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                2.</td>
                            <td class="style2">
                                How did you feel about the length of the programme?</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style2">
                                <asp:RadioButtonList ID="radLength" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Too short</asp:ListItem>
                                    <asp:ListItem Value="2">Just right</asp:ListItem>
                                    <asp:ListItem Value="3">Too long</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                3.</td>
                            <td class="style2">
                                How did you feel about the pacing of the programme?</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style2">
                                <asp:RadioButtonList ID="radPacing" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Too fast</asp:ListItem>
                                    <asp:ListItem Value="2">Just right</asp:ListItem>
                                    <asp:ListItem Value="3">Too slow</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;</td>
                            <td class="style6">
                                Post -training:</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                1.</td>
                            <td class="style2">
                                How will you apply what you have learned from the programme into practice?</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style2">
                                <asp:TextBox ID="txtPractice" runat="server" Width="795px" Height="95px" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                2.</td>
                            <td class="style2">
                                What support do you need from the department to help you apply your learning&#39;s 
                                into practice?</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style2">
                                <asp:TextBox ID="txtSupport" runat="server" Width="795px" Height="95px" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                        <td colspan="3">
                        <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>
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
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>
    </td><td style="width:50%"></td></tr></table>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ImageButton1" />
    </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
