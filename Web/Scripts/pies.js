var cc = cc || {};
cc.pies = cc.pies || {};

cc.pies.Index = function(pies, options) {
    var self = this;
    options = options || { findUrl: '' };
    options.editing = options.editing || { isEditable: false, owner: '', actions: { delete: '' } };
    self.tags = ko.observableArray();
    self.selectedTag = ko.observable('');
    self.pies = ko.observableArray();
    self.findUrl = options.findUrl;
    self.find = function () {
        var tag = self.selectedTag() || '';
        $.get(self.findUrl + '/' + tag, function (data) {
            self.pies($.parseJSON(data));
        });
    };
    
    if (options.editing.isEditable === true) {
        self.pies($.map(pies, function (pie) { return new cc.pies.Pie(pie, options.editing.actions); }));
        $.mhub.init(options.editing.owner);
        $.mhub.subscribe($.mhub.messages.pieDeleted, function(data) {
            var unwrapped = self.pies();
            for (var i = 0, len = unwrapped.length; i < len; i++) {
                var pie = unwrapped[i];
                if (pie.id === data.id) {
                    self.pies.remove(pie);
                    break;
                }
            }
        });       
    } else {        
        self.pies(pies);
    }
};

cc.pies.Pie = function (model, actions) {
    var self = this;
    self.actions = actions;
    self.delete = function() {
        $.ajax({ url: self.actions.delete + '/' + self.id, type: 'delete'});
    };
    $.extend(self, model);
};

cc.pies.edit = cc.pies.edit|| {};
cc.pies.edit.Pie = function (model, pieActions, ingredientActions) {
    var self = this;
    self.id = model.id;
    self.pieActions = pieActions;
    self.ingredientActions = ingredientActions;

    function toObservables(ingredients) {
        return $.map(ingredients, function (i) {
            return new cc.pies.edit.Ingredient(self.id, i, self.ingredientActions);
        });
    }

    self.caption = ko.observable(model.caption);
    self.allIngredients = ko.observableArray(model.allIngredients);
    self.ingredientToAdd = ko.observable('');
    self.editableIngredients = ko.observableArray(toObservables(model.editableIngredients));
    self.tags = ko.observable(model.tags);
    self.pieMessage = ko.observable();
    
    self.caption.subscribe(function (caption) {
        if (caption) {
            $.post(self.pieActions.updateCaption, { id: self.id, caption: caption });
        }
    });

    self.tags.subscribe(function(tags) {
        $.post(self.pieActions.updateTags, { id: self.id, tags: tags });
    });

    self.addIngredient = function () {
        var description = self.ingredientToAdd();
        if (description) {
            $.post(self.ingredientActions.add, { id: self.id, description: description }, function () {
                self.ingredientToAdd('');
            });
        }
    };

    $.mhub.init(model.id);    
    $.mhub.subscribe($.mhub.messages.captionUpdated, function (data) {
        self.caption(data);
    });    
    $.mhub.subscribe($.mhub.messages.ingredientsUpdated, function(data) {        
        self.editableIngredients(toObservables(data.ingredients));
        var ingredients = toObservables(data.ingredients);
        if (data.filler.percent > 0) {
            ingredients.push(new cc.pies.edit.Ingredient(self.id, data.filler));
        }
        self.allIngredients(ingredients);
    });    
    $.mhub.subscribe($.mhub.messages.percentageChanged, function (data) {
        var ingredient = Enumerable.From(self.editableIngredients()).Single(function (i) { return i.id === data.id; });
        ingredient.message(data.message);
        ingredient.reverting = true;
        ingredient.percent(data.currentPercent);
        ingredient.reverting = false;
    });    
    $.mhub.subscribe($.mhub.messages.messageReceived, function (data) {
        self.pieMessage(data);
    });
};

cc.pies.edit.Ingredient = function (pieId, model, actions) {
    var self = this;
    self.pieId = pieId,
    self.id = model.id;
    self.percent = ko.observable(model.percent);
    self.description = ko.observable(model.description);
    self.color = ko.observable(model.color);
    self.formattedPercent = ko.computed(function () {
        return self.percent() + '%';
    });
    self.message = ko.observable();
    self.actions = actions;
    self.remove = function() {
        $.ajax({ url: self.actions.delete, type: 'delete', data: { id: self.id, pieId: self.pieId } });
    };
    self.reverting = false;
    self.percent.subscribe(function (newPercent) {
        if (!self.reverting) {
            $.post(self.actions.updatePercentage, { id: self.id, pieId: self.pieId, percent: newPercent });
        }
    });
    self.description.subscribe(function (newDescription) {
        $.post(self.actions.updateDescription, { id: self.id, pieId: self.pieId, description: newDescription });
    });
    self.color.subscribe(function (newColor) {
        $.post(self.actions.updateColor, { id: self.id, pieId: self.pieId, color: newColor });
    });
}