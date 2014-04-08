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
        var shared = this._getShared();
        Ext.Ajax.request({
            url: !shared ? '/api/home/getrecent': '/api/home/get/' + shared,
            method: 'GET',        
            scope: this,
            success: this.showPies,
            callback: function () {
                Pies.app.fireEvent('notBusy');
            }
        });
    },
    _getShared: function() {
        var regex = new RegExp("[\\?&]share=([^&#]*)"), results = regex.exec(location.search);
        return !results ? null : decodeURIComponent(results[1].replace(/\+/g, " "));
    },
    showPies: function (xhr, opts) {
        opts.scope.getHome().setData(Ext.JSON.decode(xhr.responseText));
    }
});