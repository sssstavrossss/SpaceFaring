//Needs Globalize and datePicker to work 
let CustomDateTimePick = function () {

    let initAfterToday = function (cultureName) {
        dateInit(cultureName);
        timeInit();
    }

    let getToday = function () {
        let date = new Date();
        let today = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + '0' + date.getDate();
        return Globalize.parseDate(today, "yyyy-MM-dd");
    };

    let dateInit = function (cultureName) {
        $('#datePicker').datetimepicker({
            format: 'L',
            locale: cultureName
        }).data("DateTimePicker").minDate(getToday()).showClose(true).keepInvalid(true);
    };

    let timeInit = function () {
        $('#timePicker').datetimepicker({
            format: 'LT'
        }).data("DateTimePicker").showClose(true);
    };

    return {
        initAfterToday: initAfterToday
    };
}();
