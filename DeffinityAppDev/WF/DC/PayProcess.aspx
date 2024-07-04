<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayProcess.aspx.cs" Inherits="DeffinityAppDev.WF.DC.PayProcess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div style="display:none;visibility:hidden;">   <% if( Session["ThreeDSACSURL"] != null) {  %>
      <p>The below form collects all of the information and submits to the gateway url. This can be set to submit automatically, or be used as a confirmation page.</p>

    <p>Your transaction requires 3D Secure Authentication</p>
    <body onload="setTimeout(function() { document.myform.submit() }, 5000)">
    <form action="<%: Session["ThreeDSACSURL"].ToString() %>" method="post" name="myform" >
        <input type="hidden" name="MD" value="<%: Session["ThreeDSMD"].ToString() %>"/>
        <input type="hidden" name="PaReq" value="<%: Session["ThreeDSPaReq"].ToString() %>"/>
        <input type="hidden" name="TermUrl" value="<%: Session["PageUrl"].ToString() %>"/>
         

        <input type="submit" value="Continue"/>
    </form>
        </body>
     <% }  %>
        </div>
   
    <form id="form1" runat="server">
        <div style="font-size:45px;">
            Payment processing, please wait 



              <input type="hidden" id="portfolioid" runat="server" />
       <%-- <input type="hidden"  />
        <input type="hidden" name="TermUrl" value="<%: Session["PageUrl"].ToString() %>"/>--%>
        </div>
    </form>
</body>
</html>
