$(document).ready(function () {
    //kendo.culture("en-GB");
    $.validator.addMethod('date',
        function (value, element) {
            return this.optional(element) || kendo.parseDate(value)
        });
});