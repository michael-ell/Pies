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
describe("Editing Pies - ", function () {
    describe("When editing a pie", function () {
        var sut;

        beforeEach(function () {
            this.addMatchers(testing.matchers);
            testing.setup();

            spyOn($.mhub, 'init');
            sut = new cc.pies.edit.Pie(new cr8.Pie());
        });


        it("should listen to messages related to the pie", function () {
            expect($.mhub.init).toHaveBeenCalledWith(sut.id);
        });
    });

    describe("When a pies caption is updated", function () {
        var sut, expectedCaption;
        
        beforeEach(function () {
            this.addMatchers(testing.matchers);
            testing.setup();          
            sut = new cc.pies.edit.Pie(new cr8.Pie());

            $.mhub.send($.mhub.messages.captionUpdated, expectedCaption);
        });


        it("should display the new caption", function() {
            expect(sut.caption()).toBe(expectedCaption);
        });
    });   
});