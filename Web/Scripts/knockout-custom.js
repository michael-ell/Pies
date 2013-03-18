ko.bindingHandlers.blur = {
    init: function (element, valueAccessor) {
        $(element).blur(function() {
            var observable = valueAccessor();
            observable($(this).val());
        });
    }
};