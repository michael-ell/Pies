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
    update: function(el, valueAccessor) {
        var color = ko.utils.unwrapObservable(valueAccessor());
        if (color) {
            $('.ui-slider-handle', $(el)).css('background', color).css('border-color', ko.utils.adjustColor(color, -50));
        }
    }
};

ko.bindingHandlers.color = {
    update: function (el, valueAccessor, allBindingsAccessor) {
        var color = ko.utils.unwrapObservable(valueAccessor());
        if (color) {
            var opts = allBindingsAccessor().colorOptions || {backgroundAdjust: 0, borderAdjust: 0};
            $(el).css('background', ko.utils.adjustColor(color, opts.backgroundAdjust)).css('border-color', ko.utils.adjustColor(color, opts.borderAdjust));
        }
    }
};

ko.bindingHandlers.pieChart = {
    instance: null,
    colors: ['#AA4643', '#4572A7', '#89A54E', '#DB843D', '#80699B', '#3D96AE', '#92A8CD', '#A47D7C', '#B5CA92'],
    init: function (el, valueAccessor, allBindingsAccessor) {
        var map = ko.bindingHandlers.pieChart.map(allBindingsAccessor);
        var data = ko.bindingHandlers.pieChart.data(ko.utils.unwrapObservable(valueAccessor()), map);
        ko.bindingHandlers.pieChart.instance =
            new Highcharts.Chart({
                chart: {
                    renderTo: el,
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false
                },
                title: {
                    text: ''
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            color: '#000000',
                            connectorColor: '#000000',
                            formatter: function() {
                                return '<b>' + this.point.name + '</b>';
                            }
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    data: ko.bindingHandlers.pieChart.transform(data, map),
                    name: 'XXX'
                }]
            });
    },
    update: function (el, valueAccessor, allBindingsAccessor) {
        var map = ko.bindingHandlers.pieChart.map(allBindingsAccessor);
        var data = ko.bindingHandlers.pieChart.data(ko.utils.unwrapObservable(valueAccessor()), map);
        ko.bindingHandlers.pieChart.instance.series[0].setData(ko.bindingHandlers.pieChart.transform(data, map), false);
        setTimeout(function () { ko.bindingHandlers.pieChart.instance.redraw(); }, 1000);
    },
    map: function (allBindingsAccessor) {
        return allBindingsAccessor().map || { text: '', value: '' };
    },
    data: function(obs, map) {
        if (map && map.data) {
            return obs[map.data] || [];
        }
        return obs;
    },
    transform: function (data, map) {
        var max = ko.bindingHandlers.pieChart.colors.length;
        var j = 0;
        return $.map(data, function(slice, i) {
            var color = ko.bindingHandlers.pieChart.colors[j];
            j = i <= max ? j + 1 : 0;
            if (slice.color && typeof(slice.color) === 'function') {
                slice.color(color);
            }
            return { name: ko.utils.unwrapObservable(slice[map.text]), y: ko.utils.unwrapObservable(slice[map.value]), color: color };
        });
    }
};

ko.utils.adjustColor = function(color, amt) {
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
};
