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
        var me = opts.scope;
        Pies.hub.Bus.start(me._user.id).subscribe(Pies.hub.Messages.pieDeleted, me.pieDeleted, me);
        me.getView().setData(Ext.JSON.decode(xhr.responseText));
    },
    deletePie: function (scope) {
        Ext.Msg.confirm("Trash Pie", "Are you sure you want to do that?", function (answer) {
            if (answer == 'yes') {
                var pie = scope.getData();
                if (pie) {
                    Ext.Ajax.request({
                        url: '/api/mypies/delete/' + pie.id,
                        method: 'DELETE'
                    });
                }
            }
        });
    },
    pieDeleted: function (data) {
        var view = this.getView();
        Ext.each(view.getItems().items, function (item) {
            if (item.getPie().id == data.id) {
                view.remove(item);
                return false;
            }
            return true;
        });
    },
    done: function() {
        Pies.hub.Bus.stop();
    }
});