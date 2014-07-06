Ext.define('Pies.controller.Home', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.view.Home'],
    config: {
        refs: { view: '.pies-home' },
        control: {
            'button[action=home-refresh]': {
                tap: 'getPies'
            }
        }
    },
    init: function() {
        Pies.app.on(Pies.hub.Messages.getPies, this.getPies, this);
    },
    getPies: function () {
        Pies.app.fireEvent(Pies.hub.Messages.busy);
        var shared = this._getShared();
        Ext.Ajax.request({
            url: !shared ? '/api/home/getrecent': '/api/home/get/' + shared,
            method: 'GET',        
            scope: this,
            success: this.showPies,
            callback: function () {
                Pies.app.fireEvent(Pies.hub.Messages.notBusy);
            }
        });
    },
    _getShared: function() {
        var regex = new RegExp("[\\?&]share=([^&#]*)"), results = regex.exec(location.search);
        return !results ? null : decodeURIComponent(results[1].replace(/\+/g, " "));
    },
    showPies: function (xhr, opts) {
        var data = Ext.JSON.decode(xhr.responseText);
        opts.scope.getView().setData(Array.isArray(data) ? data : [data]);
    }
});