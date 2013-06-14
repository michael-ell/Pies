var testing = testing || {};
testing.setup = function () {
    $.mhub.start = function() {
        //do nothing...
    };
};

testing.verify = testing.verify || {};
testing.verify.matchingArray = function(expected, actual) {
    return true;
};
