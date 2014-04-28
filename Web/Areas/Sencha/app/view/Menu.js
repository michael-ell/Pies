Ext.define('Pies.view.Menu', {
    extend: 'Ext.Menu',
    requires: ['Pies.model.MenuItem', 'Ext.dataview.DataView'],
    xtype: 'pies-menu',
    config: {
        layout: 'vbox',
        zIndex: 4,
        width: '80%',
        padding: '45 0 0 0',
        items: [
            {
                xtype: 'container',
                layout: 'hbox',
                cls: 'user-menu-item',
                items: [
                    { xtype: 'spacer' },
                    {
                        xtype: 'container',
                        html: "<div class='center'><div class='user'><div class='user-photo'>?</div></div><div class='user-name'>Sign in</div></div>"
                    },
                    { xtype: 'spacer' }
                ]
            },
            {
                xtype: 'dataview',
                itemId: 'menu-items',
                scrollable: null,
                cls: 'menu-item',
                store: { model: 'Pies.model.MenuItem' },             
                itemTpl: '<div>{title}</div>'   
            }
        ]
    },
    applyData: function (data) {
        this.down('.dataview').getStore().setData(data);
        return data;
    }
});