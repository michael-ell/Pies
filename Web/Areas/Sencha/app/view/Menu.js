Ext.define('Pies.view.Menu', {
    extend: 'Ext.Menu',
    requires: ['Pies.model.MenuItem', 'Ext.dataview.DataView'],
    xtype: 'pies-menu',
    config: {
        layout: 'vbox',
        zIndex: 4,
        height: '100%',
        padding: '45 0 0 0',
        items: [
            {
                xtype: 'container',
                layout: 'card',
                height: '120px',
                items: [
                    {
                        xtype: 'container',
                        layout: 'hbox',
                        cls: 'user-menu-item',
                        items: [
                            { xtype: 'spacer' },
                            {
                                html: "<div class='center'><div class='user'><div class='user-photo'>?</div></div><div class='user-name'>Sign in</div></div>"
                            },
                            { xtype: 'spacer' }
                        ]
                    },
                    {
                        cls: 'sign-in'
                    }
                ],
                listeners: [
                    {
                        element: 'element',
                        event: 'tap',
                        fn: function () {
                            if (this.items.indexOf(this.getActiveItem()) == 0) {
                                this.animateActiveItem(1, { type: 'slide' });
                            } else {
                                this.animateActiveItem(0, { type: 'slide', direction: 'right' });
                            }
                        }
                    }
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