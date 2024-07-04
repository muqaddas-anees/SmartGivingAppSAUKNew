<%@ Page Language="C#" AutoEventWireup="true" Inherits="Training_trViewFeedback" Codebehind="trViewFeedback.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
  <table align="center"><tr><td></td><td>
        
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
                   
                    </td>
            </tr>
            
      
            <tr >
                <td class="p_section2 data_carrier_block" valign="top">
                    <table>
                      
                        <tr>
                            <td>
                                </td>
                            <td class="style6">
                                Programme Details:</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                      
                        <tr>
                            <td>
                                </td>
                            <td class="style2">
                                <table class="style3">
                                    <tr>
                                        <td class="tblCell">
                                            Programme title:</td>
                                        <td>
                                            <asp:Label ID="lblProgrammTitle" runat="server" Font-Bold="True" ></asp:Label>
                                        </td>
                                        <td>
                                            </td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Facilitators name:</td>
                                        <td>
                                            <asp:Label ID="lblFacilitatorsName" runat="server" Font-Bold="True" ></asp:Label>
                                        </td>
                                        <td>
                                            Date:</td>
                                        <td>
                                            <asp:Label ID="lblDate" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Location:</td>
                                        <td>
                                            <asp:Label ID="lblLocation" runat="server" Font-Bold="True" ></asp:Label>
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
                                           </td>
                                        <td>
                                            </td>
                                        <td>
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
                            <td class="style6">
                                Personal Details:</td>
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
                                        <td>
                                            Name:</td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True" ></asp:Label>
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
                                            <asp:Label ID="lblJobTitle" runat="server"  Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            Department:</td>
                                        <td>
                                            <asp:Label ID="lblDept" runat="server" Font-Bold="True"></asp:Label>
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
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                                <td></td>
                            <td  colspan="3">
                                <asp:Label ID="lblPeComments1" runat="server" Font-Bold="True"></asp:Label>
                                </td>
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
                                <asp:Label ID="lblPeComments2" runat="server" Font-Bold="True"></asp:Label>
                              </td>
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
                            <td align="left" colspan="3">
                                <asp:Label ID="lblPeComments3" runat="server" Font-Bold="True"></asp:Label>
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
                                           <ajaxToolkit:Rating id="RatingPerformance" ReadOnly="true" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="6" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
                        </ajaxToolkit:Rating></td>
                                        <td>
                                            Excellent</td>
                                    </tr>
                                </table>
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
                                </td>
                        </tr>
                        <tr>
                                <td>
                               </td>
                            <td colspan="3">
                                <asp:Label ID="lblpeRatingComments" runat="server" Font-Bold="True"></asp:Label>
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
                                           <ajaxToolkit:Rating id="RatingKnowledge" ReadOnly="true" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
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
                                              <ajaxToolkit:Rating id="RatingObviousPrep" ReadOnly="true" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar" >
        
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
                                            <ajaxToolkit:Rating id="RatingStyle" runat="server"  ReadOnly="true" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
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
                                        <ajaxToolkit:Rating id="RatingResponsiveness" runat="server" ReadOnly="true" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
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
                                            <ajaxToolkit:Rating id="RatingLrnClimate" ReadOnly="true" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0" CssClass="ratingStar">
        
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
                                <td><table>
                                    <tr>
                                        <td>
                                           Any other comments:</td>
                                        <td>
                                            <asp:Label ID="lblAnyOtherComments"  ReadOnly="true" runat="server" Font-Bold="True"></asp:Label>
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
                            <td align="right">
                                2.</td>
                            <td class="style2">
                                How did you feel about the length of the programme?</td>
                            <td>
                               </td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td class="style2">
                                <asp:RadioButtonList ID="radLength" runat="server" RepeatDirection="Horizontal" 
                                    Font-Bold="true" Enabled="False">
                                    <asp:ListItem Value="1">Too short</asp:ListItem>
                                    <asp:ListItem Value="2">Just right</asp:ListItem>
                                    <asp:ListItem Value="3">Too long</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td align="right">
                                3.</td>
                            <td class="style2">
                                How did you feel about the pacing of the programme?</td>
                            <td>
                                </td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td class="style2">
                                <asp:RadioButtonList ID="radPacing" runat="server" RepeatDirection="Horizontal" 
                                    Font-Bold="true" Enabled="False">
                                    <asp:ListItem Value="1">Too fast</asp:ListItem>
                                    <asp:ListItem Value="2">Just right</asp:ListItem>
                                    <asp:ListItem Value="3">Too slow</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                </td>
                            <td>
                               </td>
                        </tr>
                        <tr>
                            <td align="center">
                                </td>
                            <td class="style6">
                                Post -training:</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td align="right">
                                1.</td>
                            <td class="style2">
                                How will you apply what you have learned from the programme into practice?</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td class="style2">
                                <asp:Label ID="lblPtComments1" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td align="right">
                                2.</td>
                            <td class="style2">
                                What support do you need from the department to help you apply your learning&#39;s 
                                into practice?</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td class="style2">
                            <p>
                                <asp:Label ID="lblptComments2" runat="server" Font-Bold="True"></asp:Label></p>
                            </td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                            <td align="center">
                                &nbsp;</td>
                            <td>
                                </td>
                            <td>
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </td><td></td></tr></table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

