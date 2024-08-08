document.addEventListener('DOMContentLoaded', function () {
    var requestId = document.getElementById('request-id').value;
    console.log("Request ID:", requestId);
    
    initializeKendoTimeline(requestId);
});

function initializeKendoTimeline(requestId) {
    var timelineInit = AppFunctions.getAjaxResponse('/Requests/GetRequestStatus/' + requestId, 'GET', null);
    console.log(timelineInit)

    timelineInit.success = function (response) {
        console.log("Timeline Data:", response);
        if (response && response.length > 0) {
            renderTimeline(response);
        } else {
            console.log("No data available for the timeline.");
        }
    };

    timelineInit.error = function (jqXHR, textStatus, errorThrown) {
        console.error("Error fetching timeline data:", textStatus, errorThrown);
    };

    $.ajax(timelineInit);
}

function renderTimeline(data) {
    $("#timeline-container").kendoTimeline({
        dataSource: {
            data: data,
            schema: {
                model: {
                    fields: { 
                        date: { from: "CreatedAt", type: "date" },
                        text: { from: "Notes" },
                        text: { from: "Status" },
                        title: { from: "CreatedBy" }
                    }
                }
            }
        },
        alternatingMode: true,
        collapsibleEvents: true,
        orientation: "vertical"
    });
}