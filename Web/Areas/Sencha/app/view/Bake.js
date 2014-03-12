Ext.define('Pies.view.Bake', {
    extend: 'Ext.Container',
    requires: ['Pies.view.Pie', 'Ext.form.FieldSet'],
    xtype: 'pies-bake', 
    config: {
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
        pie: null,
        title: 'Bake'
    },
    applyPie: function (pie) {
        return Ext.create('Pies.view.Pie', { pie: { data: pie } });
    },
    updatePie: function (np, op) {
        if (op) { this.remove(op); }
        if (np) { this.add(np); }
    }
});
