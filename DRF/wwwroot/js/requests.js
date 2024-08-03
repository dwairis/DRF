
var reqInit = AppFunctions.getAjaxResponse('/Requests/GetRequests', 'GET', null);

reqInit.success = function (response) {
    var dataSource = new kendo.data.DataSource({
        data: response,
        schema: {
            model: {
                fields: {
                    RequestId: { type: "number" },
                    ProgramTitle: { type: "string" },
                    ProjectStartDate: { type: "date" },
                    ProjectEndDate: { type: "date" }
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
            { field: "Id", title: "ID", width: "50px" },
            { field: "ProgramTitle", title: "Program Title", width: "200px" },
            { field: "ProjectStartDate", title: "Start Date", format: "{0:MM/dd/yyyy}", width: "150px" },
            { field: "ProjectEndDate", title: "End Date", format: "{0:MM/dd/yyyy}", width: "150px" },
            { command: { text: "View Details", click: viewDetails }, title: " ", width: "150px" }
        ]
    });
    
    function viewDetails(e) {
        e.preventDefault();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        window.location.href = '/requestDetails/' + dataItem.Id;
    }
};

$.ajax(reqInit);
