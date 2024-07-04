Ext.define("MyApp.DemoGanttPanel", {
    extend: "Gnt.panel.Gantt",
    requires: [
       'Gnt.plugin.TaskContextMenu',
'Gnt.column.StartDate',
'Gnt.column.EndDate',
'Gnt.column.Duration',
'Gnt.column.PercentDone',
'Sch.plugin.TreeCellEditing',
'Sch.plugin.Pan',
'Gnt.panel.Gantt',
'Gnt.widget.AssignmentCellEditor',
'Gnt.column.ResourceAssignment',
'Gnt.model.Assignment',
'Gnt.plugin.DependencyEditor',
'Gnt.plugin.Printable'
    ],
    rightLabelField: 'Responsible',
    highlightWeekends: true,
    showTodayLine: true,
    loadMask: true,
    enableProgressBarResize: true,
    bodyCls: 'gantt-body',

    initComponent: function () {

        Ext.apply(this, {
            // Experimental
            layout: 'border',

            // Experimental
            lockedGridConfig: {
                split: true,
                animCollapse: false,
                region: 'west',
                width: 620,
                collapseDirection: 'left',
                collapsible: false,
                listeners: {
                    beforeedit: function (obj) {
                        debugger;
                        if (obj.record.data.PercentDone == 0) {
                            //Ext.Msg.alert('Alert','message');
                           // return false;
                        }
                    }
                }
            },

            // Experimental
            schedulerConfig: {
                scroll: true,
                border: false,
                animCollapse: false,
                region: 'center',
                collapseDirection: 'right',
                collapsible: false,
                listeners: {
                    beforecollapse: function () {
                        this.lastLockedWidth = this.lockedGrid.getWidth();
                        this.lockedGrid.setWidth(this.getWidth() - 35);
                    },
                    expand: function () {
                        this.lockedGrid.setWidth(this.lastLockedWidth);
                    },
                    scope: this
                }
            },

            leftLabelField: {
                dataIndex: 'Name',
                editor: { xtype: 'textfield' }
            },

            // Add some extra functionality
            plugins: [
                Ext.create("Gnt.plugin.TaskContextMenu"),
                Ext.create("Sch.plugin.Pan"),
                Ext.create('Sch.plugin.TreeCellEditing', { clicksToEdit: 2 }),
                 this.depEditor = new Gnt.plugin.DependencyEditor({
                     showLag: false,
                     constrain: true,

                     buttons: [
                        {
                            text: 'Ok',
                            scope: this,
                            handler: function () {
                                var formPanel = this.depEditor;
                                formPanel.getForm().updateRecord(formPanel.dependencyRecord);
                                this.depEditor.collapse();
                            }
                        },
                        {
                            text: 'Cancel',
                            scope: this,
                            handler: function () {
                                this.depEditor.collapse();
                            }
                        },
                        {
                            text: 'Delete',
                            scope: this,
                            handler: function () {
                                var formPanel = this.depEditor,
                                    record = this.depEditor.dependencyRecord;

                                record.stores[0].remove(record);
                                formPanel.collapse();
                            }
                        }
                    ]
                 }),
                  new Gnt.plugin.Printable({
                      printRenderer: function (task, tplData) {
                          if (task.isMilestone()) {
                              return;
                          } else if (task.isLeaf()) {
                              var availableWidth = tplData.width - 4,
                        progressWidth = Math.floor(availableWidth * task.get('PercentDone') / 100);

                              return {
                                  // Style borders to act as background/progressbar
                                  progressBarStyle: Ext.String.format('width:{2}px;border-left:{0}px solid #7971E2;border-right:{1}px solid #E5ECF5;', progressWidth, availableWidth - progressWidth, availableWidth)
                              };
                          } else {
                              var availableWidth = tplData.width - 2,
                        progressWidth = Math.floor(availableWidth * task.get('PercentDone') / 100);

                              return {
                                  // Style borders to act as background/progressbar
                                  progressBarStyle: Ext.String.format('width:{2}px;border-left:{0}px solid #FFF3A5;border-right:{1}px solid #FFBC00;', progressWidth, availableWidth - progressWidth, availableWidth)
                              };
                          }
                      },

                      beforePrint: function (sched) {
                          var v = sched.getSchedulingView();
                          this.oldRenderer = v.eventRenderer;
                          this.oldMilestoneTemplate = v.milestoneTemplate;
                          v.milestoneTemplate = printableMilestoneTpl;
                          v.eventRenderer = this.printRenderer;
                      },

                      afterPrint: function (sched) {
                          var v = sched.getSchedulingView();
                          v.eventRenderer = this.oldRenderer;
                          v.milestoneTemplate = this.oldMilestoneTemplate;
                      }
                  })],
            // Define an HTML template for the tooltip
            tooltipTpl: new Ext.XTemplate(
                '<h4 class="tipHeader">{Name}</h4>',
                '<table width="100%">',   //class="taskTip"(removed)
                    '<tr><td>Start:</td> <td align="right">{[Ext.Date.format(values.StartDate, "d/m/Y")]}</td></tr>',
                    '<tr><td>End:</td> <td align="right">{[Ext.Date.format(values.EndDate, "d/m/Y")]}</td></tr>',
                    '<tr><td>Progress:</td><td align="right">{PercentDone}%</td></tr>',
                    '<tr><td>Notes:</td><td align="right">{Notes}</td></tr>',
                    '<tr><td>Resources:</td><td align="right">{AssignedResourceNames}</td></tr>',
                    
                '</table>'
            ).compile()

            // Define the static columns


            // Define the buttons that are available for user interaction
           // tbar: this.createToolbar()
        });

        this.callParent(arguments);
    },

    createToolbar: function () {
        return [{
            xtype: 'buttongroup',
            title: 'View tools',
            columns: 3,
            items: [
                {
                    iconCls: 'icon-prev',
                    text: 'Previous',
                    scope: this,
                    handler: function () {
                        this.shiftPrevious();
                    }
                },
                {
                    iconCls: 'icon-next',
                    text: 'Next',
                    scope: this,
                    handler: function () {
                        this.shiftNext();
                    }
                },
                {
                    text: 'Collapse all',
                    iconCls: 'icon-collapseall',
                    scope: this,
                    handler: function () {
                        this.collapseAll();
                    }
                },
                {
                    text: 'View full screen',
                    iconCls: 'icon-fullscreen',
                    disabled: !this._fullScreenFn,
                    handler: function () {
                        this.showFullScreen();
                    },
                    scope: this
                },
                {
                    text: 'Zoom to fit',
                    iconCls: 'zoomfit',
                    handler: function () {
                        this.zoomToFit();
                    },
                    scope: this
                },
                {
                    text: 'Expand all',
                    iconCls: 'icon-expandall',
                    scope: this,
                    handler: function () {
                        this.expandAll();
                    }
                }
            ]
        },
        {
            xtype: 'buttongroup',
            title: 'View resolution',
            columns: 3,
            items: [{
                text: '6 weeks',
                scope: this,
                handler: function () {
                    this.switchViewPreset('weekAndMonth');
                }
            },
                {
                    text: '10 weeks',
                    scope: this,
                    handler: function () {
                        this.switchViewPreset('weekAndDayLetter');
                    }
                },
                {
                    text: '1 year',
                    scope: this,
                    handler: function () {
                        this.switchViewPreset('monthAndYear');
                    }
                },
                {
                    text: '5 years',
                    scope: this,
                    handler: function () {
                        var start = new Date(this.getStart().getFullYear(), 0);

                        this.switchViewPreset('monthAndYear', start, Ext.Date.add(start, Ext.Date.YEAR, 5));
                    }
                },
                {
                    text: 'Indent',
                    iconCls: 'indent',
                    handler: this.int()
                },
            {
                text: 'Outdent',
                iconCls: 'outdent',
                handler: this.out()
            }
            ]
        },
            {
                xtype: 'buttongroup',
                title: 'Set percent complete',
                columns: 5,
                defaults: { scale: "large" },
                items: [{
                    text: '0%<div class="percent percent0"></div>',
                    scope: this,
                    handler: function () {
                        this.applyPercentDone(0);
                    }
                },
                    {
                        text: '25%<div class="percent percent25"><div></div></div>',
                        scope: this,
                        handler: function () {
                            this.applyPercentDone(25);
                        }
                    },
                    {
                        text: '50%<div class="percent percent50"><div></div></div>',
                        scope: this,
                        handler: function () {
                            this.applyPercentDone(50);
                        }
                    },
                    {
                        text: '75%<div class="percent percent75"><div></div></div>',
                        scope: this,
                        handler: function () {
                            this.applyPercentDone(75);
                        }
                    },
                    {
                        text: '100%<div class="percent percent100"><div></div></div>',
                        scope: this,
                        handler: function () {
                            this.applyPercentDone(100);
                        }
                    }
                ]
            },
            '->',
            {
                xtype: 'buttongroup',
                title: 'Try some features...',
                columns: 3,
                items: [
                {
                    text: 'Highlight critical chain',
                    iconCls: 'togglebutton',
                    scope: this,
                    enableToggle: true,
                    handler: function (btn) {
                        var v = this.getSchedulingView();
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
                        this.taskStore.getRootNode().cascadeBy(function (task) {
                            if (Sch.util.Date.getDurationInDays(task.get('StartDate'), task.get('EndDate')) > 7) {
                                var el = this.getSchedulingView().getElementFromEventRecord(task);
                                el && el.frame('lime');
                            }
                        }, this);
                    }
                },
                {
                    iconCls: 'togglebutton',
                    text: 'Filter: Tasks with progress < 30%',
                    scope: this,
                    enableToggle: true,
                    toggleGroup: 'filter',
                    handler: function (btn) {
                        if (btn.pressed) {
                            this.taskStore.filter(function (task) {
                                return task.get('PercentDone') < 30;
                            });
                        } else {
                            this.taskStore.clearFilter();
                        }
                    }
                },
                {
                    iconCls: 'togglebutton',
                    text: 'Cascade changes',
                    scope: this,
                    enableToggle: true,
                    handler: function (btn) {
                        this.setCascadeChanges(btn.pressed);
                    }
                },
                {
                    iconCls: 'action',
                    text: 'Scroll to last task',
                    scope: this,

                    handler: function (btn) {
                        var latestEndDate = new Date(0),
                            latest;
                        this.taskStore.getRootNode().cascadeBy(function (task) {
                            if (task.get('EndDate') >= latestEndDate) {
                                latestEndDate = task.get('EndDate');
                                latest = task;
                            }
                        });
                        this.getSchedulingView().scrollEventIntoView(latest, true);
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
        ];
    },

    applyPercentDone: function (value) {
        this.getSelectionModel().selected.each(function (task) { task.setPercentDone(value); });
    },

    showFullScreen: function () {
        this.el.down('.x-panel-body').dom[this._fullScreenFn]();
    },

    _fullScreenFn: (function () {
        var docElm = document.documentElement;

        if (docElm.requestFullscreen) {
            return "requestFullscreen";
        }
        else if (docElm.mozRequestFullScreen) {
            return "mozRequestFullScreen";
        }
        else if (docElm.webkitRequestFullScreen) {
            return "webkitRequestFullScreen";
        }
    })()
});

var printableMilestoneTpl = new Gnt.template.Milestone({
    prefix: 'foo',
    printable: true,
    imgSrc: 'images/milestone.png'
});