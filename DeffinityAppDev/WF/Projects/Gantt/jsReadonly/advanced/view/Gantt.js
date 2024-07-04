Ext.define('Gnt.examples.advanced.view.Gantt', {
    extend : 'Gnt.panel.Gantt',
    xtype  : 'advanced-gantt',

    requires : [
        'Ext.form.field.Text',
        'Ext.form.field.ComboBox',
        'Sch.plugin.TreeCellEditing',
        'Sch.plugin.Pan',
        'Gnt.plugin.taskeditor.TaskEditor',
        'Gnt.plugin.taskeditor.ProjectEditor',
        'Gnt.column.Sequence',
        'Gnt.column.Name',
        'Gnt.column.StartDate',
        'Gnt.column.EndDate',
        'Gnt.column.Duration',
        'Gnt.column.ConstraintType',
        'Gnt.column.ConstraintDate',
        'Gnt.column.PercentDone',
        'Gnt.column.Predecessor',
        'Gnt.column.ManuallyScheduled',
        'Gnt.column.AddNew',
        'Gnt.column.DeadlineDate',
        // @cut-if-gantt->
        'Gnt.plugin.Printable',
        'Gnt.column.ShowInTimeline',
        // <-@
        'Gnt.examples.advanced.plugin.TaskArea',
        'Gnt.examples.advanced.plugin.TaskContextMenu',
        'Gnt.examples.advanced.field.Filter'
        
        
    ],
    readOnly: true,
    showTodayLine           : true,
    loadMask                : true,
    enableProgressBarResize : true,
    showRollupTasks         : true,
    eventBorderWidth        : 0,
    rowHeight               : 32,
    viewPreset              : 'weekAndDayLetter',

    projectLinesConfig : {
        // Configure the gantt to mark project start dates w/ lines.
        // Options are:
        // 'start' - to show lines for start dates,
        // 'end' - to show lines for end dates,
        // 'both' - to show lines for both start and end dates.
        linesFor : 'start'
    },

    allowDeselect : true,

    selModel : {
        type : 'spreadsheet'
    },

    // Define properties for the left 'locked' and scrollable tree grid
    lockedGridConfig : {
        width : 400
    },

    // Define properties for the left 'locked' and scrollable tree view
    lockedViewConfig : {
        // Adds a CSS class returned to each row element
        getRowClass : function (rec) {
            return rec.isRoot() ? 'root-row' : '';
        }
    },

    // Define a custom HTML template for regular tasks
    taskBodyTemplate : '<div class="sch-gantt-progress-bar" style="width:{progressBarWidth}px;{progressBarStyle}" unselectable="on">' +
    '<span class="sch-gantt-progress-bar-label">{[Math.round(values.percentDone)]}%</span>' +
    '</div>',

    // Define an HTML template for the tooltip
    tooltipTpl : '<strong class="tipHeader">{Name}</strong>' +
    '<table class="taskTip">' +
    '<tr><td>Start:</td> <td align="right">{[values._record.getDisplayStartDate("d/m/Y")]}</td></tr>' +
    '<tr><td>End:</td> <td align="right">{[values._record.getDisplayEndDate("d/m/Y")]}</td></tr>' +
    '<tr><td>Progress:</td><td align="right">{[ Math.round(values.PercentDone)]}%</td></tr>' +
    '</table>',

    // Define what should be shown in the left label field, along with the type of editor
    leftLabelField : {
        dataIndex : 'Name',
        editor    : { xtype : 'textfield' }
    },

    plugins : [
        'advanced_taskcontextmenu',
        'scheduler_pan',
        'gantt_taskeditor',
        'gantt_projecteditor',
        {
            ptype : 'gantt_dependencyeditor',
            width : 320
        },
        {
            pluginId : 'taskarea',
            ptype    : 'taskarea'
        },
        {
            ptype        : 'scheduler_treecellediting',
            clicksToEdit : 2,
            pluginId     : 'editingInterface'
        },
        {
            ptype : 'gantt_clipboard',
            // data copied in raw format can be copied and pasted to gantt
            // data in text format is copied to system clipboard and can be pasted anywhere
            source : ['raw','text']
        },
        // @cut-if-gantt->
        {
            ptype              : 'gantt_printable',
            exportDialogConfig : {
                form                 : {
                    stateId : 'gantt-example-advanced'
                },
                showDPIField         : true,
                showColumnPicker     : true,
                dateRangeRestriction : false
            }
        },
        // <-@
        'gantt_selectionreplicator'
    ],

    // Define the static columns
    // Any regular Ext JS columns are ok too
    columns : [
        {
            xtype : 'namecolumn',
            width : 200,
            items : {
                xtype : 'gantt-filter-field'
            }
        },
        {
            xtype : 'startdatecolumn',
            width: 130,
            renderer: Ext.util.Format.dateRenderer('d/m/Y'),
            editor: new Ext.form.DateField({
                format: 'd/m/Y'
            })
        },
        {
            xtype: 'enddatecolumn',
            width: 130,
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
      
        // Uncomment to try this column
        //{
        //    xtype : 'deadlinecolumn'
        //},
        //{
        //    xtype : 'constrainttypecolumn'
        //},
        //{
        //    xtype : 'constraintdatecolumn'
        //},
         {
             header: 'Status',
             width: 100,
             dataIndex: 'ItemStatus',
             editor: Combo,
             renderer: Ext.ux.comboBoxRenderer(Combo),
             locked: true
         },
           {
               xtype: 'durationcolumn',
               width: 100
           },
           {
               header: 'RAG Status',
               width: 100,
               dataIndex: 'RAGStatus',
               editor: new Ext.form.ComboBox({
                   store: ['GREEN', 'RED', 'AMBER'],
                   triggerAction: 'all',
                   mode: 'local'
               }),
               locked: true
           },
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
            xtype             : 'predecessorcolumn',
            useSequenceNumber : true
        },
        {
            xtype : 'addnewcolumn'
        }
    ],

    eventRenderer : function (task, tplData) {
        var style,
            segments, i,
            result;

        if (task.get('Color')) {
            style = Ext.String.format('background-color: #{0};border-color:#{0}', task.get('Color'));

            if (!tplData.segments) {
                result = {
                    // Here you can add custom per-task styles
                    style : style
                };
            }
            // if task is segmented we cannot use above code
            // since it will set color of background visible between segments
            // in this case instead we need to provide "style" for each individual segment
            else {
                segments = tplData.segments;
                for (i = 0; i < segments.length; i++) {
                    segments[i].style = style;
                }
            }
        }

        return result;
    }
});


