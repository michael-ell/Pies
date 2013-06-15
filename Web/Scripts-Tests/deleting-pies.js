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
describe("Deleting Pies: ", function () {
    describe("When deleting a pie", function () {
        var sut, pieToDelete, actions;
        
        beforeEach(function () {
            this.addMatchers(testing.setup());
            
            pieToDelete = new cr8.Pie();
            actions = new cr8.Actions();
            spyOn($, 'ajax').andCallFake(function() {});            
            sut = new cc.pies.Pie(pieToDelete, actions);
            
            sut.delete();
        });


        it("should delete the pie", function() {
            expect($.ajax).toHaveBeenCalledWithArgument({
                url: actions.delete + "/" + pieToDelete.id,
                type: 'delete'
            });
        });
    });
    
    describe("When a pie is deleted", function () {
        var sut, pieToDelete;

        beforeEach(function () {
            this.addMatchers(testing.setup());
            
            pieToDelete = new cr8.Pie();
            sut = new cc.pies.Index([pieToDelete], new cr8.Options().withEditing());

            $.mhub.send($.mhub.messages.pieDeleted, pieToDelete);
        });

        it("should no longer show the pie", function () {
            expect(sut.pies()).not.toHaveItem(pieToDelete);
        });
    });       
});