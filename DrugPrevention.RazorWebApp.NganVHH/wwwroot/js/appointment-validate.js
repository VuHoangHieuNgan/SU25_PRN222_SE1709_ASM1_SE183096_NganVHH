// Validate form cuộc hẹn: chỉ validate các trường đang enable (không bị disabled)
function validateAppointmentForm() {
    var isValid = true;

    // Validate UserID
    var userInput = document.getElementById('AppointmentsNganVHH_UserID');
    var userErrorSpan = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.UserID"]');
    if (userInput && !userInput.disabled && (!userInput.value || userInput.value === '')) {
        isValid = false;
        if (userErrorSpan) {
            userErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Bắt buộc chọn người dùng!';
            userErrorSpan.classList.add('show');
        }
        userInput.classList.add('is-invalid');
    } else if (userInput && !userInput.disabled) {
        if (userErrorSpan) {
            userErrorSpan.innerHTML = "";
            userErrorSpan.classList.remove('show');
        }
        userInput.classList.remove('is-invalid');
    }

    // Validate ConsultantID
    var consultantInput = document.getElementById('AppointmentsNganVHH_ConsultantID');
    var consultantErrorSpan = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.ConsultantID"]');
    if (consultantInput && !consultantInput.disabled && (!consultantInput.value || consultantInput.value === '')) {
        isValid = false;
        if (consultantErrorSpan) {
            consultantErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Bắt buộc chọn chuyên gia!';
            consultantErrorSpan.classList.add('show');
        }
        consultantInput.classList.add('is-invalid');
    } else if (consultantInput && !consultantInput.disabled) {
        if (consultantErrorSpan) {
            consultantErrorSpan.innerHTML = "";
            consultantErrorSpan.classList.remove('show');
        }
        consultantInput.classList.remove('is-invalid');
    }

    // Validate AppointmentDateTime
    var dateInput = document.getElementById('AppointmentsNganVHH_AppointmentDateTime');
    var dateErrorSpan = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.AppointmentDateTime"]');
    if (dateInput && !dateInput.disabled) {
        if (!dateInput.value) {
            isValid = false;
            if (dateErrorSpan) {
                dateErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Bắt buộc chọn ngày giờ!';
                dateErrorSpan.classList.add('show');
            }
            dateInput.classList.add('is-invalid');
        } else {
            // Parse local datetime
            var parts = dateInput.value.split(/[-T:]/);
            var inputDate = new Date(parts[0], parts[1] - 1, parts[2], parts[3], parts[4]);
            var now = new Date();
            if (inputDate < now) {
                isValid = false;
                if (dateErrorSpan) {
                    dateErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Ngày giờ phải là hiện tại hoặc tương lai!';
                    dateErrorSpan.classList.add('show');
                }
                dateInput.classList.add('is-invalid');
            } else {
                if (dateErrorSpan) {
                    dateErrorSpan.innerHTML = "";
                    dateErrorSpan.classList.remove('show');
                }
                dateInput.classList.remove('is-invalid');
            }
        }
    }

    // Validate Duration
    var durationInput = document.getElementById('AppointmentsNganVHH_Duration');
    var durationErrorSpan = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.Duration"]');
    if (durationInput && !durationInput.disabled) {
        var val = parseInt(durationInput.value);
        if (!durationInput.value || isNaN(val) || val < 30 || val > 480) {
            isValid = false;
            if (durationErrorSpan) {
                durationErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Thời lượng từ 30 đến 480 phút!';
                durationErrorSpan.classList.add('show');
            }
            durationInput.classList.add('is-invalid');
        } else {
            if (durationErrorSpan) {
                durationErrorSpan.innerHTML = "";
                durationErrorSpan.classList.remove('show');
            }
            durationInput.classList.remove('is-invalid');
        }
    }

    // Validate ConsultationType (radio) - chỉ validate khi có ít nhất 1 radio không bị disabled
    var consultRadios = document.querySelectorAll('input[name="AppointmentsNganVHH.ConsultationType"]');
    var consultChecked = null;
    var hasEnabledConsultRadio = false;
    consultRadios.forEach(function(radio) {
        if (!radio.disabled) {
            hasEnabledConsultRadio = true;
            if (radio.checked) consultChecked = radio;
        }
    });
    var consultError = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.ConsultationType"]');
    if (hasEnabledConsultRadio && !consultChecked) {
        isValid = false;
        if (consultError) {
            consultError.innerHTML = '<i class="fas fa-exclamation-circle"></i>Bắt buộc chọn hình thức tư vấn!';
            consultError.classList.add('show');
        }
    } else if (consultError) {
        consultError.innerHTML = "";
        consultError.classList.remove('show');
    }

    // Validate AssessmentType (bắt buộc)
    var assessmentInput = document.getElementById('AppointmentsNganVHH_AssessmentType');
    var assessmentErrorSpan = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.AssessmentType"]');
    if (assessmentInput && !assessmentInput.disabled && (!assessmentInput.value || assessmentInput.value.trim() === '')) {
        isValid = false;
        if (assessmentErrorSpan) {
            assessmentErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Bắt buộc nhập loại đánh giá!';
            assessmentErrorSpan.classList.add('show');
        }
        assessmentInput.classList.add('is-invalid');
    } else if (assessmentInput && !assessmentInput.disabled) {
        if (assessmentErrorSpan) {
            assessmentErrorSpan.innerHTML = "";
            assessmentErrorSpan.classList.remove('show');
        }
        assessmentInput.classList.remove('is-invalid');
    }

    // Validate IsInterpreterNeeded (radio) - chỉ validate khi có ít nhất 1 radio không bị disabled
    var interpreterRadios = document.querySelectorAll('input[name="AppointmentsNganVHH.IsInterpreterNeeded"]');
    var interpreterChecked = null;
    var hasEnabledInterpreterRadio = false;
    interpreterRadios.forEach(function(radio) {
        if (!radio.disabled) {
            hasEnabledInterpreterRadio = true;
            if (radio.checked) interpreterChecked = radio;
        }
    });
    var interpreterError = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.IsInterpreterNeeded"]');
    if (hasEnabledInterpreterRadio && !interpreterChecked) {
        isValid = false;
        if (interpreterError) {
            interpreterError.innerHTML = '<i class="fas fa-exclamation-circle"></i>Bắt buộc chọn phiên dịch!';
            interpreterError.classList.add('show');
        }
    } else if (interpreterError) {
        interpreterError.innerHTML = "";
        interpreterError.classList.remove('show');
    }

    // Validate FeedbackRating (không bắt buộc, nhưng nếu có thì phải là số nguyên từ 1-5)
    var feedbackInput = document.getElementById('AppointmentsNganVHH_FeedbackRating');
    var feedbackErrorSpan = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.FeedbackRating"]');
    if (feedbackInput && !feedbackInput.disabled && feedbackInput.value && feedbackInput.value.trim() !== '') {
        var feedbackValue = feedbackInput.value.trim();
        
        // Kiểm tra có phải số không
        if (isNaN(feedbackValue)) {
            isValid = false;
            if (feedbackErrorSpan) {
                feedbackErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Đánh giá phải là số!';
                feedbackErrorSpan.classList.add('show');
            }
            feedbackInput.classList.add('is-invalid');
        }
        // Kiểm tra có phải số thập phân không (có dấu chấm hoặc phẩy)
        else if (feedbackValue.includes('.') || feedbackValue.includes(',')) {
            isValid = false;
            if (feedbackErrorSpan) {
                feedbackErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Đánh giá phải là số nguyên (1, 2, 3, 4, hoặc 5), không được là số thập phân như 3.5!';
                feedbackErrorSpan.classList.add('show');
            }
            feedbackInput.classList.add('is-invalid');
        }
        // Kiểm tra range từ 1-5
        else {
            var feedbackNum = parseInt(feedbackValue);
            if (feedbackNum < 1 || feedbackNum > 5) {
                isValid = false;
                if (feedbackErrorSpan) {
                    feedbackErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Đánh giá phải từ 1 đến 5 sao!';
                    feedbackErrorSpan.classList.add('show');
                }
                feedbackInput.classList.add('is-invalid');
            } else {
                // Valid - clear errors
                if (feedbackErrorSpan) {
                    feedbackErrorSpan.innerHTML = "";
                    feedbackErrorSpan.classList.remove('show');
                }
                feedbackInput.classList.remove('is-invalid');
            }
        }
    } else if (feedbackInput && !feedbackInput.disabled) {
        // Field is empty but enabled - clear any existing errors (since it's optional)
        if (feedbackErrorSpan) {
            feedbackErrorSpan.innerHTML = "";
            feedbackErrorSpan.classList.remove('show');
        }
        feedbackInput.classList.remove('is-invalid');
    }

    // Validate Notes (không bắt buộc, nhưng nếu có thì kiểm tra độ dài)
    var notesInput = document.getElementById('AppointmentsNganVHH_Notes');
    var notesErrorSpan = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.Notes"]');
    if (notesInput && !notesInput.disabled && notesInput.value && notesInput.value.length > 1000) {
        isValid = false;
        if (notesErrorSpan) {
            notesErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Ghi chú tối đa 1000 ký tự!';
            notesErrorSpan.classList.add('show');
        }
        notesInput.classList.add('is-invalid');
    } else if (notesInput && !notesInput.disabled) {
        if (notesErrorSpan) {
            notesErrorSpan.innerHTML = "";
            notesErrorSpan.classList.remove('show');
        }
        notesInput.classList.remove('is-invalid');
    }

    return isValid;
}

document.addEventListener('DOMContentLoaded', function () {
    var form = document.querySelector('form');
    if (form) {
        form.addEventListener('submit', function (e) {
            if (!validateAppointmentForm()) e.preventDefault();
        });
    }
    
    // Real-time validation cho FeedbackRating
    var feedbackInput = document.getElementById('AppointmentsNganVHH_FeedbackRating');
    if (feedbackInput) {
        feedbackInput.addEventListener('input', function () {
            validateFeedbackRating();
        });
        feedbackInput.addEventListener('blur', function () {
            validateFeedbackRating();
        });
    }
    
    // Validate khi bấm nút SignalR
    var btnHubCreate = document.getElementById('btnHubCreate');
    if (btnHubCreate) {
        btnHubCreate.addEventListener('click', function (e) {
            if (!validateAppointmentForm()) {
                e.preventDefault();
                return false;
            }
        });
    }
    var btnHubUpdate = document.getElementById('btnHubUpdate');
    if (btnHubUpdate) {
        btnHubUpdate.addEventListener('click', function (e) {
            if (!validateAppointmentForm()) {
                e.preventDefault();
                return false;
            }
        });
    }
});

// Function riêng để validate FeedbackRating (dùng cho real-time validation)
function validateFeedbackRating() {
    var feedbackInput = document.getElementById('AppointmentsNganVHH_FeedbackRating');
    var feedbackErrorSpan = document.querySelector('span[data-valmsg-for="AppointmentsNganVHH.FeedbackRating"]');
    
    if (!feedbackInput || feedbackInput.disabled) return true;
    
    if (feedbackInput.value && feedbackInput.value.trim() !== '') {
        var feedbackValue = feedbackInput.value.trim();
        
        // Kiểm tra có phải số không
        if (isNaN(feedbackValue)) {
            if (feedbackErrorSpan) {
                feedbackErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Đánh giá phải là số!';
                feedbackErrorSpan.classList.add('show');
            }
            feedbackInput.classList.add('is-invalid');
            return false;
        }
        // Kiểm tra có phải số thập phân không (có dấu chấm hoặc phẩy)
        else if (feedbackValue.includes('.') || feedbackValue.includes(',')) {
            if (feedbackErrorSpan) {
                feedbackErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Đánh giá phải là số nguyên (1, 2, 3, 4, hoặc 5), không được là số thập phân như 3.5!';
                feedbackErrorSpan.classList.add('show');
            }
            feedbackInput.classList.add('is-invalid');
            return false;
        }
        // Kiểm tra range từ 1-5
        else {
            var feedbackNum = parseInt(feedbackValue);
            if (feedbackNum < 1 || feedbackNum > 5) {
                if (feedbackErrorSpan) {
                    feedbackErrorSpan.innerHTML = '<i class="fas fa-exclamation-circle"></i>Đánh giá phải từ 1 đến 5 sao!';
                    feedbackErrorSpan.classList.add('show');
                }
                feedbackInput.classList.add('is-invalid');
                return false;
            } else {
                // Valid - clear errors
                if (feedbackErrorSpan) {
                    feedbackErrorSpan.innerHTML = "";
                    feedbackErrorSpan.classList.remove('show');
                }
                feedbackInput.classList.remove('is-invalid');
                return true;
            }
        }
    } else {
        // Field is empty - clear any existing errors (since it's optional)
        if (feedbackErrorSpan) {
            feedbackErrorSpan.innerHTML = "";
            feedbackErrorSpan.classList.remove('show');
        }
        feedbackInput.classList.remove('is-invalid');
        return true;
    }
}
