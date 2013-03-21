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
    self.ingredients = ko.observableArray();
    self.ingredientValues = ko.observableArray();

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

    var pie = $.connection.pie;
    pie.client.ingredientAdded = function (data) {
        var ingredient = new pies.cr8.Ingredient(data.id, data.description, data.percent, data.pieId, self.updateIngredientPercentageUrl);
        self.ingredients.push(ingredient);
    };
    pie.client.ingredientPercentUpdated = function(data) { self.ingredientValues.push(data.percent); };
    //pie.client.ingredientToAddPercentageRejected = function (data) {
    //    $.mhub.send(pies.messages.percentRejected(data.Id), data);
    //};
    $.connection.hub.start();
};

pies.cr8.Ingredient = function (id, desc, percent, pieId, updateIngredientPercentageUrl) {
    var self = this;

    self.id = id;
    self.pieId = pieId,
    self.updateIngredientPercentageUrl = updateIngredientPercentageUrl;
    self.percent = ko.observable(percent);
    self.description = ko.observable(desc);
    self.percent.subscribe(function (newPercent) {
        $.post(updateIngredientPercentageUrl, { id: self.id, pieId: self.pieId, percent: newPercent });
    });
}