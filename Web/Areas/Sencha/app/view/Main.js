Ext.define('Pies.view.Main', {
    extend: 'Ext.TabPanel',
    config: {
        tabBarPosition: 'bottom',
        items: [
            { xtype: 'homecard' },
            { xtype: 'bakecard' },
            { xtype: 'mypiescard' }
        ]
    }
});
