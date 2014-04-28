Ext.define('Pies.view.EditPie', {
    extend: 'Ext.Container',
    requires: ['Ext.form.FieldSet'],
    xtype: 'pies-edit',
    config: {
        items: [
            {
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
            }
        ]
    }
});
