Ext.define('Pies.view.PieItem', {
    extend: 'Ext.dataview.component.DataItem',
    requires: [
        'Ext.chart.PolarChart',
        'Ext.chart.series.Pie',
        'Ext.Button',
        'Pies.model.Ingredient'
    ],
    xtype: 'pieItem',
    config: {
        pie: true,
        //nameButton: true,
        dataMap: {
            //getPie: {                
                //setStore: Ext.create('Ext.data.Store', { model: 'Pies.model.Ingredient', data: 'allIngredients' }),
            //},
            //getNameButton: {
            //    setText: 'caption'
            //}
        }        
    },
    applyPie: function () {
     
        //return Ext.factory({
        //    colors: ["#115fa6", "#94ae0a", "#a61120", "#ff8809", "#ffd13e"],
        //    //store: Ext.create('Ext.data.Store', { model: 'Pies.model.Ingredient', data: r[0].data.allIngredients }),
        //    store: {
        //        fields: ['description', 'percent'],
        //        data: [{ description: 'a', percent: 20 }, { description: 'b', percent: 30 }, { description: 'c', percent: 50 }]
        //    },
        //    series: [{
        //        type: 'pie',
        //        labelField: 'description',
        //        xField: 'percent'
        //    }]
        //}, Ext.chart.PolarChart, this.getPie());
        //var container = Ext.create('Ext.Panel', {
            
        //});
        
        return Ext.create('Ext.chart.PolarChart', {
            height: 200, 
            colors: ["#115fa6", "#94ae0a", "#a61120", "#ff8809", "#ffd13e"],
            //store: Ext.create('Ext.data.Store', { model: 'Pies.model.Ingredient', data: r[0].data.allIngredients }),
            store: {
                fields: ['description', 'percent'],
                data: [{ description: 'axx', percent: 20 }, { description: 'b', percent: 30 }, { description: 'c', percent: 50 }]
            },
            series: [{
                type: 'pie',
                labelField: 'description',
                xField: 'percent'
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

    //applyNameButton: function (config) {
    //    return Ext.factory(config, Ext.Button, this.getNameButton());
    //},
    //updateNameButton: function (newButton, oldButton) {
    //    if (newButton) {
    //        this.add(newButton);
    //    }
    //    if (oldButton) {
    //        this.remove(oldButton);
    //    }
    //}
});
