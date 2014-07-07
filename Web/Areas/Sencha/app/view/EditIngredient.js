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
                        xtype: 'textfield',
                        itemId: 'description',
                        placeHolder: 'Description',
                        listeners: {
                            focus: function () {
                                var di = this.up('dataitem');
                                var dv = this.up('dataview');
                                var idx = dv.getStore().indexOf(di.getRecord());
                                if (idx > 0) {
                                    setTimeout(function() {
                                        dv.getScrollable().getScroller().scrollTo(0, di.bodyElement.dom.clientHeight * idx, true);
                                    }, 400);
                                }
                            }
                        }
                    },
                    {
                        xtype: 'sliderfield',
                        itemId: 'percentage',
                        minValue: 0,
                        maxValue: 100,
                        increment: 5,
                        listeners: {
                            change: function(me, sl, thumb, nv) {
                                me.fireEvent('percentChange', { scope: me, percent: nv });
                            }
                        }
                        
                    }
                ]
            }
        ]
    },
    applyRecord: function (record) {
        var i = record.data;
        this.down('textfield').setValue(i.description).setData(i);
        this.down('sliderfield').setValue(i.percent).setData(i);
        //var bc = 'border-color:' + i.color;
        //this.down('fieldset').child('div').setStyle(bc);
        //this.down('textfield').setValue(i.description).setStyle(bc);
        //this.down('sliderfield').setStyle(bc);
        return record;
    },
    updatePercent: function(percent, reason) {
        this.down('sliderfield').setValue(percent);
    }
});