///
/// <reference path="../scripts/jquery-1.9.1.js" />
/// <reference path="../scripts/jquery.signalR-1.0.1.js" />
/// <reference path="../scripts/knockout-2.2.1.js" />
/// <reference path="../scripts/pies.js" />
/// 
describe("Displaying Pies - ", function () {

    describe("ViewModel Specs - ", function () {
        describe("When displaying pies that are not editable", function () {
            var sut, expectedPies;

            beforeEach(function () {

                sut = new cc.pies.index.ViewModel([]);
            });

            it("should display the pies", function () {
                expect(true).toBe(true);
            });
        });
    });
});
