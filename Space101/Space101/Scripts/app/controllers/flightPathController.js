let FlightPathController = ((flightPathService) => {
    let deleteButton;
    let hardDeleteButton;
    let table;
    let modelName = 'Flight Path';

    let initializeDataTable = function (container) {
        table = $(`${container}`).DataTable({
            "order": [[0, 'asc']],
            "columns": [
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

        let init = (function () {
            $(container).on('click', '.js-delete', function () {
                deleteButton = $(this);
                assignAlerts(doneAlertType, failAlertType);
                deleteFlightPath(confirmType);
            });
        })();

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deleteFlightPath = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = deleteButton.attr("data-flightPath-id");
                flightPathService().deleteFlightPath(id, doneDelete, failDelete);
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

        let init = (function () {
            $(container).on('click', '.js-hard-delete', function () {
                hardDeleteButton = $(this);
                assignAlerts(doneAlertType, failAlertType);
                deleteFlightPath(confirmType);
            });
        })();

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deleteFlightPath = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = hardDeleteButton.attr("data-flightPath-id");
                flightPathService().hardDeleteFlightPath(id, doneDelete, failDelete);
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
    let initializeDestinations = function (listenContainer, fillContainer, distanceContainer) {
        let departures;
        let destinations;
        let distance;

        let initializeForm = (function () {
            departures = $(listenContainer);
            destinations = $(fillContainer);
            distance = $(distanceContainer);
            $(`${listenContainer} option[value=""]`).prop('disabled', true);
            if (departures.val() == null) {
                $(fillContainer).prop('disabled', true);
            }
            else {
                populateDestinations(listenContainer, destinations);
            }
        })();

        let initializeDepartureEvent = (function () {
            departures.change(function (e) {
                distance.text("");
                populateDestinations(listenContainer, destinations);
            });
        })();

        let initializeDistanceEvent = (function () {
            destinations.change(function (e) {
                distance.text("");
                findDistance('#departureId', '#destinationId', '#distance-display');
            });
        })();

        let findDistance = function (departureContainer, destinationContainer, distanceContainer) {
            let departurePlanetId = $(`${departureContainer} option:selected`).val();
            let destinationPlanetId = $(`${destinationContainer} option:selected`).val();
            flightPathService().getDistance(departurePlanetId, destinationPlanetId, doneDistance, failDistance)
        }

        let doneDistance = function (distanceNum) {
            distance.text(distanceNum.toFixed(2));
        }

        let failDistance = function () {
            console.log('failed!');
        }

        let populateDestinations = function (listenContainer, destinations) {
            destinations.prop('disabled', true).removeClass('input-validation-error');

            let departurePlanetId = $(`${listenContainer} option:selected`).val();

            if (!departurePlanetId) {
                destinations.html('');
                destinations.append($('<option>)').val(null).text('Select Destination').prop('disabled', true).prop('selected', true));
            }
            else {
                flightPathService().availableDestinations(departurePlanetId, doneGet, failGet);
            };
        };

        let doneGet = function (availableDestinations) {

            enableSelect(destinations);
            if (availableDestinations.length > 0) {
                validSelect(destinations, availableDestinations);
            }
            else {
                invalidSelect(destinations);
            }
        }

        let failGet = function () {
            console.log('failed!');
        };

        let enableSelect = function (select) {
            select.prop('disabled', false);
            select.html("");
        };

        let validSelect = function (dropDown, data) {
            dropDown.append($('<option>)').val(null).text('Select Planet').prop('disabled', true).prop('selected', true));
            for (const planet of data) {
                let option = $('<option>').val(planet.planetID).text(planet.name);
                dropDown.append(option);
            }
        };

        let invalidSelect = function (dropDown) {
            dropDown.append($('<option>)').val(null).text('All flight paths are in database').prop('disabled', true).prop('selected', true));
            dropDown.addClass('input-validation-error');
        };
    };

    return {
        initializeDataTable: initializeDataTable,
        initializeDelete: initializeDelete,
        initializeHardDelete: initializeHardDelete,
        initializeDestinations: initializeDestinations
    }

})(FlightPathService);