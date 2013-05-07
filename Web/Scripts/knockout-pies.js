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

ko.bindingHandlers.message = {
    init: function (el) {
        $(el).hide();
    },
    update: function (el, valueAccessor) {
        if (valueAccessor()) {
            $(el).fadeIn(1000);
            setTimeout(function () { $(el).fadeOut(1000); }, 3000);
        }
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

ko.bindingHandlers.colorPicker = {
    init: function (el, valueAccessor) {       
        $(el).spectrum(
            {
                color: ko.utils.unwrapObservable(valueAccessor()),
                showInput: true,
                showInitial: true,
                showPalette: true,
                showSelectionPalette: true,
                maxPaletteSize: 10,
                preferredFormat: "hex",
                palette: [
                    ["rgb(0, 0, 0)", "rgb(67, 67, 67)", "rgb(102, 102, 102)",
                    "rgb(204, 204, 204)", "rgb(217, 217, 217)","rgb(255, 255, 255)"],
                    ["rgb(152, 0, 0)", "rgb(255, 0, 0)", "rgb(255, 153, 0)", "rgb(255, 255, 0)", "rgb(0, 255, 0)",
                    "rgb(0, 255, 255)", "rgb(74, 134, 232)", "rgb(0, 0, 255)", "rgb(153, 0, 255)", "rgb(255, 0, 255)"], 
                    ["rgb(230, 184, 175)", "rgb(244, 204, 204)", "rgb(252, 229, 205)", "rgb(255, 242, 204)", "rgb(217, 234, 211)", 
                    "rgb(208, 224, 227)", "rgb(201, 218, 248)", "rgb(207, 226, 243)", "rgb(217, 210, 233)", "rgb(234, 209, 220)", 
                    "rgb(221, 126, 107)", "rgb(234, 153, 153)", "rgb(249, 203, 156)", "rgb(255, 229, 153)", "rgb(182, 215, 168)", 
                    "rgb(162, 196, 201)", "rgb(164, 194, 244)", "rgb(159, 197, 232)", "rgb(180, 167, 214)", "rgb(213, 166, 189)", 
                    "rgb(204, 65, 37)", "rgb(224, 102, 102)", "rgb(246, 178, 107)", "rgb(255, 217, 102)", "rgb(147, 196, 125)", 
                    "rgb(118, 165, 175)", "rgb(109, 158, 235)", "rgb(111, 168, 220)", "rgb(142, 124, 195)", "rgb(194, 123, 160)",
                    "rgb(166, 28, 0)", "rgb(204, 0, 0)", "rgb(230, 145, 56)", "rgb(241, 194, 50)", "rgb(106, 168, 79)",
                    "rgb(69, 129, 142)", "rgb(60, 120, 216)", "rgb(61, 133, 198)", "rgb(103, 78, 167)", "rgb(166, 77, 121)",
                    "rgb(91, 15, 0)", "rgb(102, 0, 0)", "rgb(120, 63, 4)", "rgb(127, 96, 0)", "rgb(39, 78, 19)", 
                    "rgb(12, 52, 61)", "rgb(28, 69, 135)", "rgb(7, 55, 99)", "rgb(32, 18, 77)", "rgb(76, 17, 48)"]
                ],                
                change: function(color) {
                    var observable = valueAccessor();
                    observable(color.toHexString());
                }
            });
    },
    update: function (el, valueAccessor) {
        var color = ko.utils.unwrapObservable(valueAccessor());
        if (color) {
            $(el).spectrum("set", color);
        }
    }
};

ko.bindingHandlers.pieChart = {
    instance: null,
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
                legend: {
                    itemStyle : {
                        fontSize: '16px'
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
        return $.map(data, function(ingredient) {
            return { name: ko.utils.unwrapObservable(ingredient[map.text]),
                     y: ko.utils.unwrapObservable(ingredient[map.value]),
                     color: ko.utils.unwrapObservable(ingredient[map.color])
            };
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

ko.bindingHandlers.autocomplete = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var allBindings = allBindingsAccessor(),
            options = valueAccessor();
        $(element).autocomplete({
            autoFocus: true,
            delay: 500,
            minLength: 1,
            source: options.sourceUrl,
            select: function (event, ui) {
                allBindings.selected(ui.item.value);
            },
            change: function (event, ui) {
                allBindings.selected(ui.item == null ? null : ui.item.value);
            }
        }).on('paste', function () {
            return false;
        }).focus(function () {
            this.select();
        }).addClass(options.classes);

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).autocomplete("destroy");
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
