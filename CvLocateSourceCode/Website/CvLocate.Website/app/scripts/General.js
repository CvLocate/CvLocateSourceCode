function getWeekInMonth(date) {

    // get day in week from last day in perv month
    var lastDayPrevMonth = moment(date).subtract(1, 'months').endOf('month');
    var lastDayInWeekPrevMonth = lastDayPrevMonth.days();

    if (lastDayInWeekPrevMonth + date.getDate() - 1 < 7) return 1;

    return Math.floor((lastDayInWeekPrevMonth + date.getDate() - 1) / 7) + 1

}

function GetResourceValue(key) {
    debugger;
    //var x= common.resourceManager.getResources()
    //          .then(function getResourcesSuccess(data) {
    //              debugger;
    //              // vm.resources = data;
    //          });
}

function jsDecimals(e, TrueKey1, TrueKey2) {
    var evt = (e) ? e : window.event;
    var key = (evt.keyCode) ? evt.keyCode : evt.which;
    if (key != null) {
        key = parseInt(key, 10);
        if (key == 46 || key == 8 || (TrueKey1 && key == TrueKey1) || (TrueKey2 && key == TrueKey2))
            return true;

        if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
            return false;
        }
        else {
            if (evt.shiftKey) {
                return false;
            }
        }
    }
    return true;
}


function jsDecimalsDate(e) {
    return jsDecimals(e, 191, 111)
}

function jsDecimalsTime(e) {
    return jsDecimals(e, 58,186)
}


function parseDMYtoMDY(value) {
    if (value) {
        var date = value.split("/");
        var d = parseInt(date[0], 10),
            m = parseInt(date[1], 10),
            y = parseInt(date[2], 10);
        return (m + "/" + d + "/" + y);
    }
}


function CheckTimeLateThanStartTime(elment) {
    var EndTime = $(elment).attr("endtime");
    var StartTime = $(elment).attr("starttime");

    var startDate = new Date("1/1/1900 " + StartTime);
    var endDate = new Date("1/1/1900 " + EndTime);

    $("#errorMustBeLaterMgs").hide();
    if (StartTime && EndTime && (startDate > endDate || startDate == endDate || StartTime == EndTime)) {
        $("#errorMustBeLaterMgs").show();
    }

}