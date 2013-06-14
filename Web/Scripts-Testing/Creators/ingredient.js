var cr8 = cr8 || {};
cr8.Ingredient = function () {
    var self = this;
    self.id = "x";
    self.percent = 20;
    self.description = "some ingredient";
    self.color = "blue";
    self.matches = function (other) {
        if (!other) return false;
        return self.id == other.id &&
               self.percent == other.percent &&
               self.description == other.description &&
               self.color == other.color;
    };
    return self;
};
