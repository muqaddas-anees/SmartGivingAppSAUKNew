<%@ Page Title="" Language="C#" MasterPageFile="~/WF/DC/Feedback/Feedback.Master" AutoEventWireup="true" CodeBehind="FeedbackFrontpage.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Feedback.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
   
    <div class="panel page-body transparent" align="center" style="padding-top: 10em">
         <div class="col-md-3">
             </div>
        <div class="col-md-6">
					
					<!-- Default panel -->
					<div class="card shadow-sm">
						
						<div class="panel-body" style="text-align:center;">
						<h2> How did we do ? </h2>
							<p>We are always looking to improve our service and value your opinion. Please take a moment to tell us how we performed by taking this short survey.</p>

                            <br /><br />
                            <a id="btnNext" runat="server" href="Feedbackentry.aspx" class="btn btn-success btn-lg">Let's go</a>
                            <br />
                       
						</div>
					</div>
					
				</div>
        <div class="col-md-3">
             </div>
      <%-- <div class="col-md-4">

       </div><div class="col-md-3" style="padding-left:5em">
            <h2> </h2>
            <h5>
                
            </h5>
       <div style="padding-top:21em" >
           
        
           </div>--%>
        </div>
    
</asp:Content>
