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
    update: function (el, valueAccessor, allBindingsAccessor) {
        ko.bindingHandlers.color.update($('.ui-slider-handle', $(el)), valueAccessor, allBindingsAccessor);
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
        var map = ko.bindingHandlers.pieChart.chartMap(allBindingsAccessor);
        var obs = ko.utils.unwrapObservable(valueAccessor());
        var data = ko.bindingHandlers.pieChart.data(obs, map);
        var opts = ko.bindingHandlers.pieChart.options(allBindingsAccessor); 
        ko.bindingHandlers.pieChart.instance =
            new Highcharts.Chart({
                chart: {
                    renderTo: el,
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    height: opts.height,
                    width: opts.width                    
                },
                title: {
                    text: ko.bindingHandlers.pieChart.title(obs, map),
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: { enabled: false },
                        showInLegend: true
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Percent',                                     
                    data: ko.bindingHandlers.pieChart.transform(data, allBindingsAccessor),
                }]
            });
    },
    update: function (el, valueAccessor, allBindingsAccessor) {
        var map = ko.bindingHandlers.pieChart.chartMap(allBindingsAccessor);
        var data = ko.bindingHandlers.pieChart.data(ko.utils.unwrapObservable(valueAccessor()), map);
        ko.bindingHandlers.pieChart.instance.series[0].setData(ko.bindingHandlers.pieChart.transform(data, allBindingsAccessor), true);
    },
    options: function(allBindingsAccessor) {
        return allBindingsAccessor().pieOptions || { height: null, width: null };
    },
    chartMap: function (allBindingsAccessor) {
        return ko.bindingHandlers.pieChart.options(allBindingsAccessor).chartMap || { title: '' };
    },
    dataMap: function (allBindingsAccessor) {
        return ko.bindingHandlers.pieChart.options(allBindingsAccessor).dataMap || { text: '', value: '' };
    },
    data: function(obs, map) {
        if (map && map.data) {
            return obs[map.data] || [];
        }
        return obs;
    },
    title: function (obs, map) {
        var val = '?';
        if (map && map.title) {
            return obs[map.title] || val;
        }
        return obs || val;
    },
    transform: function (data, allBindingsAccessor) {
        var map = ko.bindingHandlers.pieChart.dataMap(allBindingsAccessor);
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

ko.bindingHandlers.pieChartTitle = {
    update: function (el, valueAccessor) {
       if (ko.bindingHandlers.pieChart.instance) {
           ko.bindingHandlers.pieChart.instance.setTitle({ text: ko.bindingHandlers.pieChart.title(ko.utils.unwrapObservable(valueAccessor())) });
       }
    },
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
