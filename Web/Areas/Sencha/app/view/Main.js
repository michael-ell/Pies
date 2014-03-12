Ext.define('Pies.view.Main', {
    extend: 'Ext.Container',
    requires: ['Pies.view.Home', 'Pies.view.Bake', 'Pies.view.MyPies', 'Ext.TitleBar'],
    xtype: 'pies-main',
    config: {
        layout: 'card',
        items: [
            {
                xtype: 'titlebar',
                docked: 'top',
                items: [
                    {
                        xtype: 'button',
                        iconCls: 'list',
                        ui: 'plain',
                        align: 'right',
                        action: 'toggleMenu'
                    }
                ],
                zIndex: 5
            }
        ],
        views: null,
        title: 'Pies'
    },
    applyViews: function (views) {
        this.add(views);
        return views;
    },
    applyTitle: function(title) {
        this.down('.titlebar').setTitle(title);
    }
});
