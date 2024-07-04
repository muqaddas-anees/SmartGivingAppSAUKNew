var start1 = getQuerystring('start');//'01/01/2012'; // Ext.get('ext').dom.value;
var end1 = getQuerystring('end');// '01/01/2013'//Ext.get('end').dom.value;
var status = getQuerystring('status');
var start = new Date(start1);
var end = new Date(end1);
var ProjectReference = getQuerystring('project');

Ext.Ajax.timeout = 120000;
//set default date format
Ext.Date.defaultFormat = 'd/m/Y';

Ext.application({
    appFolder           : 'js/advanced',
    name                : 'Gnt.examples.advanced',
    extend              : 'Gnt.examples.advanced.Application',
    autoCreateViewport  : false,
    // force to get startDate based on the first task start
    startDate           : start // null
});

function getQuerystring(key, default_) {
    
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}

function getQuerystringByID(key, default_,qid) {

    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[qid];
}

Ext.ux.comboBoxRenderer = function (combo) {
    return function (value) {
        var idx = combo.store.find(combo.valueField, value);
        var rec = combo.store.getAt(idx);
        return rec.get(combo.displayField);
    };
}



//ajaxProcessingCount = 0;

//handleRequestError = function (errorMessage) {
//    Ext.getBody().unmask();

//    if (Ext.Msg.isVisible()) {
//        Ext.Msg.hide();
//    }
//};

//Ext.Ajax.on('beforerequest', function (conn, options, eOpts) {
//    var message = 'Loading...';

//    ajaxProcessingCount += 1;

//    if (Ext.getBody()) {
//        Ext.getBody().mask(message);
//    }
//});

//Ext.Ajax.on('requestcomplete', function (conn, response, options, eOpts) {
//    ajaxProcessingCount -= 1;

//    // Your code here...

//    if (ajaxProcessingCount === 0 && Ext.getBody()) {
//        Ext.getBody().unmask();
//    }
//});

//Ext.Ajax.on('requestexception', function (conn, response, options, eOpts) {
//    ajaxProcessingCount -= 1;

//    // Your code here...

//    if (ajaxProcessingCount === 0 && Ext.getBody()) {
//        Ext.getBody().unmask();
//    }
//});
