var cr8 = cr8 || {};
cr8.Xhr = function () {
    var self = this;
    self.status = 0;
    self.responseText = "";
    self.thatIsSuccessful = function() {
        self.status = 200;
        return self;
    };
    self.withResponse = function (text) {
        self.responseText = text;
        return self;
    };
    return self;
};
