document.addEventListener('DOMContentLoaded', function () {
    // Retrieve the request ID
    var requestId = document.getElementById('request-id').value;
    console.log("Request ID:", requestId);

    // Initialize the Kendo Timeline with the request ID
    initializeKendoTimeline(requestId);
});

function initializeKendoTimeline(requestId) {
    // Use AppFunctions for AJAX call
    var timelineInit = AppFunctions.getAjaxResponse('/Requests/GetRequestUpdates/' + requestId, 'GET', null);
    console.log(timelineInit)

    timelineInit.success = function (response) {
        //console.log("Timeline Data:", response); // Debugging log
        if (response && response.length > 0) {
            renderTimeline(response);
        } else {
            console.log("No data available for the timeline.");
        }
    };

    timelineInit.error = function (jqXHR, textStatus, errorThrown) {
        //console.error("Error fetching timeline data:", textStatus, errorThrown);
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
                        text: { from: "Update" },
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