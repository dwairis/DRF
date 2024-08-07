var requestId;
$(document).ready(function () {
    requestId = window.location.pathname.split('/').pop();
    var apiUrl = '/Requests/GetRequestDetails/' + requestId;
    var isEditable = true; // Set this variable to true or false to control editability

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
            messages: {
                reset: false,
                done: false
            }
        });
    }
    
    function generateBoxedContent(title, content, fieldName) {
        var inputField = isEditable ? `<input type="text" name="${fieldName}" value="${content}" class="form-control" />` : `<input type="text" name="${fieldName}" value="${content}" class="form-control" readonly />`;

        return `
        <div class="card mb-3">
            <div class="card-header">
                <h5 class="mb-0">${title}</h5>
            </div>
            <div class="card-body">
                <p class="mb-0">${inputField}</p>
            </div>
        </div>`;
    }

    function generateProgramContent(model) {
        return `
            ${generateBoxedContent('Third Party Organization', model.thirdPartyOrganization, 'thirdPartyOrganization')}
            ${generateBoxedContent('Program Title', model.programTitle, 'programTitle')}
            ${generateBoxedContent('Donors', model.donors.join(', '), 'donors')}
            ${generateBoxedContent('Partners', model.partners.join(', '), 'partners')}
            <button class="btn btn-primary mt-2" onclick="saveChanges()">Save Changes</button>
        `;
    }

    function generateBriefOnProgramContent(model) {
        return `
            ${generateBoxedContent('Brief on Program', model.briefOnProgram, 'briefOnProgram')}
            <button class="btn btn-primary mt-2" onclick="saveChanges()">Save Changes</button>
        `;
    }

    function generateTargetContent(model) {
        return `
            ${generateBoxedContent('Target Sectors', model.targetSectors ? model.targetSectors.join(', ') : 'N/A', 'targetSectors')}
            ${generateBoxedContent('Target Request', model.targetRequest, 'targetRequest')}
            ${generateBoxedContent('Total Target', model.totalTarget, 'totalTarget')}
            ${generateBoxedContent('Referral Delivery Deadline', new Date(model.referralDeliveryDL).toLocaleDateString(), 'referralDeliveryDL')}
            ${generateBoxedContent('Referral Total', model.referralTotal, 'referralTotal')}
            <button class="btn btn-primary mt-2" onclick="saveChanges()">Save Changes</button>
        `;
    }

    function generateCriteriaContent(model) {
        return `
            ${generateBoxedContent('Criteria', model.criteria, 'criteria')}
            <button class="btn btn-primary mt-2" onclick="saveChanges()">Save Changes</button>
        `;
    }

    function generateTimelineContent(model) {
        return `
            ${generateBoxedContent('Project Start Date', new Date(model.projectStartDate).toLocaleDateString(), 'projectStartDate')}
            ${generateBoxedContent('Project End Date', new Date(model.projectEndDate).toLocaleDateString(), 'projectEndDate')}
            ${generateBoxedContent('Contact Person', model.contactPerson, 'contactPerson')}
            ${generateBoxedContent('Hired Self-Employed', model.hiredSelfEmployed ? 'Yes' : 'No', 'hiredSelfEmployed')}
            ${generateBoxedContent('Counterpart', model.counterPart, 'counterPart')}
            <button class="btn btn-primary mt-2" onclick="saveChanges()">Save Changes</button>
        `;
    }

    function generateWorkflowContent(model) {
        return `
            <div class="timeline">
                <ul id="workflow-timeline">
                    <!-- Timeline items will be dynamically added here by JavaScript -->
                </ul>
            </div>
            <button class="btn btn-primary mt-2" onclick="saveChanges()">Save Changes</button>
        `;
    }

    function generateDataContent(model) {
        return `
            <div class="data-upload">
                <h4>Upload Data File</h4>
                <input name="files" id="dataFileUpload" type="file" />
            </div>
            ${generateBoxedContent('Current Status', model.currentStatus, 'currentStatus')}
            ${generateBoxedContent('Data File URL', `<a href="${model.dataFileUrl}" target="_blank">${model.dataFileUrl}</a>`, 'dataFileUrl')}
            ${generateBoxedContent('Created By', model.createdBy, 'createdBy')}
            ${generateBoxedContent('Created At', new Date(model.createdAt).toLocaleDateString(), 'createdAt')}
            <button class="btn btn-primary mt-2" onclick="saveChanges()">Save Changes</button>
        `;
    }

    function generateNotesContent(model) {
        return `<div class="activity-notes box-content">
                    <h4>Status and Activity Notes</h4>
                    <div class="box"><strong>Current Status:</strong> <span>${model.currentStatus}</span></div>
                    <div class="box"><strong>Data File URL:</strong> <span><a href="${model.dataFileUrl}" target="_blank">${model.dataFileUrl}</a></span></div>
                    <div class="box"><strong>Created By:</strong> <span>${model.createdBy}</span></div>
                    <div class="box"><strong>Created At:</strong> <span>${model.createdAt}</span></div>
                    <div class="comment-box">
                        <h5>Add a Comment</h5>
                        <textarea id="comment" class="form-control" rows="4" placeholder="Enter your comment here..."></textarea>
                        <button id="submitComment" class="btn btn-primary mt-2">Submit Comment</button>
                    </div>
                    <button class="btn btn-primary mt-2" onclick="saveChanges()">Save Changes</button>
                </div>`
            , generateBoxedContent('Notes', model.notes ? model.notes : 'N/A', 'notes');
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
                case "accepted":
                    colorClass = "success";
                    break;
                case "rejected":
                    colorClass = "danger";
                    break;
            }

            var timelineItem = `
                <li class="timeline-item ${colorClass}">
                    <div class="timeline-content">
                        <h5 class="timeline-title">${item.update}</h5>
                        <p class="timeline-date">${new Date(item.createdAt).toLocaleDateString()}</p>
                        <p class="timeline-author">Updated by: ${item.createdBy}</p>
                    </div>
                </li>`;

            timelineList.append(timelineItem);
        });
    }

   

    // Initialize the Bootstrap timeline
    initializeBootstrapTimeline(requestId);
});

// Function to save changes
function saveChanges() {
    console.log("Save button clicked");
    // Collect the updated data from the form fields
    var updatedData = {
        Id: requestId,
        thirdPartyOrganization: $('input[name="thirdPartyOrganization"]').val(),
        programTitle: $('input[name="programTitle"]').val(),
        donors: $('input[name="donors"]').val().split(', '),
        partners: $('input[name="partners"]').val().split(', '),
        briefOnProgram: $('input[name="briefOnProgram"]').val(),
        targetSectors: $('input[name="targetSectors"]').val().split(', '),
        targetRequest: $('input[name="targetRequest"]').val(),
        totalTarget: $('input[name="totalTarget"]').val(),
        referralDeliveryDL: $('input[name="referralDeliveryDL"]').val(),
        referralTotal: $('input[name="referralTotal"]').val(),
        criteria: $('input[name="criteria"]').val(),
        projectStartDate: $('input[name="projectStartDate"]').val(),
        projectEndDate: $('input[name="projectEndDate"]').val(),
        contactPerson: $('input[name="contactPerson"]').val(),
        hiredSelfEmployed: $('input[name="hiredSelfEmployed"]').is(':checked'),
        counterpart: $('input[name="counterPart"]').val(),
        currentStatus: $('input[name="currentStatus"]').val(),
        notes: $('input[name="notes"]').val(),
        dataFileUrl: $('input[name="dataFileUrl"]').val(),
        createdBy: $('input[name="createdBy"]').val(),
        createdAt: $('input[name="createdAt"]').val()
    };
    console.log("Updated Data:", updatedData);

    // AJAX request to update the request details
    var req = AppFunctions.getAjaxResponse('/Requests/UpdateRequest', 'POST', updatedData);

    req.success = function (response) {
        if (response.success) {
            AppFunctions.showSuccessMsg("Request details updated successfully!");
        } else {
            AppFunctions.showErrorMsg("Failed to update request details.");
        }
    };

    req.error = function () {
        AppFunctions.showErrorMsg("An error occurred while updating request details.");
    };

    $.ajax(req);
}
