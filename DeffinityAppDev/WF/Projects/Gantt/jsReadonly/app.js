//set default date format
Ext.Date.defaultFormat = 'd/m/Y';

Ext.application({
    appFolder: 'jsReadonly/advanced',
    name                : 'Gnt.examples.advanced',
    extend              : 'Gnt.examples.advanced.Application',
    autoCreateViewport  : false,
    // force to get startDate based on the first task start
    startDate: null
});


//var mask = new Ext.LoadMask(Ext.getBody(), { store: 'Gnt.data.TaskStore' });

//mask.show();

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





Ext.ux.comboBoxRenderer = function (combo) {
    return function (value) {

        var idx = combo.store.find(combo.valueField, value);
        var rec = combo.store.getAt(idx);
        return rec.get(combo.displayField);
    };
}

