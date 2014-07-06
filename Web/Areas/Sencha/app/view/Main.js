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
                cls: 'titlebar',
                items: [
                    {
                        iconCls: 'list',
                        ui: 'plain',
                        action: 'toggleMenu'
                    }
                ],
                zIndex: 5
            }
        ],
        views: null,
        title: 'Pies',
        titlebarButton: null
    },
    applyViews: function (views) {
        this.add(views);
        return views;
    },
    applyTitle: function(title) {
        this.down('.titlebar').setTitle(title);
    },
    applyTitlebarButton: function (button) {
        var tb = this.down('titlebar');
        if (this._added) {
            tb.remove(this._added);
        }
        if (button) {
            button.config.align = 'right';
            this._added = tb.add(button);
        }
    },
    updateTitlebarButton: function (ntbb, otbb) {
        var c = this.down('titlebar');
        if (otbb) { c.remove(otbb); }
        if (ntbb) { c.add(ntbb); }
    }
});
