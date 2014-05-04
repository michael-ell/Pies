Ext.define('Pies.controller.Bake', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.model.Pie', 'Pies.view.EditIngredient', 'Ext.data.reader.Json'],
    config: {
        views: ['Bake'],
        refs: { view: '.pies-bake .pies-edit', caption: '.pies-edit #caption', preview: '.pies-bake .pies-pie' },
        control: {
            '.pies-bake': {
                activate: 'createPie'
            },
            caption: {
                change: 'updateCaption'
            },
            '.pies-edit button[action=addIngredient]': {
                tap: 'addIngredient'
            },
            '.pies-edit #description': {
                change: 'updateIngredientDescription'
            },
            '.pies-edit #percentage': {
                percentChange: 'updateIngredientPercentage'
            }
        },
        pie: null
    },
    constructor: function () { 
        this.callParent(arguments);
        var hub = $.connection.pie;
        hub.client.captionUpdated = function (data) {
            var c = Pies.app.getController('Bake');
            c.getCaption().setValue(data);
            c.getPreview().updateCaption(data);
        };
        hub.client.ingredientsUpdated = function (data) {
            var c = Pies.app.getController('Bake');
            c.getView().updateIngredients(data);
            c.getPreview().updateIngredients(data);
        };
    },
    createPie: function () {
        Ext.Ajax.request({         
            url: '/api/pie/create',
            scope: this,            
            success: this.showPie
        });
    },
    showPie: function(xhr, opts) {
        var created = Ext.JSON.decode(xhr.responseText);
        var me = opts.scope;
        $.connection.hub.stop();
        $.connection.hub.start().done(function () {
            $.connection.pie.server.join(created.id);
            console.log('joined hub: ' + created.id);
        });        
        console.log('created pie ' + created.id + '...');
        me.setPie(created);
        me.getView().setPie(created);
        me.getPreview().setPie(created);
        me.getCaption().setValue(created.caption);
    },
    updateCaption: function (scope, caption) {
        Ext.Ajax.request({
            url: '/api/pie/updateCaption',
            jsonData: { id: this.getPie().id, caption: caption }
        });
    },
    addIngredient: function () {
        Ext.Ajax.request({
            url: '/api/pie/addIngredient',
            jsonData: { id: this.getPie().id, description: '?' }
        });
    },
    updateIngredientDescription: function (scope, desc) {
        Ext.Ajax.request({
            url: '/api/pie/updateIngredientDescription',
            jsonData: { pieId: this.getPie().id, id: scope.getData().id, description: desc }
        });
    },
    updateIngredientPercentage: function (opts) {
        Ext.Ajax.request({
            url: '/api/pie/updateIngredientPercentage',
            jsonData: { pieId: this.getPie().id, id: opts.scope.getData().id, percent: opts.percent }
        });
    }
});