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
/// 
describe("Displaying Pies: ", function () {
    describe("When displaying pies that are NOT editable", function () {
        var sut, expectedPies;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            expectedPies = [cr8.Pie()];
            sut = new cc.pies.Index(expectedPies, new cr8.Options().noEditing());
        });

        it("should display the pies", function () {
            var pies = sut.pies();
            for (var i = 0; i < expectedPies.length; i++) {
                expect(expectedPies[i].matches(pies[i])).toBe(true);                    
            }
        });
    });
        
    describe("When displaying pies that are editable", function () {
        var sut, options, expectedPies;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            spyOn($.mhub, 'init');
            options = new cr8.Options().withEditing();
            expectedPies = [cr8.Pie()];                
            sut = new cc.pies.Index(expectedPies, options);
        });

        it("should start listening to messages for the pies owner", function() {
            expect($.mhub.init).toHaveBeenCalledWith(options.editing.owner);
        });

        it("should display the pies", function () {
            var pies = sut.pies();
            for (var i = 0; i < expectedPies.length; i++) {
                expect(expectedPies[i].matches(pies[i])).toBe(true);
            }
        });
    });

    describe("When finding pies and the user has specified a tag", function () {
        var sut, options, expectedTag, expectedPies;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            options = new cr8.Options().withEditing();
            expectedPies = [new cr8.Pie()];
            expectedTag = "abc";
            spyOn($, 'get').andCallFake(function (url, callback) {
                if (url === options.findUrl + '/' + expectedTag) {
                    callback(expectedPies);
                }
            });
            sut = new cc.pies.Index([], new cr8.Options());
            sut.selectedTag(expectedTag);
            sut.find();
        });

        it("should display the pies that match the tag", function() {
            var pies = sut.pies();
            for (var i = 0; i < expectedPies.length; i++) {
                expect(expectedPies[i].matches(pies[i])).toBe(true);
            }
        });
    });
    
    describe("When finding pies and the user has NOT specified a tag", function () {
        var sut, options, expectedPies;

        beforeEach(function () {
            this.addMatchers(testing.setup());

            options = new cr8.Options().withEditing();
            expectedPies = [new cr8.Pie()];
            spyOn($, 'get').andCallFake(function (url, callback) {
                if (url === options.findUrl + '/') {
                    callback(expectedPies);
                }
            });
            sut = new cc.pies.Index([], new cr8.Options());
            sut.find();
        });

        it("should display all pies", function () {
            var pies = sut.pies();
            for (var i = 0; i < expectedPies.length; i++) {
                expect(expectedPies[i].matches(pies[i])).toBe(true);
            }
        });
    });
});