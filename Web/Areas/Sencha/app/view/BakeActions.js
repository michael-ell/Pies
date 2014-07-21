Ext.define('Pies.view.BakeActions', {
    extend: 'Ext.Panel',
    xtype: 'pies-bake-actions',
    config: {
        layout: 'hbox',
        modal: true,
        hideOnMaskTap: true,
        items: [
            {
                xtype: 'button',
                iconCls: 'chart',
                ui: 'plain',
                action: 'preview'
            },
            {
                xtype: 'button',
                iconCls: 'plus',
                ui: 'plain',
                action: 'new'
            }
        ]
    }
});