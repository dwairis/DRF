var reqInit = AppFunctions.getAjaxResponse('/RequestForm/OnCreateInit', 'GET', null);
var Donors, Organization, Partners;
reqInit.success = function (response) {

    Organization = $('#Organization').kendoDropDownList({
        optionLabel: '-- Select Organization --',
        dataTextField: 'text',
        select: AppFunctions.disableDropdownItem(),
        template: AppFunctions.getDropdownItemStyle(),
        dataValueField: 'value',
        dataSource: response.organizationList,
        value: 1,
        dataBound: function () {
            this.enable(false);
        }
    }).data('kendoDropDownList');

    Donors = $('#Donor').kendoDropDownList({
        optionLabel: '-- Select Donor --',
        dataTextField: 'text',
        select: AppFunctions.disableDropdownItem(),
        template: AppFunctions.getDropdownItemStyle(),
        dataValueField: 'value',
        dataSource: response.donorsList,
    }).data('kendoDropDownList');

    Partners = $('#Partners').kendoMultiSelect({
        optionLabel: '-- Select Donor --',
        dataTextField: 'text',
        select: AppFunctions.disableDropdownItem(),
        template: AppFunctions.getDropdownItemStyle(),
        dataValueField: 'value',
        dataSource: response.partnersList,
    }).data('kendoMultiSelect');
}
$.ajax(reqInit);