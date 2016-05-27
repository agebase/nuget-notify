jQuery.validator.unobtrusive.adapters.add(
    "atleastonerequired",
    ["properties"],
    function(options) {
        options.rules["atleastonerequired"] = options.params;
        options.messages["atleastonerequired"] = options.message;
    }
);

jQuery.validator.addMethod("atleastonerequired",
    function(value, element, params) {
        var properties = params.properties.split(",");
        var values = $.map(properties,
            function(property) {
                var val = $("#" + property).val();
                return val !== "" ? val : null;
            });
        return values.length > 0;
    },
    ""
);