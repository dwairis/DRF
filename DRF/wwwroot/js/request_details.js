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
                { title: "Overview", content: generateOverviewContent(model) },
                { title: "Project Details", content: generateProjectDetailsContent(model) },
                { title: "Criteria", content: generateCriteriaContent(model) },
                { title: "Timeline", content: generateTimelineContent(model) },
                { title: "Metadata", content: generateMetadataContent(model) },
                { title: "Status and Activity Notes", content: generateNotesContent(model) }
            ]
        });
    }

    function generateOverviewContent(model) {
        return `<div>
                    <p><strong>Third Party Organization:</strong> ${model.ThirdPartyOrganization}</p>
                    <p><strong>Program Title:</strong> ${model.ProgramTitle}</p>
                    <p><strong>Donors:</strong> ${model.Donors.join(', ')}</p>
                    <p><strong>Partners:</strong> ${model.Partners.join(', ')}</p>
                    <p><strong>Request ID:</strong> ${model.Id}</p>
                </div>`;
    }

    function generateProjectDetailsContent(model) {
        return `<div>
                    <p><strong>Brief on Program:</strong> ${model.BriefOnProgram}</p>
                </div>`;
    }

    function generateCriteriaContent(model) {
        return `<div>
                    <p><strong>Total Target:</strong> ${model.TotalTarget}</p>
                    <p><strong>Target Request:</strong> ${model.TargetRequest}</p>
                    <p><strong>Referral Delivery Deadline:</strong> ${model.ReferralDeliveryDL}</p>
                    <p><strong>Referral Total:</strong> ${model.ReferralTotal}</p>
                </div>`;
    }

    function generateTimelineContent(model) {
        return `<div>
                    <p><strong>Project Start Date:</strong> ${model.ProjectStartDate}</p>
                    <p><strong>Project End Date:</strong> ${model.ProjectEndDate}</p>
                    <p><strong>Contact Person:</strong> ${model.ContactPerson}</p>
                    <p><strong>Hired Self-Employed:</strong> ${model.HiredSelfEmployed ? 'Yes' : 'No'}</p>
                    <p><strong>Counterpart:</strong> ${model.CounterPart}</p>
                </div>`;
    }

    function generateMetadataContent(model) {
        return `<div>
                    <p><strong>Criteria:</strong> ${model.Criteria}</p>
                </div>`;
    }

    function generateNotesContent(model) {
        return `<div>
                    <p><strong>Current Status:</strong> ${model.CurrentStatus}</p>
                    <p><strong>Data File URL:</strong> <a href="${model.DataFileUrl}" target="_blank">${model.DataFileUrl}</a></p>
                    <p><strong>Created By:</strong> ${model.CreatedBy}</p>
                    <p><strong>Created At:</strong> ${model.CreatedAt}</p>
                </div>`;
    }
});
