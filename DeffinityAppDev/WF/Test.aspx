<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Main.master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DeffinityAppDev.WF.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <%
        string key = CacheNames.DefaultNames.JournalEnable.ToString();
        System.Web.HttpContext.Current.Cache.Remove(key);
        %>
    <style>
        .control-label{ text-align:right;}
    </style>
    

      <div class="card shadow-sm">
						<div class="card-header">
							<h3 class="panel-title form-inline"> 
                               Header
                                </h3>
							<div class="card-toolbar">
								
                                
							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
             <asp:PlaceHolder ID="ph" runat="server"></asp:PlaceHolder>
                                </div>
                </div>
            </div>


     <asp:TextBox ID="txtfromtime" runat="server" ClientIDMode="Static" Text="15:00"></asp:TextBox>
   <asp:TextBox ID="txttotime" runat="server" ClientIDMode="Static" Text="13:00"></asp:TextBox>



    <br />
    <br />
    user id <asp:TextBox ID="txtid" runat="server" Text="1"></asp:TextBox>
    <asp:Button ID="btngetid" runat="server" OnClick="btngetid_Click" Text="Submit" />
    <asp:Label ID="lblresult" runat="server"></asp:Label>
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script src="../Content/assets/js/jquery-1.11.1.min.js"></script>
    <script src="../Content/assets/js/moment.min.js"></script>

    <script>
        $(document).ready(function () {

            var f = $('#txtfromtime').val();
            var e = $('#txttotime').val();
            debugger;
          
            var startTime = moment(f, "HH:mm");
            var endTime = moment(e, "HH:mm");
            debugger;
            var r = endTime.isBefore(startTime);
           // alert(r);
        });
        
        function comparetime(fromtime, totime)
        {
            var sTime = moment(fromtime, "HH:mm");
            var tTime = moment(totime, "HH:mm");

            return tTime.isBefore(sTime)
        }
    </script>
</asp:Content>
