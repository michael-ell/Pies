Ext.define('Pies.view.Bake', {
    extend: 'Ext.Panel',
    requires: ['Ext.TitleBar'],
    xtype: 'bakecard', 
    config: {
        title: 'Bake',
        iconCls: 'action',
        items: [
            {
                docked: 'top',
                xtype: 'titlebar',
                title: 'Bake'
            }
        ]
    }
});
