var cr8 = cr8 || {};
cr8.Pie = function () {
    var self = this;
    self.id = "x";
    self.caption = "My Pie";
    self.allIngredients = [new cr8.Ingredient('a'), new cr8.Ingredient('b')];
    self.editableIngredients = [new cr8.Ingredient('b')];
    self.tags = ['x', 'y', 'z'];
    self.matches = function (other) {
        if (!other) return false;
        return self.id == other.id &&
               (self.caption == other.caption || self.caption == other.caption()) &&
               testing.verify.matchingArray(self.allIngredients, other.allIngredients) &&
               testing.verify.matchingArray(self.editableIngredients, other.editableIngredients) &&
               testing.verify.matchingArray(self.tags, other.tags);
    };
    self.toJSON = function() {
        return JSON.stringify(self);
    };
    return self;
};
