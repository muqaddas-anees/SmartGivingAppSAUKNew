<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Survey_Question_List.aspx.cs" Inherits="DeffinityAppDev.App.PollAndSurvey.Survey.Survey_Question_List" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_options" runat="server">
    
</asp:Content>
<asp:Content ID="ContentMain" runat="server" ContentPlaceHolderID="MainContent">
     <style>
           .mycheckBig input {width:25px; height:25px;}
           .mycheckBig label {padding-left:8px;font-size:25px}
       </style>
    <style>
        .modalBackground {
            overflow: scroll;
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }



        select {
            width: 390px;
            height: 45px;
            background-color: #F5F8FA;
            /*border-collapse:collapse;*/
            border-radius: 5px;
            border-color: #F5F8FA;
        }
    </style>



                                     <div class="row">

                                        <div class="col-lg-6">

                                              <div class="row">
                                                 <asp:Label ID="lblQuestion"  Font-Size="40px"  runat="server" /><br /><br /><br />
                                               <asp:HiddenField ID="HiddenField1" runat="server" />
                                                  <asp:HiddenField ID="Huser_guid" runat="server" />
                                                  <asp:HiddenField ID="Hevent_unid" runat="server" />
                                            </div>

                                              <div class="row">
                                                   <asp:Label ID="lblDescription" Text=""  runat="server"  Font-Size="30px"/><br /><br />
                                                  </div>

                                            
                                    <div runat="server" id="DisplayMultipleChoise">


                                          <div class="row">
                                                  <div class="col-lg-6">
                                                      <asp:HiddenField ID="hmoney" runat="server" />
                                                 <asp:RadioButtonList ID="rdList" runat="server" CssClass="mycheckBig"></asp:RadioButtonList><br /><br />
                                                      </div>
                                                 </div>

                                        


                                    </div>




                                    <div runat="server" id="DisplayText">


                                        <div class="row">
                                                  <div class="col-lg-6">
                                                    <asp:TextBox ID="txtTextvalue" runat="server"></asp:TextBox>

                                                    <br />
                                                    <br />
                                               </div>
                                            </div>

                                    </div>




                                    <div runat="server" id="DisplaySingleSlection">

                                        <div class="row">
                                                  <div class="col-lg-6">
                                                    <asp:TextBox ID="txtSingleSelection" runat="server"></asp:TextBox>

                                                    <br />
                                                    <br />
                                               </div>
                                            </div>

                                    </div>



                                    <div runat="server" id="DisplayDetailedAnswer">

                                       <div class="row">
                                                  <div class="col-lg-6">
                                                    <asp:TextBox ID="TextAreaDetailedAnswer" runat="server" placeholder="Detailed Answer" TextMode="MultiLine" Rows="10"></asp:TextBox>


                                                    <br />
                                                    <br />
                                               </div>
                                           </div>

                                    </div>

                                      <div class="row mb-20">
                                            <div class="col-lg-6  py-6 px-9">
                                                 <asp:Button ID="btnPrevious" runat="server" Text="Previous" ToolTip="Previous" OnClick="PreviousButtonClick" Style="margin-right: 20px;" />

                                                </div>

                                            <div class="col-lg-6 d-flex justify-content-end py-6 px-9">
                                                  <asp:Button ID="btnFinesh" runat="server" Text="Finish" ToolTip="Finish Survey" OnClick="SubmitButtonClick" />

                                                    <asp:Button ID="btnNext" runat="server" Text="Next" ToolTip="Next" OnClick="NextButtonClick" />
                                                </div>
                                                 
                                          </div>


                                                 <div class="row">
                                                     <asp:Button ID="btnReport" runat="server" Text="Finish" ToolTip="View Report" OnClick="btnReport_Click" />
                                                     </div>
                                            </div>


                                          <div class="col-lg-5">

                                             
                                              <asp:Image ID="imgQR" runat="server" CssClass="img-fluid" />
                                        
                                            </div>
                                         </div>




                                   


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>



</asp:Content>
