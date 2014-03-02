Ext.define('Pies.controller.Home', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.view.Home'],
    config: {
        refs: {
            home: '.homecard'
        }
    },
    launch: function () {
        var me = this;
        setTimeout(function () {
            me.getPies(me);
        }, 0);
    },
    getPies: function (scope) {
        Pies.app.fireEvent('busy');
        Ext.Ajax.request({
            url: '/sencha/home/getrecent',
            method: 'GET',        
            scope: scope,
            success: scope.showPies,
            callback: function () {
                Pies.app.fireEvent('notBusy');
            }
        });
    },
    showPies: function (xhr, opts) {
        opts.scope.getHome().setData(Ext.JSON.decode(xhr.responseText));
    }
});