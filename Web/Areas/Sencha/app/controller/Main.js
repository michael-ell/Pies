Ext.define('Pies.controller.Main', {
    extend: 'Ext.app.Controller',
    config: {
        refs: { Home: '#home' },
        control: {
            Home: {
                onLoadPies: 'loadPies'
            }
        },
    },
    pies : null,
    loadPies: function () {
        if (this.pies == null) {
            this.pies = Ext.create('Pies.store.RecentPies',
                {
                    listeners: {
                        load: function(store, records, successful, operation, eOpts) {
                        }
                    }
                });
        }
    }
});