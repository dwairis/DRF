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
        filterable: true,
        reorderable: true,
        resizable: true,
        width: "100%",
        toolbar: ["search", "excel", "pdf"],
        excel: {
            fileName: "Request_Export.xlsx",
            filterable: true
        },
        pdf: {
            allPages: true,
            avoidLinks: true,
            paperSize: "A4",
            margin: { top: "2cm", left: "1cm", right: "1cm", bottom: "1cm" },
            landscape: true,
            repeatHeaders: true,
            scale: 0.8
        },
        columns: [ //values
            { field: "id", title: "ID", width: "60px" },
            { field: "programTitle", title: "Program Title", width: "200px" },
            { field: "projectStartDate", title: "Start Date", format: "{0:MM/dd/yyyy}", width: "100px" },
            { field: "projectEndDate", title: "End Date", format: "{0:MM/dd/yyyy}", width: "100px" },
            {
                field: "thirdPartyOrganization", title: "Organization", width: "200px", values: [
                    { text: "UNHCR", value: 1 },
                    { text: "WFP", value: 2 },
                    { text: "Organization C", value: 3 },
                ] },
            {
                field: "currentStatus", title: "Current Status", width: "100px", values: [
                    { text: "New", value: 'N' },
                    { text: "Accepted", value: 'A' },
                    { text: "Rejected", value: 'R' },
                ] },
            { command: { text: "View Details", click: viewDetails }, title: " ", width: "100px" }
        ]
    });

    function viewDetails(e) {
        e.preventDefault();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        console.log("Selected Data Item: ", dataItem);

        if (dataItem && dataItem.id) {
            window.location.href = '/Requests/Details/' + dataItem.id;
        } else {
            console.error("ID not found!");
        }
    }

};

$.ajax(reqInit);
