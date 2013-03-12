(function () {
    $.mhub = {
        msgHub: {},
        reset: function () {
            this.msgHub = null;
            this.msgHub = { };
        },
        create: function (name) {
            if (!this.msgHub[name]) {
                this.msgHub[name] = [];
                return true;
            } else {
                // this name being used;
                return false;
            }
        },
        remove: function (name) {
            if (this.msgHub[name]) {
                delete this.msgHub[name];
            }
        },
        send: function (name, msg) {
            var len, i;
            if (this.msgHub[name]) {
                len = this.msgHub[name].length;
                for (i = 0; i < len; i++) {
                    if (typeof (this.msgHub[name][i].callback) === "function") {
                        if (this.msgHub[name][i].scope) {
                            var that = this.msgHub[name][i].scope;
                            this.msgHub[name][i].callback.apply(that, [name, msg]);
                        } else {
                            this.msgHub[name][i].callback(msg);
                        }
                    } else {
                        throw ("Error in mhub.send, callback is not a function on mhub name = " + name);
                    }
                }
                return true;
            } else {
                // no such hub
                return false;
            }
        },
        subscribe: function (name, callback, scope) {
            if (this.msgHub[name]) {
                this.msgHub[name].push({ "callback": callback, "scope": scope });
                return true;
            } else {
                //this no such hub;
                return false;
            }
        },
        unsubscribe: function (name, callback, scope) {
            if (this.msgHub[name]) {
                var index = -1;
                for (var i = 0; i < this.msgHub[name].length; i++) {
                    var handler = this.msgHub[name][i];
                    if (handler.callback == callback && handler.scope == scope) {
                        index = i;
                        break;
                    }
                }
                if (index !== -1) {
                    this.msgHub[name].splice(index, 1);
                }
                return true;
            } else {
                //this no such hub;
                return false;
            }
        }
    };
} ());