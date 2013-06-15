var testing = testing || {};

testing.setup = function () {
    $.mhub.start = function () {
        //do nothing...
    };
};

testing.matchers = {
    toBeSameAs: function (expected) {
        if (expected.matches) {
            return expected.matches(this.actual);
        }
        return false;
    },

    toHaveSameValues: function (expected) {
        var array1 = expected, array2 = this.actual;
        if (array1 && array2) {
            if (array1.length !== array2.length) {
                return false;
            }
            for (var i = 0; i < array1.length; i++) {
                if (array1[i] !== array2[i]) {
                    return false;
                }
            }
            return true;
        }
        return !array1 && !array2;
    },

    toHaveItem: function (expected) {
        if (this.actual && this.actual instanceof Array) {
            for (var i = 0; i < this.actual.length; i++) {
                if (expected.matches && $.isFunction(expected.matches) && expected.matches(this.actual[i])) {
                    return true;
                } else if (this.actual[i] === expected) {
                    return true;
                }
            }
        }
        return this.actual === expected;
    },
    
    toHaveBeenCalledWithArgumentContaining: function (expected) {
        if (!this.actual.wasCalled) return false;

        for (var property in expected) {
            for (var i = 0; i < this.actual.argsForCall.length; i++) {
                for (var j = 0; j < this.actual.argsForCall[i].length; j++) {
                    if (this.actual.argsForCall[i][j][property] && this.actual.argsForCall[i][j][property] == expected[property]) {
                        return true;
                    }
                }
            }
        }
        return false;
    }
};

testing.verify = {
    matchingArray: function(expected, actual) {
        if (expected && actual) {
            if (expected.length !== actual.length) {
                return false;
            }
            for (var i = 0; i < expected.length; i++) {
                if (expected[i].matches && $.isFunction(expected[i].matches)) {
                    if (!expected[i].matches(actual[i])) {
                        return false;
                    }
                } else if (expected[i] !== actual[i]) {
                    return false;
                }
            }
            return true;
        }
        return !expected && !actual;
    }
};
