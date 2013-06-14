$.mhub.messages = {
    pieDeleted: 'pd',
    captionUpdated: 'cu',
    ingredientsUpdated: 'iu',
    percentageChanged: 'pc',
    messageReceived: 'mr'
};

$.mhub.init = function (group) {
    
    $.mhub.create($.mhub.messages.pieDeleted);
    $.mhub.create($.mhub.messages.captionUpdated);
    $.mhub.create($.mhub.messages.ingredientsUpdated);
    $.mhub.create($.mhub.messages.percentageChanged);
    $.mhub.create($.mhub.messages.messageReceived);

    var hub = $.connection.pie;
    hub.client.pieDeleted = function (data) {
        $.mhub.send($.mhub.messages.pieDeleted, data);
    };
    hub.client.captionUpdated = function (data) {
        $.mhub.send($.mhub.messages.captionUpdated, data);
    };
    hub.client.ingredientsUpdated = function (data) {
        $.mhub.send($.mhub.messages.ingredientsUpdated, data);
    };
    hub.client.percentageChanged = function (data) {
        $.mhub.send($.mhub.messages.percentageChanged, data);
    };
    hub.client.showMessage = function (data) {
        $.mhub.send($.mhub.messages.messageReceived, data);
    };
    $.connection.hub.start().done(function () {
        if(group) {
            hub.server.join(group);
        }
    });
}