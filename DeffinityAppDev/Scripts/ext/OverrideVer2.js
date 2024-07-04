Ext.data.Operation.override({
    commitRecords: function (serverRecords) {
        var me = this,
            mc, index, clientRecords, serverRec, clientRec,
            oldId, newId;

        if (!me.actionSkipSyncRe.test(me.action)) {
            clientRecords = me.getRecords();

            if (clientRecords && clientRecords.length) {

                if (clientRecords.length > 1) {
                    mc = new Ext.util.MixedCollection();
                    mc.addAll(serverRecords);

                    for (index = clientRecords.length; index--; ) {
                        clientRec = clientRecords[index];
                        serverRec = serverRecords[index];

                        oldId = clientRec.getId();
                        clientRec.copyFrom(serverRec);
                        newId = clientRec.getId();
                        if (oldId !== newId) {
                            clientRec.fireEvent('idchanged', clientRec, oldId, newId);
                        }
                    }
                } else {
                    clientRec = clientRecords[0];
                    serverRec = serverRecords[0];
                    if (serverRec && (clientRec.phantom || clientRec.getId() === serverRec.getId())) {
                        oldId = clientRec.getId();
                        clientRec.copyFrom(serverRec);
                        newId = clientRec.getId();
                        if (oldId !== newId) {
                            clientRec.fireEvent('idchanged', clientRec, oldId, newId);
                        }
                    }
                }

                if (me.actionCommitRecordsRe.test(me.action)) {
                    for (index = clientRecords.length; index--; ) {
                        clientRecords[index].commit();
                    }
                }
            }
        }
    }
});