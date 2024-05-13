console.log("backoffice.js loaded correctly!");

// INICIO TARIFAS

function LoadTarifas(active) {

    ShowOrHideMainLoader();

    //Load function
    $("#backoffice-container").load(
        "/BackOffice/LoadTarifas",
        {
            active: active
        },
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }

        }
    );

}

function LoadUsuarios(active) {

    ShowOrHideMainLoader();

    //Load function
    $("#backoffice-container").load(
        "/BackOffice/LoadUsuarios",
        {
            active: active
        },
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }

        }
    );

}

function SaveTarifa() {

    ShowOrHideMainLoader();

    var descripcion = $("#modal-add-tarifa-descripcion").val();
    var precio = $("#modal-add-tarifa-precio").val();

    $.ajax({
        url: '/BackOffice/SaveTarifa',
        type: 'POST',
        data: {
            descripcion: descripcion,
            precio: precio
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-add-tarifa-close").click();

            LoadToastErrors(response);

            $("#modal-add-tarifa-descripcion").val("");
            $("#modal-add-tarifa-precio").val("");

            LoadTarifas(true);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-add-tarifa-close").click();

            $("#modal-add-tarifa-descripcion").val("");
            $("#modal-add-tarifa-precio").val("");
        }
    });

}

function EditarTarifa() {

    ShowOrHideMainLoader();

    var id = $("#modal-edit-tarifa-id").val();
    var descripcion = $("#modal-edit-tarifa-descripcion").val();
    var precio = $("#modal-edit-tarifa-precio").val();

    $.ajax({
        url: '/BackOffice/EditarTarifa',
        type: 'POST',
        data: {
            id: id,
            descripcion: descripcion,
            precio: precio
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-edit-tarifa-close").click();

            LoadToastErrors(response);

            LoadTarifas(true);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-edit-tarifa-close").click();
        }
    });

}

function ShowDisabledTarifa(id) {

    $("#modal-disabled-tarifa").modal("toggle");
    $("#modal-disabled-tarifa-id").val(id);

}

function ShowEnabledTarifa(id) {

    $("#modal-enabled-tarifa").modal("toggle");
    $("#modal-enabled-tarifa-id").val(id);

}

function ShowEditarTarifa(id) {

    ShowOrHideMainLoader();

    //Load function
    $("#modal-edit-tarifa-body").load(
        "/BackOffice/ShowEditarTarifa",
        {
            id: id
        },
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }
            else {
                $("#modal-edit-tarifa").modal("toggle");
            }
        }
    );

}

function DisabledTarifa() {

    ShowOrHideMainLoader();

    var id = $("#modal-disabled-tarifa-id").val();

    $.ajax({
        url: '/BackOffice/DisabledTarifa',
        type: 'POST',
        data: {
            id: id
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-disabled-tarifa-close").click();

            LoadToastErrors(response);

            LoadTarifas(true);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-disabled-tarifa-close").click();
        }
    });
}

function EnabledTarifa() {

    ShowOrHideMainLoader();

    var id = $("#modal-enabled-tarifa-id").val();

    $.ajax({
        url: '/BackOffice/EnabledTarifa',
        type: 'POST',
        data: {
            id: id
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-enabled-tarifa-close").click();

            LoadToastErrors(response);

            LoadTarifas(false);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-enabled-tarifa-close").click();
        }
    });
}

// FIN TARIFAS

// INICIO SERVICIOS

function LoadServicios(active) {

    ShowOrHideMainLoader();

    //Load function
    $("#backoffice-container").load(
        "/BackOffice/LoadServicios",
        {
            active: active
        },
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }

        }
    );

}

function SaveServicio() {

    ShowOrHideMainLoader();

    var descripcion = $("#modal-add-servicio-descripcion").val();
    var idTarifa = $("#modal-add-servicio-tarifa").val();

    $.ajax({
        url: '/BackOffice/SaveServicio',
        type: 'POST',
        data: {
            descripcion: descripcion,
            idTarifa: idTarifa
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-add-servicio-close").click();

            LoadToastErrors(response);

            $("#modal-add-servicio-descripcion").val("");
            $("#modal-add-servicio-tarifa").val(0);

            LoadServicios(true);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-add-servicio-close").click();

            $("#modal-add-servicio-descripcion").val("");
            $("#modal-add-servicio-precio").val("");
        }
    });

}

function EditarServicio() {

    ShowOrHideMainLoader();

    var id = $("#modal-edit-servicio-id").val();
    var idServicio = $("#modal-edit-servicio-idservicio").val();
    var descripcion = $("#modal-edit-servicio-descripcion").val();
    var idTarifa = $("#modal-edit-servicio-tarifa").val();

    $.ajax({
        url: '/BackOffice/EditarServicio',
        type: 'POST',
        data: {
            id: id,
            descripcion: descripcion,
            idTarifa: idTarifa,
            idServicio: idServicio
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-edit-servicio-close").click();

            LoadToastErrors(response);

            LoadServicios(true);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-edit-servicio-close").click();
        }
    });

}

function ShowDisabledServicio(idServicio, idTarifa) {

    $("#modal-disabled-servicio").modal("toggle");
    $("#modal-disabled-servicio-idservicio").val(idServicio);
    $("#modal-disabled-servicio-idtarifa").val(idTarifa);

}

function ShowEnabledServicio(idServicio, idTarifa) {

    $("#modal-enabled-servicio").modal("toggle");
    $("#modal-enabled-servicio-idservicio").val(idServicio);
    $("#modal-enabled-servicio-idtarifa").val(idTarifa);

}

function ShowEditarServicio(idServicio, idTarifa) {

    ShowOrHideMainLoader();

    //Load function
    $("#modal-edit-servicio-body").load(
        "/BackOffice/ShowEditarServicio",
        {
            idServicio: idServicio,
            idTarifa: idTarifa
        },
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }
            else {
                $("#modal-edit-servicio").modal("toggle");
            }
        }
    );

}

function DisabledServicio() {

    ShowOrHideMainLoader();

    var idServicio = $("#modal-disabled-servicio-idservicio").val();
    var idTarifa = $("#modal-disabled-servicio-idtarifa").val();

    $.ajax({
        url: '/BackOffice/DisabledServicio',
        type: 'POST',
        data: {
            idServicio: idServicio,
            idTarifa: idTarifa
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-disabled-servicio-close").click();

            LoadToastErrors(response);

            LoadServicios(true);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-disabled-servicio-close").click();
        }
    });
}

function EnabledServicio() {

    ShowOrHideMainLoader();

    var idServicio = $("#modal-enabled-servicio-idservicio").val();
    var idTarifa = $("#modal-enabled-servicio-idtarifa").val();

    $.ajax({
        url: '/BackOffice/EnabledServicio',
        type: 'POST',
        data: {
            idServicio: idServicio,
            idTarifa: idTarifa
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-enabled-servicio-close").click();

            LoadToastErrors(response);

            LoadServicios(false);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-enabled-servicio-close").click();
        }
    });
}

// FIN SERVICIOS