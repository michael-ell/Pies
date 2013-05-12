var cc = cc || {};
cc.pies = cc.pies || {};

cc.pies.index = pies.index || {};
cc.pies.index.ViewModel = function(pies, options) {
    var self = this;
    options = options || { findUrl: ''};
    options.editing = options.editing || { isEditable: false, actions: { delete: '' } };
    self.tags = ko.observableArray();
    self.selectedTag = ko.observable();
    self.pies = ko.observableArray();
    self.findUrl = options.findUrl;
    self.find = function () {
        if (self.selectedTag()) {
            $.get(self.findUrl + '/' + self.selectedTag(), function (data) {
                self.pies($.parseJSON(data));
            });
        }
    };
    
    if (options.editing.isEditable === true) {
        self.pies($.map(pies, function (pie) { return new cc.pies.index.Pie(pie, options.editing.actions); }));

        var hub = $.connection.pie;
        hub.client.pieDeleted = function (data) {
            for (var i = 0, len = self.pies.length; i < len; i++) {
                var pie = self.pies[i]();
                if (pie.id === data.id) {
                    break;
                }
            }
        };
        $.connection.hub.start().done(function () {
            hub.server.join(self.id);
        });        
    } else {
        self.pies(pies);
    }
};

cc.pies.index.Pie = function (dto, actions) {
    var self = this;
    self.actions = actions;
    self.isDeleted = ko.observable(false);
    self.delete = function() {
        $.ajax({ url: self.actions.delete + '/' + self.id, type: 'DELETE'});
    };
    $.extend(self, dto);
};

cc.pies.cr8 = pies.cr8 || {};
cc.pies.cr8.Pie = function (dto, pieActions, ingredientActions) {    
    var self = this;
    self.id = dto.id;
    self.pieActions = pieActions;
    self.ingredientActions = ingredientActions;

    function toObservables(ingredients) {
        return $.map(ingredients, function (i) {
            return new cc.pies.cr8.Ingredient(self.id, i, self.ingredientActions);
        });
    }

    self.caption = ko.observable(dto.caption);
    self.allIngredients = ko.observableArray(dto.allIngredients);
    self.ingredientToAdd = ko.observable('');
    self.editableIngredients = ko.observableArray(toObservables(dto.editableIngredients));
    self.tags = ko.observable(dto.tags);
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

    var hub = $.connection.pie;
    hub.client.captionUpdated = function(data) {
        self.caption(data);
    };
    hub.client.ingredientsUpdated = function (data) {
        var ingredients = toObservables(data.ingredients);
        self.editableIngredients(ingredients);
        if (data.filler.percent > 0) {
            ingredients.push(new pies.cr8.Ingredient(self.id, data.filler));
        }
        self.allIngredients(ingredients);
    };
    hub.client.percentageChanged = function (data) {
        var ingredient = Enumerable.From(self.editableIngredients()).Single(function (i) { return i.id === data.id; });
        ingredient.message(data.message);
        ingredient.reverting = true;
        ingredient.percent(data.currentPercent);
        ingredient.reverting = false;
    };
    hub.client.showMessage = function(data) {
        self.pieMessage(data);
    };
    $.connection.hub.start().done(function () {
        hub.server.join(dto.id);
    });
};

cc.pies.cr8.Ingredient = function (pieId, dto, actions) {
    var self = this;
    self.pieId = pieId,
    self.id = dto.id;
    self.percent = ko.observable(dto.percent);
    self.description = ko.observable(dto.description);
    self.color = ko.observable(dto.color);
    self.formattedPercent = ko.computed(function () {
        return self.percent() + '%';
    });
    self.message = ko.observable();
    self.actions = actions;
    self.remove = function() {
        $.ajax({ url: self.actions.delete, type: 'DELETE', data: { id: self.id, pieId: self.pieId } });
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