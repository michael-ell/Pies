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
describe("Editing Ingredients: ", function () {
    describe("When editing an ingredient", function () {
        var sut, model;

        beforeEach(function () {
            jasmine.addMatchers(testing.setup());

            spyOn($.mhub, 'init');
            model = new cr8.Ingredient();
            sut = new cc.pies.edit.Ingredient('abc', model, new cr8.Actions());
        });

        it("should display the proper percentage of the ingredient within the pie", function() {
            expect(sut.percent()).toBe(model.percent);
        });
        
        it("should display a formatted percentage of the ingredient within the pie", function () {
            expect(sut.formattedPercent()).toBe(model.percent + '%');
        });
        
        it("should display the description of the ingredient", function () {
            expect(sut.description()).toBe(model.description);
        });
        
        it("should display the color of the ingredient", function () {
            expect(sut.color()).toBe(model.color);
        });
        
        it("should not have an ingredient message", function () {
            expect(sut.message()).toBeFalsy();
        });
    });  
});