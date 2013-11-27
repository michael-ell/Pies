Ext.define('Pies.view.Home', {
    extend: 'Ext.Panel',
    requires: ['Ext.TitleBar', 'Ext.dataview.DataView', 'Pies.store.RecentPies', 'Pies.view.PieItem'],
    xtype: 'homecard',
    config: {
        id: 'home',
        title: 'Home',
        iconCls: 'home',
        layout: 'fit',
        items: [
            //{
            //    docked: 'top',
            //    xtype: 'titlebar',
            //    title: 'Home'
            //},           
            //{
            //    xtype: 'dataview',
            //    scrollable: 'vertical',
            //    store: Ext.create('Pies.store.RecentPies'),
            //    useComponents: true,
            //    defaultType: 'pieItem'
            //}
            {
                xtype: 'carousel',
                id: 'pies',
                store: Ext.create('Pies.store.RecentPies'),
                listeners: {
                    initialize: function (carousel) {
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
                                            scrollable: 'vertical',
                                            store: chart.getLegendStore(),
                                            useComponents: true,
                                            defaultType: 'legendItem',
                                        }
                                    ]
                                });
                                carousel.add(container);
                            });
                        });
                        //carousel.setItems(items);
                        carousel.setActiveItem(0);
                    }
                }
            }
        ],
        //listeners: {
        //    //Controller ref / control listener
        //    activate: function () {
        //        //Ext.getStore('recentPies').load();
        //        //this.fireEvent('onLoadPies', this);
        //    }
        //}
    }
});
