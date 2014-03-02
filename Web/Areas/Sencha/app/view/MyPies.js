Ext.define('Pies.view.MyPies', {
    extend: 'Ext.Panel',
    xtype: 'mypiescard', 
    config: {
        title: 'My Pies',
        iconCls: 'user',
        items: [
            {
                xtype: 'container',
                cls: 'header',
                html: 'Sorry...closed during renovations'
            }        
        ]
    }
});

