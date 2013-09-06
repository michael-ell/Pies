Ext.define('Pies.store.RecentPies', {
    extend: 'Ext.data.Store',
    model: 'Pies.model.Pie',
    proxy: {
        type: 'ajax',
        url: '/sencha/home/getrecent',
        headers: { 'Content-Type': 'application/json;' }
    },
    autoLoad: true
});