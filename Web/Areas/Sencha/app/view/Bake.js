Ext.define('Pies.view.Bake', {
    extend: 'Ext.Container',
    requires: ['Pies.view.EditPie', 'Pies.view.Pie', 'Ext.form.FieldSet'],
    xtype: 'pies-bake',
    config: {
        layout: 'card',
        items: [
            {
                xtype: 'pies-edit'
            },
            {
                xtype: 'pies-pie'
            }
        ],
        title: 'Bake'
    },
    getTitlebarButton : function() {
        return Ext.create('Ext.Button', {
            iconCls: 'chart',
            ui: 'plain',
            listeners: {
                tap: {
                    scope: this,
                    fn: function() {
                        this.animateActiveItem(this.items.indexOf(this.getActiveItem()) == 0 ? 1 : 0, { duration: 500, type: 'fade' });
                    }
                }
            }
        });
    }
});
