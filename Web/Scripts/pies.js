var cc = cc || {};
cc.pies = cc.pies || {};

cc.pies.Controller = function(pies, options) {
    var me = this;
    options = options || { findUrl: '' };
    options.editing = options.editing || { isEditable: false, owner: '', actions: { remove: '' } };
    me.tags = ko.observableArray();
    me.selectedTag = ko.observable('');
    me.pies = ko.observableArray();
    me.findUrl = options.findUrl;
    me.find = function () {
        var tag = me.selectedTag() || '';
        $.get(me.findUrl + '/' + tag, function (data) {
            me.pies(data);
        });
    };
    if (options.editing.isEditable === true) {
        me.pies($.map(pies, function (pie) { return new cc.pies.Pie(pie, options.editing.actions, me); }));
        $.mhub.init(options.editing.owner);
        $.mhub.subscribe($.mhub.messages.pieDeleted, function(data) {
            var unwrapped = me.pies();
            for (var i = 0, len = unwrapped.length; i < len; i++) {
                var pie = unwrapped[i];
                if (pie.id === data.id) {
                    me.pies.remove(pie);
                    break;
                }
            }
        });       
    } else {        
        me.pies(pies);
    }
};

cc.pies.Pie = function (dto, actions, controller) {
    var me = this;
    me.actions = actions;
    me.remove = function() {
        $.ajax({ url: me.actions.remove + '/' + me.id, type: 'delete'});
    };   
    me.guess = function () {
        controller.startGuessing(me.id);
    };
    $.extend(me, dto);
};

cc.pies.edit = cc.pies.edit|| {};
cc.pies.edit.Pie = function (dto, pieActions, ingredientActions) {
    var me = this;
    me.id = dto.id;
    me.pieActions = pieActions;
    me.ingredientActions = ingredientActions;

    function toViewModels(ingredients) {
        return $.map(ingredients, function (ingredient) {
            return new cc.pies.edit.Ingredient(me.id, ingredient, me.ingredientActions);
        });
    }

    me.caption = ko.observable(dto.caption);
    me.allIngredients = ko.observableArray(dto.allIngredients);
    me.ingredientToAdd = ko.observable();
    me.editableIngredients = ko.observableArray(toViewModels(dto.editableIngredients));
    me.tags = ko.observable(dto.tags);
    me.isPrivate = ko.observable(dto.isPrivate);
    me.pieMessage = ko.observable();
    
    me.caption.subscribe(function (caption) {
        if (caption) {
            $.post(me.pieActions.updateCaption, { id: me.id, caption: caption });
        }
    });

    me.tags.subscribe(function(tags) {
        $.post(me.pieActions.updateTags, { id: me.id, tags: tags });
    });
    
    me.isPrivate.subscribe(function (val) {
        $.post(me.pieActions.updateIsPrivate, { id: me.id, isPrivate: val });
    });

    me.addIngredient = function () {
        var description = me.ingredientToAdd();
        if (description) {
            $.post(me.ingredientActions.add, { id: me.id, description: description }, function () {
                me.ingredientToAdd('');
            });
        }
    };

    $.mhub.init(dto.id);
    $.mhub.subscribe($.mhub.messages.captionUpdated, function (data) {
        me.caption(data);
    });    
    $.mhub.subscribe($.mhub.messages.ingredientsUpdated, function(data) {        
        me.editableIngredients(toViewModels(data.ingredients));
        var ingredients = toViewModels(data.ingredients);
        if (data.filler.percent > 0) {
            ingredients.push(new cc.pies.edit.Ingredient(me.id, data.filler));
        }
        me.allIngredients(ingredients);
    });    
    $.mhub.subscribe($.mhub.messages.percentageChanged, function (data) {
        var ingredient = Enumerable.From(me.editableIngredients()).Single(function (i) { return i.id === data.id; });
        ingredient.message(data.message);
        ingredient.reverting = true;
        ingredient.percent(data.currentPercent);
        ingredient.reverting = false;
    });    
    $.mhub.subscribe($.mhub.messages.messageReceived, function (data) {
        me.pieMessage(data);
    });
};

cc.pies.edit.Ingredient = function (pieId, model, actions) {
    var me = this;
    me.pieId = pieId,
    me.id = model.id;
    me.percent = ko.observable(model.percent);
    me.description = ko.observable(model.description);
    me.color = ko.observable(model.color);
    me.formattedPercent = ko.computed(function () {
        return me.percent() + '%';
    });
    me.message = ko.observable(model.message);
    me.actions = actions;
    me.remove = function() {
        $.ajax({ url: me.actions.remove, type: 'delete', data: { id: me.id, pieId: me.pieId } });
    };
    me.reverting = false;
    me.percent.subscribe(function (newPercent) {
        if (!me.reverting) {
            $.post(me.actions.updatePercentage, { id: me.id, pieId: me.pieId, percent: newPercent });
        }
    });
    me.description.subscribe(function (newDescription) {
        $.post(me.actions.updateDescription, { id: me.id, pieId: me.pieId, description: newDescription });
    });
    me.color.subscribe(function (newColor) {
        $.post(me.actions.updateColor, { id: me.id, pieId: me.pieId, color: newColor });
    });
}