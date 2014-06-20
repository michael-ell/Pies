Ext.define('Pies.view.Home', {
    extend: 'Ext.Container',
    requires: ['Pies.view.Pie', 'Ext.carousel.Carousel'],
    xtype: 'pies-home',
    config: {
        layout: 'vbox',
        scrollable: true,
        items: [
            {            
                //xtype: 'carousel',
                //direction: 'vertical',
                //indicator: false
            }
        ]
    },
    applyData: function (data) {
        var views = [];
        Ext.each(data, function (pie) {
            views.push({
                xtype: 'pies-pie',
                pie: pie
            });
        });
        //this.down('carousel').add(views);
        this.add(views);
        return data;
    }
});
