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
describe("Updating is private: ", function () {

    describe("When a user updates the private pie indicator", function () {
        var sut, actions, expected;

        beforeEach(function () {
            jasmine.addMatchers(testing.setup());

            spyOn($, 'post').and.callFake(function () { });
            expected = true;
            actions = new cr8.Actions();
            sut = new cc.pies.edit.Pie(new cr8.Pie(), actions);

            sut.isPrivate(expected);
        });
        

        it("should call the correct location that updates is private", function () {
            expect($.post.calls.argsFor(0)[0]).toBe(actions.updateIsPrivate);
        });

        it("should update the correct pie", function () {
            expect($.post.calls.argsFor(0)[1].id).toBe(sut.id);
        });

        it("should update the is private flag for the pie", function () {
            expect($.post.calls.argsFor(0)[1].isPrivate).toBe(expected);
        });
    });
});