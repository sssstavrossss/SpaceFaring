let UsersFlightsService = function () {

    let getFlights = function (doneGetData, failGetData) {
        $.ajax({
            url: `/api/UsersFlights`,
            method: "GET"
            })
            .done(function (data) {
                doneGetData(data);
            })
            .fail(failGetData);
    };

    let getChangedFlight = function (id, appendFlight) {
        $.ajax({
            url: `/api/UsersFlights/${id}`,
            method: "GET"
        })
            .done(function (data) {
                console.log(data);
                appendFlight(data);
            })
            .fail(function () {
                console.log('failed to get changed flight!');
            });
    };

    let getFilteredFlights = function (object, doneGetSearchData, failGetData) {
        $.ajax({
            url: `/api/UsersFlights`,
            method: "POST",
            data: JSON.stringify(object),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        })
            .done(function (data) {
                doneGetSearchData(data);
            })
            .fail(failGetData);
    };

    let getUserFavorites = function (doneFav, failFav) {
        $.ajax({
            url: `/api/UserFavorites`,
            method: "GET"
        })
            .done(function (data) {
                doneFav(data);
            })
            .fail(failFav);
    };

    let updateUserFavorites = function (doneFavUpdate, data) {
        $.ajax({
            url: `/api/UserFavorites`,
            method: "POST",
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8'
        })
            .done(function () {
                doneFavUpdate();
            })
            .fail(function () { console.log('failed to update favorite!'); });
    };

    return {
        getFlights: getFlights,
        getFilteredFlights: getFilteredFlights,
        getChangedFlight: getChangedFlight,
        getUserFavorites: getUserFavorites,
        updateUserFavorites: updateUserFavorites
    };

}();