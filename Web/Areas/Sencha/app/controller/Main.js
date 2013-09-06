Ext.define('Pies.controller.Main', {
    extend: 'Ext.app.Controller',
    config: {},
    launch: function () {
        Ext.create('Pies.store.RecentPies', 
            {
                listeners: { load: function (store, records, successful, operation, eOpts ) { } }
            });
    }    
});