Ext.define('Pies.view.Menu', {
    extend: 'Ext.Menu',
    requires: ['Pies.model.MenuItem', 'Ext.dataview.DataView'],
    xtype: 'pies-menu',
    config: {
        layout: 'vbox',
        padding: '45 0 0 0',
        zIndex: 4,
        items: [
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