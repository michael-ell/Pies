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
describe("Updating a pie caption - ", function () {

    describe("When a user enters a caption for a pie that is empty", function () {
        var sut;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            spyOn($, 'post').andCallFake(function () { });
            sut = new cc.pies.edit.Pie(new cr8.Pie(), new cr8.Actions());

            sut.caption('');
        });
        
        it("should not attempt to update the pie caption", function() {
            expect($.post).not.toHaveBeenCalled();
        });
    });
    
    describe("When a user enters a caption for a pie that is NOT empty", function () {
        var sut, actions, expectedCaption;

        beforeEach(function () {
            this.addMatchers(testing.setup());
            
            spyOn($, 'post').andCallFake(function () { });
            expectedCaption = "my wicked pie";
            actions = new cr8.Actions();
            sut = new cc.pies.edit.Pie(new cr8.Pie(), actions);

            sut.caption(expectedCaption);
        });

        it("should call the correct location that updates the caption", function () {
            expect($.post.calls[0].args[0]).toBe(actions.updateCaption);
        });
        
        it("should update the correct pie", function () {
            expect($.post.calls[0].args[1].id).toBe(sut.id);
        });
        
        it("should update the pie caption to be the user entered caption", function () {
            expect($.post.calls[0].args[1].caption).toBe(expectedCaption);
        });
    });
    
    describe("When a pie caption is updated", function () {
        var sut, expectedCaption;
        
        beforeEach(function () {
            this.addMatchers(testing.setup());

            expectedCaption = "my awesome pie";
            sut = new cc.pies.edit.Pie(new cr8.Pie(), new cr8.Actions());

            $.mhub.send($.mhub.messages.captionUpdated, expectedCaption);
        });

        it("should display the new caption", function() {
            expect(sut.caption()).toBe(expectedCaption);
        });
    });   
});