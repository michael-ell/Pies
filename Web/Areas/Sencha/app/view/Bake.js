Ext.define('Pies.view.Bake', {
    extend: 'Ext.Panel',
    requires: ['Ext.TitleBar', 'Ext.form.FieldSet'],
    xtype: 'bakecard', 
    config: {
        id: 'bake',
        title: 'Bake',
        iconCls: 'action',
        items: [
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
                        id: 'tags',
                        label: 'Tags',
                        name: 'tags',
                        listeners: {
                            change: function () {
                                this.fireEvent('onTagsChanged', this.getValue());
                            }
                        }
                    }
                ]
            }
        ]
    }
});
