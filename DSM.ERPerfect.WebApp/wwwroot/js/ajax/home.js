console.log("home.js loaded correctly!");

// INICIO ESTADÍSTICAS

function GetPaymentBills() {

    ShowOrHideMainLoader();

    //Load function
    $("#chart-container").load(
        "/Home/GetPaymentBills",
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }

        }
    );

}

function GetTop5ServicioMes() {

    ShowOrHideMainLoader();

    //Load function
    $("#chart-container").load(
        "/Home/GetTop5ServicioMes",
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }

        }
    );

}

// FIN ESTADÍSTICAS

// INICIO CLIENTES

function LoadClientes(active) {

    ShowOrHideMainLoader();

    //Load function
    $("#client-container").load(
        "/Home/LoadClientes",
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

function SaveCliente() {

    ShowOrHideMainLoader();

    var nombre = $("#modal-add-cliente-nombre").val();
    var apellidos = $("#modal-add-cliente-apellidos").val();
    var telefono = $("#modal-add-cliente-telefono").val();
    var email = $("#modal-add-cliente-email").val();
    var dni = $("#modal-add-cliente-dni").val();

    $.ajax({
        url: '/Home/SaveCliente',
        type: 'POST',
        data: {
            nombre: nombre,
            apellidos: apellidos,
            telefono: telefono,
            email: email,
            dni: dni
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-add-cliente-close").click();

            LoadToastErrors(response);

            $("#modal-add-cliente-nombre").val('');
            $("#modal-add-cliente-apellidos").val('');
            $("#modal-add-cliente-telefono").val('');
            $("#modal-add-cliente-email").val('');
            $("#modal-add-cliente-dni").val('');

            LoadClientes(true);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-add-cliente-close").click();

            $("#modal-add-cliente-descripcion").val("");
            $("#modal-add-cliente-precio").val("");
        }
    });

}

function EditarCliente() {

    ShowOrHideMainLoader();

    var id = $("#modal-edit-cliente-id").val();
    var nombre = $("#modal-edit-cliente-nombre").val();
    var apellidos = $("#modal-edit-cliente-apellidos").val();
    var telefono = $("#modal-edit-cliente-telefono").val();
    var email = $("#modal-edit-cliente-email").val();
    var dni = $("#modal-edit-cliente-dni").val();

    $.ajax({
        url: '/Home/EditarCliente',
        type: 'POST',
        data: {
            id: id,
            nombre: nombre,
            apellidos: apellidos,
            telefono: telefono,
            email: email,
            dni: dni
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-edit-cliente-close").click();

            LoadToastErrors(response);

            LoadClientes(true);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-edit-cliente-close").click();
        }
    });

}

function ShowDisabledCliente(id) {

    $("#modal-disabled-cliente").modal("toggle");
    $("#modal-disabled-cliente-id").val(id);

}

function ShowEnabledCliente(id) {

    $("#modal-enabled-cliente").modal("toggle");
    $("#modal-enabled-cliente-id").val(id);

}

function ShowEditarCliente(id) {

    ShowOrHideMainLoader();

    //Load function
    $("#modal-edit-cliente-body").load(
        "/Home/ShowEditarCliente",
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
                $("#modal-edit-cliente").modal("toggle");
            }
        }
    );

}

function DisabledCliente() {

    ShowOrHideMainLoader();

    var id = $("#modal-disabled-cliente-id").val();

    $.ajax({
        url: '/Home/DisabledCliente',
        type: 'POST',
        data: {
            id: id
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-disabled-cliente-close").click();

            LoadToastErrors(response);

            LoadClientes(true);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-disabled-cliente-close").click();
        }
    });
}

function EnabledCliente() {

    ShowOrHideMainLoader();

    var id = $("#modal-enabled-cliente-id").val();

    $.ajax({
        url: '/Home/EnabledCliente',
        type: 'POST',
        data: {
            id: id
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-enabled-cliente-close").click();

            LoadToastErrors(response);

            LoadClientes(false);
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-enabled-cliente-close").click();
        }
    });
}

// FIN CLIENTES

// INICIO FACTURAS PENDIENTES

function LoadFacturasPendientes() {

    ShowOrHideMainLoader();

    //Load function
    $("#facturas-container").load(
        "/Home/LoadFacturasPendientes",
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }

        }
    );

}

function SaveFacturaPendiente() {

    ShowOrHideMainLoader();

    var idCliente = $("#modal-add-factura-pendiente-cliente").val();
    var idServicio = $("#modal-add-factura-pendiente-servicio").val();
    var idFormaPago = $("#modal-add-factura-pendiente-tipo-pago").val();

    $.ajax({
        url: '/Home/SaveFacturaPendiente',
        type: 'POST',
        data: {
            idCliente: idCliente,
            idServicio: idServicio,
            idFormaPago: idFormaPago
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-add-factura-pendiente-close").click();

            LoadToastErrors(response);

            $("#modal-add-factura-pendiente-cliente").val(0);
            $("#modal-add-factura-pendiente-servicio").val(0);
            $("#modal-add-factura-pendiente-tipo-pago").val(0);

            LoadFacturasPendientes();
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-add-factura-pendiente-close").click();

            $("#modal-add-factura-pendiente-cliente").val(0);
            $("#modal-add-factura-pendiente-servicio").val(0);
            $("#modal-add-factura-pendiente-tipo-pago").val(0);
        }
    });

}

function ShowEditarFacturaPendiente(id) {

    ShowOrHideMainLoader();

    //Load function
    $("#modal-edit-factura-pendiente-body").load(
        "/Home/ShowEditarFacturaPendiente",
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
                $("#modal-edit-factura-pendiente").modal("toggle");
            }
        }
    );

}

function EditFacturaPendiente() {

    ShowOrHideMainLoader();

    var idFactura = $("#modal-edit-factura-pendiente-id").val();
    var idCliente = $("#modal-edit-factura-pendiente-cliente").val();
    var idServicio = $("#modal-edit-factura-pendiente-servicio").val();
    var idFormaPago = $("#modal-edit-factura-pendiente-tipo-pago").val();

    $.ajax({
        url: '/Home/EditFacturaPendiente',
        type: 'POST',
        data: {
            idFactura: idFactura,
            idCliente: idCliente,
            idServicio: idServicio,
            idFormaPago: idFormaPago
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-edit-factura-pendiente-close").click();

            LoadToastErrors(response);

            LoadFacturasPendientes();
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            LoadToastErrors(errors);

            $("#modal-edit-factura-pendiente-close").click();
        }
    });

}

function ShowCobrarFacturaPendiente(id) {

    $("#modal-cobrar-factura-pendiente").modal("toggle");
    $("#modal-cobrar-factura-pendiente-id").val(id);

}

function CobrarFacturaPendiente() {

    ShowOrHideMainLoader();

    var idFactura = $("#modal-cobrar-factura-pendiente-id").val();

    $.ajax({
        url: '/Home/CobrarFacturaPendiente',
        type: 'POST',
        data: {
            idFactura: idFactura
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-cobrar-factura-pendiente-close").click();

            LoadToastErrors(response);

            LoadFacturasPendientes();
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            $("#modal-cobrar-factura-pendiente-close").click();

            LoadToastErrors(errors);
        }
    });

}

function LoadFacturasCobradas() {

    ShowOrHideMainLoader();

    //Load function
    $("#facturas-container").load(
        "/Home/LoadFacturasCobradas",
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }

        }
    );

}

function ShowAbonarFactura(id) {

    $("#modal-factura-abonada").modal("toggle");
    $("#modal-factura-abonada-id").val(id);

}

function AbonarFactura() {

    ShowOrHideMainLoader();

    var idFactura = $("#modal-factura-abonada-id").val();

    $.ajax({
        url: '/Home/AbonarFactura',
        type: 'POST',
        data: {
            idFactura: idFactura
        },
        success: function (response) {

            ShowOrHideMainLoader();

            $("#modal-factura-abonada-close").click();

            LoadToastErrors(response);

            LoadFacturasCobradas();
        },
        error: function (xhr, status, error) {
            ShowOrHideMainLoader();

            var errors = JSON.parse(xhr.responseText);

            $("#modal-factura-abonada-close").click();

            LoadToastErrors(errors);
        }
    });

}

function LoadFacturasAbonadas() {

    ShowOrHideMainLoader();

    //Load function
    $("#facturas-container").load(
        "/Home/LoadFacturasAbonadas",
        function (response, status, xhr) {

            ShowOrHideMainLoader();

            if (status == "error") {
                var errors = JSON.parse(response);

                LoadToastErrors(errors);
            }

        }
    );

}

// FIN FACTURAS PENDIENTES