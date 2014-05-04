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
                        placeHolder: 'Caption'
                    }
                ]
            },
            {
                flex: 0,
                xtype: 'titlebar',
                title: 'Ingredients',
                titleAlign: 'left',   
                cls: 'quiet',
                ui: 'plain',
                items: [
                    {
                        iconCls: 'add',
                        cls: 'btn',
                        ui: 'plain',
                        align: 'right',
                        action: 'addIngredient'
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
        this.down('dataview').getStore().setData(pie.editableIngredients);
        return pie;
    },
    updateIngredients: function(data) {
        this.down('dataview').getStore().setData(data.ingredients);
    }
});
