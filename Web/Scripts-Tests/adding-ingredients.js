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
describe("Adding ingredients to a pie: ", function () {

    describe("When a user tries to add an ingredient to a pie without a description", function () {
        var sut;

        beforeEach(function () {
            jasmine.addMatchers(testing.setup());

            spyOn($, 'post').and.callFake(function () {});
            sut = new cc.pies.edit.Pie(new cr8.Pie(), new cr8.Actions());
            sut.ingredientToAdd('');
            
            sut.addIngredient();
        });

        it("should not attempt to add the ingredient", function () {
            expect($.post).not.toHaveBeenCalled();
        });
    });

    describe("When a user adds an ingredient to a pie with a description", function () {
        var sut, actions, expectedIngredient;

        beforeEach(function () {
            jasmine.addMatchers(testing.setup());

            spyOn($, 'post').and.callFake(function(url, data, success) {
                success();
            });
            expectedIngredient = "blueberries";
            actions = new cr8.Actions();
            sut = new cc.pies.edit.Pie(new cr8.Pie(), actions, actions);
            sut.ingredientToAdd(expectedIngredient);
            
            sut.addIngredient();
        });        

        it("should call the correct location that adds the ingredient", function () {
            expect($.post.calls.argsFor(0)[0]).toBe(actions.add);
        });

        it("should add the ingredient to the correct pie", function () {
            expect($.post.calls.argsFor(0)[1].id).toBe(sut.id);
        });

        it("should update the tags for the pie", function () {
            expect($.post.calls.argsFor(0)[1].description).toBe(expectedIngredient);
        });

        it("should clear the ingredient that was added", function() {
            expect(sut.ingredientToAdd()).toBe('');
        });
    });
});