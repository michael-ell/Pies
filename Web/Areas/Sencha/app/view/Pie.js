Ext.define("Pies.view.Pie", {
    extend: 'Ext.Container',
    requires: ['Pies.model.Ingredient', 'Pies.view.LegendItem', 'Ext.chart.PolarChart', 'Ext.chart.series.Pie', 'Ext.dataview.DataView'],
    xtype: 'pies-pie',
    config: {
        items: [{
            itemId: 'caption',
            tpl: '<div class="header">{.}</div>'
        }],
        pie: null
    },
    applyPie: function (pie) {
        var ingredients = pie.allIngredients;
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
        return {
            caption: pie.caption,
            chart: chart,
            legend: {
                xtype: 'dataview',
                cls: 'legend',
                scrollable: null,
                store: chart.getLegendStore(),
                useComponents: true,
                defaultType: 'pies-legend'
            }
        };
    },
    updatePie: function (np, op) {
        if (op) {
            this.remove(op.chart);
            this.remove(op.legend);
        }
        if (np) {
            this.updateCaption(np.caption);
            this.add(np.chart);
            this.add(np.legend);
        }
    },
    updateCaption: function (caption) {
        this.down('#caption').setData(caption);
        this._caption = caption;
    },
    updateIngredients: function (data) {
        if (data.filler.percent > 0) {
            data.ingredients.push(data.filler);
        }
        this.setPie({
            caption: this._caption,
            allIngredients: data.ingredients
        });
    }
});