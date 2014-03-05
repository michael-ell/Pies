Ext.Loader.setConfig({
    enabled: true,
    disableCaching: false,   //prevent cache busting
    paths: {
        'Pies': '../../../areas/sencha/app'      //relative to where tests are run
    }       
});
var App = Ext.create('Ext.app.Application', { name: 'Pies' });
Ext.Viewport = { add: function () { }, setMenu: function () { } };