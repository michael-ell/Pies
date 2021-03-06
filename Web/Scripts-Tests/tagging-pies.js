﻿///
/// <reference path="../scripts/jquery-1.9.1.js" />
/// <reference path="../scripts/jquery.mHub.js" />
/// <reference path="../scripts/jquery.mHub.signalR.js" />
/// <reference path="../scripts/knockout-3.1.0.js" />
/// <reference path="../scripts/pies.js" />
/// <reference path="../scripts-testing/helpers/testing.js" />
/// <reference path="../scripts-testing/creators/pie.js" />
/// <reference path="../scripts-testing/creators/ingredient.js" />
/// <reference path="../scripts-testing/creators/options.js" />
/// <reference path="../scripts-testing/creators/actions.js" />
/// 
describe("Tagging a pie caption: ", function () {

    describe("When a user tags a pie", function () {
        var sut, actions, expectedTags;

        beforeEach(function () {
            jasmine.addMatchers(testing.setup());

            spyOn($, 'post').and.callFake(function () { });
            expectedTags = "a b c";
            actions = new cr8.Actions();
            sut = new cc.pies.edit.Pie(new cr8.Pie(), actions);

            sut.tags(expectedTags);
        });
        

        it("should call the correct location that updates the tags", function () {
            expect($.post.calls.argsFor(0)[0]).toBe(actions.updateTags);
        });

        it("should update the correct pie", function () {
            expect($.post.calls.argsFor(0)[1].id).toBe(sut.id);
        });

        it("should update the tags for the pie", function () {
            expect($.post.calls.argsFor(0)[1].tags).toBe(expectedTags);
        });
    });
});