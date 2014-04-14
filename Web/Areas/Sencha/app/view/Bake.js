Ext.define('Pies.view.Bake', {
    extend: 'Ext.Container',
    requires: ['Pies.view.Pie', 'Ext.form.FieldSet'],
    xtype: 'pies-bake', 
    config: {
        layout: 'card',
        scrollable: true,       
        items: [
            {
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
                    }
                ]
            },
            {
                xtype: 'pies-pie'
            }  
        ],
        pie: null,
        title: 'Bake',
        titlebarButton: {
            xtype: 'button',
            iconCls: 'chart',
            text: 'preview'
        }
    },
    applyPie: function (pie) {
        return pie;
        //return Ext.create('Pies.view.Pie', { pie: { data: pie } });
    }
    //updatePie: function (np, op) {
    //    if (op) { this.remove(op); }
    //    if (np) { this.add(np); }
    //}
});
