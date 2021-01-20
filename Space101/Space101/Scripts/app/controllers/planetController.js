let PlanetController = ((planetService) => {
    let deleteButton;
    let hardDeleteButton;
    let table;
    let doneAlert;
    let failAlert;
    let modelName = 'Planet';

    let initializeDatatable = function (container) {
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
            deletePlanet(confirmType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deletePlanet = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = deleteButton.attr("data-planet-id");
                planetService().deletePlanet(id, doneDelete, failDelete);
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
        $(container).on('click', '.js-hard-delete', function () {
            hardDeleteButton = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deletePlanet(confirmType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deletePlanet = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = hardDeleteButton.attr("data-planet-id");
                planetService().hardDeletePlanet(id, doneDelete, failDelete);
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
    let initPlanetForm = function () {
        $("#saved-images").on("click", ".js-remove-image", function (e) {
            e.preventDefault();

            let btn = $(this);
            let btnId = btn.attr("data-btn-id");
            let checkBoxId = `planetFileFormViewModels_${btnId}__DeleteIt`;
            let image = $(`[data-img-id=${btnId}]`)

            let shouldDelete = $(`#${checkBoxId}`);

            if (shouldDelete.val() == "true") {
                shouldDelete.val(false);
                image.removeClass("removed");
                btn.removeClass("btn-warning").addClass("btn-danger").text("Remove");
            }
            else {
                shouldDelete.val(true);
                image.addClass("removed")
                btn.removeClass("btn-danger").addClass("btn-warning").text("Undo");
            }

        });

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

        $("#add-file").on("click", function (e) {
            e.preventDefault();

            let inputs = $("#image-input-container [type=file]").get();

            for (var i in inputs) {
                if (inputs[i].files.length < 1) {
                    inputs[i].remove();
                }
            }

            let button = $(this);
            let numberId = button.attr("data-custom").split("-")[1];
            let targetId = `image-${numberId}`;

            createInput(targetId);

            let target = $(`#${targetId}`);
            target.click();

            target.on("input", function (e) {
                const targetEle = $(this);
                const files = targetEle.prop("files");

                if (files.length > 0) {
                    handleFiles(files, targetEle);

                    let newId = `image-${parseInt(numberId) + 1}`;
                    button.attr("data-custom", newId);
                }
                else {
                    $(this).remove;
                }

            });

            function createInput(id) {

                let inputContainer = $("#image-input-container");
                let inputElement = $(` <div class="form-group">
                                    <input type="file" id="${id}" name="Images" class="form-control" multiple accept=".jpg, .jpeg, .png"/>
                                   </div>`);
                inputContainer.append(inputElement);
            };

            function handleFiles(files, targetElement) {

                const preview = $('#image-preview');
                const div = $("<div>").addClass("image-container panel panel-default");
                const containerHeding = $("<div>").addClass("panel-heading").text("Uploaded Images");
                const containerBody = $("<div>").addClass("panel-body");
                const btn = $("<span>").addClass("js-delete btn btn-danger right").text("Remove Images").attr("id", targetElement.attr("id"));
                containerHeding.append(btn);

                btn.on("click", function (e) {
                    e.preventDefault();

                    let btn = $(this);
                    let id = btn.attr("id");

                    $(`#${id}`).closest(".form-group").remove();
                    btn.closest(".image-container").remove();
                });

                for (let i = 0; i < files.length; i++) {
                    const file = files[i];

                    const img = $("<img>").addClass("img-thumbnail img-responsive");

                    containerBody.append(img);
                    div.append(containerHeding).append(containerBody);

                    const reader = new FileReader();

                    reader.onload = (function (image) {
                        return function (e) {
                            image.attr("src", e.target.result);
                        };
                    })(img);
                    reader.readAsDataURL(file);
                }
                preview.append(div);
            };
        });
    }

    return {
        initializeDatatable: initializeDatatable,
        initializeDelete: initializeDelete,
        initializeHardDelete: initializeHardDelete,
        initPlanetForm: initPlanetForm
    }

})(PlanetService);