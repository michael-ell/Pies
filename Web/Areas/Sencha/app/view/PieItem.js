Ext.define('Pies.view.PieItem', {
    extend: 'Ext.dataview.component.DataItem',
    requires: [
        'Ext.chart.PolarChart',
        'Ext.chart.series.Pie',
        'Pies.model.Ingredient'
    ],
    xtype: 'pieItem',
    config: {
        pie: {}       
    },
    applyPie: function () {
        window.console.log(this.config.record.data.caption);

        var ingredients = this.config.record.data.allIngredients;
        var colors = [];
        for (var i = 0; i < ingredients.length; i++) {
            colors.push(ingredients[i].color);
        }
        
        return Ext.create('Ext.chart.PolarChart', {
            height: 200, 
            colors: colors,
            legend: {
                position: 'bottom',
            },
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
    },
    updatePie: function (newPie, oldPie) {
        if (oldPie) {
            this.remove(oldPie);
        }
        if (newPie) {
            this.add(newPie);
        }
    },
});
