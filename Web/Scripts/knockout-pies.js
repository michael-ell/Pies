ko.bindingHandlers.blur = {
    init: function (el, valueAccessor) {
        $(el).blur(function() {
            var obs = valueAccessor();
            obs($(this).val());
        });
    }
};

ko.bindingHandlers.onEnter = {
    init: function(el, valueAccessor) {
        $(el).keyup(function(e) {
            if (e.which === 13) {
                var $el = $(this);
                $el.blur();
                var obs = valueAccessor();
                obs();
                $el.focus();
            }
        });
    }
};

ko.bindingHandlers.slider = {
    init: function (el, valueAccessor, allBindingsAccessor) {
        var opts = {
            change: function(e, ui) {
                var observable = valueAccessor();
                observable(ui.value);
            }
        };
        $(el).slider($.extend(opts, allBindingsAccessor().sliderOptions || {}));
    },
    
    update: function (el, valueAccessor) {
        $(el).slider('value', ko.utils.unwrapObservable(valueAccessor()));
    }
};

ko.bindingHandlers.pieChart = {
    instance: null,
    init: function (el, valueAccessor) {
        ko.bindingHandlers.pieChart.instance = new Highcharts.Chart({
            chart: {
                renderTo: el.id,
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage}%</b>',
                percentageDecimals: 1
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        color: '#000000',
                        connectorColor: '#000000',
                        formatter: function () {
                            return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';
                        }
                    }
                }
            },
            series: [{
                type: 'pie',
                data: ko.utils.unwrapObservable(valueAccessor())
            }]
        });
    },
    update: function (el, valueAccessor, allBindingsAccessor) {
        var slices = ko.utils.unwrapObservable(valueAccessor());
        var map = allBindingsAccessor().map || { text: '', value: '' };
        slices = $.map(slices, function (slice) {
            return { name: ko.utils.unwrapObservable(slice[map.text]), y: ko.utils.unwrapObservable(slice[map.value]) };
        });
       ko.bindingHandlers.pieChart.instance.series[0].setData(slices);
    }
};