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
describe("Removing ingredients: ", function () {
    describe("When the user removes an ingredient", function () {
        var sut, actions, expectedPieId;
        
        beforeEach(function () {
            this.addMatchers(testing.setup());
            
            spyOn($, 'ajax').andCallFake(function() { });
            expectedPieId = 'abc';
            actions = new cr8.Actions();
            sut = new cc.pies.edit.Ingredient(expectedPieId, new cr8.Ingredient(), actions);
            
            sut.remove();
        });


        it("should call the correct location that removes the ingredient", function () {
            expect($.ajax.calls[0].args[0].url).toBe(actions.delete);
            expect($.ajax.calls[0].args[0].type).toBe('delete');
        });

        it("should remove the ingredient from the correct pie", function () {
            expect($.ajax.calls[0].args[0].data.pieId).toBe(expectedPieId);
        });

        it("should remove the correct ingredient", function () {
            expect($.ajax.calls[0].args[0].data.id).toBe(sut.id);
        });
    });    
});