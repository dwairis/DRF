$(document).ready(function () {
    var requestId = window.location.pathname.split('/').pop();
    var apiUrl = '/Requests/GetRequestDetails/' + requestId;

    $.ajax({
        url: apiUrl,
        type: 'GET',
        success: function (response) {
            initializeWizard(response);
            initializeBootstrapTimeline(requestId); // Initialize the Bootstrap timeline with request updates
        },
        error: function () {
            alert("Failed to load request details. Please try again.");
        }
    });

    function initializeWizard(model) {
        $("#details-wizard").kendoWizard({
            loadOnDemand: true,
            reloadOnSelect: false,
            stepper: {
                linear: false // Allows clicking on headers to navigate
            },
            steps: [
                { title: "Program", content: generateProgramContent(model) },
                { title: "Brief on Program", content: generateBriefOnProgramContent(model) },
                { title: "Target", content: generateTargetContent(model) },
                { title: "Criteria", content: generateCriteriaContent(model) },
                { title: "Timeline", content: generateTimelineContent(model) },
                { title: "Workflow", content: generateWorkflowContent(model) },
                { title: "Data", content: generateDataContent(model) },
                { title: "Status/Activity Notes", content: generateNotesContent(model) }
            ],
            // Remove reset and done buttons
            messages: {
                reset: false,
                done: false
            }
        });
    }

    function generateBoxedContent(title, content) {
        return `
            <div class="card mb-3">
                <div class="card-header">
                    <h5 class="mb-0">${title}</h5>
                </div>
                <div class="card-body">
                    <p class="mb-0">${content}</p>
                </div>
            </div>`;
    }

    function generateProgramContent(model) {
        return `
            ${generateBoxedContent('Third Party Organization', model.thirdPartyOrganization)}
            ${generateBoxedContent('Program Title', model.programTitle)}
            ${generateBoxedContent('Donors', model.donors.join(', '))}
            ${generateBoxedContent('Partners', model.partners.join(', '))}
        `;
    }

    function generateBriefOnProgramContent(model) {
        return generateBoxedContent('Brief on Program', model.briefOnProgram);
    }

    function generateTargetContent(model) {
        return `
            ${generateBoxedContent('Target Sectors', model.targetSectors ? model.targetSectors.join(', ') : 'N/A')}
            ${generateBoxedContent('Target Request', model.targetRequest)}
            ${generateBoxedContent('Total Target', model.totalTarget)}
            ${generateBoxedContent('Referral Delivery Deadline', new Date(model.referralDeliveryDL).toLocaleDateString())}
            ${generateBoxedContent('Referral Total', model.referralTotal)}
        `;
    }

    function generateCriteriaContent(model) {
        return generateBoxedContent('Criteria', model.criteria);
    }

    function generateTimelineContent(model) {
        return `
            ${generateBoxedContent('Project Start Date', new Date(model.projectStartDate).toLocaleDateString())}
            ${generateBoxedContent('Project End Date', new Date(model.projectEndDate).toLocaleDateString())}
            ${generateBoxedContent('Contact Person', model.contactPerson)}
            ${generateBoxedContent('Hired Self-Employed', model.hiredSelfEmployed ? 'Yes' : 'No')}
            ${generateBoxedContent('Counterpart', model.counterPart)}
        `;
    }

    function generateWorkflowContent(model) {
        // Use the new timeline HTML file to show the timeline
        return `
            <div class="timeline">
                <ul id="workflow-timeline" class="horizontal-timeline">
                    <!-- Timeline items will be dynamically added here by JavaScript -->
                </ul>
            </div>`;
    }

    function generateDataContent(model) {
        return `
            <div class="data-upload">
                <h4>Upload Data File</h4>
                <input name="files" id="dataFileUpload" type="file" /> 
            </div><br>
            ${generateBoxedContent('Current Status', model.currentStatus)}
            ${generateBoxedContent('Data File URL', `<a href="${model.dataFileUrl}" target="_blank">${model.dataFileUrl}</a>`)}
            ${generateBoxedContent('Created By', model.createdBy)}
            ${generateBoxedContent('Created At', new Date(model.createdAt).toLocaleDateString())}
        `;
    }

    function generateNotesContent(model) {
        return `<div class="activity-notes box-content">
                <h4>Status and Activity Notes</h4>
                <div class="box"><strong>Current Status:</strong> <span>${model.currentStatus}</span></div>
                <div class="box"><strong>Data File URL:</strong> <span><a href="${model.dataFileUrl}" target="_blank">${model.dataFileUrl}</a></span></div>
                <div class="box"><strong>Created By:</strong> <span>${model.createdBy}</span></div>
                <div class="box"><strong>Created At:</strong> <span>${model.createdAt}</span></div>
                <div class="comment-box"> <br>
                    <h5>Add a Comment</h5>
                    <textarea id="comment" class="form-control" rows="4" placeholder="Enter your comment here..."></textarea>
                    <button id="submitComment" class="btn btn-primary mt-2" disabled>Submit Comment</button>
                </div>
            </div>`;
    }


    // Bootstrap Timeline Integration
    function initializeBootstrapTimeline(requestId) {
        var timelineInit = AppFunctions.getAjaxResponse('/Requests/GetRequestUpdates/' + requestId, 'GET', null);

        timelineInit.success = function (response) {
            console.log("Timeline Data:", response);
            if (response && response.length > 0) {
                renderBootstrapTimeline(response);
            } else {
                console.log("No data available for the timeline.");
            }
        };

        timelineInit.error = function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching timeline data:", textStatus, errorThrown);
        };

        $.ajax(timelineInit);
    }

    function renderBootstrapTimeline(data) {
        var timelineList = $("#workflow-timeline");
        timelineList.empty();

        data.forEach(function (item, index) {
            // Assign a color based on the update type or other criteria
            var colorClass = "primary"; // Default color
            switch (item.update.toLowerCase()) {
                case "needs more clarification":
                    colorClass = "warning";
                    break;
                case "accepted by x":
                    colorClass = "success";
                    break;
                case "rejected by y":
                    colorClass = "danger";
                    break;
                default:
                    colorClass = "info"; // Default to info if no match
            }

            var timelineItem = `
                <li class="timeline-item ${colorClass}">
                    <div class="timeline-dot"></div>
                    <div class="timeline-content">
                        <h5 class="timeline-title">${item.update}</h5>
                        <p class="timeline-date">${new Date(item.createdAt).toLocaleDateString()}</p>
                        <p class="timeline-author">Updated by: ${item.createdBy}</p>
                    </div>
                </li>`;

            timelineList.append(timelineItem);
        });
    }

    initializeBootstrapTimeline(requestId);
});
