Ext.define('Pies.controller.Home', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.store.RecentPies', 'Pies.view.Pie', 'Ext.carousel.Carousel'],
    config: {
        views: ['Home'],
        refs: { home: '#home' },
        control: {
            home: {
                initialize: 'loadPies'
            }
        }
    },
    loadPies: function () {
        var carousel = arguments[0].query('carousel')[0];        
        Ext.getStore('recentPies').load(function (recent) {
            Ext.each(recent, function (pie) {
                carousel.add({
                    xtype: 'pie',
                    pie: {
                        data: pie.data,
                        showCaption: true,
                        showLegend: true
                    }                        
                });
            });
        });
        carousel.setActiveItem(0);    
    }
});