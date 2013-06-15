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

    describe("When a percentage for an ingredient is changed", function () {
        //var sut, actions, expectedIngredient;

        //beforeEach(function () {
        //    this.addMatchers(testing.setup());

        //    spyOn($, 'post').andCallFake(function(url, data, success) {
        //        success();
        //    });
        //    expectedIngredient = "blueberries";
        //    actions = new cr8.Actions();
        //    sut = new cc.pies.edit.Pie(new cr8.Pie(), actions, actions);
        //    sut.ingredientToAdd(expectedIngredient);
            
        //    sut.addIngredient();
        //});        

        //it("should call the correct location that adds the ingredient", function () {
        //    expect($.post.calls[0].args[0]).toBe(actions.add);
        //});

        //it("should add the ingredient to the correct pie", function () {
        //    expect($.post.calls[0].args[1].id).toBe(sut.id);
        //});

        //it("should update the tags for the pie", function () {
        //    expect($.post.calls[0].args[1].description).toBe(expectedIngredient);
        //});

        //it("should clear the ingredient that was added", function() {
        //    expect(sut.ingredientToAdd()).toBe('');
        //});
    });
});