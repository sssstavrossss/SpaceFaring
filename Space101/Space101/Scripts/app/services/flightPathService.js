let FlightPathService = function () {
    let deleteFlightPath = function (id, done, fail) {
        $.ajax({
            url: `/api/flightpaths/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    let hardDeleteFlightPath = function (id, done, fail) {
        $.ajax({
            url: `/api/flightpaths/hardDelete/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    let availableDestinations = function (id, done, fail) {
        $.ajax({
            url: `/api/flightpaths/destinations/${id}`,
            method: "GET"
        })
            .done(done)
            .fail(fail);
    };

    let getDistance = function (departurePlanetId, destinationPlanetId, doneDistance, failDistance) {

        $.ajax({
            url: `/api/flightpaths/findDistance/${departurePlanetId}/${destinationPlanetId}`,
            method: "GET"
        })
            .done(doneDistance)
            .fail(failDistance);
    }

    return {
        deleteFlightPath: deleteFlightPath,
        availableDestinations: availableDestinations,
        getDistance: getDistance,
        hardDeleteFlightPath: hardDeleteFlightPath
    };
}