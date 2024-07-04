<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Survey_Display.aspx.cs" Inherits="DeffinityAppDev.App.Events.PollAndSurvey.Survey.Survey_Display" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
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
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
