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
describe("Updating the color of an ingredient: ", function () {

    describe("When a user changes the color of an ingredient", function () {
        var sut, actions, expectedPieId, expectedColor;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            spyOn($, 'post').andCallFake(function () { });
            var model = new cr8.Ingredient();
            expectedPieId = 'abc';
            expectedColor = model.color + ' - x';            
            actions = new cr8.Actions();
            sut = new cc.pies.edit.Ingredient(expectedPieId, model, actions);

            sut.color(expectedColor);
        });

        it("should call the correct location that updates the color", function () {
            expect($.post.calls[0].args[0]).toBe(actions.updateColor);
        });
        
        it("should update the correct pie", function () {
            expect($.post.calls[0].args[1].pieId).toBe(expectedPieId);
        });

        it("should update the correct ingredient", function () {
            expect($.post.calls[0].args[1].id).toBe(sut.id);
        });

        it("should update the ingredients color", function () {
            expect($.post.calls[0].args[1].color).toBe(expectedColor);
        });
    });
});