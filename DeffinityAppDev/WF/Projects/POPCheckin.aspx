<%@ Page Language="C#" AutoEventWireup="true" Inherits="POPCheckin" Codebehind="POPCheckin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="js/jquery-1.3.2.js" type="text/javascript"></script>
<script src="js/jquery.MultiFile.js" type="text/javascript"></script>
<%--<link rel="stylesheet" type="text/css" href="stylcss/deffinity_frame.css" />--%>
<head id="Head2" runat="server">
<title><%= Resources.DeffinityRes.DocCheckIn%></title>
    
</head>
<body>
    <form id="form2" runat="server">
           
            <asp:Panel style="margin:0; position:absolute" ID="PnlFileDownload" Visible="true"  runat="server"  >
           <asp:Label runat="server" SkinID="Loading"></asp:Label> 
            </asp:Panel>
            <asp:Panel  style="margin:20px;" ID="PnlFileUpload"  Font-Bold="true" runat="server"                 
                ScrollBars="None" Width="350px" Height="175px">
				
				<h1 style="background:#19a7d5; color:#fff; font-size:18px; margin:0; padding:10px;"><%= Resources.DeffinityRes.CheckIn%></h1>
                <br />
                <br />
                <h2 style="font-size:13px; margin:0; padding:10px;"><%= Resources.DeffinityRes.Sel_File_ChkIn%></h2>
               
                            <div><asp:FileUpload ID="FileUpload1" runat="server" maxlength="1" class="multi" /></div>
                    
                            <div>
                                <br />
                            <asp:Button ID="btnUpload" SkinID="btnDefault" Text="Check In the file" runat="server" OnClick="btnUpload_Click" />
                           </div>
                                    
                            <div style="font-size:13px; font-weight:normal"><asp:Label ID="lblMsg" runat="server" ForeColor="red"></asp:Label></div>
                              
            </asp:Panel>
        
    
    
    </form>
</body>
</html>