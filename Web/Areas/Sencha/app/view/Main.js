Ext.define('Pies.view.Main', {
    extend: 'Ext.TabPanel',
    requires: ['Pies.view.Home', 'Pies.view.Bake', 'Pies.view.MyPies'],
    config: {
        tabBarPosition: 'bottom',
        items: [
            { xtype: 'homecard' },
            { xtype: 'bakecard' },
            { xtype: 'mypiescard' }
        ]
    }
});
