console.log("site.js loaded correctly!");

function ShowOrHideMainLoader() {
    var loadingHide = $('#main-loader').attr('class').indexOf("d-none");
    if (loadingHide > 0) {
        $("#main-loader").removeClass("d-none");
    } else {
        $("#main-loader").addClass("d-none");
    }
}

function AddToastAndShowIt(title, message, typeOfMessage) {

    // typeOfMessage: 0 = info; 1 = warning; 2 = error;
    var toastIcon = "";
    if (typeOfMessage == 0) {
        toastIcon = "<i class='text-primary me-2 bi bi-info-circle-fill'></i>";
    }
    else if (typeOfMessage == 1) {
        toastIcon = "<i class='text-warning me-2 bi bi-exclamation-circle-fill'></i>";
    }
    else if (typeOfMessage == 2) {
        toastIcon = "<i class='text-danger me-2 bg-bi bi-x-circle-fill'></i>";
    }

    // Generate a unique ID for each toast
    const toastId = 'toast-' + Math.floor(Math.random() * 1000);

    // Create a new toast element
    const toast = `
    <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true" style="width: 450px;">
        <div class="toast-header fw-bold">
            ${toastIcon}
            <strong class="me-auto">${title}</strong>
            <small></small>
            <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
        <div class="toast-body fw-bold">
            ${message}
        </div>
    </div>
    `;

    // Create a new div element
    const newToast = document.createElement('div');
    newToast.classList.add("my-2");

    // Set the HTML content to the generated toast
    newToast.innerHTML = toast;

    // Append the new toast element to the toast container
    document.getElementById('generic-toast-container').appendChild(newToast);

    // Initialize the toast
    new bootstrap.Toast(newToast.querySelector('.toast')).show();

    // Listen for the toast's hide event
    newToast.addEventListener('hidden.bs.toast', function () {
        // Remove the corresponding HTML element when the toast is hidden
        newToast.remove();
    });
}

function LoadToastErrors(errors) {

    // Loop the errors to show them.
    errors.forEach(function (error) {

        var typeOfMessage = 0;  // Info
        if (error.isException === true) {
            typeOfMessage = 2;  // Error
        }
        else if (error.isException === false) {
            typeOfMessage = 1;  // Warning
        }

        AddToastAndShowIt(error.method, error.errorMessage, typeOfMessage);
    })

}

function LoadDatatableFramework(tableId) {

    new DataTable(tableId, {
        language: {
            url: '../lib/datatables/js/languages/datatable-spanish-labels.json'
        },
        pagingType: 'simple_numbers'
    });

}