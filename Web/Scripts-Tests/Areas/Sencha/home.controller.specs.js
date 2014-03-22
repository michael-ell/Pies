///
/// <reference path="../../../areas/sencha/touch/sencha-touch.js"/>
/// <reference path="../../../Scripts-Testing/Helpers/sencha.pies.setup.js"/>
/// <reference path="../../../Scripts-Testing/Helpers/testing.js"/> 
/// <reference path="../../../Scripts-Testing/Creators/xhr.js"/> 
///
describe("Displaying pies:", function () {

    describe("When asking to display pies", function() {
        var sut, home, expectedPies;

        beforeEach(function() {
            jasmine.addMatchers(testing.matchers);
            jasmine.clock().install();
            sut = Ext.create('Pies.controller.Home', { application: App });

            home = { setData: function () {} };
            spyOn(home, 'setData');
            spyOn(sut, 'getHome').and.returnValue(home);

            expectedPies = ['some pie'];
            spyOn(Ext.Ajax, 'request').and.callFake(function(config) {
                config.success(new cr8.Xhr(), config);
            });
            spyOn(Ext.JSON, 'decode').and.callFake(function () {
                return expectedPies;
            });

            sut.getPies.call(sut);
            jasmine.clock().tick(10);
        });
        
        it("should get the recent pies", function () {
            expect(Ext.Ajax.request).toHaveBeenCalledWithArgumentContaining({
                url: '/sencha/home/getrecent',
                method: 'GET'
            });
        });

        it("should display the pies", function () {
            expect(home.setData).toHaveBeenCalledWith(expectedPies);
        });
    });    
});