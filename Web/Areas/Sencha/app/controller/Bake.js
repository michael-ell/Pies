﻿Ext.define('Pies.controller.Bake', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.model.Pie', 'Pies.view.EditIngredient', 'Ext.data.reader.Json'],
    config: {
        views: ['Bake'],
        refs: { view: '.pies-bake', caption: '.pies-edit #caption', preview: '.pies-bake .pies-pie' },
        control: {
            view: {
                activate: 'createPie'
            },
            caption: {
                change: 'updateCaption'
            },
            '.pies-edit #tags': {
                change: 'updateTags'
            },
            'button[action=addIngredient]': {
                tap: 'addIngredient'
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
        me.getView().setPie(created);
        me.setPie(created);        
    },
    updateCaption: function (scope, caption) {
        console.log('updating caption to ' + caption + '...');
        Ext.Ajax.request({
            url: '/api/pie/updateCaption',
            jsonData: { id: this.getPie().id, caption: caption }
        });
    },
    updateTags: function (scope, tags) {
        console.log('updating tags to ' + tags + '...');
        Ext.Ajax.request({
            url: '/api/pie/updateTags',
            jsonData: { id: this.getPie().id, tags: tags }
        });
    },
    addIngredient: function () {
        Ext.Ajax.request({
            url: '/api/pie/addIngredient',
            jsonData: { id: this.getPie().id, description: '?' }
        });
        //if (!this.editor) {
        //    this.editor = Ext.Viewport.add({ xtype: 'pies-ei' });
        //}
        //this.editor.show();
    }
});