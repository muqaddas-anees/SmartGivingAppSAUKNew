<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Poll_Display.aspx.cs" Inherits="DeffinityAppDev.App.PollAndSurvey.Polls.Poll_Display" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="server">
    Poll
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <style>
           .mycheckBig input {width:25px; height:25px;}
           .mycheckBig label {padding-left:8px;font-size:25px}
       </style>
    <style type="text/css">
  .BigCheckBox input { width:25px; height:25px;   border-radius: 50%; background-color:#7239EA; padding:40px; padding-bottom:4px;  }
   .BigCheckBox label { font-size:17px; margin-left:8px;margin-bottom:5px;padding-bottom:5px;  }

  .checkbox-round {
    width: 1.3em;
    height: 1.3em;
    background-color: white;
    border-radius: 50%;
    vertical-align: middle;
    border: 1px solid #ddd;
   
    -webkit-appearance: none;
    outline: none;
    cursor: pointer;
}

  .checkbox-round:checked {
    background-color: gray;
}

</style>
       <div class="row">

                                        <div class="col-lg-6">

                                            <div class="row">
                                                 <asp:Label ID="lblQuestion" Text=""  runat="server" Font-Size="40px" /><br /><br /><br />
                                               <asp:HiddenField ID="hmoney" runat="server" />
                                            </div>

                                              <div class="row">
                                                   <asp:Label ID="lblDescription" Text=""  runat="server"  Font-Size="30px"/><br /><br />
                                                  </div>

                                             <div class="row">
                                                  <div class="col-lg-6">
                                                 <asp:RadioButtonList ID="rdList" runat="server" CssClass="mycheckBig"></asp:RadioButtonList><br /><br />
                                                      </div>
                                                 </div>
                                             <div class="row">
                                                  <div class="col-lg-6">
                                                 <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" SkinID="btnDefault" Text="View Result" />
                                                      </div>
                                                 </div>

                                        </div>
                                          <div class="col-lg-5">
                                              <asp:Image ID="imgQR" runat="server" CssClass="img-fluid" />
                                        </div>
                                    </div>





                                     

          


                                    <div runat="server" id="DisplayMultipleChoise" style="display:none;">


                                        <table style="width: 90%">






                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="R1" GroupName="poll2" OnCheckedChanged="OnOptionChanged" runat="server" AutoPostBack="true" />
                                                  
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="R2" GroupName="poll2" OnCheckedChanged="OnOptionChanged" runat="server" AutoPostBack="true" />
                                                    
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="R3" GroupName="poll2" OnCheckedChanged="OnOptionChanged" runat="server" AutoPostBack="true" />
                                                    
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="R4" GroupName="poll2" OnCheckedChanged="OnOptionChanged" runat="server" AutoPostBack="true" />
                                                   
                                                   
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>




                                        </table>


                                    </div>


















































       <div>
           
      





































         

   

          
           

             

         

          


          


           <br />
           <br />
           <br />
           <br />
         



             <asp:Repeater runat="server" ID="rptPollCountGrid">
        <HeaderTemplate>
            <table  width="250"  >
                <thead>
                    <th>
                        Vote for
                    </th>
                    <th>
                        Vote Count
                    </th>
                </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblOptions" Text='<%# Eval("key") %>' runat="server" Width="100" />
                </td>
                <td>
                    <asp:Label ID="lblCount" Text='<%# Eval("cnt") %>' runat="server" Width="100" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
        
    </asp:Repeater>




</div>

    <script type="text/javascript">
    function getCheckedRadios() {
        alert(document.getElementById('1').checked);
    }
    </script>


</asp:Content>
