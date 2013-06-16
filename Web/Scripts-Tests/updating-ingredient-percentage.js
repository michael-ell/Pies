///
/// <reference path="../scripts/jquery-1.9.1.js" />
/// <reference path="../scripts/jquery.mHub.js" />
/// <reference path="../scripts/jquery.mHub.signalR.js" />
/// <reference path="../scripts/linq.js" />
/// <reference path="../scripts/knockout-2.2.1.js" />
/// <reference path="../scripts/pies.js" />
/// <reference path="../scripts-testing/helpers/testing.js" />
/// <reference path="../scripts-testing/creators/pie.js" />
/// <reference path="../scripts-testing/creators/ingredient.js" />
/// <reference path="../scripts-testing/creators/options.js" />
/// <reference path="../scripts-testing/creators/actions.js" />
/// 
describe("Updating the percentage of an ingredient: ", function () {

    describe("When a user changes the percentage of an ingredient", function () {
        var sut, actions, expectedPieId, expectedPercentage;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            spyOn($, 'post').andCallFake(function () { });
            var model = new cr8.Ingredient();
            expectedPieId = 'abc';
            expectedPercentage = model.percent + 5;            
            actions = new cr8.Actions();
            sut = new cc.pies.edit.Ingredient(expectedPieId, model, actions);

            sut.percent(expectedPercentage);
        });

        it("should call the correct location that updates the percentage", function () {
            expect($.post.calls[0].args[0]).toBe(actions.updatePercentage);
        });
        
        it("should update the correct pie", function () {
            expect($.post.calls[0].args[1].pieId).toBe(expectedPieId);
        });

        it("should update the correct ingredient", function () {
            expect($.post.calls[0].args[1].id).toBe(sut.id);
        });

        it("should update the ingredients percentage", function () {
            expect($.post.calls[0].args[1].percent).toBe(expectedPercentage);
        });
    });

    describe("When a percentage for an ingredient is changed", function () {
        var sut, changed;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            var model = new cr8.Pie();
            changed = new cr8.ChangePercentage().for(model.editableIngredients[0]);
            sut = new cc.pies.edit.Pie(model, new cr8.Actions());

            $.mhub.send($.mhub.messages.percentageChanged, changed);
        });        

        it("should display the current percentage for the ingredient", function () {
            var ingredient = Enumerable.From(sut.editableIngredients()).Single(function (i) { return i.id === changed.id; });
            expect(ingredient.percent()).toBe(changed.currentPercent);
        });

        it("should display any associated message with the change", function () {
            var ingredient = Enumerable.From(sut.editableIngredients()).Single(function (i) { return i.id === changed.id; });
            expect(ingredient.message()).toBe(changed.message);
        });
    });
});