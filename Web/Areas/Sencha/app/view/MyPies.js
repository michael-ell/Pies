//Ext.define('Pies.view.MyPies', {
//    extend: 'Ext.Panel',
//    requires: ['Ext.TitleBar'],
//    xtype: 'mypiescard', 
//    config: {
//        title: 'My Pies',
//        iconCls: 'user',
//        items: [
//            {
//                xtype: 'container',
//                cls: 'header',
//                html: 'Sorry...closed during renovations'
//            }        
//        ]
//    }
//});
Ext.define('Pies.view.MyPies', {
    extend: 'Ext.Panel',
    requires: ['Ext.TitleBar'],
    xtype: 'mypiescard',
    config: {
        title: 'My Pies',
        iconCls: 'user',
        layout: 'fit',
        items: [
            {
                xtype: 'titlebar',
                ui: 'plain',
                docked: 'top',
                title: 'Some Title',
                items: [
                    {
                        iconCls: 'settings',
                        ui: 'plain',
                        align: 'right',
                        listeners: {
                            tap: function () {
                                var x = this.up('panel').query('#x')[0];     
                                x.animateActiveItem(x.items.indexOf(x.getActiveItem()) == 0 ? 1 : 0, { duration: 500, type: 'fade'});
                            }
                        }
                    }
                ]
            },
            {
                xtype: 'container',
                layout: 'card',
                id: 'x',
                //defaults: {
                //    layout: 'fit'
                //},
                items: [
                    {                        
                        html: '1'
                    },
                    {
                        html: '2'
                    }
                ]
            }
        ]
    }
});
