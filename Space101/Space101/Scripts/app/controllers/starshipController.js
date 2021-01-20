let StarshipController = ((starshipService) => {
    let deleteButton;
    let hardDeleteButton;
    let table;
    let modelName = 'Starship';

    let initializeDatatable = function (container) {
        table = $(`${container}`).DataTable({
            "order": [[0, 'asc']],
            "columns": [
                null,
                null,
                null,
                null,
                null,
                null,
                { "searchable": false, "orderable": false }
            ]
        });
    };
    let initializeDelete = function (container, confirmType, doneAlertType, failAlertType) {
        let doneAlert;
        let failAlert;

        $(container).on('click', '.js-delete', function () {
            deleteButton = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deleteTerrain(confirmType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deleteTerrain = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = deleteButton.attr("data-starship-id");
                starshipService().deleteStarship(id, doneDelete, failDelete);
            }
        }

        let doneDelete = function () {
            table
                .row(deleteButton.parents('tr'))
                .remove()
                .draw();

            if (doneAlert) {
                doneAlert(modelName, "deleted");
            }
        };

        let failDelete = function (jqxhr) {
            let message = JSON.parse(jqxhr.responseText).message;
            if (failAlert)
                failAlert(modelName, "deleted", message);
        };
    };
    let initializeHardDelete = function (container, confirmType, doneAlertType, failAlertType) {
        let doneAlert;
        let failAlert;

        $(container).on('click', '.js-hard-delete', function () {
            hardDeleteButton = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deleteTerrain(confirmType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deleteTerrain = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = hardDeleteButton.attr("data-starship-id");
                starshipService().hardDeleteStarship(id, doneDelete, failDelete);
            }
        }

        let doneDelete = function () {
            table
                .row(hardDeleteButton.parents('tr'))
                .remove()
                .draw();

            if (doneAlert) {
                doneAlert(modelName, "deleted");
            };
        }

        let failDelete = function (jqxhr) {
            let message = JSON.parse(jqxhr.responseText).message;
            if (failAlert)
                failAlert(modelName, "deleted", message);
        };
    };

    return {
        initializeDatatable: initializeDatatable,
        initializeDelete: initializeDelete,
        initializeHardDelete: initializeHardDelete
    }

})(StarshipService);