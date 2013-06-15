var cr8 = cr8 || {};
cr8.Ingredient = function (id) {
    var self = this;
    self.id = !id ? 'x' : id;
    self.percent = 20;
    self.description = self.id + ' description';
    self.color = "blue";
    self.matches = function (other) {
        if (!other) return false;
        return self.id == other.id &&
               (self.percent == other.percent || self.percent == other.percent())  &&
               (self.description == other.description || self.description == other.description()) &&
               (self.color == other.color || self.color == other.color());
    };
    return self;
};

cr8.IngredientsUpdated = function () {
    var self = this;
    self.ingredients = [new cr8.Ingredient()];
    self.filler = new cr8.Ingredient('filler');
    self.withoutFiller = function () {
        self.filler.percent = 0;
        return self;
    };
    return self;
};
