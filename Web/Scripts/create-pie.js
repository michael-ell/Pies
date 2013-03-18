var pies = pies || {};
pies.cr8 = pies.cr8 || {};

pies.cr8.ViewModel = function(id, updateCaptionUrl, addIngredientUrl) {
    var self = this;

    self.id = id;
    self.updateCaptionUrl = updateCaptionUrl;
    self.caption = ko.observable('');

    self.caption.subscribe(function (caption) {
        if (caption) {
            $.post(self.updateCaptionUrl, { id: self.id, caption: caption });
        }
    });
       
}