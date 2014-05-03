Ext.define('Pies.view.EditIngredient', {
    extend: 'Ext.dataview.component.DataItem',
    requires: ['Ext.field.Slider'],
    xtype: 'pies-ei',
    config: {
        items: [
            {
                xtype: 'fieldset',
                items: [
                    {
                        xtype: 'textfield'
                    },
                    {
                        xtype: 'sliderfield',
                        minValue: 0,
                        maxValue: 100,
                        increment: 5
                    }
                ]
            }
        ]
    },
    applyRecord: function (record) {
        var i = record.data;
        //var bc = 'border-color:' + i.color;
        //this.down('fieldset').child('div').setStyle(bc);
        this.down('textfield').setValue(i.description);
        //this.down('textfield').setValue(i.description).setStyle(bc);
        //this.down('sliderfield').setStyle(bc);
        return record;
    }
});