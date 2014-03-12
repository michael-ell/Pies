Ext.define('Pies.view.EditIngredient', {
    extend: 'Ext.Panel',
    xtype: 'pies-ei',
    config: {
        modal: true,
        hideOnMaskTap: true,
        centered: true,
        width: 260,
        showAnimation: {
            type: 'popIn',
            duration: 250,
            easing: 'ease-out'
        },
        hideAnimation: {
            type: 'popOut',
            duration: 250,
            easing: 'ease-out'
        },
        items: [
            {
                xtype: 'textfield',
                placeHolder: 'Description'
            },
            {
                xtype: 'sliderfield',
                minValue: 0,
                maxValue: 100,
                increment: 5
            }            
        ]
    }
});