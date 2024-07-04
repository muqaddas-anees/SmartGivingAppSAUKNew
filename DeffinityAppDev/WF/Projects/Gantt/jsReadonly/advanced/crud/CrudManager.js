
var ProjectReference = getQuerystring('project');
//alert(ProjectReference);
Ext.define('Gnt.examples.advanced.crud.CrudManager', {
    extend: 'Gnt.data.CrudManager',
    alias: 'crudmanager.advanced-crudmanager',
    autoLoad: true,
    autoSync: true,
    transport: {
        load: {
            method: 'GET',
            paramName: 'q',
            url: '../../../ganttcrud/load/' + ProjectReference
        },
        sync: {
            method: 'POST',
            url: '../../../ganttcrud/sync/' + ProjectReference
        }
    }
});
