console.log("home.js loaded correctly!");

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