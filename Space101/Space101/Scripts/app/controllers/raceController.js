let RaceController = function (racesService) {
    let deleteButton;
    let hardDeleteButton;
    let table;
    let doneAlert;
    let failAlert;
    let modelName = 'Race';

    let initializeDataTable = function (container) {
        table = $(`${container}`).DataTable({
            "order": [[0, 'asc']],
            "columns": [
                null,
                { "searchable": false, "orderable": false },
                null,
                null,
                null,
                null,
                { "searchable": false, "orderable": false }
            ]
        });
    };
    let initializeDelete = function (container, confirmType, doneAlertType, failAlertType) {

        $(container).on('click', '.js-delete', function () {
            deleteButton = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deleteRace(confirmType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deleteRace = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = deleteButton.attr("data-race-id");
                racesService().deleteRace(id, doneDelete, failDelete);
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
        }
    };
    let initializeHardDelete = function (container, confirmType, doneAlertType, failAlertType) {

        $(container).on('click', '.js-hard-delete', function () {
            hardDeleteButton = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deleteRace(confirmType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deleteRace = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = hardDeleteButton.attr("data-race-id");
                racesService().hardDeleteRace(id, doneDelete, failDelete);
            }
        }

        let doneDelete = function () {
            table
                .row(hardDeleteButton.parents('tr'))
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
    let initializeAvatarPreview = function () {
        $("#UploadedAvatar").on("input", function () {
            const target = $(this);
            const file = target.prop("files")[0];
            const preview = $('#avatar-preview');
            preview.html("");

            const img = $("<img>").addClass("img-thumbnail img-responsive");
            preview.append(img);

            const reader = new FileReader();

            reader.onload = (function (image) {
                return function (e) {
                    image.attr("src", e.target.result);
                };
            })(img);
            reader.readAsDataURL(file);
        });
    }

    return {
        initializeDataTable: initializeDataTable,
        initializeDelete: initializeDelete,
        initializeAvatarPreview: initializeAvatarPreview,
        initializeHardDelete: initializeHardDelete
    }

}(RaceService);