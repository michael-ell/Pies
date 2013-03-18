var pies = pies || {};
pies.cr8 = pies.cr8 || {};

pies.cr8.ViewModel = function (id, updateCaptionUrl, addIngredientUrl) {    
    var self = this;    
    self.id = id;
    self.updateCaptionUrl = updateCaptionUrl;
    self.addIngredientUrl = addIngredientUrl;
    self.caption = ko.observable('');
    self.ingredientToAdd = ko.observable('');
    self.ingredients = ko.observableArray();
    
    self.caption.subscribe(function (caption) {
        if (caption) {
            $.post(self.updateCaptionUrl, { id: self.id, caption: caption });
        }
    });

    self.addIngredient = function () {
        var description = self.ingredientToAdd();
        if (description) {
            $.post(self.addIngredientUrl, { id: self.id, description: description });
        }
    };
    
    var pie = $.connection.pie;
    pie.client.ingredientAdded = function (data) { self.ingredients.push(data); };
    //pie.client.ingredientToAddPercentageRejected = function (data) {
    //    $.mhub.send(pies.messages.percentRejected(data.Id), data);
    //};
    $.connection.hub.start();
}