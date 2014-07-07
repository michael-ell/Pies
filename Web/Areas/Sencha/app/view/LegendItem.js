Ext.define('Pies.view.LegendItem', {
    extend: 'Ext.dataview.component.DataItem',
    xtype: 'pies-legend',
    applyRecord: function (record) {
        if (record) {
            var item = record.data;
            var marker = "<span class='legend-marker' style='background-color:" + item.mark + "'></span>";
            return Ext.create('Ext.Container', {
                html: "<div class='legend-item-container'><div class='legend-item'>" + marker + (item.name || '?') + "</div></div>"
            });
        }
        return record;
    },
    updateRecord: function (newItem, oldItem) {
        if (oldItem) {
            this.remove(oldItem);
        }
        if (newItem) {
            this.add(newItem);
        }
    }
});
