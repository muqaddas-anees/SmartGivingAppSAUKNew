<%@ Page Title="" Language="C#" MasterPageFile="~/WF/DC/Feedback/Feedback.Master" AutoEventWireup="true" CodeBehind="Feedbackentry.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Feedback.Feedbackentry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .transparent {
            background-color: rgba(0, 0, 0, 0);
        }

        /*Css for the radiobutton */
        .iradio_line-aero1 {
            position: relative;
            display: block;
            margin: 0;
            padding: 5px 15px 5px 38px;
            font-size: 13px;
            line-height: 17px;
            color:#000000;
            background: #ffffff;
            border: none;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            cursor: pointer;
        }

            .iradio_line-aero1 .icheck_line-icon1 {
                position: absolute;
                top: 50%;
                left: 13px;
                width: 13px;
                height: 11px;
                margin: -5px 0 0 0;
                padding: 0;
                overflow: hidden;
                background: url(/Content/assets/js/icheck/skins/line/line.png) no-repeat;
                border: none;
            }


            .iradio_line-aero1.hover {
                background: #B5D1D8;
            }

            .iradio_line-aero1.checked {
                background: #9cc2cb;
            }

                .iradio_line-aero1.checked .icheck_line-icon1 {
                    background-position: -15px 0;
                }



        /* HiDPI support */
        @media (-o-min-device-pixel-ratio: 5/4), (-webkit-min-device-pixel-ratio: 1.25), (min-resolution: 120dpi) {

            .iradio_line-aero1 .icheck_line-icon1 {
                background-image: url(/Content/assets/js/icheck/skins/line/line@2x.png);
                -webkit-background-size: 60px 13px;
                background-size: 60px 13px;
            }
        }
    </style>
      <%--script for the radiobutton --%>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $('input.icheck-15').each(function (i, el) {
                var self = $(el),
                    label = self.next(),
                    label_text = label.text();

                label.remove();

                self.iCheck({

                    radioClass: 'iradio_line-aero1',
                    insert: '<div class="icheck_line-icon1"></div>' + label_text
                });
            });
        });


    </script>

    <%--style & script for the rating --%>
 <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css"/>
    <link href="../../../Content/css/bars-square.css" rel="stylesheet" />
    <script src="../../../Content/jquery.barrating.js"></script>
    <script src="../../../Content/examples.js"></script>
    <script src="../../../Content/assets/js/icheck/icheck.min.js"></script>   

    <script>
      
   

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>--%>
   <%-- <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css"/>--%>
    <link rel="stylesheet" href="../../../Content/bootstrap-star-rating/css/star-rating.css" media="all" type="text/css"/>
   <%-- <link rel="stylesheet" href="../../../Content/bootstrap-star-rating/css/themes/krajee-fa/theme.css" media="all" type="text/css"/>
    <link rel="stylesheet" href="../../../Content/bootstrap-star-rating/css/themes/krajee-svg/theme.css" media="all" type="text/css"/>
    <link rel="stylesheet" href="../../../Content/bootstrap-star-rating/css/themes/krajee-uni/theme.css" media="all" type="text/css"/>--%>
    <script src="../../../Content/bootstrap-star-rating/js/star-rating.js"></script>
    <script src="../../../Content/bootstrap-star-rating/js/star-rating.js" type="text/javascript"></script>
    <%--<script src="../../../Content/bootstrap-star-rating/js/themes/krajee-fa/theme.js" type="text/javascript"></script>
    <script src="../../../Content/bootstrap-star-rating/js/themes/krajee-svg/theme.js" type="text/javascript"></script>
    <script src="../../../Content/bootstrap-star-rating/js/themes/krajee-uni/theme.js" type="text/javascript"></script>--%>

    <div class="page-body">
        <div class="page-container">
            <div id="rootwizard" class="form-wizard transparent">

                <ul class="tabs transparent">
                    <li class="active">
                        <a href="#tab1" data-toggle="tab" hidden="hidden"></a>
                    </li>
                    <li>
                        <a href="#tab2" data-toggle="tab"></a>
                    </li>
                    <li>
                        <a href="#tab3" data-toggle="tab"></a>
                    </li>
                    <li hidden="hidden">
                        <a href="#tab4" data-toggle="tab"></a>
                    </li>
                    <li hidden="hidden">
                        <a href="#tab5" data-toggle="tab"></a>
                    </li>
                   
                   
                </ul>
                <%-- <div style="padding-top:3em">--%>
                <div class="progress-indicator">
                    <span></span>
                </div>

                <div class="tab-content">

                    <!-- Tabs Content -->
                    <div class="tab-pane  active" id="tab1">
                        <div class="panel panel-body transparent " align="center" style="padding-top: 10em;align-content:center;text-align:center">

                             <div class="col-md-3">
                                  <ul class="pager wizard card shadow-sm transparent fixed">

                        <li class="previous" style="float:left">
                          <i><img src="ui-18-256.png" height="50" width="50" /></i>
                        </li>
                                 
                    </ul>
                             </div>
                                      <div class="col-md-6" style="text-align:center">
					
					<!-- Default panel -->
					<div class="card shadow-sm" style="align-content:center;text-align:center">
                         <h3>How satisfied are you with the <asp:Literal ID="lblServiceProvider" runat="server"></asp:Literal>  ?</h3>
                            <div class="row">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-4">
                                    <div class="rate1" style="font-size:35px"></div>
                                    <asp:HiddenField ID="hrate1" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                        </div>
            </div>
                                       <div class="col-md-3">
                                            <ul class="pager wizard card shadow-sm transparent fixed">
                      
                        <li class="next" style="float:right">
                            <i><img src="ui-14-256.png" height="50" width="50" /></i>
                            
                        </li>
                    </ul>
                                       </div>

                             
                        </div>
                    </div>

                    <div class="tab-pane" id="tab2">
                        <div class="panel panel-body transparent " align="center" style="padding-top: 10em">
                             <div class="col-md-3">
                                  <ul class="pager wizard card shadow-sm transparent fixed">
                                  
                        <li class="previous" style="float:left">
                           <i><img src="ui-18-256.png" height="50" width="50" /></i>
                        </li>
                        
                    </ul>
                                 </div>
                            <div class="col-md-6">
                                <div class="card shadow-sm" style="align-content:center;text-align:center">
                                 <h3>How satisfied are you with customer service you spoke with?</h3>
                            <div class="row">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-4">
                                    <div class="rate2" style="font-size:35px"></div>
                                    <asp:HiddenField ID="hrate2" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                                    </div>
                                 </div>
                            <div class="col-md-3">
                                 <ul class="pager wizard card shadow-sm transparent fixed">
                        <li  class="next" style="float:right">
                            <i><img src="ui-14-256.png" height="50" width="50" /></i>
                            
                        </li>
                    </ul>
                                 </div>
                            
                        </div>

                    </div>

                    <div class="tab-pane " id="tab3">
                        <div class="panel panel-body transparent " align="center" style="padding-top: 10em">
                             <div class="col-md-3">
                                  <ul class="pager wizard card shadow-sm transparent fixed">

                      <li class="previous" style="float:left">
                          <i><img src="ui-18-256.png" height="50" width="50" /></i>
                        </li>
                       
                    </ul>
                                 </div>
                              <div class="col-md-6">
                                  <div class="card shadow-sm" style="align-content:center;text-align:center">
                                       <h3>How satisfied are you with the customer portal to submit your claim?</h3>
                                       <div class="row">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-4">
                                    <div class="rate3" style="font-size:35px"></div>
                                    <asp:HiddenField ID="hrate3" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                                       <div class="row">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtImprovements" runat="server" TextMode="MultiLine" SkinID="txtMulti_80" style="width:100%"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                </div>
                                </div>
                           
                                      </div>
                                 </div>
                              <div class="col-md-3">
                                    <ul class="pager wizard card shadow-sm transparent fixed">

                      <li  class="next" style="float:right">
                            <i><img src="ui-14-256.png" height="50" width="50" /></i>
                            
                        </li>
                       
                    </ul>
                                 </div>
                        </div>
                    </div>

                    <div class="tab-pane" id="tab4">

                        
                         <div class="panel panel-body transparent" align="center" style="padding-top: 10em">
                              <div class="col-md-3">
                                   <ul class="pager wizard card shadow-sm transparent fixed">

                       <li class="previous" style="float:left">
                          <i><img src="ui-18-256.png" height="50" width="50" /></i>
                        </li>
                    </ul>
                             </div>
                         <div class="col-md-6">
                              <div class="card shadow-sm" style="align-content:center;text-align:center">
                                  
                             <h3>How satisfied are you with your overall claim experience?</h3>
                            <div class="row">
                                <div class="col-sm-4"></div>
                                <div class="col-sm-4">
                                    <div class="rate4" style="font-size:35px"></div>
                                    <asp:HiddenField ID="hrate4" runat="server" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            </div>
                             </div>
                         <div class="col-md-3">
                              <ul class="pager wizard card shadow-sm transparent fixed">
                        <li class="next" style="float:right">
                            <i><img src="ui-14-256.png" height="50" width="50" /></i>
                            
                        </li>
                    </ul>
                             </div>
                        </div>
                    </div>
                    

                    <div class="tab-pane" id="tab5">
                       <div class="panel panel-body transparent " align="center" style="padding-top: 10em">
                           <div class="col-md-3">
                                 <ul class="pager wizard card shadow-sm transparent fixed">
                       <li class="previous" style="float:left">
                          <i><img src="ui-18-256.png" height="50" width="50" /></i>
                        </li>
                    </ul>
                               </div>
                             <div class="col-md-6">
                               <div class="card shadow-sm" style="align-content:center;text-align:center">
                                   <h3>Additional Comments?</h3>
                            <div class="row">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-8">
                                   <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" SkinID="txtMulti_80" style="width:100%"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                                   </div>
                               </div>
                            <div class="col-md-3">
                                  <ul class="pager wizard card shadow-sm transparent fixed">
                        <li style="text-align:right;float:right">
                             <asp:Button ID="Button1" runat="server" Text="Submit"  class="btn btn-secondary" OnClick="btn1_Click" />
                            
                        </li>
                    </ul>
                               </div>
                          
                        </div>
				
                    </div>
                   
                </div>

            </div>
        </div>
    </div>
    <script src="dist/stars.min.js"></script>
     <script type="text/javascript">
         $(".rate1").stars({
             click: function (i) {
                 //alert("Star " + i + " selected.");
                 $("[id$='hrate1']").val(i);
             }
         });
         $(".rate2").stars({
             click: function (i) {
                 //alert("Star " + i + " selected.");
                 $("[id$='hrate2']").val(i);
             }
         });
         $(".rate3").stars({
             click: function (i) {
                 //alert("Star " + i + " selected.");
                 $("[id$='hrate3']").val(i);
             }
         });
         $(".rate4").stars({
             click: function (i) {
                 //alert("Star " + i + " selected.");
                 $("[id$='hrate4']").val(i);
             }
         });
         </script>
</asp:Content>
