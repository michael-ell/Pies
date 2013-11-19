Ext.define('Pies.view.MyPies', {
    extend: 'Ext.Panel',
    requires: ['Ext.TitleBar'],
    xtype: 'mypiescard', 
    config: {
        title: 'My Pies',
        iconCls: 'user',
        items: [
            {
                docked: 'top',
                xtype: 'titlebar',
                title: 'My Pies'
            },
            {
                xtype: 'container',
                cls: 'header',
                html: 'Sorry, also closed'
            }
        ]
    }
});
