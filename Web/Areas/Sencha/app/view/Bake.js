Ext.define('Pies.view.Bake', {
    extend: 'Ext.Panel',
    requires: ['Pies.view.Pie', 'Ext.form.FieldSet'],
    xtype: 'bakecard', 
    config: {
        id: 'bake',
        title: 'Bake',
        iconCls: 'action',
        scrollable: true,       
        items: [
            {
                xtype: 'fieldset',
                items: [
                    {
                        xtype: 'textfield',
                        id: 'caption',
                        label: 'Caption',
                        name: 'caption'           
                    },
                    {
                        xtype: 'textfield',
                        id: 'tags',
                        label: 'Tags',
                        name: 'tags'
                    }
                ]
            },
            {
                xtype: 'button',
                text: 'Add Ingredient',
                margin: '0 10 0 10',
                action: 'addIngredient'
            }
        ],
        pie: null
    },
    applyPie: function (pie) {
        return Ext.create('Pies.view.Pie', { pie: { data: pie } });
    },
    updatePie: function (np, op) {
        if (op) { this.remove(op); }
        if (np) { this.add(np); }
    }
});
