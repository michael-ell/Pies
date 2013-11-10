Ext.define('Pies.view.Main', {
    extend: 'Ext.TabPanel',
    xtype: 'main',
    requires: ['Ext.TitleBar', 'Pies.view.Home', 'Pies.view.Bake', 'Pies.view.MyPies'],
    config: {
        tabBarPosition: 'bottom',
        items: [
            { xtype: 'homecard' },
            { xtype: 'bakecard' },
            { xtype: 'mypiescard' }
        ]
    }
});
