Ext.define('Gnt.examples.advanced.store.Tasks', {
    extend : 'Gnt.data.TaskStore',

    requires                  : ['Gnt.examples.advanced.model.Project'],
    reApplyFilterOnDataChange : false,
    alias                     : 'store.advanced-task-store',
    model                     : 'Gnt.examples.advanced.model.Task',
    loadMask: true
   // weekendsAreWorkday: true
});



//var mask = new Ext.LoadMask(Ext.getBody(), { store: 'Gnt.data.TaskStore' });

//mask.show();