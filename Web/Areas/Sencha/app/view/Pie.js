Ext.define("Pies.view.Pie", {
    extend: 'Ext.Container',
    requires: ['Pies.model.Ingredient', 'Pies.view.LegendItem', 'Ext.chart.PolarChart', 'Ext.chart.series.Pie'],
    xtype: 'pies-pie',
    config: {
        pie: null
    },
    applyPie: function (config) {
        var ingredients = config.data.allIngredients;
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
        if (config.showCaption) {
            this.add({
                xtype: 'container',
                html: '<div class="header">' + config.data.caption + '</div>'
            });
        }
        this.add(chart);
        if (config.showLegend) {
            this.add(
                {
                    xtype: 'dataview',
                    cls: 'legend',
                    scrollable: null,
                    store: chart.getLegendStore(),
                    useComponents: true,
                    defaultType: 'pies-legend'
                });
        }
    }
});