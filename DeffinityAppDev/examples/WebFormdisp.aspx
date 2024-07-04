<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormdisp.aspx.cs" Inherits="test4._5.examples.WebFormdisp" %>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <title>KEditor - Kademi Content Editor</title>
        <link rel="stylesheet" type="text/css" href="./plugins/bootstrap-3.4.1/css/bootstrap.min.css" data-type="keditor-style" />
        <link rel="stylesheet" type="text/css" href="./plugins/font-awesome-4.7.0/css/font-awesome.min.css" data-type="keditor-style" />
        <!-- Start of KEditor styles -->
        <link rel="stylesheet" type="text/css" href="../dist/css/keditor.css" data-type="keditor-style" />
        <link rel="stylesheet" type="text/css" href="../dist/css/keditor-components.css" data-type="keditor-style" />
        <!-- End of KEditor styles -->
        <link rel="stylesheet" type="text/css" href="./plugins/code-prettify/src/prettify.css" />
        <link rel="stylesheet" type="text/css" href="./css/examples.css" />
    </head>
    
    <body>

         <form id="form1" runat="server">
             </form>
       
        <div id="divdisply">

        </div>
        

       
        <script type="text/javascript" src="./plugins/jquery-1.11.3/jquery-1.11.3.min.js"></script>
        <script type="text/javascript" src="./plugins/bootstrap-3.4.1/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="./plugins/jquery-ui-1.12.1.custom/jquery-ui.min.js"></script>
        <script type="text/javascript" src="./plugins/ckeditor-4.11.4/ckeditor.js"></script>
        <script type="text/javascript" src="./plugins/formBuilder-2.5.3/form-builder.min.js"></script>
        <script type="text/javascript" src="./plugins/formBuilder-2.5.3/form-render.min.js"></script>
        <!-- Start of KEditor scripts -->
        <script type="text/javascript" src="../dist/js/keditor.js"></script>
        <script type="text/javascript" src="../dist/js/keditor-components.js"></script>
        <!-- End of KEditor scripts -->
        <script type="text/javascript" src="./plugins/code-prettify/src/prettify.js"></script>
        <script type="text/javascript" src="./plugins/js-beautify-1.7.5/js/lib/beautify.js"></script>
        <script type="text/javascript" src="./plugins/js-beautify-1.7.5/js/lib/beautify-html.js"></script>
     <%--   <script type="text/javascript" src="./js/examples.js"></script>--%>
        <script type="text/javascript" data-keditor="script">

            (function ($) {
                $(function () {
                    initModalSource();
                    initModalContent();
                    initToolbar();
                   
                });

                function initToolbar() {
                    var toolbar = $('<div class="toolbar"></div>');
                    var btnViewSource = $('<button type="button" class="view-source"><i class="fa fa-code"></i> Display Design</button>');
                    var btnViewContent = $('<button type="button" class="view-content"><i class="fa fa-file-text-o"></i> Get content</button>');
                    var btnSaveContent = $('<button type="button" class="view-content"><i class="fa fa-file-text-o"></i> Save</button>');
                    var btnBackToList = $('<a href="./" class="view-content"><i class="fa fa-list"></i> Examples list</a>');

                    toolbar.appendTo(document.body);
                    toolbar.append(btnViewSource);
                    toolbar.append(btnViewContent);
                    toolbar.append(btnSaveContent);
                    toolbar.append(btnBackToList);


                    btnViewSource.on('click', function ()
                    {
                        var modal = $('#modal-content');
                        modal.find('.content-html').html(
                            beautifyHtml(
                                $('#content-area').keditor('getContent')

                            )
                        );
                        var varContent = $('#content-area').keditor('getContent');




                        {

                            var obj = { userID: "1" };
                            alert("Get Design");
                            
                            $.ajax({
                                url: '/savecontrent/viewcontentdata',
                                method: 'Post',
                                contentType: 'application/json',
                                data: JSON.stringify(obj),
                                success: function (res) {
                                   // $('#datatodisplay').val(res);
                                     
                                    
                                    $('#divdisply').append(res);
                                   // $('#contentdisplay').val(res);

                                }
                            })
                        }





                    });
                   

                    
                    
                    

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

                            var obj = { varContent: varContent }
                            alert("ajax");
                            alert(varContent);
                            $.ajax({
                                url: '/savecontrent/insertcontentdata',
                                method: 'Post',
                                contentType: 'application/json',
                                data: JSON.stringify(obj),
                                success: function (res) {
                                    alert('Data Inserted Successfully!!!');

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

            })(jQuery);
            $(function () {
                $('#content-area').keditor();
            });
        </script>
    </body>
</html>

