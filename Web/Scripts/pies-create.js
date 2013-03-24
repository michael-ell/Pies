var pies = pies || {};
pies.cr8 = pies.cr8 || {};

pies.cr8.Pie = function(id, updateCaptionUrl, addIngredientUrl, updateIngredientPercentageUrl) {
    var self = this;
    self.id = id;
    self.updateCaptionUrl = updateCaptionUrl;
    self.addIngredientUrl = addIngredientUrl;
    self.updateIngredientPercentageUrl = updateIngredientPercentageUrl;
    self.caption = ko.observable('');
    self.ingredientToAdd = ko.observable('');
    self.editableIngredients = ko.observableArray();
    self.allIngredients = ko.observableArray();

    self.caption.subscribe(function(caption) {
        if (caption) {
            $.post(self.updateCaptionUrl, { id: self.id, caption: caption });
        }
    });

    self.addIngredient = function() {
        var description = self.ingredientToAdd();
        if (description) {
            $.post(self.addIngredientUrl, { id: self.id, description: description });
        }
    };

    var hub = $.connection.pie;
    hub.client.pieIngredientsUpdated = function (data) {
        var ingredients = $.map(data.ingredients, function(i) {
            return new pies.cr8.Ingredient(i.id, i.description, i.percent, i.pieId, self.updateIngredientPercentageUrl);
        });
        self.editableIngredients(ingredients);
        if (data.filler.percent > 0) {
            ingredients.push(new pies.cr8.Ingredient(data.filler.id, data.filler.description, data.filler.percent, data.filler.pieId));
        }
        self.allIngredients(ingredients);
    };
    $.connection.hub.start();
};

pies.cr8.Ingredient = function (id, desc, percent, pieId, updateIngredientPercentageUrl) {
    var self = this;

    self.id = id;
    self.pieId = pieId,
    self.percent = ko.observable(percent);
    self.description = ko.observable(desc);
    if (updateIngredientPercentageUrl) {
        self.updateIngredientPercentageUrl = updateIngredientPercentageUrl;
        self.percent.subscribe(function(newPercent) {
            $.post(updateIngredientPercentageUrl, { id: self.id, pieId: self.pieId, percent: newPercent });
        });
    }
}