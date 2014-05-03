Ext.define('Pies.view.EditPie', {
    extend: 'Ext.Container',
    requires: ['Pies.model.Ingredient', 'Pies.view.EditIngredient', 'Ext.form.FieldSet', 'Ext.dataview.DataView'],
    xtype: 'pies-edit',
    config: {
        layout: 'vbox',
        items: [
            {
                flex: 0,
                xtype: 'fieldset',
                items: [
                    {
                        xtype: 'textfield',
                        itemId: 'caption',
                        label: 'Caption',
                        name: 'caption'
                    },
                    {
                        xtype: 'textfield',
                        itemId: 'tags',
                        label: 'Tags',
                        name: 'tags'
                    }
                ]
            },
            {
                flex: 1,
                xtype: 'dataview',
                scrollable: 'vertical',
                store: { model: 'Pies.model.Ingredient' },
                useComponents: true,
                defaultType: 'pies-ei'
            }
        ],
        pie: null
    },
    applyPie: function (pie) {
        this.down('dataview').getStore().setData(pie.allIngredients);
        return pie;
    }
});
