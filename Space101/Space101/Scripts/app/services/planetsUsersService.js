let PlanetsUsersService = function () {
    let getAllPlanets = function (doneData, failData, assignArrays) {
        $.ajax({
                    type: 'GET',
                    url: '/api/planets'
        })
            .done(function (data) {
                assignArrays(data);
                doneData(data);
            })
            .fail(failData);
    };
    return {
        getAllPlanets: getAllPlanets
    }
}();