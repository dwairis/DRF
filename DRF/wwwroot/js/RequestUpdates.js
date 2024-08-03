function initializeKendoTimeline(requestId) {
    var timelineInit = AppFunctions.getAjaxResponse('/Requests/GetRequestUpdates', 'GET', { requestId: requestId });

    timelineInit.success = function (response) {
        console.log("Timeline Data:", response); // Debugging log
        if (response && response.length > 0) {  // Check if response is not empty
            $("#timeline").kendoTimeline({
                dataSource: {
                    data: response,
                    schema: {
                        model: {
                            fields: {
                                date: { from: "CreatedAt", parse: function (value) { return new Date(value); }, type: "date" },
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
        } else {
            console.log("No data available for the timeline.");
        }
    }
}