Ext.define('Pies.view.MyPies', {
    extend: 'Ext.Container',
    xtype: 'pies-mine', 
    config: {
        layout: 'vbox',
        scrollable: true
    },
    applyData: function (data) {
        var views = [];
        Ext.each(data, function (pie) {
            views.push({
                xtype: 'pies-pie',
                pie: pie,
                mine: true
            });
        });
        this.removeAll();
        this.add(views);
        return data;
    }
});

