<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="test4._5.examples.WebForm1" %>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <title>Content Editor</title>
        <link rel="stylesheet" type="text/css" href="./plugins/bootstrap-3.4.1/css/bootstrap.min.css" data-type="keditor-style" />
        <link rel="stylesheet" type="text/css" href="./plugins/font-awesome-4.7.0/css/font-awesome.min.css" data-type="keditor-style" />
        <!-- Start of KEditor styles -->
        <link rel="stylesheet" type="text/css" href="../dist/css/keditor.css" data-type="keditor-style" />
        <link rel="stylesheet" type="text/css" href="../dist/css/keditor-components.css" data-type="keditor-style" />
        <!-- End of KEditor styles -->
        <link rel="stylesheet" type="text/css" href="./plugins/code-prettify/src/prettify.css" />
        <link rel="stylesheet" type="text/css" href="./css/examples.css" />

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
<script>
    </head>
    
    <body>

         <form id="form1" runat="server">
              <script>
                  $(document).ready(function () {

                      // Gets the video src from the data-src on each button

                      var $videoSrc;
                      $('.video-btn').click(function () {
                          $videoSrc = $(this).data("src");
                      });
                      console.log($videoSrc);



                      // when the modal is opened autoplay it  
                      $('#myModal').on('shown.bs.modal', function (e) {

                          // set the video src to autoplay and not to show related video. Youtube related video is like a box of chocolates... you never know what you're gonna get
                          $("#video").attr('src', $videoSrc + "?autoplay=1&amp;modestbranding=1&amp;showinfo=0");

                      })



                      // stop playing the youtube video when I close the modal
                      $('#myModal').on('hide.bs.modal', function (e) {
                          // a poor man's stop video
                          $("#video").attr('src', $videoSrc);
                      })






                      // document ready  
                  });



              </script>

             <div class="row mb-6" style="">
                   <div class="col-md-2 pull-center" style="text-align:center">
                       </div>
                  <div class="col-md-5 pull-center" style="text-align:center">
                      <div id="lbldmg"></div>
                      </div>
                  <div class="col-md-5 pull-right" style="float:right;">
                       <asp:HyperLink ID="bntPreview" runat="server" Target="_blank" Text="Preview" SkinID="Button" BackColor="#7239EA" BorderColor="#7239EA" style="float:right;margin:10px;padding:10px;width:150px;" ></asp:HyperLink>
                 <asp:Button ID="btnSave" runat="server" SkinID="btnDefault" ClientIDMode="Static" Text="Save" BackColor="#7239EA" BorderColor="#7239EA" style="float:right;margin:10px;padding:10px;width:150px;" />
                      
  <button type="button" class="btn btn-Green video-btn" data-toggle="modal" data-src="https://player.vimeo.com/video/820573975?h=f0526679b7&badge=0" data-target="#myModal" style="float:right;margin:10px;padding:10px;width:150px;background-color:#50CD89;color:white;" >
  Video Tutorial
</button>

<!-- Modal -->
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">

      
      <div class="modal-body">

       <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>        
        <!-- 16:9 aspect ratio -->
<div class="embed-responsive embed-responsive-16by9">
  <iframe class="embed-responsive-item" src="https://player.vimeo.com/video/820573975?h=f0526679b7&badge=0" id="video"  allowscriptaccess="always" allow="autoplay"></iframe>
</div>
        
        
      </div>

    </div>
  </div>
</div> 
                    
                     </div>
             </div>
             
             <asp:HiddenField ID="hid" runat="server" ClientIDMode="Static" Value="0" />
             <asp:HiddenField ID="hpath" runat="server" ClientIDMode="Static" value="" />
            
      <%--  <asp:Panel ID="LogoPanel" runat="server" CssClass="text-center p-5">
    <asp:Image ID="LogoImage" runat="server" ImageUrl="~/path/to/default/image.jpg" Width="100" Height="100" CssClass="img-fluid" />
    <br />
    <asp:Button ID="ChangeImageButton" runat="server" Text="Change Image" CssClass="btn btn-primary mt-3" OnClick="ChangeImageButton_Click" />
</asp:Panel>--%>

<%--<asp:FileUpload ID="ImageUploader" runat="server" CssClass="d-none" OnChanged="ImageUploader_Changed" />--%>

        <div data-keditor="html">
            <div id="content-area"></div>
            
        </div>
         <div id="content"></div>
             
        <script type="text/javascript" src="./plugins/jquery-1.11.3/jquery-1.11.3.min.js"></script>
        <script type="text/javascript" src="./plugins/bootstrap-3.4.1/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="./plugins/jquery-ui-1.12.1.custom/jquery-ui.min.js"></script>
        <script type="text/javascript" src="./plugins/ckeditor-4.11.4/ckeditor.js"></script>
        <script type="text/javascript" src="./plugins/formBuilder-2.5.3/form-builder.min.js"></script>
        <script type="text/javascript" src="./plugins/formBuilder-2.5.3/form-render.min.js"></script>
        <!-- Start of KEditor scripts -->
        <script type="text/javascript" src="../dist/js/keditor.js?id=4444"></script>
        <script type="text/javascript" src="../dist/js/keditor-components.js?id=33333"></script>
        <!-- End of KEditor scripts -->
        <script type="text/javascript" src="./plugins/code-prettify/src/prettify.js?id=2q11q"></script>
        <script type="text/javascript" src="./plugins/js-beautify-1.7.5/js/lib/beautify.js?id=3qq"></script>
        <script type="text/javascript" src="./plugins/js-beautify-1.7.5/js/lib/beautify-html.js?id=3qq"></script>
     <%--   <script type="text/javascript" src="./js/examples.js"></script>--%>


        <script type="text/javascript">
           <%-- document.getElementById('<%= ChangeImageButton.ClientID %>').onclick = function () {
        document.getElementById('<%= ImageUploader.ClientID %>').click();
                return false; // Prevent postback
            };--%>
        </script>
        <script type="text/javascript" data-keditor="script">

            (function ($) {
                $(function () {
                    initModalSource();
                    initModalContent();
                    initToolbar();
                });

                function initToolbar() {
                    var toolbar = $('<div class="toolbar"></div>');
                  //  var btnViewSource = $('<button type="button" class="view-source"><i class="fa fa-code"></i> View source</button>');
                    var btnViewContent = $('<button type="button" class="view-content"><i class="fa fa-file-text-o"></i> Get content</button>');
                    var btnSaveContent = $('<button type="button" class="view-content"><i class="fa fa-file-text-o"></i> Save</button>');
                  //  var btnBackToList = $('<a href="./" class="view-content"><i class="fa fa-list"></i> Examples list</a>');

                    toolbar.appendTo(document.body);
                   // toolbar.append(btnViewSource);
                    toolbar.append(btnViewContent);
                    toolbar.append(btnSaveContent);
               btnViewContent.on('click', function () {
                        var modal = $('#modal-content');
                        modal.find('.content-html').html(
                            beautifyHtml(
                                $('#content-area').keditor('getContent')
                            )
                        );

                        modal.modal('show');
                    });

                    btnSaveContent.on('click', function () {
                       
                        var modal = $('#modal-content');
                        modal.find('.content-html').html(
                            beautifyHtml(
                                $('#content-area').keditor('getContent')
                            )
                        );
                        var varContent = $('#content-area').keditor('getContent');
                        {
                            var obj = { varContent: varContent, orgid: $('#hid').val() }
                            $.ajax({
                                //url: '/savecontrent/insertcontentdata',
                                url: '<%:ResolveClientUrl("~/savecontrent/insertcontentdata")%>',
                                async: false,
                                method: 'Post',
                                contentType: 'application/json',
                                data: JSON.stringify(obj),
                                success: function (res) {
                                    //alert('Saved Successfully');
                                    $('#lbldmg').html('Saved Successfully');
                                   
                                }
                            })
                        }

                       
                    });
                }

                function initModalContent() {
                    var modal = $(
                        '<div id="modal-content" class="modal fade" tabindex="-1">' +
                        '    <div class="modal-dialog modal-lg">' +
                        '        <div class="modal-content">' +
                        '            <div class="modal-header">' +
                        '                <button type="button" class="close" data-dismiss="modal">&times;</button>' +
                        '                <h4 class="modal-title">Content</h4>' +
                        '            </div>' +
                        '            <div class="modal-body">' +
                        '                <pre class="prettyprint lang-html content-html"></pre>' +
                        '            </div>' +
                        '            <div class="modal-footer">' +
                        '                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>' +
                        '            </div>' +
                        '        </div>' +
                        '    </div>' +
                        '</div>'
                    );

                    modal.appendTo(document.body);
                }

                function initModalSource() {
                    var modal = $(
                        '<div id="modal-source" class="modal fade" tabindex="-1">' +
                        '    <div class="modal-dialog modal-lg">' +
                        '        <div class="modal-content">' +
                        '            <div class="modal-header">' +
                        '                <button type="button" class="close" data-dismiss="modal">&times;</button>' +
                        '                <h4 class="modal-title">Source</h4>' +
                        '            </div>' +
                        '            <div class="modal-body">' +
                        '                <ul class="nav nav-tabs">' +
                        '                    <li class="active"><a href="#source-html" data-toggle="tab"><i class="fa fa-html5"></i> HTML</a></li>' +
                        '                    <li ><a href="#source-js" data-toggle="tab"><i class="fa fa-code"></i> JavaScript</a></li>' +
                        '                </ul>' +
                        '                <div class="tab-content">' +
                        '                    <div class="tab-pane active" id="source-html">' +
                        '                        <pre class="prettyprint lang-html source-html"></pre>' +
                        '                    </div>' +
                        '                    <div class="tab-pane" id="source-js">' +
                        '                        <pre class="prettyprint lang-js source-js"></pre>' +
                        '                    </div>' +
                        '                </div>' +
                        '            </div>' +
                        '            <div class="modal-footer">' +
                        '                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>' +
                        '            </div>' +
                        '        </div>' +
                        '    </div>' +
                        '</div>'
                    );

                    var htmlCode = $('[data-keditor="html"]').html();
                    var htmlInclude = $('<div />').html($('[data-keditor="html-include"]').clone()).html();
                   
                    htmlInclude = htmlInclude.replace('data-keditor="html-include"', '');
                    htmlInclude = htmlInclude.replace(/\</g, '&lt;').replace(/\>/g, '&gt;');
                    
                    modal.find('.source-html').html(beautifyHtml(htmlCode + htmlInclude));

                    var jsCode = $('[data-keditor="script"]').html();
                    jsCode = jsCode.replace(/\</g, '&lt;').replace(/\>/g, '&gt;');
                    modal.find('.source-js').html(beautifyJs(jsCode));
                   
                    modal.appendTo(document.body);
                }

                function beautifyHtml(htmlCode) {
                    htmlCode = html_beautify(htmlCode, {
                        'indent_size': '4',
                        'indent_char': ' ',
                        'space_after_anon_function': true,
                        'end_with_newline': true
                    });
                    htmlCode = htmlCode.replace(/</g, '&lt;').replace(/>/g, '&gt;');
                   
                    return PR.prettyPrintOne(htmlCode, 'lang-html');
                }

                function beautifyJs(jsCode) {
                    jsCode = js_beautify(jsCode, {
                        'indent_size': '4',
                        'indent_char': ' ',
                        'space_after_anon_function': true,
                        'end_with_newline': true
                    });

                    return PR.prettyPrintOne(jsCode, 'lang-js');
                }


                $('#btnSave').on('click', function () {
                   
                    var modal = $('#modal-content');
                    modal.find('.content-html').html(
                        beautifyHtml(
                            $('#content-area').keditor('getContent')
                        )
                    );
                    var varContent = $('#content-area').keditor('getContent');
                    {
                        var obj = { varContent: varContent, orgid: $('#hid').val() }
                        $.ajax({
                            url: '<%:ResolveClientUrl("~/savecontrent/insertcontentdata")%>',
                            async: false,
                            method: 'Post',
                            contentType: 'application/json',
                            data: JSON.stringify(obj),
                            success: function (res) {
                               // alert('Saved Successfully');
                                $('#lbldmg').show();
                                $('#lbldmg').html("<div class='alert alert-success'>Saved Successfully </div>").delay(5000).fadeOut('slow');

                            }
                        })
                    }
                   
                    return false;
                });

            })(jQuery);
            $(function () {
                $('#content-area').keditor();
                var _page = $("[id*=hpath]").val();
                
                var _url = '<%:ResolveClientUrl("~/"+hpath.Value)%>';
                let correctUrlString = _url.replace(/&amp;/g, '&');
                $.ajax({
                    url: correctUrlString,
                    type: 'get',
                    async: false,
                    success: function (data) {
                       
                        console.log($(data));
                       
                        $('#content-area').keditor('setContent', data);

                       
                    }
                });

                //var file = $("[id *= 'hpath']").val();
                //alert('file:' + file);

                //$('#content').load(file);
                //alert($('#content').val());

                //hpath.Value
//                $('#content-area').keditor('setContent', `
//    <div class="row">
//        <div class="col-md-6" data-type="container-content">
//            <div data-type="component-text">New content</div>
//        </div>
//    </div>
//`);

               
                //$("#apply-youtube").click(function () {
                //   
                //    console.log('apply youtube');
                //    //alert('test');
                //   
                //    alert($(this).closest('input .text-apply-youtube').val());
                //});

            });

           
        </script>
              </form>
       
    </body>
</html>

