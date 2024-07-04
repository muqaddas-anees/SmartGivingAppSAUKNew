(function ($) {
    $(function () {
        initModalSource();
        initModalContent();
        initToolbar();
    });

    function initToolbar() {
        var toolbar = $('<div class="toolbar"></div>');
        var btnViewSource = $('<button type="button" class="view-source"><i class="fa fa-code"></i> View source</button>');
        var btnViewContent = $('<button type="button" class="view-content"><i class="fa fa-file-text-o"></i> Get content</button>');
        var btnBackToList = $('<a href="./" class="view-content"><i class="fa fa-list"></i> Examples list</a>');
        var save = $('<a href="./" class="view-content"><i class="fa fa-save"></i> save</a>');
        toolbar.appendTo(document.body);
        toolbar.append(btnViewSource);
        toolbar.append(btnViewContent);
        toolbar.append(btnBackToList);
        toolbar.append(save);
        btnViewSource.on('click', function () {
             
            $('#modal-source').modal('show');
        });

        btnViewContent.on('click', function () {
            var modal = $('#modal-content');
            modal.find('.content-html').html(
                beautifyHtml(
                    $('#content-area').keditor('getContent')
                )
            );
            var varContent = $('#content-area').keditor('getContent');
            modal.modal('show');

        });

        save.on('click', function () {

            alert('Save');
            var modal = $('#modal-content');
            modal.find('.content-html').html(
                beautifyHtml(
                    $('#content-area').keditor('getContent')
                )
            );

            var varContent = $('#content-area').keditor('getContent');
            alert("r");
           
            alert(varContent);
            textToWrite = varContent;
            // put the data in a Blob object...
            var textFileAsBlob = new Blob([textToWrite], { type: 'text/html' });
            // create a hyperlink <a> element tag that will be automatically clicked below...
            var downloadLink = document.createElement("a");
            // set the file name...
            downloadLink.download = "fileName.html";
            // set the <a> tag href as a URL to point to the Blob
            downloadLink.href = window.webkitURL.createObjectURL(textFileAsBlob);
            // automaitcally click the <a> element to go to the URL to save the textFileAsBlob...
            var url = window.location.pathname;
            var folderpath = url.substring(0, url.lastIndexOf('/') );
            alert(url);
            alert(folderpath);
            downloadLink.click();
           
           
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
        debugger;
        var htmlInclude = $('<div />').html($('[data-keditor="html-include"]').clone()).html();
        debugger;
        htmlInclude = htmlInclude.replace('data-keditor="html-include"', '');
        htmlInclude = htmlInclude.replace(/\</g, '&lt;').replace(/\>/g, '&gt;');
        debugger;
        modal.find('.source-html').html(beautifyHtml(htmlCode + htmlInclude));

        var jsCode = $('[data-keditor="script"]').html();
        jsCode =  jsCode.replace(/\</g, '&lt;').replace(/\>/g, '&gt;');
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