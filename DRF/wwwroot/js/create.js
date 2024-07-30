var reqInit = AppFunctions.getAjaxResponse('/RequestForm/OnCreateInit', 'GET', null);
reqInit.success = function (response) {

    console.log(response);
}
$.ajax(reqInit);