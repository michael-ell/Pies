Ext.define('Pies.view.Main', {
    extend: 'Ext.tab.Panel',
    xtype: 'main',
    requires: [
        'Ext.TitleBar'
    ],
    config: {
        tabBarPosition: 'bottom',

        items: [
            {
                title: 'Home',
                iconCls: 'home',

                styleHtmlContent: true,
                scrollable: true,

                items: {
                    docked: 'top',
                    xtype: 'titlebar',
                    title: 'Home'
                }
            },
            {
                title: 'Bake',
                iconCls: 'action',

                items: [
                    {
                        docked: 'top',
                        xtype: 'titlebar',
                        title: 'Bake'
                    }
                ]
            },
            {
                title: 'My Pies',
                iconCls: 'user',

                items: [
                    {
                        docked: 'top',
                        xtype: 'titlebar',
                        title: 'My Pies'
                    }
                ]
            }
        ]
    }
});
