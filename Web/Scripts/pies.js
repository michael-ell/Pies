var pies = pies || {};

pies.Show = function(pies) {
    var self = this;
    self.pies = ko.observableArray(pies);
};

pies.cr8 = pies.cr8 || {};
pies.cr8.Pie = function (id, updateCaptionUrl, ingredientUrls) {
    var self = this;
    self.id = id;
    self.updateCaptionUrl = updateCaptionUrl;
    self.ingredientUrls = ingredientUrls;
    self.caption = ko.observable();
    self.ingredientToAdd = ko.observable('');
    self.editableIngredients = ko.observableArray();
    self.allIngredients = ko.observableArray([{percent: 100, description: 'Filler'}]);

    self.caption.subscribe(function (caption) {
        if (caption) {
            $.post(self.updateCaptionUrl, { id: self.id, caption: caption });
        }
    });

    self.addIngredient = function () {
        var description = self.ingredientToAdd();
        if (description) {
            $.post(self.ingredientUrls.add, { id: self.id, description: description }, function () {
                self.ingredientToAdd('');
            });
        }
    };

    var hub = $.connection.pie;
    hub.client.captionUpdated = function(data) {
        self.caption(data);
    };
    hub.client.ingredientsUpdated = function (data) {
        var ingredients = $.map(data.ingredients, function (i) {
            return new pies.cr8.Ingredient(i, self.ingredientUrls);
        });
        self.editableIngredients(ingredients);
        if (data.filler.percent > 0) {
            ingredients.push(new pies.cr8.Ingredient(data.filler));
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
    $.connection.hub.start().done(function () {
        hub.server.join(id);
    });
};

pies.cr8.Ingredient = function (dto, urls) {
    var self = this;

    self.id = dto.id;
    self.percent = ko.observable(dto.percent);
    self.description = ko.observable(dto.description);
    self.color = ko.observable(dto.color);
    self.pieId = dto.pieId,
    self.formattedPercent = ko.computed(function () {
        return self.percent() + '%';
    });
    self.message = ko.observable();
    self.urls = urls;
    self.remove = function() {
        $.ajax({ url: self.urls.delete, type: 'DELETE', data: { id: self.id, pieId: self.pieId } });
    };
    self.reverting = false;
    self.percent.subscribe(function (newPercent) {
        if (!self.reverting) {
            $.post(self.urls.updatePercentage, { id: self.id, pieId: self.pieId, percent: newPercent });
        }
    });
}