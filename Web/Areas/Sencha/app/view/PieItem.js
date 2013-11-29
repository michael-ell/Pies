Ext.define('Pies.view.PieItem', {
    extend: 'Ext.dataview.component.DataItem',
    requires: [
        'Ext.chart.PolarChart',
        'Ext.chart.series.Pie',
        'Pies.model.Ingredient',
        'Pies.view.LegendItem'
    ],
    xtype: 'pieItem',
    config: {
        pie: {}       
    },
    applyPie: function () {
        var ingredients = this.config.record.data.allIngredients;
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
                    html: '<div class="header">' + this.config.record.data.caption + '</div>'
                },
                chart,
                {
                    xtype: 'dataview',
                    cls: 'legend',
                    scrollable: null,
                    store: chart.getLegendStore(),
                    useComponents: true,
                    defaultType: 'legendItem'
                }
            ]
        });
        return container;
    },
    updatePie: function (newPie, oldPie) {
        if (oldPie) {
            this.remove(oldPie);
        }
        if (newPie) {
            this.add(newPie);
        }
    }
});
