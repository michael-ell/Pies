Ext.define('Pies.controller.Home', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.view.Home'],
    config: {
        refs: {
            home: '.pies-home'
        }
    },
    init: function() {
        Pies.app.on('getPies', this.getPies, this);
    },
    getPies: function () {
        Pies.app.fireEvent('busy');
        Ext.Ajax.request({
            url: '/sencha/home/getrecent',
            method: 'GET',        
            scope: this,
            success: this.showPies,
            callback: function () {
                Pies.app.fireEvent('notBusy');
            }
        });
    },
    showPies: function (xhr, opts) {
        opts.scope.getHome().setData(Ext.JSON.decode(xhr.responseText));
    }
});