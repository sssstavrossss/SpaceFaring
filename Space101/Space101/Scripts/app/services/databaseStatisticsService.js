let DatabaseStatisticsService = () => {
    let getPlanetsPerClimate = function (done, fail) {
        $.ajax({
            type: "GET",
            url: "/api/DatabaseStatistics/PlanetsPerClimate"
        })
            .done(done)
            .fail(fail);
    };
    let getPlanetsPerTerrain = function (done, fail) {
        $.ajax({
            type: "GET",
            url: "/api/DatabaseStatistics/PlanetsPerTerrain"
        })
            .done(done)
            .fail(fail);
    };
    let getFlightsPerStatus = function (done, fail) {
        $.ajax({
            type: "GET",
            url: "/api/DatabaseStatistics/FlightsPerStatus"
        })
            .done(done)
            .fail(fail);
    };
    let getFlightsDetails = function (done, fail) {
        $.ajax({
            type: "GET",
            url: "/api/DatabaseStatistics/FlightsStatistics"
        })
            .done(done)
            .fail(fail);
    };
    let getFlightsPerDate = function (done, fail) {
        $.ajax({
            type: "GET",
            url: "/api/DatabaseStatistics/FlightsPerDate"
        })
            .done(done)
            .fail(fail);
    };
    let getDataBaseNumbers = function (done, fail) {
        $.ajax({
            type: "GET",
            url: "/api/DatabaseStatistics/DatabaseNumbers"
        })
            .done(done)
            .fail(fail);
    };

    return {
        getPlanetsPerClimate: getPlanetsPerClimate,
        getPlanetsPerTerrain: getPlanetsPerTerrain,
        getFlightsPerStatus: getFlightsPerStatus,
        getFlightsDetails: getFlightsDetails,
        getFlightsPerDate: getFlightsPerDate,
        getDataBaseNumbers: getDataBaseNumbers
    };
};