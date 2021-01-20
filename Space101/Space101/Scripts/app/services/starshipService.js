let StarshipService = function () {
    let deleteStarship = function (id, done, fail) {
        $.ajax({
            url: `/api/starships/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    let hardDeleteStarship = function (id, done, fail) {
        $.ajax({
            url: `/api/starships/hardDelete/${id}`,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        deleteStarship: deleteStarship,
        hardDeleteStarship: hardDeleteStarship
    };
}