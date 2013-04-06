ko.bindingHandlers.blur = {
    init: function (el, valueAccessor) {
        $(el).blur(function () {
            var obs = valueAccessor();
            obs($(this).val());
        });
    }
};

ko.bindingHandlers.onEnter = {
    init: function (el, valueAccessor) {
        $(el).keyup(function (e) {
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
            change: function (e, ui) {
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

ko.bindingHandlers.sliderColor = {
    update: function (el, valueAccessor) {
        var color = ko.utils.unwrapObservable(valueAccessor());
        if (color) {
            $('.ui-slider-handle', $(el)).css('background', color).css('border-color', ko.bindingHandlers.sliderColor.adjust(color, -50));
        }
    },
    adjust: function(color, amt){
        var usePound = false;
        if (color[0] == "#") {
            color = color.slice(1);
            usePound = true;
        }
        
        var num = parseInt(color, 16);
        var r = (num >> 16) + amt;
        if (r > 255) r = 255;
        else if (r < 0) r = 0;
        var b = ((num >> 8) & 0x00FF) + amt;

        if (b > 255) b = 255;
        else if (b < 0) b = 0;

        var g = (num & 0x0000FF) + amt;
        if (g > 255) g = 255;
        else if (g < 0) g = 0;

        return (usePound ? "#" : "") + (g | (b << 8) | (r << 16)).toString(16);
    }
};

ko.bindingHandlers.pieChart = {
    instance: null,
    colors: ['#4572A7', '#AA4643', '#89A54E', '#80699B', '#3D96AE', '#DB843D', '#92A8CD', '#A47D7C', '#B5CA92'],
    init: function (el, valueAccessor) {
        ko.bindingHandlers.pieChart.instance = new Highcharts.Chart({
            chart: {
                renderTo: el.id,
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false
            },
            title: {
                text: ''
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
                data: ko.utils.unwrapObservable(valueAccessor()),
                name: 'My Pie'
            }]
        });
    },
    update: function (el, valueAccessor, allBindingsAccessor) {
        var slices = ko.utils.unwrapObservable(valueAccessor());
        var map = allBindingsAccessor().map || { text: '', value: '' };
        var max = ko.bindingHandlers.pieChart.colors.length;
        var j = 0;
        slices = $.map(slices, function (slice, i) {
            var color = ko.bindingHandlers.pieChart.colors[j];
            j = i <= max ? j + 1 : 0;
            if (slice.color && typeof (slice.color) === 'function') {
                slice.color(color);
            }
            return { name: ko.utils.unwrapObservable(slice[map.text]), y: ko.utils.unwrapObservable(slice[map.value]), color: color };
        });
        ko.bindingHandlers.pieChart.instance.series[0].setData(slices, false);
        setTimeout(function () { ko.bindingHandlers.pieChart.instance.redraw(); }, 1000);
    }
};