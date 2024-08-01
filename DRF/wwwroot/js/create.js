

var reqInit = AppFunctions.getAjaxResponse('/RequestForm/OnCreateInit', 'GET', null);
var Donors, Organization, Partners;
var vmDate, validatorStep1, validatorStep2, validatorStep3, validatorStep4, validatorStep5, wizard, ThirdPartyOrganization, Donors, Partners, TargetSectors, Counterpart;
reqInit.success = function (response) {
    vmDate = response;


    wizard = $("#wizard").kendoWizard({
        loadOnDemand: true,
        reloadOnSelect: false,
        stepper: {

            linear: true
        },
        steps: [
            {
                title: "Program",
                buttons: [{
                    name: "next", text: "Next", click: function (o) {

                        if (validatorStep1.validate()) {
                            wizard.select(1);
                        }
                    }
                }],
                contentUrl: "/html_pages/step1.html"
            }, {
                title: "Brief on Program",
                contentUrl: "/html_pages/step2.html",
                buttons: [{
                    name: "previous", text: "Previous", click: function (o) {
                        wizard.select(0);
                    }
                }, {
                    name: "next", text: "Next", click: function (o) {

                        if (validatorStep2.validate()) {
                            wizard.select(2);
                        }
                    }
                }],
            }, {
                title: "Target",
                contentUrl: "/html_pages/step3.html",
                buttons: [{ name: "previous", text: "Previous" }, {
                    name: "next", text: "Next", click: function (o) {

                        if (validatorStep3.validate()) {
                            wizard.select(3);
                        }
                    }
                }],
            }, {
                title: "Criteria",
                contentUrl: "/html_pages/step4.html",
                buttons: [{ name: "previous", text: "Previous" }, {
                    name: "next", text: "Next", click: function (o) {

                        if (validatorStep4.validate()) {
                            wizard.select(4);
                        }
                    }
                }],
            }, {
                title: "Timeline",
                contentUrl: "/html_pages/step5.html",
                buttons: [{ name: "previous", text: "Previous" }, {
                    name: "next", text: "Next", click: function (o) {

                        if (validatorStep5.validate()) {
                            wizard.select(5);
                        }
                    }
                }],
            }, {
                title: "Review",
                buttons: [{ name: "previous", text: "Previous" }, { name: "done", text: "Submit" }],
                contentUrl: "/html_pages/reviewsteps.html",
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

                step5.ProjectEndDate= step5.ProjectEndDate == 'year-month-day' ? null : step5.ProjectEndDate;
                step5.ProjectStartDate = step5.ProjectStartDate == 'year-month-day' ? null : step5.ProjectStartDate;
                step5.ReferralDeliveryDL = step5.ReferralDeliveryDL == 'year-month-day' ? null : step5.ReferralDeliveryDL;
                
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
            debugger;
            if (e.button && e.button.element) {
                if (!e.button.element.is('[data-wizard-previous]')) {
                    e.preventDefault();
                }
            } else {
                e.preventDefault();
            }

        },
        reset: function () {
            var form = $('#attendeeDetails').getKendoForm();

            if (form) {
                form.clear();
            }
        }
    }).data("kendoWizard");


}
$.ajax(reqInit);