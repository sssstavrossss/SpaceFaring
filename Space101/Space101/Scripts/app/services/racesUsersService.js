let RacesUsersService = function () {
    let getAllRaces = function (doneData, failData, assignArrays) {
        $.ajax({
            type: 'GET',
            url: '/api/races'
        })
            .done(function (data) {
                assignArrays(data);
                doneData(data);
            })
            .fail(failData);
    };
    return {
        getAllRaces: getAllRaces
    }
}();