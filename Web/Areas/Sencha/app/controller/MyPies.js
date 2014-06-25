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
    constructor: function() {
        this.callParent(arguments);
        Pies.app.on(Pies.hub.Messages.userSignedIn, this.userSignedIn, this);
    },
    userSignedIn: function (user) {
        this._user = user;
    },
    getPies: function () {
        if (this._user) {
            Pies.app.fireEvent(Pies.hub.Messages.busy);
            Ext.Ajax.request({
                url: '/api/mypies/getall',
                method: 'GET',
                scope: this,
                success: this.showPies,
                callback: function() {
                    Pies.app.fireEvent(Pies.hub.Messages.notBusy);
                }
            });
        }
    },
    showPies: function (xhr, opts) {
        Pies.hub.Bus.start(this._user.id);
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