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
    
    describe("When a user enters a caption for a pie that is not empty", function () {
        var sut, expectedCaption;

        beforeEach(function () {
            this.addMatchers(testing.matchers);
            testing.setup();
            
            spyOn($, 'post').andCallFake(function () { });
            sut = new cc.pies.cr8.Pie(new cr8.Pie(), new cr8.Actions());

            sut.caption(expectedCaption);
        });


        it("should update the caption", function () {
            //expect(sut.caption()).toBe(expectedCaption);
        });
    });
    
    describe("When a pies caption is updated", function () {
        var sut, expectedCaption;
        
        beforeEach(function () {
            this.addMatchers(testing.matchers);
            testing.setup();

            expectedCaption = "my awesome pie";
            sut = new cc.pies.cr8.Pie(new cr8.Pie(), new cr8.Actions());

            $.mhub.send($.mhub.messages.captionUpdated, expectedCaption);
        });


        it("should display the new caption", function() {
            expect(sut.caption()).toBe(expectedCaption);
        });
    });   
});