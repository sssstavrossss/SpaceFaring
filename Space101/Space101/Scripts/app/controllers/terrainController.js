let TerrainController = ((terrainService) => {
    let button;
    let table;
    let doneAlert;
    let failAlert;
    let modelName = 'Terrain';

    let init = function (container, confirmType, doneAlertType, failAlertType) {
        initializeDataTable(container);

        $(container).on('click', '.js-delete', function () {
            button = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deleteTerrain(confirmType);
        });
    };

    let initializeDataTable = function (container) {
        table = $(`${container}`).DataTable({
            "order": [[0, 'asc']],
            "columns": [
                null,
                null,
                null,
                { "searchable": false, "orderable": false }
            ]
        });
    };

    let assignAlerts = function (doneAlertType, failAlertType) {
        doneAlert = doneAlertType;
        failAlert = failAlertType;
    }

    let deleteTerrain = function (confirmationBox) {
        confirmationBox(modelName, confirmCallback);
    };

    let confirmCallback = function (result) {
        if (result) {
            let id = button.attr("data-terrain-id");
            terrainService().deleteTerrain(id, doneDelete, failDelete);
        }
    }

    let doneDelete = function () {
        table
            .row(button.parents('tr'))
            .remove()
            .draw();

        if (doneAlert)
            doneAlert(modelName);
    };

    let failDelete = function () {
        if (failAlert)
            failAlert(modelName);
    };

    return {
        init: init,
    }

})(TerrainService);