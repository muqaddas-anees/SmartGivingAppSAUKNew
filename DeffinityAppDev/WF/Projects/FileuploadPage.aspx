<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master"
     AutoEventWireup="true" Inherits="FileuploadPage" Codebehind="FileuploadPage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <%--<script src="Scripts/jquery-1.9.0.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            function getParameterByName(name) {
                name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                    results = regex.exec(location.search);
                return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
            }
            $('#lbl_loading').hide();
            $('#div_buttons').show();
            $("#pnlExceptionDownload").hide();
            $("#Button1").click(function (evt) {
               
                evt.preventDefault();
                $('#lbl_loading').show();
                $('#div_buttons').hide();
                var dVal = getParameterByName("project");
                var UID = getParameterByName("UID");
                
                if (dVal != 0) {
                    var fileUpload = $("#FileUploadExcel").get(0);
                    var files = fileUpload.files;
                    if (files.length > 0) {
                        var data = new FormData();
                        for (var i = 0; i < files.length; i++) {
                            data.append(files[i].name, files[i]);
                        }
                        debugger;
                        var options = {};
                        options.url = "UploadHandler.ashx?project=" + dVal + "&uid=" + UID;
                        options.type = "POST";
                        options.data = data;
                        options.contentType = false;
                        options.processData = false;
                        options.enctype = "multipart/form-data";
                        options.success = function (result) {
                         
                            if (result == '0') {
                                $("#SuccessMessage").html('Uploaded successfully!');
                                $("#errorMsg").hide();
                                $("#BtnException").hide();
                            }
                            else {
                                $("#SuccessMessage").html('');
                                $("#errorMsg").show();
                                $("#BtnException").show();
                            }
                            $("#pnlExceptionDownload").show();
                            $('#lbl_loading').hide();
                            $('#div_buttons').show();
                            $('#FileUploadExcel').val('');
                        };
                        options.error = function (err) {
                            $("#ErrorMessage").html(err.statusText);
                            $('#lbl_loading').hide();
                            $('#div_buttons').show();
                            $('#FileUploadExcel').val('');
                        };
                        $.ajax(options);
                    }
                    else {
                        $("#ErrorMessage").html("Please select file");
                        $('#lbl_loading').hide();
                        $('#div_buttons').show();
                        $('#FileUploadExcel').val('');
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
    <asp:Label ID="lblsessionuid" runat="server" Visible="false" ClientIDMode="Static"></asp:Label>
        <div id="SuccessMessage" style="color:green"></div>
        <div id="ErrorMessage" style="color:red"></div>
   </div>
    <div>
          <table>
        <tr>
            <td>
                 <asp:Label ID="Label1" runat="server" Text="Select&nbsp;file&nbsp;to&nbsp;upload:"
                         ClientIDMode="Static"></asp:Label>
            </td>
            <td>
                    <asp:FileUpload ID="FileUploadExcel" runat="server" ClientIDMode="Static"/>
            </td>
            <td>
                <span style="margin-right: 80px">
                <asp:Label ID="lbl_loading" runat="server" SkinID="Loading" Text="Loading..." ClientIDMode="Static"></asp:Label></span>
                <div id="div_buttons">
                      <asp:Button ID="Button1" runat="server" ClientIDMode="Static" Text="Upload"/>
                    </div>
            </td>
        </tr>
    </table>
      
         <asp:Panel ID="pnlExceptionDownload" runat="server" ClientIDMode="Static" Width="100%">
               <p id="errorMsg" style="color:red;">Errors have occurred during the upload. entries which were valid have been applied to the system. Please check the exception 
                   report below for invalid entries. Once you have corrected them please try the upload again.</p> 
             <p id="BtnException"> Please <asp:HyperLink ID="linkException" runat="server" Text="click here"></asp:HyperLink> to download exception file </p>

         </asp:Panel>
    </div>
</asp:Content>

