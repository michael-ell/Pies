///
/// <reference path="../scripts/jquery-1.9.1.js" />
/// <reference path="../scripts/jquery.mHub.js" />
/// <reference path="../scripts/jquery.mHub.signalR.js" />
/// <reference path="../scripts/knockout-2.2.1.js" />
/// <reference path="../scripts/pies.js" />
/// <reference path="../scripts-testing/helpers/testing.js" />
/// <reference path="../scripts-testing/creators/pie.js" />
/// <reference path="../scripts-testing/creators/ingredient.js" />
/// <reference path="../scripts-testing/creators/options.js" />
/// <reference path="../scripts-testing/creators/actions.js" />
/// 
describe("Ingredient changes: ", function () {

    describe("When ingredients are updated for a pie", function () {
        var sut, updated;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            updated = new cr8.IngredientsUpdated();
            sut = new cc.pies.edit.Pie(new cr8.Pie(), new cr8.Actions());

            $.mhub.send($.mhub.messages.ingredientsUpdated, updated);
        });
        
        it("should display the pie with the updated ingredients", function () {
           updated.ingredients.push(updated.filler);
           expect(testing.verify.matchingArray(updated.ingredients, sut.allIngredients())).toBe(true);
        });

        it("should display the new editable ingredients", function () {
            expect(testing.verify.matchingArray(updated.ingredients, sut.editableIngredients())).toBe(true);
        });
    });
});