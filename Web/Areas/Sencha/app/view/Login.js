Ext.define('Pies.view.Login', {
    extend: 'Ext.Container',
    requires: ['Pies.model.MenuItem', 'Ext.dataview.DataView'],
    xtype: 'pies-login',
    config: {
        layout: 'card',
        height: '120px',
        items: [
            {
                xtype: 'container',
                layout: 'hbox',
                cls: 'user-menu-item',
                items: [
                    { xtype: 'spacer', flex: 1 },
                    {
                        flex: 1,
                        items: [
                            {
                                layout: 'hbox',
                                items: [
                                    { xtype: 'spacer', flex: 1 },
                                    {
                                        items: [
                                            {
                                                cls: 'user',
                                                items: [
                                                    {
                                                        itemId: 'user-photo',
                                                        cls: 'user-photo center',
                                                        html: '?'
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    { xtype: 'spacer', flex: 1 }
                                ]
                            },
                            {
                                itemId: 'user-name',
                                tpl: "<div class='center'>{.}</div>"
                            }
                        ]
                    },
                    { xtype: 'spacer', flex: 1 }
                ]
            },
            {
                xtype: 'container',
                cls: 'sign-in',
                layout: 'hbox',
                defaults: {
                    flex: 1,
                    xtype: 'button',
                    cls: 'nav-button'
                },
                items: [
                    {
                        text: 'Google+',
                        action: 'google'
                    },
                    {
                        text: 'Microsoft',
                        action: 'microsoft',
                        disabled: true
                    },
                    {
                        text: 'Twitter',
                        action: 'twitter',
                        disabled: true
                    }
                ]
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
    displayUser: function (user) {
        if (user.image) {
            this.down('#user-photo').setHtml('').setStyle({
                backgroundImage: 'url(' + user.image.url + ')',
                backgroundPosition: 'center'
            });
        }
        this.down('#user-name').setData(user.displayName);
    }
});