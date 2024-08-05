$(document).ready(function () {
    var requestId = window.location.pathname.split('/').pop();
    var apiUrl = '/Requests/GetRequestDetails/' + requestId;

    $.ajax({
        url: apiUrl,
        type: 'GET',
        success: function (response) {
            initializeWizard(response);
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
        return `<div id="timeline-container"></div>`;
    }

    function generateDataContent(model) {
        return `
         <div class="data-upload">
            <h4>Upload Data File</h4>
            <input name="files" id="dataFileUpload" type="file" />
        </div>,
            ${generateBoxedContent('Current Status', model.currentStatus)}
            ${generateBoxedContent('Data File URL', `<a href="${model.dataFileUrl}" target="_blank">${model.dataFileUrl}</a>`)}
            ${generateBoxedContent('Created By', model.createdBy)}
            ${generateBoxedContent('Created At', new Date(model.createdAt).toLocaleDateString())}
        `;
    }

    function generateNotesContent(model) {
        return `<div class="activity-notes box-content">
                    <h4>Status and Activity Notes</h4>
                    <div class="box"><strong>Current Status:</strong> <span>${model.CurrentStatus}</span></div>
                    <div class="box"><strong>Data File URL:</strong> <span><a href="${model.DataFileUrl}" target="_blank">${model.DataFileUrl}</a></span></div>
                    <div class="box"><strong>Created By:</strong> <span>${model.CreatedBy}</span></div>
                    <div class="box"><strong>Created At:</strong> <span>${model.CreatedAt}</span></div>
                    <div class="comment-box">
                        <h5>Add a Comment</h5>
                        <textarea id="comment" class="form-control" rows="4" placeholder="Enter your comment here..."></textarea>
                        <button id="submitComment" class="btn btn-primary mt-2">Submit Comment</button>
                    </div>
                </div>`
        ,generateBoxedContent('Notes', model.notes ? model.notes : 'N/A');
    }

    // Initialize Kendo Timeline with request updates
    document.addEventListener('DOMContentLoaded', function () {
        initializeKendoTimeline(requestId);
    });

    function initializeKendoTimeline(requestId) {
        var timelineInit = AppFunctions.getAjaxResponse('/Requests/GetRequestUpdates/' + requestId, 'GET', null);

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
                            date: { from: "createdAt", type: "date" },
                            text: { from: "Update" },
                            title: { from: "createdBy" }
                        }
                    }
                }
            },
            alternatingMode: true,
            collapsibleEvents: true,
            orientation: "vertical"
        });
    }
});
