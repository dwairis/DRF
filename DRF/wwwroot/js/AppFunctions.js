var AppFunctions = {};
var preloader = $("#loader");
toastr.options.positionClass = 'toast-bottom-right';
AppFunctions.showSucessMsg = function (msg) {
    toastr.success(msg);
}

AppFunctions.showErrorMsg = function (msg) {
    toastr.error(msg);
}
AppFunctions.showErrorMsgWithDetails = function (title, htmlContent) {
    toastr.error(`<b>${title}</b> ${htmlContent}`);
}
AppFunctions.showInfoMsg = function (msg) {
    toastr.info(msg);
}

AppFunctions.showLoading = function () {
    preloader.fadeIn();
}
AppFunctions.hideLoading = function () {
    preloader.fadeOut();
}


AppFunctions.getAjaxResponse = function (url, type, data) {
    var headers;
    var token = $('input[name="__RequestVerificationToken"]').val();
    if (token) {
        headers = { "RequestVerificationToken": token };
    }

    var obj = {
        url: url,
        dataType: "json",
        headers: headers,
        type: type,
        contentType: "application/json;charset=UTF-8",

        beforeSend: function () {
            AppFunctions.showLoading();
        },
        complete: function () {
            AppFunctions.hideLoading();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 401) {
                location.href = "/Account/SignIn";
            }
            else if (jqXHR.status == 403) {
                location.href == "/Errors/403";
            } else if (jqXHR.status == 429) {
                AppFunctions.showErrorMsg('Sorry, you have reached the maximum number of requests allowed. Please wait for some time before trying again');
            } else if (jqXHR.status == 404) {
                location.href = "/Errors/404";
            } else {
                AppFunctions.showErrorMsg('An unexpected error occurred, please contact the system administrator');
            }

        }
    }
    if (data)
        obj.data = JSON.stringify(data);

    return obj;
}

AppFunctions.Prompt = function (content, defaultValue, title) {
    return $("<div></div>").kendoPrompt({
        title: title,
        value: defaultValue,
        width: 500,
        content: content
    }).data("kendoPrompt").open().result;
}

AppFunctions.SerializeForm = function (formId) {
    var values = {};
    $.each($('#' + formId).serializeArray(), function (i, field) {
        values[field.name] = field.value ? field.value : null;
    });
    return values;
}

var decodeEntities = (function () {
    var element = document.createElement('div');

    function decodeHTMLEntities(str) {
        if (str && typeof str === 'string') {
            // strip script/html tags
            str = str.replace(/<script[^>]*>([\S\s]*?)<\/script>/gmi, '');
            str = str.replace(/<\/?\w(?:[^"'>]|"[^"]*"|'[^']*')*>/gmi, '');
            element.innerHTML = str;
            str = element.textContent;
            element.textContent = '';
        }

        return str;
    }

    return decodeHTMLEntities;
})();

AppFunctions.getDropdownItemStyle = function (att = 'isActive') {
    return function (e) { return `<span class="${(!e.isActive ? 'k-state-disabled' : '')}">${e.text}</span>`; }
}

AppFunctions.getGridDropdownEditor = function (data, groupName) {
    return function (container, options) {
        $('<input required name="' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: groupName ? {
                    data: data,
                    group: { field: groupName }
                } : data,
                optionLabel: '-- Select Option --',
                valuePrimitive: true,
                select: AppFunctions.disableDropdownItem(),
                template: AppFunctions.getDropdownItemStyle(),
            });
    }
}

AppFunctions.getMasterGridDropdownEditor = function (data, isRequired = true, onChange = null) {
    return function (container, options) {
        $(`<input ${isRequired ? 'required' : ''} name='${options.field}'/>`)
            .appendTo(container)
            .kendoDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: data,
                optionLabel: '-- Select Option --',
                valuePrimitive: true,
                select: AppFunctions.disableDropdownItem(),
                template: AppFunctions.getDropdownItemStyle(),
                change: onChange ? onChange : undefined
            });
    }
}
AppFunctions.disableDropdownItem = function (att = 'isActive') {
    return function (e) {
        if (!e.dataItem[att]) {
            e.preventDefault();
        }
    }
}
AppFunctions.getDropdownItemStyle = function (att = 'isActive') {
    return function (e) { return `<span class="${(!e.isActive ? 'k-state-disabled' : '')}">${e.text}</span>`; }
}

AppFunctions.setCookie = function (cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

AppFunctions.getCookie = function (cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

AppFunctions.eraseCookie = function (c_name) {
    AppFunctions.setCookie(c_name, "", -1);
}


AppFunctions.getDatePattern = function (pattern, date) {
    var monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"];

    var todayDate = date;

    var date = todayDate.getDate().toString();
    var month = todayDate.getMonth().toString();
    var year = todayDate.getFullYear().toString();
    var formattedMonth = (todayDate.getMonth() < 10) ? "0" + month : month;
    var formattedDay = (todayDate.getDate() < 10) ? "0" + date : date;
    var result = "";

    switch (pattern) {
        case "M/d/yyyy":
            formattedMonth = formattedMonth.indexOf("0") == 0 ? formattedMonth.substring(1, 2) : formattedMonth;
            formattedDay = formattedDay.indexOf("0") == 0 ? formattedDay.substring(1, 2) : formattedDay;

            result = formattedMonth + '/' + formattedDay + '/' + year;
            break;

        case "M/d/yy":
            formattedMonth = formattedMonth.indexOf("0") == 0 ? formattedMonth.substring(1, 2) : formattedMonth;
            formattedDay = formattedDay.indexOf("0") == 0 ? formattedDay.substring(1, 2) : formattedDay;
            return formattedMonth + '/' + formattedDay + '/' + year.substr(2);
            break;

        case "MM/dd/yy":
            return formattedMonth + '/' + formattedDay + '/' + year.substr(2);
            break;

        case "MM/dd/yyyy":
            return formattedMonth + '/' + formattedDay + '/' + year;
            break;

        case "yy/MM/dd":
            return year.substr(2) + '/' + formattedMonth + '/' + formattedDay;
            break;


        case "yyyy-MM-dd":
            return year + '-' + formattedMonth + '-' + formattedDay;
            break;

        case "dd-MMM-yy":
            return formattedDay + '-' + monthNames[todayDate.getMonth()].substr(3) + '-' + year.substr(2);
            break;

        case "MMMM d, yyyy":
            return todayDate.toLocaleDateString("en-us", { day: 'numeric', month: 'long', year: 'numeric' });
            break;


    }
}
AppFunctions.appendObject = function (dest, source) {
    for (var key in source) {
        if (source.hasOwnProperty(key)) {
            dest[key] = source[key];
        }
    }
}
AppFunctions.getFileAjaxResponse = function (url, type, data) {
    var headers;
    var token = $('input[name="__RequestVerificationToken"]').val();
    if (token) {
        headers = { "RequestVerificationToken": token };
    }
    var obj = {
        url: url,
        headers: headers,
        type: type,
        data: data,
        contentType: false,
        processData: false,
        enctype: "multipart/form-data",
        beforeSend: function () {
            AppFunctions.showLoading();
        },
        complete: function () {
            AppFunctions.hideLoading();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 401) {
                location.href = "/Account/SignIn";
            }
            else if (jqXHR.status == 403) {
                location.href == "/Errors/403";
            } else if (jqXHR.status == 429) {
                AppFunctions.showErrorMsg('Sorry, you have reached the maximum number of requests allowed. Please wait for some time before trying again');
            } else if (jqXHR.status == 404) {
                location.href = "/Errors/404";
            } else {
                AppFunctions.showErrorMsg('An unexpected error occurred, please contact the system administrator');
            }

        }
    }
    if (data)
        obj.data = data;

    return obj;
}



AppFunctions.downloadExcel = function (e, documentName) {
    e.preventDefault();
    var workbook = new kendo.ooxml.Workbook(e.workbook);
    let blob = workbook.toBlob();
    let downloadLink = document.createElement("a");
    downloadLink.href = URL.createObjectURL(blob);
    downloadLink.download = documentName + ".xlsx";
    document.body.appendChild(downloadLink);
    downloadLink.click();
    document.body.removeChild(downloadLink);
};

AppFunctions.renderTimeDropdown = function (listOfTimes) {
    listOfTimes.empty();
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">8:00 AM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">8:30 AM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">9:00 AM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">9:30 AM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">10:00 AM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">10:30 AM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:00 AM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">11:30 AM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">12:00 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">12:30 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">1:00 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">1:30 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">2:00 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">2:30 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">3:00 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">3:30 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">4:00 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">4:30 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">5:00 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">5:30 PM</li>');
    listOfTimes.append('<li tabindex="-1" role="option" class="k-item" unselectable="on">6:00 PM</li>');
}
AppFunctions.readTextFile = function (file, callback) {
    var rawFile = new XMLHttpRequest();
    rawFile.overrideMimeType("application/json");
    rawFile.open("GET", file, true);
    rawFile.onreadystatechange = function () {
        if (rawFile.readyState === 4 && rawFile.status == "200") {
            callback(rawFile.responseText);
        }
    }
    rawFile.send(null);
}

AppFunctions.getParameterByName = function (name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
AppFunctions.DownloadExcel = function (e, documentName) {
    e.preventDefault();
    var workbook = new kendo.ooxml.Workbook(e.workbook);
    let blob = workbook.toBlob();
    let downloadLink = document.createElement("a");
    downloadLink.href = URL.createObjectURL(blob);
    downloadLink.download = documentName + ".xlsx";
    document.body.appendChild(downloadLink);
    downloadLink.click();
    document.body.removeChild(downloadLink);
};

AppFunctions.validatorSettings = {
    rules: {},
    messages: {

        required: 'This field is mandatory',
    },
    validateInput: function (e) {
        var lbl = document.getElementById("lbl" + e.field);
        if (lbl) {
            if (e.valid) {
                lbl.classList.remove('invalid-input');
            } else {
                lbl.classList.add('invalid-input');
            }
        }
    },
    validate: function (e) {
        if (!e.valid) {
            AppFunctions.showErrorMsg('There are errors in the form fields, please check the data before submitting the form');
        }
    }
}