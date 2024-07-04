
var ProjectReference = getQuerystring('project');

Ext.define('Gnt.examples.advanced.model.Task', {
    extend: 'Gnt.model.Task',
    
    fields  : [
        { name : 'index', type : 'int', persist : true },
        { name : 'expanded', type : 'bool', persist : false },
        { name : 'Color', type : 'string' },
        { name: 'ShowInTimeline', type: 'bool' },
        { name: 'ProjectReference', type: 'int', defaultValue: ProjectReference },
        { name: 'ItemStatus', defaultValue: 1 },
         { name: 'CategoryID', type: 'int' },
         { name: 'TeamID', type: 'int' },
         { name: 'RAGStatus', type: 'string', defaultValue: 'GREEN' },
         { name: 'TeamIds', type: 'string' },
    { name: 'QA', defaultValue: 'N' },
     { name: 'RAGRequired', defaultValue: 'N' },
      { name: 'ListPosition', type: 'int' },
      { name: 'Note' }
    ],

    showInTimelineField : 'ShowInTimeline'
});


