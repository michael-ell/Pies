Ext.define('Pies.view.Home', {
    extend: 'Ext.Panel',
    requires: ['Pies.view.Pie', 'Ext.carousel.Carousel'],
    xtype: 'homecard',
    config: {
        title: 'Home',
        iconCls: 'home',
        layout: 'fit',
        items: [
            {
                xtype: 'carousel',
                direction: 'vertical',
                indicator: false
            }
        ]
    },
    applyData: function (data) {
        var views = [];
        Ext.each(data, function (pie) {
            views.push({
                xtype: 'pie',
                pie: {
                    data: pie,
                    showCaption: true,
                    showLegend: true
                }
            });
        });
        this.down('carousel').add(views);
        return data;
    }
});
