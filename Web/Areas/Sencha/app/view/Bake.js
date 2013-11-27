Ext.define('Pies.view.Bake', {
    extend: 'Ext.Panel',
    requires: ['Ext.TitleBar', 'Ext.form.FieldSet'],
    xtype: 'bakecard', 
    config: {
        id: 'bake',
        title: 'Bake',
        iconCls: 'action',
        items: [
            //{
            //    docked: 'top',
            //    xtype: 'titlebar',
            //    title: 'Bake'
            //},
            {
                xtype: 'fieldset',
                items: [
                    {
                        xtype: 'textfield',
                        id: 'caption',
                        label: 'Caption',
                        name: 'caption',
                        listeners: {
                            change: function() {
                                this.fireEvent('onCaptionChanged', this.getValue());
                            }
                        }                        
                    },
                    {
                        xtype: 'textfield',
                        label: 'Ingredient',
                        name: 'ingredient'
                    },
                    {
                        xtype: 'textfield',
                        label: 'Tags',
                        name: 'tags'
                    }
                ]
            }
        ]
    }
});
