Ext.define('Pies.controller.Bake', {
    extend: 'Ext.app.Controller',
    requires: ['Pies.model.Pie', 'Pies.view.EditIngredient', 'Pies.hub.Bus', 'Pies.hub.Messages', 'Ext.data.reader.Json'],
    config: {
        views: ['Bake'],
        refs: { view: '.pies-bake .pies-edit', caption: '.pies-edit #caption', preview: '.pies-bake .pies-pie' },
        control: {
            '.pies-bake': {
                show: 'createPie',
                hide: 'done'
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
        Pies.hub.Bus.start(created.id)
                          .subscribe(Pies.hub.Messages.captionUpdated, me.captionUpdated, me)
                          .subscribe(Pies.hub.Messages.ingredientsUpdated, me.ingredientsUpdated, me)
                          .subscribe(Pies.hub.Messages.percentageChanged, me.percentageChanged, me);
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
    captionUpdated: function (data) {
        this.getCaption().setValue(data);
        this.getPreview().updateCaption(data);
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
    ingredientsUpdated: function (data) {
        this.getView().updateIngredients(data);
        this.getPreview().updateIngredients(data);        
    },
    updateIngredientPercentage: function (opts) {
        Ext.Ajax.request({
            url: '/api/pie/updateIngredientPercentage',
            jsonData: { pieId: this.getPie().id, id: opts.scope.getData().id, percent: opts.percent }
        });
    },
    percentageChanged: function (data) {
        var ingredients = this.getView().query('.pies-ei') || [];
        for (var index = 0, count = ingredients.length; index < count; index++) {
            var i = ingredients[index];
            if (i.getRecord().data.id == data.id) {
                i.updatePercent(data.currentPercent, data.message);
                break;
            }
        }
    },
    done: function() {
        Pies.hub.Bus.stop();
    }
});