Ext.define('Pies.controller.Home', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.store.RecentPies'],
    config: {
        views: ['Home'],
        refs: { home: '#home' },
        control: {
            home: {
                activate: 'loadPies'
            }
        },
    },
    loadPies: function () {
        //Ext.getStore('recentPies').load();
    }
});