Ext.define('Gnt.examples.advanced.Application', {
    extend          : 'Ext.app.Application',

    mixins          : ['Gnt.mixin.Localizable'],

    requires        : [
        'Sch.app.CrudManagerDomain',
        'Gnt.examples.advanced.locale.En',
        'Gnt.examples.advanced.store.Calendars',
        'Gnt.examples.advanced.store.Tasks',
        'Gnt.examples.advanced.crud.CrudManager',
        'Gnt.data.undoredo.Manager',
        'Ext.window.MessageBox'
    ],

    namespaces      : 'Gnt.examples.advanced',

    paths           : {
        'Gnt'   : './js/Gnt',
        'Sch'   : './js/Sch'
    },

    stores          : [
        'Locales'
    ],

    views           : [
        'MainViewport'
    ],

    routes          : {
        ':lc'   : {
            before  : 'onBeforeLocaleEstablished',
            action  : 'onLocaleEstablished'
        }
    },

    defaultToken    : 'en',

    listen          : {
        // Right now we just listen to locale-change on controllers domain, any controller fired that event might
        // initiate a locale change procedure
        controller : {
            '*' : {
                'locale-change' : 'onLocaleChange'
            }
        },

        crudmanager : {
            'advanced-crudmanager' : {
                'loadfail' : 'onCrudError',
                'syncfail' : 'onCrudError'
            }
        }
    },

    glyphFontFamily : 'FontAwesome',

    mainView        : null,

    crudManager     : null,

    undoManager     : null,

    currentLocale: 'en',
    loadMask: true,
    isMasked:true,

    /**
     * Mapping for the startDate config of the gantt panel
     */
    startDate       : start, //null,

    /**
     * Mapping for the endDate config of the gantt panel
     */
    endDate         : end,// null,

    constructor : function (config) {
        var me = this;

        me.crudManager = new Gnt.examples.advanced.crud.CrudManager({
            taskStore : new Gnt.examples.advanced.store.Tasks({
                calendarManager : new Gnt.examples.advanced.store.Calendars()
            })
        });

        // Creating undo/redo manager
        me.undoManager = new Gnt.data.undoredo.Manager({
            transactionBoundary : 'timeout',
            stores              : [
                me.crudManager.getCalendarManager(),
                me.crudManager.getTaskStore(),
                me.crudManager.getResourceStore(),
                me.crudManager.getAssignmentStore(),
                me.crudManager.getDependencyStore()
            ]
        });

        this.callParent(arguments);

        //printdialog is stateful
        Ext.state.Manager.setProvider(Ext.create('Ext.state.CookieProvider'));
    },

    /**
     * This method will be called on CRUD manager exception and display a message box with error details.
     */
    onCrudError : function (crud, response, responseOptions) {
        //Ext.Msg.show({
        //    title    : this.L('error'),
        //    msg      : response.message || this.L('requestError'),
        //    icon     : Ext.Msg.ERROR,
        //    buttons  : Ext.Msg.OK,
        //    minWidth : Ext.Msg.minWidth
        //});
    },

    /**
     * When we've got a request to change locale we simply use redirectTo() for locale changing route handlers
     * fired, which in their turn know how to properly change locale.
     */
    onLocaleChange : function (lc, lcRecord) {
        this.redirectTo(lc);
    },

    /**
     * This method will be executed upon location has change and upon application startup with default token in case
     * location hash is empty. This method is called *before* corresponding route change action handler, and it's
     * cappable of stopping/resument the switch action, thus we use it to load locale required script files.
     */
    onBeforeLocaleEstablished : function (lc, action) {
        var me          = this,
            lcRecord    = me.getLocalesStore().getById(lc);

        switch (true) {
            case lcRecord && !me.mainView && me.currentLocale != lc:

                Ext.Loader.loadScript({
                    // load Ext JS locale for the chosen language
                    url     : 'http://www.bryntum.com/examples/extjs-6.0.1/build/classic/locale/locale-' + lc + '.js',
                    onLoad  : function() {
                        var cls = lcRecord.get('cls');
                        // load the gantt locale for the chosen language
                        Ext.require('Gnt.examples.advanced.locale.' + cls, function () {
                            // Some of Ext JS localization wrapped with Ext.onReady(...)
                            // so we have to do the same to instantiate UI after Ext JS localization is applied
                            Ext.onReady(function() { action.resume(); });
                        });
                    }
                });

                break;

            case lcRecord && !me.mainView && me.currentLocale == lc:

                action.resume();
                break;

            case lcRecord && me.mainView && true:

                // Main view is already created thus we have to execute hard reload otherwise locale related
                // scripts won't be properly applied.
                me.deactivate();
                action.stop();
                window.location.hash = '#' + lc;
                window.location.reload(true);
                break;

            default:
                action.stop();
        }
    },

    /**
     * Since we are supporting such locale management we can't use application's autoCreateViewport option, since
     * we have to load all required locale JS files before any GUI creation. Loading is done in the 'before' handler,
     * so here in 'action' handler we are ready to create our main view.
     */
    onLocaleEstablished: function (lc) {
       
        var me      = this,
            crud    = me.crudManager,
            undo    = me.undoManager;

        me.currentLocale    = lc;
       
        me.mainView         = me.getMainViewportView().create({
            viewModel       : {
                type        : 'advanced-viewport',
                data        : {
                    crud                : crud,
                    undoManager         : undo,
                    taskStore           : crud.getTaskStore(),
                    calendarManager     : crud.getCalendarManager(),
                    currentLocale       : me.currentLocale,
                    availableLocales    : me.getLocalesStore()
                }
            },
            crudManager     : crud,
            undoManager     : undo,
            startDate       : me.startDate,
            endDate         : me.endDate
        });
    }
});





var Combo = new Ext.form.ComboBox({

    store: new Ext.data.ArrayStore({
        idIndex: 0,
        fields: [{ name: 'id', type: 'int' }, 'name'],
        data: [
				    [1, 'Pending'],
				    [2, 'In Progress'],
				    [3, 'Completed'],
				    [4, 'Awaiting 3rd Party']

        ]
    }),
    triggerAction: 'all',
    valueField: 'id',
    displayField: 'name',
    mode: 'local'
});

var QACombo1 = new Ext.form.ComboBox(
{
    store: new Ext.data.ArrayStore({
        idIndex: 0,
        fields: ['id', 'name'],
        data: [
        ['Y', 'Yes'],
        ['N', 'No']
        ]
    }),
    triggerAction: 'all',
    valueField: 'id',
    displayField: 'name',
    mode: 'local'
});

var RAGCombo1 = new Ext.form.ComboBox(
{
    store: new Ext.data.ArrayStore({
        idIndex: 0,
        fields: ['id', 'name'],
        data: [
        ['Y', 'Yes'],
        ['N', 'No']
        ]
    }),
    triggerAction: 'all',
    valueField: 'id',
    displayField: 'name',
    mode: 'local'
});



Ext.ux.comboBoxRenderer1 = function (combo) {
    return function (value) {
        
        if (value == null) {
            value = 'N';
        }

        if (value == 0) {
            value = 'N';
        }

        if (value.trim() == 'N') {
            value = 'N';
        }
       
        if (value.trim() == 'Y') {
            value = 'Y';
        }
        var idx = combo.store.find(combo.valueField, value);
        var rec = combo.store.getAt(idx);
        return rec.get(combo.displayField);
    };
}
var teamCombo1 = Ext.create('Ext.ux.CheckCombo',
		{
		    // renderTo: 'checkcombowithoutall',
		    valueField: 'id',
		    displayField: 'type',
		    store: new Ext.data.JsonStore({
		        idIndex: 0,
		        autoLoad: true,
		        proxy: new Ext.data.HttpProxy({
		            type: 'ajax',
		            headers: { "Content-Type": 'application/json' },
		            url: '../webservices/Contractors.asmx/GetTeamList1',
		            method: 'GET',
		            reader: {
		                root: 'd',
		                type: 'json'
		            }

		        }),
		        fields: ['id', 'type']
		    })
		});
var categoryCombo = new Ext.form.ComboBox({
    width: 100,

    store: new Ext.data.JsonStore({
        idIndex: 0,
        autoLoad: true,
        proxy: new Ext.data.HttpProxy({
            type: 'ajax',
            headers: { "Content-Type": 'application/json' },
            url: '../webservices/TasksNewVersion.asmx/GetTaskCategoryList',
            method: 'GET',

            reader: {
                root: 'd',
                type: 'json'
            }

        }),
        fields: [{ name: 'id', type: 'int' }, 'name']
    }),
    //              store:new Ext.data.ArrayStore({
    //                 idIndex:0,
    //                 fields:[{ name:'id', type:'int'}, 'name'],
    //                 data:[
    //                    [1, 'Mike'],
    //                    [2, 'Dinesh'],
    //                    [3, 'Indra'],
    //                    [4, 'Nadeem'],
    //                    [5, 'Anna']
    //                 ]
    //              }),
    triggerAction: 'all',
    valueField: 'id',
    displayField: 'name',
    mode: 'local'
});
Ext.ux.comboBoxRendererCategory = function (combo) {
    return function (value) {

        if (typeof value != "undefined") {
            if (value != 0) {
                var idx = combo.store.find(combo.valueField, value);
                if (idx < 0)
                { return ''; }
                else {
                    var rec = combo.store.getAt(idx);

                    return rec.get(combo.displayField);
                }
            }
            else
                return '';
        }
        else
            return '';
    };
}
//function for disable the editing 
function Disable() {
    var status = getQuerystring('status');
    // var s ='new Ext.form.DateField({format: ''d/m/Y'' })'
    if (status == 2) {
        return false;
    }
    else {
        return true;
    }
}
//function for hidden  the columns 
function Hidden() {
    var status = getQuerystring('status');
    if (status == 2) {
        return true;
    }
    else {
        return false;
    }

}
function Hidden1() {
    var status = getQuerystring('status');
    if (status != 2) {
        return true;
    }
    else {
        return false;
    }

}

function p_end_date() {
    var status = getQuerystring('status');
    if (status == 1) {
        return 'EndDate';
    }
    else {
        return 'EndDate';
    }

}

function p_start_date() {
    var status = getQuerystring('status');
    if (status == 1) {
        return 'StartDate';
    }
    else {
        return 'StartDate';
    }

}
function get_start_date() {

    var start1 = getQuerystring('start');
    var start = new Date(start1);
    return start;
}
function get_end_date() {
    var end1 = getQuerystring('end');
    var end = new Date(end1);
    return end;
}
Ext.ux.comboBoxRendererTeam = function (combo) {
    return function (value) {
        
        var rval = ''
        if (value != '') {
            var res = value.split(",");
            for (var i = 0; i < res.length; i++) {
                var idx = combo.store.find(combo.valueField, res[i]);
                if (idx >= 0) {
                    var rec = combo.store.getAt(idx);
                    rval = rval + rec.get(combo.displayField);
                    if (i != res.length - 1)
                        rval = rval + ', ';
                }
            }
            return rval;

        }
        else
            return '';
    };
}


var Combo = new Ext.form.ComboBox({

    store: new Ext.data.ArrayStore({
        idIndex: 0,
        fields: [{ name: 'id', type: 'int' }, 'name'],
        data: [[1, 'Pending'], [2, 'In Progress'], [3, 'Completed'], [4, 'Awaiting 3rd Party']]
    }),
    triggerAction: 'all',
    valueField: 'id',
    displayField: 'name',
    mode: 'local',
    xtype: 'combobox'
});



Ext.ux.comboBoxRenderer = function (combo) {
    return function (value) {
        
        var idx = combo.store.find(combo.valueField, value);
        var rec = combo.store.getAt(idx);
        //(idx === -1) ? "" :rec.get(combo.displayField);
        return (idx === -1) ? "" : rec.get(combo.displayField);
    };
}
