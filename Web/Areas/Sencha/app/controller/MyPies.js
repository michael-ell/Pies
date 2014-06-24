Ext.define('Pies.controller.MyPies', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.view.MyPies', 'Pies.hub.Bus', 'Pies.hub.Messages'],
    config: {
        refs: { view: '.pies-mine' },
        control: {
            view: {
                show: 'getPies',
                hide: 'done'
            },
            '.pies-mine button[action="delPie"]' : {
                 tap: 'deletePie'
            }
        }
    },
    getPies: function () {
        Pies.app.fireEvent('busy');
        Ext.Ajax.request({
            url: '/api/mypies/getall',
            method: 'GET',        
            scope: this,
            success: this.showPies,
            callback: function () {
                Pies.app.fireEvent('notBusy');
            }
        });
    },
    showPies: function (xhr, opts) {
        //Pies.hub.Bus.start()
        opts.scope.getView().setData(Ext.JSON.decode(xhr.responseText));
    },
    deletePie: function (scope) {
        var pie = scope.getData();
        if (pie) {
            Ext.Ajax.request({
                url: '/api/mypies/delete/' + pie.id,
                method: 'DELETE'
            });
        }
    },
    pieDeleted: function(data) {
        //debugger;
    },
    done: function() {
        Pies.hub.Bus.stop();
    }
});