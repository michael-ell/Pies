Ext.define('Pies.controller.Home', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.store.RecentPies', 'Ext.carousel.Carousel'],
    config: {
        views: ['Home'],
        refs: { home: '#home' },
        control: {
            home: {
                activate: 'loadPies'
            }
        },
    },
    loadPies: function () {
        //debugger;
        //Ext.getStore('recentPies').load();
        var carousel = arguments[0].query('carousel')[0];
        Ext.getStore('recentPies').load(function (recent) {
            Ext.each(recent, function (pie) {
                var ingredients = pie.data.allIngredients;
                var colors = [];
                for (var i = 0; i < ingredients.length; i++) {
                    colors.push(ingredients[i].color);
                }

                var chart = Ext.create('Ext.chart.PolarChart', {
                    height: 200,
                    colors: colors,
                    store: Ext.create('Ext.data.Store', { model: 'Pies.model.Ingredient', data: ingredients }),
                    series: [{
                        type: 'pie',
                        labelField: 'description',
                        xField: 'percent',
                        renderer: function (sprite) {
                            sprite.attr.doCallout = false;
                            sprite.attr.label = '';
                        }
                    }]
                });

                var container = Ext.create('Ext.Container', {
                    items: [
                        {
                            xtype: 'container',
                            html: '<div class="header">' + pie.data.caption + '</div>'
                        },
                        chart,
                        {
                            xtype: 'dataview',
                            cls: 'legend',
                            scrollable: null,
                            store: chart.getLegendStore(),
                            useComponents: true,
                            defaultType: 'legendItem',
                        }
                    ]
                });
                carousel.add(container);
            });
        });
        carousel.setActiveItem(0);    
    }
});