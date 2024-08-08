

var reqInit = AppFunctions.getAjaxResponse('/Requests/OnDetailsInit/' + requestId, 'GET', null);
var Donors, Organization, Partners;
var vmDate, validatorStep1, validatorStep2, validatorStep3, validatorStep4, validatorStep5, wizard, ThirdPartyOrganization, Donors, Partners, TargetSectors, Counterpart;
var data = {};
reqInit.success = function (response) {
    vmDate = response;

    data = vmDate.data;
    data.organizationId = data.thirdPartyOrganization;
    wizard = $("#details-wizard").kendoWizard({
        loadOnDemand: false,
        reloadOnSelect: false,

        stepper: { 
            indicator: "none", 
            linear: false
        },
        steps: [
            {
                title: "Program",
                buttons: vmDate.isReadOnly ? null : [{ name: "save", text: "Save Changes", click: saveChanges }],
                contentUrl: "/html_pages/step1.html"
            }, {
                title: "Brief on Program",
                contentUrl: "/html_pages/step2.html",
                buttons: vmDate.isReadOnly ? null : [{ name: "save", text: "Save Changes", click: saveChanges }],
            }, {
                title: "Target",
                contentUrl: "/html_pages/step3.html",
                buttons: vmDate.isReadOnly ? null : [{ name: "save", text: "Save Changes", click: saveChanges }],
            }, {
                title: "Criteria",
                contentUrl: "/html_pages/step4.html",
                buttons: vmDate.isReadOnly ? null : [{ name: "save", text: "Save Changes", click: saveChanges }],
            }, {
                title: "Timeline",
                contentUrl: "/html_pages/step5.html",
                buttons: vmDate.isReadOnly ? null : [{ name: "save", text: "Save Changes", click: saveChanges }],
            }, {
                title: "Data",
                contentUrl: "/html_pages/DataUploadPage.html",
                buttons: vmDate.isReadOnly ? null : [{ name: "save", text: "Save Changes", click: saveChanges }],
            }, {
                title: "Comments",
                contentUrl: "/html_pages/commentsOnRequests.html",
                buttons: vmDate.isReadOnly ? null : [{ name: "save", text: "Save Changes", click: saveChanges }],
            }, {
                title: "Workflow",
                buttons: [],
                contentUrl: "/html_pages/timeline.html",
            }
        ],
        done: function (e) {
            e.preventDefault();
            var isValid = true;
            if (!validatorStep1.validate()) {
                isValid = false;
                wizard.select(0);
            } else if (!validatorStep2.validate()) {
                isValid = false;
                wizard.select(1);
            } else if (!validatorStep3.validate()) {
                isValid = false;
                wizard.select(3);
            } else if (!validatorStep4.validate()) {
                isValid = false;
                wizard.select(3);
            } else if (!validatorStep5.validate()) {
                isValid = false;
                wizard.select(4);
            }

            if (isValid) {
                var data = {};
                var step1 = AppFunctions.SerializeForm('step1');
                var step2 = AppFunctions.SerializeForm('step2');
                var step3 = AppFunctions.SerializeForm('step3');
                var step4 = AppFunctions.SerializeForm('step4');
                var step5 = AppFunctions.SerializeForm('step5');

                step5.ProjectEndDate = step5.ProjectEndDate == 'year-month-day' ? null : step5.ProjectEndDate;
                step5.ProjectStartDate = step5.ProjectStartDate == 'year-month-day' ? null : step5.ProjectStartDate;
                step3.ReferralDeliveryDL = step3.ReferralDeliveryDL == 'year-month-day' ? null : step3.ReferralDeliveryDL;

                AppFunctions.appendObject(data, step1);
                AppFunctions.appendObject(data, step2);
                AppFunctions.appendObject(data, step3);
                AppFunctions.appendObject(data, step4);
                AppFunctions.appendObject(data, step5);

                data.Donors = Donors.value();
                data.Partners = Partners.value();
                data.TargetSectors = TargetSectors.value();

                var req = AppFunctions.getAjaxResponse('/RequestForm/OnCreatePost', 'POST', data);
                req.success = function (response) {
                    if (response.code == 200) {
                        AppFunctions.showSucessMsg(response.message);
                        setTimeout(function (o) {
                            location.href = '/requestForm/Create';
                        }, 2000);

                    } else if (response.result) {
                        AppFunctions.showErrorMsgWithDetails("Invalid or missing data", response.result);
                    } else {
                        AppFunctions.showErrorMsg(response.message);
                    }
                }
                $.ajax(req);

            }

        },
        //select: function (e) {
        //    if (e.step.options.index == 3) {
        //        updateSelection(e);
        //    }
        //},
        //contentLoad: function (e) {
        //    if (e.step.options.index == 3) {
        //        updateSelection(e);
        //    }
        //},
        select: function (e) {


        },
        reset: function () {
            var form = $('#attendeeDetails').getKendoForm();

            if (form) {
                form.clear();
            }
        }
    }).data("kendoWizard");

    $(".k-wizard-pager").hide();


}
$.ajax(reqInit);

var saveChanges = function SaveChanges(e) {

    var isValid = true;
    if (!validatorStep1.validate()) {
        isValid = false;
        wizard.select(0);
    } else if (!validatorStep2.validate()) {
        isValid = false;
        wizard.select(1);
    } else if (!validatorStep3.validate()) {
        isValid = false;
        wizard.select(3);
    } else if (!validatorStep4.validate()) {
        isValid = false;
        wizard.select(3);
    } else if (!validatorStep5.validate()) {
        isValid = false;
        wizard.select(4);
    }

    if (isValid) {
        var data = {};
        var step1 = AppFunctions.SerializeForm('step1');
        var step2 = AppFunctions.SerializeForm('step2');
        var step3 = AppFunctions.SerializeForm('step3');
        var step4 = AppFunctions.SerializeForm('step4');
        var step5 = AppFunctions.SerializeForm('step5');

        step5.ProjectEndDate = step5.ProjectEndDate == 'year-month-day' ? null : step5.ProjectEndDate;
        step5.ProjectStartDate = step5.ProjectStartDate == 'year-month-day' ? null : step5.ProjectStartDate;
        step3.ReferralDeliveryDL = step3.ReferralDeliveryDL == 'year-month-day' ? null : step3.ReferralDeliveryDL;

        AppFunctions.appendObject(data, step1);
        AppFunctions.appendObject(data, step2);
        AppFunctions.appendObject(data, step3);
        AppFunctions.appendObject(data, step4);
        AppFunctions.appendObject(data, step5);

        data.Donors = Donors.value();
        data.Partners = Partners.value();
        data.TargetSectors = TargetSectors.value();
        data.Id = requestId;

        var req = AppFunctions.getAjaxResponse('/Requests/OnUpdatePost', 'POST', data);
        req.success = function (response) {
            if (response.code == 200) {
                AppFunctions.showSucessMsg(response.message);
                setTimeout(function () {
                    location.reload();
                }, 2000)
                

            } else if (response.result) {
                AppFunctions.showErrorMsgWithDetails("Invalid or missing data", response.result);
            } else {
                AppFunctions.showErrorMsg(response.message);
            }
        }
        $.ajax(req);
    }
}