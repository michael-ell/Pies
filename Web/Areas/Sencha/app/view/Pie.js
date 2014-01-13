Ext.define("Pies.view.Pie", {
    extend: 'Ext.Container',
    requires: ['Ext.chart.PolarChart', 'Ext.chart.series.Pie', 'Pies.model.Ingredient'],
    xtype: 'pie',
    config: {
        pie: null
    },
    applyPie: function (settings) {
        var ingredients = settings.data.allIngredients;
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
        if (settings.showCaption) {
            this.add({
                xtype: 'container',
                html: '<div class="header">' + settings.data.caption + '</div>'
            });
        }
        this.add(chart);
        if (settings.showLegend) {
            this.add(
                {
                    xtype: 'dataview',
                    cls: 'legend',
                    scrollable: null,
                    store: chart.getLegendStore(),
                    useComponents: true,
                    defaultType: 'legendItem'
                });
        }
    }
});