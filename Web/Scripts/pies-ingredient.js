var pies = pies || {};
pies.ingredient = pies.ingredient || {};

pies.ingredient.ViewModel = function(id, pieId, updateIngredientPercentageUrl) {
    var self = this;

    self.id = id;
    self.pieId = pieId,
    self.updateIngredientPercentageUrl = updateIngredientPercentageUrl;
    self.percent = ko.observable();
    self.description = ko.observable();
    self.percent.subscribe(function (newPercent) {
        $.post(updateIngredientPercentageUrl, { id: self.id, pieId: self.pieId, percent: newPercent });
    });
}