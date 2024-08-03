var requestId = window.location.pathname.split('/').pop();

var reqInit = AppFunctions.getAjaxResponse('/Requests/GetRequestDetails/' + requestId, 'GET', null);

reqInit.success = function (response) {
    var dataSource = new kendo.data.DataSource({
        data: [response],
        schema: {
            model: {
                fields: {
                    RequestId: { type: "number" },
                    ProgramTitle: { type: "string" },
                    ProjectStartDate: { type: "date" },
                    ProjectEndDate: { type: "date" },
                    Partners: { type: "array" },
                    Donors: { type: "array" }
                }
            }
        }
    });
    
    $("#request-details-grid").kendoGrid({
        dataSource: dataSource,
        height: 550,
        sortable: true,
        pageable: true,
        columns: [
            { field: "Id", title: "ID", width: "50px" },
            { field: "ProgramTitle", title: "Program Title", width: "200px" },
            { field: "ProjectStartDate", title: "Start Date", format: "{0:MM/dd/yyyy}", width: "150px" },
            { field: "ProjectEndDate", title: "End Date", format: "{0:MM/dd/yyyy}", width: "150px" },
            //{ field: "Partners", title: "Partners", template: "#= Partners.join(', ') #", width: "200px" },
            //{ field: "Donors", title: "Donors", template: "#= Donors.join(', ') #", width: "200px" }
        ]
    });
};

$.ajax(reqInit);
