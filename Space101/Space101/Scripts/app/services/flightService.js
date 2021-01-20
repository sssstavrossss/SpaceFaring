let FlightService = function () {
    let deleteFlight = function (id, done, fail) {
        $.ajax({
            url: `/api/flights/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    let hardDeleteFlight = function (id, done, fail) {
        $.ajax({
            url: `/api/flights/hardDelete/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    let changeStatus = function (flightId, statusId, done, fail) {
        $.ajax({
            url: `/api/flights/flightStatus/${flightId}/${statusId}`,
            method: "POST"
        })
            .done(done)
            .fail(fail);
    }

    let releaseSeats = function (seatIds, done, fail) {
        $.ajax({
            type: "POST",
            url: "/api/flights/seats/releaseSeats",
            data: { "": seatIds }
        })
            .done(done)
            .fail(fail);
    }
    let holdSeats = function (seatIds, done, fail) {
        $.ajax({
            type: "POST",
            url: "/api/flights/seats/holdSeats",
            data: { "": seatIds }
        })
            .done(done)
            .fail(fail);
    }

    return {
        deleteFlight: deleteFlight,
        changeStatus: changeStatus,
        releaseSeats: releaseSeats,
        holdSeats: holdSeats,
        hardDeleteFlight: hardDeleteFlight
    };
}