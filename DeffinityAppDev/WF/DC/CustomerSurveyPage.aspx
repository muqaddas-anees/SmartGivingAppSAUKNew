<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="CustomerSurveyPage.aspx.cs" Inherits="DeffinityAppDev.WF.DC.CustomerSurveyPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <div style="background-color: #339933; font: x-large; font-style: normal; color: #FFFFFF; font-size: xx-large; text-align: center;">
        It's Rewarding to<br />
        SPEAK YOUR MIND
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group row">
        <div class="col-md-12">
            <asp:Label ID="lblMsg" EnableViewState="false" runat="server"></asp:Label>
        </div>
    </div>
    <div class="form-group row">
        <div class="col-xs-12">
            <label style="padding-top: 5px;">How likely are you to recommend the service provider to your friend ?</label><br />
            <br />

        </div>
        <div class="col-md-4" >
           
            
                <input type='range' min='1' max='10' step='1' id="rng1" name="rr1" style=" border: solid 1px ;border-color:lightgray" />
               
                  </div>
        <br />
        <br />
        <br />
        <div class="col-xs-12">
            <br />
            <br />
            <label style="padding-top: 5px;">How would you rate the professionalism of a service Provider ?</label><br />
            <br />

        </div>
        <div style="padding-top: 5px;"></div>
        <div class="col-md-4">
            <input type='range' min='1' max='10' step='1' id="rng2" name="rr2" style=" border: solid 1px;border-color:lightgray "/>

        </div>
        <%--style="visibility: hidden; display: none"--%>
        <div style="display: none">
            <asp:HiddenField ID="hidden1" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hidd2" runat="server" ClientIDMode="Static" />
            <asp:Label ID="lblval1" runat="server" value="1" Visible="true"></asp:Label>
            <asp:Label ID="lblval2" runat="server" value="2" Visible="true"></asp:Label>
        </div>
        <br />
        <br />
        <br />
        <div class="col-xs-12">
            <br />
            <br />
            <label class="col-sm-3" style="padding-top: 5px;">Did the technician turn up on time ?</label><br />
            <br />
            <div class="col-md-4">

                <input type="checkbox" id="chk1" class="iswitch-lg iswitch-success" runat="server" />
            </div>

        </div>

        <div class="col-md-12">
            <label class="col-sm-3" style="padding-top: 5px;">How can we improve our service ?</label>

        </div>
        <div class="col-sm-4">
            <asp:TextBox ID="txtfeedinfo" runat="server" TextMode="MultiLine" Height="48px" Width="195px"></asp:TextBox>
        </div>
        <div class="col-md-2" style="padding-top: 6em">
<%--              <a runat="server" id="link_CustomerPortal"  onsubmit="BtnSubmit_Click" class="btn btn-secondary"
                        target="_self" title="<%$ Resources:DeffinityRes,Viewcustomerportal%>" style="padding-top:5px;">Submit Your Surve</a>--%>
            <asp:Button ID="BtnSubmit" runat="server" SkinID="btnSubmit" OnClick="BtnSubmit_Click" Text="Submit Your Survey" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script type="text/javascript">
        $(function () {
            var $document = $(document),
                $inputRange = $('input[type="range"]');

            // Example functionality to demonstrate a value feedback
            function valueOutput(element) {
                var value = element.value,
                    output = element.parentNode.getElementsByTagName('output')[0];
                if (element.name == 'rr2') {
                    document.getElementById('<%= hidd2.ClientID %>').value = value;

                } else {
                    document.getElementById('<%= hidden1.ClientID %>').value = value;
                }
            }
            for (var i = $inputRange.length - 1; i >= 0; i--) {
                valueOutput($inputRange[i]);
            };
            $document.on('input', 'input[type="range"]', function (e) {
                valueOutput(e.target);
            });
            // end


        });
    </script>
    <link href="../../Content/css/rangecss.css" rel="stylesheet" />
   
</asp:Content>
