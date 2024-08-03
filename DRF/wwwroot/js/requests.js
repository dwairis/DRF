var reqInit = AppFunctions.getAjaxResponse('/Requests/GetRequests', 'GET', null);

reqInit.success = function (response) {
    console.log("Response Data: ", response);

    var dataSource = new kendo.data.DataSource({
        data: response,
        schema: {
            model: {
                fields: {
                    id: { type: "number" },
                    programTitle: { type: "string" },
                    projectStartDate: { type: "date" },
                    projectEndDate: { type: "date" },
                    thirdPartyOrganization: { type: "string" },
                    currentStatus: { type: "string" }  
                }
            }
        },
        pageSize: 10
    });

    $("#requests-grid").kendoGrid({
        dataSource: dataSource,
        height: 550,
        sortable: true,
        pageable: true,
        columns: [
            { field: "id", title: "ID", width: "50px" },
            { field: "programTitle", title: "Program Title", width: "200px" },
            { field: "projectStartDate", title: "Start Date", format: "{0:MM/dd/yyyy}", width: "150px" },
            { field: "projectEndDate", title: "End Date", format: "{0:MM/dd/yyyy}", width: "150px" },
            { field: "thirdPartyOrganization", title: "Third Party Org", width: "200px" },
            { field: "currentStatus", title: "Current Status", width: "150px" },
            { command: { text: "View Details", click: viewDetails }, title: " ", width: "150px" }
        ]
    });

    function viewDetails(e) {
        e.preventDefault();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        console.log("Selected Data Item: ", dataItem);

        if (dataItem && dataItem.id) {
            window.location.href = '/requestDetails/' + dataItem.id;
        } else {
            console.error("ID not found!");
        }
    }
};

$.ajax(reqInit);
