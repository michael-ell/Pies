Ext.define('Pies.controller.Login', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.view.Login', 'Ext.data.JsonP'],
    config: {
        refs: { view: '.pies-login' },
        control: {
            '.button[action=google]' : {
                tap: 'googleLogin'
            }
        }
    },
    launch: function () {
        this.getView().displayUser({ displayName: 'Sign In' });
    },
    googleLogin: function () {
        gapi.auth.signIn({
            callback: function (result) {
                if (result.status.signed_in) {
                    if (result.status.method == 'AUTO') {
                        Pies.app.fireEvent(Pies.hub.Messages.busy);
                        gapi.client.load('plus', 'v1', function() {
                            var request = gapi.client.plus.people.get({
                                'userId': 'me'
                            });
                            request.execute(function(user) {
                                Pies.app.getController('Login').getView().displayUser(user);
                                
                                Ext.Ajax.request({
                                    url: '/sencha/account/login',
                                    jsonData: { id: user.id, name: user.displayName },
                                    success: function (xhr, opts) {
                                        Pies.app.fireEvent(Pies.hub.Messages.userSignedIn, opts.jsonData);
                                    }
                                });                                
                                Pies.app.fireEvent(Pies.hub.Messages.notBusy);
                            });
                        });
                    }
                } else {
                    console.log('Google+ sign-in failed: ' + result.error);
                }
            }
        });
    }
});