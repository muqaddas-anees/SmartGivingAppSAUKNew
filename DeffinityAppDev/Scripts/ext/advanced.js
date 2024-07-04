Ext.ns('App');

Ext.Loader.setConfig({
    enabled: true,
    disableCaching: true,
    paths: {
        'Gnt': '../../js/Gnt',
        'Sch': '../../../ExtScheduler2.x/js/Sch',
        'MyApp': '.'
    }
});

Ext.require([
    'MyApp.DemoGanttPanel'
]);

Ext.onReady(function () {
    Ext.QuickTips.init();

    App.Gantt.init();
});




var ProjectReference = getQuerystring('ProjectReference');

  var start1 = getQuerystring('start') ;//'01/01/2012'; // Ext.get('ext').dom.value;
  var end1 = getQuerystring('end');// '01/01/2013'//Ext.get('end').dom.value;
  var status = getQuerystring('status');
   var start = new Date(start1);

        var end = new Date(end1);

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
App.Gantt = {

    // Initialize application
    init: function (serverCfg) {
        this.gantt = this.createGantt();

        var vp = Ext.create("Ext.Viewport", {
            layout: 'border',
            items: [
                {
                    region: 'north',
                    contentEl: 'north',
                    bodyStyle: 'padding:1px'
                },
                this.gantt
            ]
        });
    },

    createGantt: function () {

        Ext.define('MyTaskModel', {
            extend: 'Gnt.model.Task',

            // A field in the dataset that will be added as a CSS class to each rendered task element

            fields: [
            { name: 'Name' },
                  { name: 'Id', type: 'int', useNull: true },
                { name: 'StartDate', type: 'date', dateFormat: 'MS' },
                { name: 'EndDate', type: 'date', dateFormat: 'MS' },
                { name: 'Priority', defaultValue: 1 },
                { name: 'parentId', type: 'auto', useNull: true },
                { name: 'index' },
                 { name: 'depth' },
                 { name: 'RAG', defaultValue: 'GREEN' },
                          { name: 'StatusID', defaultValue: 1 },
                          { name: 'QA', defaultValue: 'N' },
                          { name: 'RAGRequired', defaultValue: 'N' },
                            { name: 'ProjectStartDate', type: 'date', dateFormat: 'MS' },
                        { name: 'ProjectEndDate', type: 'date', dateFormat: 'MS' },

                 { name: 'ProjectReference', type: 'int', useNull: true, defaultValue: ProjectReference },
                 { name: 'ListPosition', type: 'int' },
                    { name: 'PID', type: 'int', defaultValue: 0 },
                    { name: 'Notes' },
                    { name: 'AssignedResourceNames' },

                     { name: 'TeamID', defaultValue: 0 },
                { name: 'CategoryID', defaultValue: 0 },
                { name: 'TeamIds' }


            ]
        });

        Ext.define('MyAssignModel', {
            extend: 'Gnt.model.Assignment',

            // A field in the dataset that will be added as a CSS class to each rendered task element

            fields: [
           { name: 'Id' },
        { name: 'ResourceId' },
        { name: 'TaskId' },
        { name: 'Units', type: 'float', defaultValue: 100 },
        { name: 'ProjectReference', type: 'int', useNull: true, defaultValue: ProjectReference }
            ]

        });
        //        var resourceStore = Ext.create("Gnt.data.ResourceStore", {
        //            model: 'Gnt.model.Resource'
        //        });
        var taskStore = Ext.create("Gnt.data.TaskStore", {
            model: 'MyTaskModel',
            autoSync: true,
            autoLoad: true,
            loadMask: true,
            proxy: {
                method: 'GET',
                type: 'ajax',
                headers: { "Content-Type": 'application/json' },
                api: {
                    read: 'webservices/TasksNewVersion.asmx/Get?ProjectReference=' + ProjectReference,
                    create: 'webservices/TasksNewVersion.asmx/Create?ProjectReference=' + ProjectReference,
                    destroy: 'webservices/TasksNewVersion.asmx/Delete',
                    update: 'webservices/TasksNewVersion.asmx/Update'
                },
                writer: {
                    type: 'json',
                    root: 'jsonData',
                    encode: false,
                    allowSingle: false
                },
                reader: {
                    type: 'json',
                    root: function (o) {

                        if (o.d) {
                            return o.d;
                        } else {
                            return o.children;
                        }
                    }
                }
            }
        });

        var mask = new Ext.LoadMask(Ext.getBody(), { store: taskStore });

        mask.show();

        var resourceStore = Ext.create("Gnt.data.ResourceStore", {
            model: 'Gnt.model.Resource',
            autoLoad: true,
            proxy: {
                method: 'GET',
                type: 'ajax',
                headers: { "Content-Type": 'application/json' },
                api: {
                    read: 'webservices/Contractors.asmx/Get?ProjectReference=' + ProjectReference
                    // read: 'webservices/Assignement.asmx/Get12'

                },
                reader: {
                    type: 'json',
                    root: 'd'
                },
                fields: [
                { name: 'Id', type: 'int' },
                { name: 'Name' }
            ]


            }
        });

        var assignmentStore = Ext.create("Gnt.data.AssignmentStore", {
            autoLoad: true,
            // model:'MyAssignModel',
            autoSync: true, // uncomment for sending updates automatically to server  

            // Must pass a reference to resource store
            resourceStore: resourceStore,
            proxy: {

                type: 'ajax',
                headers: { "Content-Type": 'application/json' },
                api: {
                    read: 'webservices/Assignement.asmx/Get?ProjectReference=' + ProjectReference,
                    create: 'webservices/Assignement.asmx/Create?ProjectReference=' + ProjectReference,
                    destroy: 'webservices/Assignement.asmx/Delete'

                },
                reader: {
                    root: 'd',
                    type: 'json'
                },
                writer: {
                    type: 'json',
                    root: 'jsonData',
                    encode: false,
                    allowSingle: false
                }
            }

        });


        var dependencyStore = Ext.create("Gnt.data.DependencyStore", {
            autoLoad: true,
            autoSync: true,
            model: 'Gnt.model.Dependency',
            proxy: {
                type: 'ajax',
                headers: { "Content-Type": 'application/json' },
                method: 'GET',
                reader: {
                    root: 'd',
                    type: 'json'
                },
                writer: {
                    root: 'jsonData',
                    type: 'json',
                    encode: false,
                    allowSingle: false
                },
                api: {
                    read: 'webservices/ProjectDep.asmx/Get?ProjectReference=' + ProjectReference,
                    create: 'webservices/ProjectDep.asmx/Create',
                    destroy: 'webservices/ProjectDep.asmx/Delete',
                    update: 'webservices/ProjectDep.asmx/Update'
                }
            }
        });
        var assignmentEditor = Ext.create('Gnt.widget.AssignmentCellEditor', {
            assignmentStore: assignmentStore,
            resourceStore: resourceStore
        });


        var cellEditing = Ext.create('Sch.plugin.TreeCellEditing', {
            clicksToEdit: 1
        });

        var g = Ext.create("MyApp.DemoGanttPanel", {
            region: 'center',
            selModel: new Ext.selection.TreeModel({ ignoreRightMouseSelection: false, mode: 'MULTI' }),

            resourceStore: resourceStore,
            assignmentStore: assignmentStore,
            taskStore: taskStore,
            dependencyStore: dependencyStore,
            stripeRows: true,
            //snapToIncrement : true,    // Uncomment this line to get snapping behavior for resizing/dragging.
            cls: 'style1',
            loadMask: true,
            startDate: start,
            endDate: Sch.util.Date.add(end, Sch.util.Date.WEEK, 20),
            enableEventDragDrop: true,
            viewPreset: 'weekAndDayLetter',
            lockedViewConfig: {
                plugins: {
                    ptype: 'treeviewdragdrop'
                }
            },



            columns: [

            {
                text: '#',


                width: 40,

                align: 'left',

                dataIndex: 'ListPosition',
                hidden: true

            },

                {
                    xtype: 'treecolumn',
                    header: 'Tasks',
                    sortable: true,
                    dataIndex: 'Name',
                    width: 200,
                    field: {
                        allowBlank: false
                    },
                    renderer: function (v, meta, r) {
                        if (!r.data.leaf) meta.tdCls = 'sch-gantt-parent-cell';

                        return v;
                    }
                },
                {
                    //xtype: 'startdatecolumn',
                    text: 'Start',
                    width: 100,
                    align: 'left',
                    dataIndex: p_start_date(),
                    hidden: Hidden(),


                    editor: getQuerystring('status') == 2 ? false : new Ext.form.DateField({ format: 'd/m/Y' }),
                    renderer: Ext.util.Format.dateRenderer('d/m/Y')
                    // editor: false
                    // editor: 
                },
                {
                    // xtype: 'enddatecolumn',
                    text: 'End',
                    width: 100,
                    align: 'left',
                    dataIndex: p_end_date(),
                    hidden: Hidden(),

                    editor: getQuerystring('status') == 2 ? false : new Ext.form.DateField({ format: 'd/m/Y' }),
                    renderer: Ext.util.Format.dateRenderer('d/m/Y')

                },
                {
                    //xtype: 'startdatecolumn',
                    text: 'Actual Start Date',
                    width: 100,
                    align: 'left',
                    dataIndex: 'StartDate',
                    hidden: Hidden1(),
                    renderer: Ext.util.Format.dateRenderer('d/m/Y'),
                    editor: new Ext.form.DateField({
                        format: 'd/m/Y'
                    })
                },
                {
                    // xtype: 'enddatecolumn',
                    text: 'Actual End Date',
                    width: 100,
                    align: 'left',
                    dataIndex: 'EndDate',
                    hidden: Hidden1(),
                    renderer: Ext.util.Format.dateRenderer('d/m/Y'),
                    editor: new Ext.form.DateField({
                        format: 'd/m/Y'
                    })
                },
                {
                    xtype: 'percentdonecolumn',
                    width: 50
                },
                {
                    xtype: 'resourceassignmentcolumn',
                    header: 'Assigned Resources',
                    width: 200,
                    editor: assignmentEditor,
                    showUnits: false


                },
                 {
                    header: 'Assigned Team',
                    width: 170,
                    dataIndex: 'TeamIds',
                    //editor: teamCombo,
                    editor: teamCombo1,
                    locked: true,
                    renderer: Ext.ux.comboBoxRendererTeam(teamCombo1)

                },             

                         
                {
                    header: 'Category',
                    width: 120,
                    dataIndex: 'CategoryID',
                    editor: categoryCombo,
                    locked: true,
                    renderer: Ext.ux.comboBoxRendererCategory(categoryCombo)

                },
                 {
                     header: 'Status',
                     width: 120,

                     dataIndex: 'StatusID',
                     editor: Combo,
                     locked: true,
                     renderer: Ext.ux.comboBoxRenderer(Combo),
                     handler: function () {
                         Ext.Msg.alert('test');
                     }
                 },
                {
                    header: 'RAG Status',
                    width: 80,
                    dataIndex: 'RAG',
                    editor: new Ext.form.ComboBox({
                        store: ['GREEN', 'RED', 'AMBER'],
                        triggerAction: 'all',
                        mode: 'local'
                    }),
                    locked: true,
                    hidden: true
                }
,
                 {
                     header: 'Checkpoint',
                     width: 60,
                     dataIndex: 'QA',
                     editor: QACombo1,
                     locked: true,
//                     hidden: true,
                     renderer: Ext.ux.comboBoxRenderer1(QACombo1)
                 },
                 {
                     header: 'Task Alert',
                     width: 60,
                     dataIndex: 'RAGRequired',
                     editor: RAGCombo1,
                     locked: true,
                     hidden: true,
                     renderer: Ext.ux.comboBoxRenderer1(RAGCombo1)
                 },
                 {
                     header: 'Notes',
                     width: 200,
                     dataIndex: 'Notes',
                     editor: true

                 }

            ],

            tbar: [
            {
                xtype: 'buttongroup',
                title: 'Alignment',
                columns: 1,

                items: [
            {
                text: 'Indent',
                iconCls: 'indent',
                handler: function () {
                    var sm = g.lockedGrid.getSelectionModel();
                    sm.selected.each(function (t) {
                        t.indent();
                    });
                }
            },
            {
                text: 'Outdent',
                iconCls: 'outdent',
                handler: function () {
                    var sm = g.lockedGrid.getSelectionModel();
                    sm.selected.each(function (t) {
                        t.outdent();
                    });
                }
            }

            ]
            },
            {
                xtype: 'buttongroup',
                title: 'Tasks',
                columns: 1,

                items: [
           {
               text: 'Add new task',
               iconCls: 'icon-add',
               handler: function () {
                   var node = g.getSchedulingView().store.last();

                   if (typeof node === "undefined") {


                       var newTask1 = new taskStore.model({
                           Name: 'New task',
                           leaf: true,
                           PercentDone: 0,
                           StartDate: start,
                           EndDate: Sch.util.Date.add(start, Sch.util.Date.DAY, 1),
                           ProjectStartDate: start,
                           ProjectEndDate: Sch.util.Date.add(start, Sch.util.Date.DAY, 1),
                           Duration: 1,
                           DurationUnit: 'd',
                           ListPosition: 1
                       });
                       g.getRootNode().appendChild(newTask1);
                       g.lockedGrid.view.focusCell({ column: 1, row: cnt });
                   }
                   else {

                       var cnt = g.store.getCount();

                       if (status = 1) {
                           start = node.get('ProjectStartDate');
                       }
                       else {
                           start = node.get('StartDate')
                       }

                       var newTask = new taskStore.model({
                           Name: 'New task',
                           leaf: true,
                           PercentDone: 0,
                           StartDate: start,
                           EndDate: Sch.util.Date.add(start, Sch.util.Date.DAY, 1),
                           ProjectStartDate: start,
                           ProjectEndDate: Sch.util.Date.add(start, Sch.util.Date.DAY, 1),
                           Duration: 1,
                           DurationUnit: 'd',
                           ListPosition: cnt + 1
                       });
                       g.getRootNode().appendChild(newTask);
                       g.lockedGrid.view.focusCell({ column: 1, row: cnt });

                   }
               }
           },
                 {
                     iconCls: 'icon-print',

                     text: 'Print',
                     handler: function () {
                         // Make sure this fits horizontally on one page.


                         //var start12 = new Date(getQuerystring('start'));


                         //g.switchViewPreset('monthAndYear', Ext.Date.add(start12, Ext.Date.MONTH, -1), Ext.Date.add(start12, Ext.Date.YEAR, 5))
                         var totalTimeSpan = g.taskStore.getTotalTimeSpan();
                         g.switchViewPreset('year', Sch.util.Date.add(totalTimeSpan.start, Sch.util.Date.YEAR, -1), Sch.util.Date.add(totalTimeSpan.end, Sch.util.Date.YEAR, 1));
                         g.print();
                     }
                 }


            ]
            },
            {
                xtype: 'buttongroup',
                title: 'View tools',

                columns: 3,
                items: [
                {
                    iconCls: 'icon-prev',
                    text: 'Previous',
                    scope: this,
                    handler: function () {
                        g.shiftPrevious();
                    }
                },
                {
                    iconCls: 'icon-next',
                    text: 'Next',
                    scope: this,
                    handler: function () {
                        g.shiftNext();
                    }
                },
                {
                    text: 'Collapse all',
                    iconCls: 'icon-collapseall',
                    scope: this,
                    handler: function () {
                        g.collapseAll();
                    }
                },
                //                {
                //                    text: 'View full screen',
                //                    iconCls: 'icon-fullscreen',
                //                    disabled: !this._fullScreenFn,
                //                    handler: function () {
                //                        g.showFullScreen();
                //                    },
                //                    scope: this
                //                },
                {
                text: 'Zoom to fit',
                iconCls: 'zoomfit',
                handler: function () {
                    // g.zoomToFit();
                    g.switchViewPreset('monthAndYear');
                },
                scope: this
            },
                {
                    text: 'Expand all',
                    iconCls: 'icon-expandall',
                    scope: this,
                    handler: function () {
                        g.expandAll();
                    }
                }

            ]
        }
            ,
        {
            xtype: 'buttongroup',
            title: 'View resolution',
            columns: 3,
            items: [{
                text: 'Weekly',
                scope: this,
                handler: function () {
                    // g.switchViewPreset('weekAndMonth');

//                    var first = new Date(), last = new Date();


//                    last.setDate(1);
//                    last = Sch.util.Date.add(first, Sch.util.Date.WEEK, 0);
//                    g.switchViewPreset('weekAndDay');
                    //                    g.setTimeSpan(start1, end1);

                    //new changes
                    //g.switchViewPreset('weekAndDayLetter');
                    //g.setTimeSpan(start1, end1);
                    //g.switchViewPreset('weekAndDayLetter', get_start_date(), get_end_date());
                    //g.setTimeSpan(get_start_date(), get_end_date());
                    var totalTimeSpan = g.taskStore.getTotalTimeSpan();
                    g.switchViewPreset('weekAndDayLetter', Sch.util.Date.add(totalTimeSpan.start, Sch.util.Date.WEEK, -1), Sch.util.Date.add(totalTimeSpan.end, Sch.util.Date.WEEK, 1));
                }
            },
                {
                    text: 'Monthly',
                    scope: this,
                    handler: function () {
                        //g.switchViewPreset('weekAndDayLetter');

//                        var first = new Date(), last = new Date();

//                        //first = first.clone();
//                        first.setDate(1);
//                        first = Sch.util.Date.add(first, Sch.util.Date.MONTH, 0);
//                        //last = last.clone();
//                        last.setDate(1);
//                        last = Sch.util.Date.add(last, Sch.util.Date.MONTH, 1);



//                        g.switchViewPreset('weekAndMonth');
//                        g.setTimeSpan(first, last);


                        //new changes

                        //g.switchViewPreset('monthAndYear');
                        //g.switchViewPreset('monthAndYear', get_start_date(), get_end_date());
                        //g.setTimeSpan(get_start_date(), get_end_date());
                        var totalTimeSpan = g.taskStore.getTotalTimeSpan();
                        g.switchViewPreset('monthAndYear', Sch.util.Date.add(totalTimeSpan.start, Sch.util.Date.MONTH, -1), Sch.util.Date.add(totalTimeSpan.end, Sch.util.Date.MONTH, 1));
                    }
                },
                {
                    text: 'Quarterly',
                    scope: this,
                    handler: function () {
//                        var start = new Date(g.getStart().getFullYear(), 0);
//                        //g.switchViewPreset('Year');
//                        // g.switchViewPreset('monthAndYear', start, Ext.Date.add(start, Ext.Date.YEAR, 5));
//                        var first = new Date(), last = new Date();

//                        // first = first.clone();
//                        first.setDate(0);
//                        first = Sch.util.Date.add(first, Sch.util.Date.MONTH, 1);
//                        // last = last.clone();
//                        last.setDate(0);
//                        last = Sch.util.Date.add(last, Sch.util.Date.MONTH, 2);



//                        g.switchViewPreset('year');
                        //                        g.setTimeSpan(first, last);

                        //new changes
                        //g.switchViewPreset('year', get_start_date(), get_end_date());
                        //g.setTimeSpan(get_start_date(), get_end_date());
                        var totalTimeSpan = g.taskStore.getTotalTimeSpan();
                        g.switchViewPreset('year', Sch.util.Date.add(totalTimeSpan.start, Sch.util.Date.YEAR, -1), Sch.util.Date.add(totalTimeSpan.end, Sch.util.Date.YEAR, 1));

                    }
                },
                {

                    text: '1 year',
                    scope: this,
                    handler: function () {
                        //g.switchViewPreset('monthAndYear');

                        //g.switchViewPreset('year', start, Ext.Date.add(start, Ext.Date.YEAR, 3));
                        var totalTimeSpan = g.taskStore.getTotalTimeSpan();
                        g.switchViewPreset('year', Sch.util.Date.add(totalTimeSpan.start, Sch.util.Date.YEAR, -1), Sch.util.Date.add(totalTimeSpan.end, Sch.util.Date.YEAR, 1));
                    }
                }
                ,
                     {

                         text: 'Normal View',
                         scope: this,
                         handler: function () {
                             //g.switchViewPreset('weekAndDayLetter', this.getStart(), this.getEnd());
                             //g.setTimeSpan(start1, end1);
                             //g.setTimeSpan(get_start_date(), get_end_date());
                             var totalTimeSpan = g.taskStore.getTotalTimeSpan();
                             g.switchViewPreset('weekAndDayLetter', Sch.util.Date.add(totalTimeSpan.start, Sch.util.Date.WEEK, -1), Sch.util.Date.add(totalTimeSpan.end, Sch.util.Date.WEEK, 1));
                         }
                     }
            ]
        },
         {
             xtype: 'buttongroup',
             title: 'Options',
             columns: 2,
             items: [
                {
                    text: 'Highlight critical path',
                    iconCls: 'togglebutton',
                    scope: this,
                    enableToggle: true,
                    handler: function (btn) {
                        var v = g.getSchedulingView();
                        if (btn.pressed) {
                            v.highlightCriticalPaths(true);
                        } else {
                            v.unhighlightCriticalPaths(true);
                        }
                    }
                },
                {
                    iconCls: 'action',
                    text: 'Highlight tasks longer than 7 days',
                    scope: this,
                    handler: function (btn) {
                        taskStore.getRootNode().cascadeBy(function (task) {
                            if (Sch.util.Date.getDurationInDays(task.get('StartDate'), task.get('EndDate')) > 7) {
                                var el = g.getSchedulingView().getElementFromEventRecord(task);
                                el && el.frame('lime');
                            }
                        }, this);
                    }
                },

                {
                    iconCls: 'action',
                    text: 'Scroll to last task',
                    scope: this,

                    handler: function (btn) {
                        var latestEndDate = new Date(0),
                            latest;
                        taskStore.getRootNode().cascadeBy(function (task) {
                            if (task.get('EndDate') >= latestEndDate) {
                                latestEndDate = task.get('EndDate');
                                latest = task;
                            }
                        });
                        g.getSchedulingView().scrollEventIntoView(latest, true);
                    }
                },
                  {
                      xtype: 'textfield',
                      emptyText: 'Search for task...',
                      scope: this,
                      width: 150,
                      enableKeyEvents: true,
                      listeners: {
                          keyup: {
                              fn: function (field, e) {
                                  var value = field.getValue();

                                  if (value) {
                                      this.taskStore.filter('Name', field.getValue(), true, false);
                                  } else {
                                      this.taskStore.clearFilter();
                                  }
                              },
                              scope: this
                          },
                          specialkey: {
                              fn: function (field, e) {
                                  if (e.getKey() === e.ESC) {
                                      field.reset();
                                  }
                                  this.taskStore.clearFilter();
                              },
                              scope: this
                          }
                      }
                  }]
         }

        ]



    });


    return g;
}
};

Ext.ux.comboBoxRenderer = function (combo) {
    return function (value) {
    
        var idx = combo.store.find(combo.valueField, value);
        var rec = combo.store.getAt(idx);
        return rec.get(combo.displayField);
    };
}



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
    idIndex:0,
    fields:['id','name'],
    data: [
    ['Y', 'Yes'],
    ['N','No']
    ]
    }),
    triggerAction:'all',
    valueField: 'id',
    displayField: 'name',
    mode: 'local'
});

var RAGCombo1 = new Ext.form.ComboBox(
{
    store: new Ext.data.ArrayStore({
    idIndex:0,
    fields:['id','name'],
    data: [
    ['Y', 'Yes'],
    ['N','No']
    ]
    }),
    triggerAction:'all',
    valueField: 'id',
    displayField: 'name',
    mode: 'local'
});



Ext.ux.comboBoxRenderer1 = function (combo) {
    return function (value) {
    
        if (value == 0) {
            value = 'N';
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
		            url: 'webservices/Contractors.asmx/GetTeamList1',
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
		            url: 'webservices/TasksNewVersion.asmx/GetTaskCategoryList',
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
        // debugger;
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