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
        //this.getView().displayUser({ displayName: 'Sign In', image: { url: 'https://lh3.googleusercontent.com/-XdUIqdMkCWA/AAAAAAAAAAI/AAAAAAAAAAA/4252rscbv5M/photo.jpg?sz=50' } });
        this.getView().displayUser({ displayName: 'Sign In' });
    },
    googleLogin: function() {
        gapi.auth.signIn({
            callback: function (result) {
                gapi.client.load('plus', 'v1', function () {     
                    var request = gapi.client.plus.people.get({
                        'userId': 'me'
                    });
                    request.execute(function (user) {
                        Pies.app.getController('Login').getView().displayUser(user);
                    });
                });
        }});
    }
});