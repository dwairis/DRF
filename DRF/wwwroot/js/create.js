var reqInit = AppFunctions.getAjaxResponse('/RequestForm/OnCreateInit', 'GET', null);
var Donors, Organization, Partners;
var vmDate, validatorStep1, validatorStep2, validatorStep3, validatorStep4, validatorStep5, wizzerd;
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
                buttons: [{ name: "prevoius", text: "Prevoius" }, {
                    name: "next", text: "Next", click: function (o) {

                        if (validatorStep2.validate()) {
                            wizard.select(2);
                        }
                    }
                }],
            }, {
                title: "Target",
                contentUrl: "/html_pages/step3.html",
                buttons: [{ name: "prevoius", text: "Prevoius" }, {
                    name: "next", text: "Next", click: function (o) {

                        if (validatorStep3.validate()) {
                            wizard.select(3);
                        }
                    }
                }],
            }, {
                title: "Criteria",
                contentUrl: "/html_pages/step4.html",
                buttons: [{ name: "prevoius", text: "Prevoius" }, {
                    name: "next", text: "Next", click: function (o) {

                        if (validatorStep4.validate()) {
                            wizard.select(4);
                        }
                    }
                }],
            }, {
                title: "Timeline",
                contentUrl: "/html_pages/step5.html",
                buttons: [{ name: "prevoius", text: "Prevoius" }, {
                    name: "next", text: "Next", click: function (o) {

                        if (validatorStep5.validate()) {
                            wizard.select(5);
                        }
                    }
                }],
            }, {
                title: "Review",
                buttons: [{ name: "prevoius", text: "Prevoius" }, { name: "done", text:"Submit" }],
                contentUrl: "/html_pages/reviewsteps.html",
            }
        ],
        done: function (e) {
            e.preventDefault();
            var form = $('#attendeeDetails').getKendoForm();
            var talkDDLValue = $("#talk").data("kendoDropDownList").value();
            var workshopDDLValue = $("#workshop").data("kendoDropDownList").value();

            if (!form.validate()) {
                e.sender.stepper.steps()[1].setValid(false);
                kendo.alert("Please complete registration form");
                e.sender.select(1);
            } else if (talkDDLValue == "" || workshopDDLValue == "") {
                e.sender.stepper.steps()[1].setValid(true);
                e.sender.stepper.steps()[2].setValid(false);
                kendo.alert("Please select the talk and workshop you want to subscribe for");
                e.sender.select(2);
            }
            else {
                if (e.sender.stepper.steps()[1].options.error) {
                    e.sender.stepper.steps()[1].setValid(true);
                    e.sender.stepper.steps()[2].setValid(true);
                }

                kendo.alert("Thank you for registering! Registration details will be sent to your email.");
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