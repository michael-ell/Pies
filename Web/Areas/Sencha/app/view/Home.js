Ext.define('Pies.view.Home', {
    extend: 'Ext.Container',
    requires: ['Pies.view.Pie', 'Ext.carousel.Carousel'],
    xtype: 'pies-home',
    config: {
        layout: 'fit',
        items: [
            {
                xtype: 'carousel',
                direction: 'horizontal',
                indicator: true
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
        this.down('carousel').add(views);
        return data;
    }
});
