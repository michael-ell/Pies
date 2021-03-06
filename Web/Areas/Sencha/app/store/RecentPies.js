﻿Ext.define('Pies.store.RecentPies', {
    extend: 'Ext.data.Store',
    requires: ['Pies.model.Pie'],
    config: {
        storeId: 'recentPies',
        model: 'Pies.model.Pie',
        proxy: {
            type: 'ajax',
            url: '/sencha/home/getrecent',
            headers: { 'Content-Type': 'application/json;' }
        },
        autoLoad: false
    }
});