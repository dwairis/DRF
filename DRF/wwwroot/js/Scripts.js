let currentStep = 0;
const steps = document.querySelectorAll('.step');
const progressBar = document.querySelector('.progress-bar');

function showStep(step) {
    steps.forEach((el, index) => {
        el.classList.toggle('active', index === step);
    });
    progressBar.style.width = `${(step + 1) * 20}%`;
    if (step === steps.length - 1) {
        fillReview();
    }
}

function nextStep() {
    const currentForm = $(steps[currentStep]).find('input, select, textarea');
    let valid = true;
    currentForm.each(function () {
        const $this = $(this);
        const id = $this.attr('id') + 'Error';
        $('#' + id).text(''); // Clear previous error messages

        if (!$this.val()) {
            $('#' + id).text('*This field is required');
            valid = false;
        } else {
            // Custom validations
            if ($this.attr('type') === 'number' && $this.val() <= 0) {
                $('#' + id).text('*Must be a positive number');
                valid = false;
            }
        }
    });
    if (valid && currentStep < steps.length - 1) {
        currentStep++;
        showStep(currentStep);
    }
}

function prevStep() {
    if (currentStep > 0) {
        currentStep--;
        showStep(currentStep);
    }
}

function fillReview() {
    $('#reviewOrganization').text($('#organization').val());
    $('#reviewOrganizationType').text($('#organizationType').val());
    $('#reviewProgramTitle').text($('#programTitle').val());
    $('#reviewDonor').text($('#donor').val());

    const partners = $('input[name="partners"]:checked').map(function () {
        return $(this).next('label').text();
    }).get().join(', ');
    $('#reviewPartners').text(partners);

    $('#reviewTargetSectors').text($('#targetSectors').val());
    $('#reviewBriefProgram').text($('#briefProgram').val());
    $('#reviewTotalTarget').text($('#totalTarget').val());
    $('#reviewProjectStart').text($('#projectStart').val());
    $('#reviewProjectEnd').text($('#projectEnd').val());
    $('#reviewTargetRequest').text($('#targetRequest').val());
    $('#reviewCriteria').text($('#criteria').val());
    $('#reviewReferralDeliveryDeadline').text($('#referralDeliveryDeadline').val());
    $('#reviewReferralsTotal').text($('#referralsTotal').val());
    $('#reviewStatus').text($('#status').val());
    $('#reviewCounterpart').text($('#counterpart').val());
    $('#reviewStatusActivityNotes').text($('#statusActivityNotes').val());
    $('#reviewContactPerson').text($('#contactPerson').val());
    $('#reviewHiredSelfEmployed').text($('#hiredSelfEmployed').val());
}

$('#multiStepForm').on('submit', function (event) {
    const currentForm = $(steps[currentStep]).find('input, select, textarea');
    let valid = true;
    currentForm.each(function () {
        const $this = $(this);
        const id = $this.attr('id') + 'Error';
        $('#' + id).text(''); // Clear previous error messages

        if (!$this.val()) {
            $('#' + id).text('*This field is required');
            valid = false;
        } else {
            // Custom validations
            if ($this.attr('type') === 'number' && $this.val() <= 0) {
                $('#' + id).text('*Must be a positive number');
                valid = false;
            }
        }
    });
    if (!valid) {
        event.preventDefault();
    } else {
        alert('Form submitted!');
    }
});
