///
/// <reference path="../scripts/jquery-1.9.1.js" />
/// <reference path="../scripts/jquery.mHub.js" />
/// <reference path="../scripts/jquery.mHub.signalR.js" />
/// <reference path="../scripts/knockout-2.2.1.js" />
/// <reference path="../scripts/pies.js" />
/// <reference path="../scripts-testing/helpers/matchers.js" />
/// <reference path="../scripts-testing/helpers/testing.js" />
/// <reference path="../scripts-testing/creators/pie.js" />/// 
/// <reference path="../scripts-testing/creators/ingredient.js" />/// 
/// 
describe("Displaying Pies - ", function () {

    describe("ViewModel Specs - ", function () {
        describe("When displaying pies that are not editable", function () {
            var sut, expectedPies;

            beforeEach(function () {
                this.addMatchers(matching.matchers);
                testing.setup();
                expectedPies = [cr8.Pie()];
                sut = new cc.pies.index.ViewModel(expectedPies);
            });

            it("should display the pies", function () {
                var pies = sut.pies();
                for (var i = 0; i < expectedPies.length; i++) {
                    expect(expectedPies[i].matches(pies[i])).toBe(true);                    
                }
            });
        });
    });
});