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
describe("Editing Pies: ", function () {
    describe("When editing a pie", function () {
        var sut, model;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            spyOn($.mhub, 'init');
            model = new cr8.Pie();
            sut = new cc.pies.edit.Pie(model);
        });

        it("should display the caption for the pie", function() {
            expect(sut.caption()).toBe(model.caption);
        });
        
        it("should display the pie", function () {
            expect(testing.verify.matchingArray(model.allIngredients, sut.allIngredients())).toBe(true);
        });
        
        it("should display the editable ingredients", function () {
            expect(testing.verify.matchingArray(model.editableIngredients, sut.editableIngredients())).toBe(true);
        });

        it("should display the tags for the pie", function () {
            expect(sut.tags()).toBe(model.tags);
        });
        
        it("should not have an ingredient to add", function () {
            expect(sut.ingredientToAdd()).toBeFalsy();
        });
        
        it("should not have a pie message", function () {
            expect(sut.pieMessage()).toBeFalsy();
        });
        
        it("should listen to messages related to the pie", function () {
            expect($.mhub.init).toHaveBeenCalledWith(sut.id);
        });
    });  
});