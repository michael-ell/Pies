Ext.define('Pies.hub.Bus', {
    requires: ['Pies.hub.Messages'],
    singleton: true,
    start: function(name) {
        this.stop();
        var hub = $.connection.pie;
        hub.client.pieDeleted = function(data) {
            Pies.app.fireEvent(Pies.hub.Messages.pieDeleted, data);
        };
        hub.client.captionUpdated = function(data) {
            Pies.app.fireEvent(Pies.hub.Messages.captionUpdated, data);
        };
        hub.client.ingredientsUpdated = function(data) {
            Pies.app.fireEvent(Pies.hub.Messages.ingredientsUpdated, data);
        };
        hub.client.percentageChanged = function(data) {
            Pies.app.fireEvent(Pies.hub.Messages.percentageChanged, data);
        };
        hub.client.showMessage = function(data) {
            Pies.app.fireEvent(Pies.hub.Messages.messageReceived, data);
        };
        $.connection.hub.start().done(function() {
            if (name) {
                hub.server.join(name);
                console.log('joined hub: ' + name);
            }
        });
        return this;
    },
    stop: function() {
        $.connection.hub.stop();
        Ext.each(this._callbacks, function(item) {
            Pies.app.un(item.msg, item.cb);
        });
        this._callbacks = [];
    },
    subscribe: function(message, callback, scope) {
        Pies.app.on(message, callback, scope || this);
        this._callbacks.push({ msg: message, cb: callback });
        return this;
    }
});