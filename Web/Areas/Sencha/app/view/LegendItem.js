Ext.define('Pies.view.LegendItem', {
    extend: 'Ext.dataview.component.DataItem',
    requires: [],
    xtype: 'legendItem',
    config: {
        legend: {}       
    },
    applyLegend: function () {
        var item = this.config.record.data;
        var marker = "<span class='legend-marker' style='background-color:" + item.mark + "'></span>";
        return Ext.create('Ext.Container', {
            html: "<div class='legend-item-container'><div class='legend-item'>" + marker + item.name + "</div></div>"
        });
    },
    updateLegend: function (newLegend, oldLegend) {
        if (oldLegend) {
            this.remove(oldLegend);
        }
        if (newLegend) {
            this.add(newLegend);
        }
    }
});
