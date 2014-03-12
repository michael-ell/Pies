var testing = testing || {};

testing.setup = function () {
    $.mhub.reset();
    $.mhub.start = function () {
        //do nothing...
    };
    return testing.matchers;
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

    toHaveItem: function (util, customEqualityTesters) {
        return {
            compare: function(actual, expected) {
                if (actual && actual instanceof Array) {
                    for (var i = 0; i < actual.length; i++) {
                        if (expected.matches && $.isFunction(expected.matches) && expected.matches(actual[i])) {
                            return {pass: true};
                        } else if (util.equals(actual[i], expected, customEqualityTesters)) {
                            return {pass: true};
                        }
                    }
                }
                return { pass: util.equals(actual, expected, customEqualityTesters) };
            }
        };
    },
    
    toHaveBeenCalledWithArgumentContaining: function () {
        return {
            compare: function(actual, expected) {
                for (var property in expected) {
                    for (var i = 0, x = actual.calls.count(); i < x ; i++) {
                        for (var j = 0; j < actual.calls.argsFor(i).length; j++) {
                            if (actual.calls.argsFor(i)[j][property] && actual.calls.argsFor(i)[j][property] == expected[property]) {
                                return {pass: true};
                            }
                        }
                    }
                }
                return {pass: false};
            }
        };
    },
    
    toHaveBeenCalledWithArgument: function () {
        return {
            compare: function(actual, expected) {
                for (var property in expected) {
                    for (var i = 0, x = actual.calls.count(); i < x ; i++) {
                        for (var j = 0; j < actual.calls.argsFor(i).length; j++) {
                            if (actual.calls.argsFor(i)[j][property] && actual.calls.argsFor(i)[j][property] != expected[property]) {
                                return {pass: false};
                            }
                        }
                    }
                }
                return {pass: true};
            }
        };
    }
};

testing.verify = {
    matchingArray: function(expected, actual, comparer) {
        if (expected && actual) {
            if (expected.length !== actual.length) {
                return false;
            }
            for (var i = 0; i < expected.length; i++) {
                if (comparer && $.isFunction(comparer)) {
                    if (!comparer(expected[i], actual[i])) {
                        return false;
                    }
                }
                else if (expected[i].matches && $.isFunction(expected[i].matches)) {
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
