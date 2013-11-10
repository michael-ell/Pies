Ext.define('Pies.view.Bake', {
    extend: 'Ext.Panel',
    requires: ['Ext.TitleBar'],
    xtype: 'bakecard', 
    config: {
        title: 'Bake',
        iconCls: 'action',
        //layout: 'hbox',
        items: [
            {
                docked: 'top',
                xtype: 'titlebar',
                title: 'Bake'
            },           
            //{
            //    html: 'Red',
            //    style: 'background-color: #B22222; color: white;',
            //},
            //{
            //    html: 'Second Panel',
            //    style: 'background-color: #5E99CC;',
            //}
        ]
    }
});
