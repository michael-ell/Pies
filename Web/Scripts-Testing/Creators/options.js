var cr8 = cr8 || {};
cr8.Options = function () {
    var self = this;
    self.findUrl = "www.find.com";
    self.editing = { isEditable: false, owner: 'xxx', actions: { delete: 'www.delete.com' } };
    self.noEditing = function() {
        self.editing.isEditable = false;
        return self;
    };
    self.withEditing = function () {
        self.editing.isEditable = true;
        return self;
    };
    return self;
};
