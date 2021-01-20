let FlightController = ((flightService) => {
    let deleteButton;
    let hardDeleteButton;
    let toggleAvailabilityButton;
    let table;
    let changeStatusButton;
    let modelName = 'Flight';

    let initializeDataTable = function (container) {
        table = $(`${container}`).DataTable({
            "order": [[0, 'asc']],
            "columns": [
                null,
                null,
                null,
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
    let initDelete = function (container, alertType, doneAlertType, failAlertType) {
        let doneAlert;
        let failAlert;

        $(container).on('click', '.js-delete', function () {
            deleteButton = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deleteFlight(alertType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deleteFlight = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = deleteButton.attr("data-flight-id");
                flightService().deleteFlight(id, doneDelete, failDelete);
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
    let initHardDelete = function (container, alertType, doneAlertType, failAlertType) {
        let doneAlert;
        let failAlert;

        $(container).on('click', '.js-hard-delete', function () {
            hardDeleteButton = $(this);
            assignAlerts(doneAlertType, failAlertType);
            deleteFlight(alertType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let deleteFlight = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                let id = hardDeleteButton.attr("data-flight-id");
                flightService().hardDeleteFlight(id, doneDelete, failDelete);
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
    let changeStatus = function (container, alertType, doneAlertType, failAlertType) {
        let doneAlert;
        let failAlert;
        let flightId;
        let statusId;
        let statusName;

        $(container).on('click', '.js-change-flight-status', function () {
            changeStatusButton = $(this);
            initializeParams();
            assignAlerts(doneAlertType, failAlertType)
            changeFlightStatus(alertType);
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }
        let initializeParams = function () {
            flightId = changeStatusButton.parent().attr("data-flight-id");
            statusId = changeStatusButton.attr("data-status-id");
            statusName = changeStatusButton.text();
        }

        let changeFlightStatus = function (confirmationBox) {
            confirmationBox(modelName, confirmCallback);
        };

        let confirmCallback = function (result) {
            if (result) {
                flightService().changeStatus(flightId, statusId, doneChangeStatus, failChangeStatus);
            }
        }

        let doneChangeStatus = function () {
            changeStatusButton.siblings('.disabled').removeClass('disabled');
            changeStatusButton.addClass('disabled');
            changeStatusButton.closest('tr').find('.flight-status-name').text(statusName);

            if (doneAlert) {
                doneAlert(modelName);
            }
        }

        let failChangeStatus = function () {
            if (failAlert)
                failAlert(modelName);
        }
    };
    let initializeChooseSeatFlightHub = function () {
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("FlightHub");

        hub.on("flightSeatsClosed", function (seatIds) {
            for (var seatId of seatIds) {
                let check = $(`div[id=${seatId}]`).find("[type=checkbox]");
                check.attr("disabled", true).addClass('inactive').attr("data-seat-id", `${seatId}`).removeAttr("name").removeAttr("value");
            }
        });

        hub.on("flightSeatsOpened", function (seatIds) {
            for (var seatId of seatIds) {
                let check = $(`div[id=${seatId}]`).find("[type=checkbox]");
                check.attr("disabled", false).removeClass('inactive').attr("name", "flightSeatIds").val("${seatId}").removeAttr("data-seat-id");
            }
        });

        connection.start();
    };

    let initializeAdminsSeatDataTable = function (container) {
        seatTable = $(`${container}`).DataTable({
            "order": [[0, 'asc']],
            "columns": [
                null,
                null,
                null,
                null,
                null,
                { "searchable": false, "orderable": false }
            ]
        });
    };
    let initializeAdminsFlightHub = function (container) {
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("FlightHub");

        function initStatuses(flight) {
            let statusDropTemplate = ``;
            for (var i in flight.FlightStatuses) {
                if (flight.FlightStatuses[i].StatusName == flight.Status) {
                    statusDropTemplate += `<button class="list-group-item disabled js-change-flight-status" data-status-id="${flight.FlightStatuses[i].FlightStatusId}">${flight.FlightStatuses[i].StatusName}</button>`;
                }
                else {
                    statusDropTemplate += `<button class="list-group-item js-change-flight-status" data-status-id="${flight.FlightStatuses[i].FlightStatusId}">${flight.FlightStatuses[i].StatusName}</button>`;
                }
            }
            return statusDropTemplate;
        };
        function checkVIP(flight) {
            if (flight.IsVIP === true)
                return "YES"
            else
                return "NO"
        };
        function addButtons(flight) {
            let isAdmin = $("#isAdmin")
            let buttonTemplate = "";
            if (isAdmin) {
                buttonTemplate = `<td>
                                        <div class="edit-btns-container">
                                           <a class="btn btn-success" href="/Flight/Edit/${flight.FlightId}">Edit</a>
                                           <div class="btn-group status-container">
                                              <button class="btn btn-danger dropdown-toggle" data-toggle="dropdown">Status<span class="caret"></span></button>
                                              <div class="dropdown-menu list-group flight-status-controler" data-flight-id="${flight.FlightId}">
                                                  ${initStatuses(flight)}
                                               </div>
                                            </div>
                                         </div>
                                         <div class="seat-btn">
                                              <a class="btn btn-warning" href="/Flight/ManageSeats/${flight.FlightId}">Manage Seats</a>
                                         </div>
                                     </td>`
            }
            return buttonTemplate;
        };

        hub.on("flightCreated", function (flight) {
            let htmlTable = $(`${container}`);
            let createTemplate = $(`<tr id=${flight.FlightId}>
                                                <td>${flight.FlightId}</td>
                                                <td>${flight.FlightDate}</td>
                                                <td>${flight.FlightTime}</td>
                                                <td>${flight.BasePriceFormatted}</td>
                                                <td>${flight.FlightPath.Departure.Name}</td>
                                                <td>${flight.FlightPath.Destination.Name}</td>
                                                <td>${flight.DistanceFormatted}</td>
                                                <td>
                                                   ${checkVIP(flight)}
                                                </td>
                                                <td>
                                                    <span class="flight-status-name">${flight.FlightStatus.StatusName}</span>
                                                </td>
                                                    ${addButtons(flight)}
                                            </tr>`);
            htmlTable.append(createTemplate);
            table.rows.add(createTemplate);
        });

        hub.on("flightUpdated", function (flight) {
            let row = $(`${container} tr[id=${flight.FlightId}]`);
            let updateTemplate = $(`
                                                <td>${flight.FlightId}</td>
                                                <td>${flight.FlightDate}</td>
                                                <td>${flight.FlightTime}</td>
                                                <td>${flight.BasePriceFormatted}</td>
                                                <td>${flight.FlightPath.Departure.Name}</td>
                                                <td>${flight.FlightPath.Destination.Name}</td>
                                                <td>${flight.DistanceFormatted}</td>
                                                <td>
                                                   ${checkVIP(flight)}
                                                </td>
                                                <td>
                                                    <span class="flight-status-name">${flight.FlightStatus.StatusName}</span>
                                                </td>
                                                    ${addButtons(flight)}`);
            row.html(updateTemplate);
        });

        hub.on("flightStatusChanged", function (flightId, statusId) {
            let row = $(`${container} tr[id=${flightId}]`);
            let statusContainer = row.find(".status-container");
            statusContainer.find(".disabled").removeClass("disabled");
            let statusName = statusContainer.find(`[data-status-id=${statusId}]`).addClass("disabled").text();
            row.find(".flight-status-name").text(statusName);
        });

        hub.on("flightDeleted", function (id) {
            if (id != deleteButton.attr("data-flight-id")) {
                table
                    .row(tr)
                    .remove()
                    .draw();
            }
        });

        connection.start();
    };
    let initializeAdminsManageSeats = function (container, AlertType, doneAlertType, failAlertType) {
        let doneAlert;
        let failAlert;
        let SeatIds = [];
        let availability;

        $(container).on('click', '.js-toggle-availability', function () {

            toggleAvailabilityButton = $(this);
            availability = toggleAvailabilityButton.attr("data-seat-availability");
            SeatIds = [];
            SeatIds.push(toggleAvailabilityButton.attr("data-seat-id"));

            assignAlerts(doneAlertType, failAlertType)

            if (availability == "False") {
                releaseSeats(AlertType);
            }
            else {
                holdSeats(AlertType);
            }
        });

        let assignAlerts = function (doneAlertType, failAlertType) {
            doneAlert = doneAlertType;
            failAlert = failAlertType;
        }

        let releaseSeats = function (confirmationBox) {
            confirmationBox("Seat", confirmCallbackRelease, "release");
        };

        let confirmCallbackRelease = function (result) {
            if (result) {
                flightService().releaseSeats(SeatIds, doneRelease, failRelease);
            }
        }

        let holdSeats = function (confirmationBox) {
            confirmationBox("Seat", confirmCallbackHold, "hold");
        };

        let confirmCallbackHold = function (result) {
            if (result) {
                flightService().holdSeats(SeatIds, doneHold, failHold);
            }
        }

        let doneRelease = function () {
            toggleAvailabilityButton.text("CLOSE").removeClass("btn-danger").addClass("btn-warning").attr("data-seat-availability", "True");
            toggleAvailabilityButton.closest("tr").removeClass("danger");

            if (doneAlert) {
                doneAlert("Seat", "released");
            }
        };

        let failRelease = function (jqxhr) {
            let message = JSON.parse(jqxhr.responseText).message;
            if (failAlert)
                failAlert("Seat", "released", message);
        }

        let doneHold = function () {
            toggleAvailabilityButton.text("OPEN").removeClass("btn-warning").addClass("btn-danger").attr("data-seat-availability", "False");
            toggleAvailabilityButton.closest("tr").addClass("danger");

            if (doneAlert) {
                doneAlert("Seat", "closed");
            }
        };

        let failHold = function () {
            if (failAlert)
                failAlert("Seat", "closed");
        }
    };
    let initializeAdminsManageSeatFlightHub = function () {
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("FlightHub");

        hub.on("flightSeatsClosed", function (seatIds) {
            for (var seatId of seatIds) {
                let btn = $(`button[data-seat-id=${seatId}]`);
                btn.text("OPEN").removeClass("btn-warning").addClass("btn-danger").attr("data-seat-availability", "False");
                btn.closest("tr").addClass("danger");

            }
        });

        hub.on("flightSeatsOpened", function (seatIds) {
            for (var seatId of seatIds) {
                let btn = $(`button[data-seat-id=${seatId}]`);
                btn.text("CLOSE").removeClass("btn-danger").addClass("btn-warning").attr("data-seat-availability", "True");
                btn.closest("tr").removeClass("danger");

            }
        });

        connection.start();
    };

    return {
        initializeDataTable: initializeDataTable,
        initDelete: initDelete,
        changeStatus: changeStatus,
        initializeChooseSeatFlightHub: initializeChooseSeatFlightHub,
        initializeAdminsFlightHub: initializeAdminsFlightHub,
        initializeAdminsManageSeats: initializeAdminsManageSeats,
        initializeAdminsSeatDataTable: initializeAdminsSeatDataTable,
        initializeAdminsManageSeatFlightHub: initializeAdminsManageSeatFlightHub,
        initHardDelete: initHardDelete
    }

})(FlightService);