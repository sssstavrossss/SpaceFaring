let UserFlightsSignalR = function () {

    let controller;

    let hubInit = function (usersFlightsController) {

        controller = usersFlightsController;
        var connection = $.hubConnection();
        var hub = connection.createHubProxy("FlightHub");

        hub.on("flightUpdated", function (flight) {
            update(flight);
        });

        hub.on("flightStatusChanged", function (flightId, statusId) {
            status(flightId, statusId);
        });

        hub.on("flightDeleted", function (id) {
            del(id);
        });

        hub.on("flightCreated", function (flight) {
            created(flight);
        });

        connection.start();

    };

    let created = function (flight) {
        if (flight.FlightStatus.FlightStatusId == 1 || flight.FlightStatus.FlightStatusId == 2)
            controller.createFlight(flight);
    };

    let del = function (id) {
        controller.deleteFlight(id);
    };

    let update = function (flight) {
        controller.updateFlight(flight);
    };

    let status = function (flightId, statusId) {
        controller.statusChange(flightId, statusId);
    }

    return {
        hubInit: hubInit
    }

}();